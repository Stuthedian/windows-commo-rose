using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace commo_rose
{
    static class Program
    {
        private static MouseOrKeyboardHook mouseOrKeyboardHook;
        private static List<Preset> presets;
        private static Preset desktop_preset;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ButtonsForm buttons_form = new ButtonsForm();

            Hook_target target;
            VirtualKeyCode virtualKey;
            (presets, target, virtualKey) = Saver.load_settings();

            desktop_preset = presets.Where(x => x.name == "Desktop").Single();
            buttons_form.current_preset = desktop_preset;

            mouseOrKeyboardHook = new MouseOrKeyboardHook(target, virtualKey, buttons_form.on_form_show, buttons_form.on_form_hide, false);

            Application.Run();
        }

        public static void Close()
        {
            if (mouseOrKeyboardHook != null)
                mouseOrKeyboardHook.ClearHook();

            Application.Exit();
        }
    }
}
