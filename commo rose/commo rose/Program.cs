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

        public const string app_name = "Commo rose";

        private static MouseOrKeyboardHook mouseOrKeyboardHook;
        private static List<Preset> presets;
        public static Preset desktop_preset { get; private set; }

        private static ButtonsForm buttons_form;
        private static Settings settings;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            buttons_form = new ButtonsForm();

            Hook_target target;
            VirtualKeyCode virtualKey;
            (presets, target, virtualKey) = Saver.load_settings();
            desktop_preset = presets.Where(x => x.name == "Desktop").Single();
            settings = new Settings(presets);

            mouseOrKeyboardHook = new MouseOrKeyboardHook(target, virtualKey, buttons_form.on_form_show, buttons_form.on_form_hide, false);

            Application.Run();
        }

        public static Preset get_preset()
        {
            uint process_id;
            GetWindowThreadProcessId(GetForegroundWindow(), out process_id);
            string foreground_process_name = System.Diagnostics.Process.GetProcessById((int)process_id).ProcessName;

            foreach (Preset preset in presets)
            {
                if (preset.processes.Contains(foreground_process_name))
                {
                    return preset;
                }
            }

            return desktop_preset;
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
