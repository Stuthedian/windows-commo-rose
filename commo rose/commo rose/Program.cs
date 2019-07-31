using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using System.Runtime.InteropServices;

namespace commo_rose
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        public const string app_name = "Commo rose";
        public const string default_preset_name = "Default";

        private static MouseOrKeyboardHook mouseOrKeyboardHook;
        private static List<Preset> presets;
        public static Preset default_preset { get; private set; }

        private static ButtonsForm buttons_form;
        private static Settings settings;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            buttons_form = new ButtonsForm();

            Hook_target target;
            VirtualKeyCode virtualKey;
            (presets, target, virtualKey) = Saver.load_settings();
            default_preset = presets.Where(x => x.name == default_preset_name).Single();

            mouseOrKeyboardHook = new MouseOrKeyboardHook(target, virtualKey, buttons_form.on_form_show, buttons_form.on_form_hide, false);
            settings = new Settings(presets, buttons_form, mouseOrKeyboardHook);

            if(args.Length > 0)
            {
                if (args[0] == "add")
                {
                    settings.add_to_task_scheduler();
                }
                else if (args[0] == "del")
                {
                    settings.delete_from_task_scheduler();
                }
                settings.update_ToolStripMenuItem();
            }

            Application.Run();
        }
        
        public static Preset get_preset()
        {
            uint process_id;
            GetWindowThreadProcessId(GetForegroundWindow(), out process_id);
            var proc = System.Diagnostics.Process.GetProcessById((int)process_id);
            string foreground_process_name = System.Diagnostics.Process.GetProcessById((int)process_id).ProcessName;

            if (foreground_process_name == "ApplicationFrameHost")
            {
                EnumChildWindows(proc.MainWindowHandle, (hwnd, lParam) =>
                {
                    uint proc_id;
                    GetWindowThreadProcessId(hwnd, out proc_id);
                    System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById((int)proc_id);
                    if (process.ProcessName != "ApplicationFrameHost")
                    {
                        foreground_process_name = process.ProcessName;
                    }
                    return true;
                }, IntPtr.Zero);
            }

            foreach (Preset preset in presets)
            {
                if (preset.processes.Contains(foreground_process_name))
                {
                    return preset;
                }
            }

            return default_preset;
        }
        
        public static void show_settings_window()
        {
            if (!settings.Visible)
                settings.Show();
            else
            {
                settings.WindowState = FormWindowState.Normal;
                settings.Activate();
            }
        }

        public static void Close()
        {
            if (mouseOrKeyboardHook != null)
                mouseOrKeyboardHook.ClearHook();

            Application.Exit();
        }
    }
}
