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

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();

            Hook_target target;
            VirtualKeyCode virtualKey;
            (presets, target, virtualKey) = Saver.load_settings();

            mouseOrKeyboardHook = new MouseOrKeyboardHook(target, virtualKey, form.on_form_show, form.on_form_hide, false);

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
