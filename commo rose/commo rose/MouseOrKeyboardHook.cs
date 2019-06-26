using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsInput.Native;

namespace commo_rose
{
    public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    internal class NativeMethods
    {
        /// <summary>
        ///     The SetWindowsHookEx function installs an application-defined hook
        ///     procedure into a hook chain. You would install a hook procedure to monitor
        ///     the system for certain types of events. These events are associated either
        ///     with a specific thread or with all threads in the same desktop as the
        ///     calling thread.
        /// </summary>
        /// <param name="hookType">
        ///     Specifies the type of hook procedure to be installed
        /// </param>
        /// <param name="callback">Pointer to the hook procedure.</param>
        /// <param name="hMod">
        ///     Handle to the DLL containing the hook procedure pointed to by the lpfn
        ///     parameter. The hMod parameter must be set to NULL if the dwThreadId
        ///     parameter specifies a thread created by the current process and if the
        ///     hook procedure is within the code associated with the current process.
        /// </param>
        /// <param name="dwThreadId">
        ///     Specifies the identifier of the thread with which the hook procedure is
        ///     to be associated.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the handle to the hook
        ///     procedure. If the function fails, the return value is 0.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType,
            HookProc callback, IntPtr hMod, uint dwThreadId);

        /// <summary>
        ///     The UnhookWindowsHookEx function removes a hook procedure installed in
        ///     a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">Handle to the hook to be removed.</param>
        /// <returns>
        ///     If the function succeeds, the return value is true.
        ///     If the function fails, the return value is false.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        ///     The CallNextHookEx function passes the hook information to the next hook
        ///     procedure in the current hook chain. A hook procedure can call this
        ///     function either before or after processing the hook information.
        /// </summary>
        /// <param name="idHook">Handle to the current hook.</param>
        /// <param name="nCode">
        ///     Specifies the hook code passed to the current hook procedure.
        /// </param>
        /// <param name="wParam">
        ///     Specifies the wParam value passed to the current hook procedure.
        /// </param>
        /// <param name="lParam">
        ///     Specifies the lParam value passed to the current hook procedure.
        /// </param>
        /// <returns>
        ///     This value is returned by the next hook procedure in the chain.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);
    }

    internal static class HookCodes
    {
        public const int HC_ACTION = 0;
        public const int HC_GETNEXT = 1;
        public const int HC_SKIP = 2;
        public const int HC_NOREMOVE = 3;
        public const int HC_NOREM = HC_NOREMOVE;
        public const int HC_SYSMODALON = 4;
        public const int HC_SYSMODALOFF = 5;
    }

    internal enum HookType
    {
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class POINT
    {
        public int x;
        public int y;
    }
        
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSLLHOOKSTRUCT
    {
        public POINT pt; // The x and y coordinates in screen coordinates. 
        public int mouseData; // The mouse wheel and button info.
        public int flags;
        public int time; // Specifies the time stamp for this message. 
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KBDLLHOOKSTRUCT
    {
        public int vkCode; // Specifies a virtual-key code
        public int scanCode; // Specifies a hardware scan code for the key
        public int flags;
        public int time; // Specifies the time stamp for this message
        public int dwExtraInfo;
    }

    internal enum MouseMessage
    {
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,

        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSEHWHEEL = 0x020E,

        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C
    }

    internal enum KeyboardMessage
    {
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105
    }
     
    public class MouseOrKeyboardHook
    {
        private action on_key_down;
        private action on_key_up;

        private bool key_hook_handled;
        private bool pass_message;
        private IntPtr _hGlobalLlHook;
        private HookProc mouse_proc;
        private HookProc keyboard_proc;

        private Hook_target _hook_target;
        public Hook_target hook_target
        {
            get { return _hook_target; }
            set
            {
                ClearHook();
                _hook_target = value;
                if (hook_target == Hook_target.Keyboard)
                {
                    _hGlobalLlHook = NativeMethods.SetWindowsHookEx(HookType.WH_KEYBOARD_LL,
                        keyboard_proc,
                        Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                }
                else if (hook_target == Hook_target.Mouse)
                {
                    _hGlobalLlHook = NativeMethods.SetWindowsHookEx(HookType.WH_MOUSE_LL,
                        mouse_proc,
                        Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                }
                if (_hGlobalLlHook == IntPtr.Zero)
                {
                    throw new Win32Exception("Unable to set hook");
                }
            }
        }
        //public MouseButtons action_button_mouse { get; set; }
        private VirtualKeyCode action_button{ get; set; }
        //private VirtualKeyCode action_button_keyboard { get; set; }

        public MouseOrKeyboardHook(Hook_target target, VirtualKeyCode action_button, action down, action up, bool pass)
        {
            on_key_down = down;
            on_key_up = up;
            key_hook_handled = false;
            mouse_proc = LowLevelMouseProc;
            keyboard_proc = LowLevelKeyboardProc;
            pass_message = pass;
            this.action_button = action_button;

            hook_target = target;
        }

        ~MouseOrKeyboardHook()
        {
            ClearHook();
        }

        public void ClearHook()
        {
            if (_hGlobalLlHook != IntPtr.Zero)
            {
                if (!NativeMethods.UnhookWindowsHookEx(_hGlobalLlHook))
                    throw new Win32Exception("Unable to clear hook");
                _hGlobalLlHook = IntPtr.Zero;
            }
        }

        private int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT a = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                KeyboardMessage wmMouse = (KeyboardMessage)wParam;
                if (wmMouse == KeyboardMessage.WM_KEYDOWN || wmMouse == KeyboardMessage.WM_SYSKEYDOWN)
                {
                    if (a.vkCode == (int)action_button)
                    {
                        if (key_hook_handled == false)
                        {
                            key_hook_handled = true;
                            on_key_down();
                        }
                        return pass_message ? NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam) : 1;
                    }
                }
                else if (wmMouse == KeyboardMessage.WM_KEYUP || wmMouse == KeyboardMessage.WM_SYSKEYUP)
                {
                    if (a.vkCode == (int)action_button)
                    {
                        key_hook_handled = false;
                        on_key_up();
                        return pass_message ? NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam) :1;
                    }
                }
            }
            return NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam);
        }

        private int LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT a = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                int xbutton_id = a.mouseData >> 16;
                MouseMessage wmMouse = (MouseMessage)wParam;
                if (wmMouse == mouse_button_to_message_down(action_button))
                {

                    if (wmMouse == MouseMessage.WM_XBUTTONDOWN)
                    {
                        int id = mouse_xbutton_to_id(action_button);
                        if (xbutton_id == id)
                        {
                            on_key_down();
                            return pass_message ? NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam) : 1;
                        }
                    }
                    else
                    {
                        on_key_down();
                        return pass_message ? NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam) : 1;
                    }
                }
                if (wmMouse == mouse_button_to_message_up(action_button))
                {
                    if (wmMouse == MouseMessage.WM_XBUTTONUP)
                    {
                        int id = mouse_xbutton_to_id(action_button);
                        if (xbutton_id == id)
                        {
                            on_key_up();
                            return pass_message ? NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam) : 1;
                        }
                    }
                    else
                    {
                        on_key_up();
                        return pass_message ? NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam) : 1;
                    }
                }
            }
            return NativeMethods.CallNextHookEx(_hGlobalLlHook, nCode, wParam, lParam);
        }

        private MouseMessage mouse_button_to_message_down(VirtualKeyCode button)
        {
            if (button == VirtualKeyCode.MBUTTON)
                return MouseMessage.WM_MBUTTONDOWN;
            else if (button == VirtualKeyCode.XBUTTON1 || button == VirtualKeyCode.XBUTTON2)
                return MouseMessage.WM_XBUTTONDOWN;
            else if (button == VirtualKeyCode.LBUTTON)
                return MouseMessage.WM_LBUTTONDOWN;
            else throw new NotImplementedException();
        }

        private MouseMessage mouse_button_to_message_up(VirtualKeyCode button)
        {
            if (button == VirtualKeyCode.MBUTTON)
                return MouseMessage.WM_MBUTTONUP;
            else if (button == VirtualKeyCode.XBUTTON1 || button == VirtualKeyCode.XBUTTON2)
                return MouseMessage.WM_XBUTTONUP;
            else if (button == VirtualKeyCode.LBUTTON)
                return MouseMessage.WM_LBUTTONUP;
            else throw new NotImplementedException();
        }

        private int mouse_xbutton_to_id(VirtualKeyCode button)
        {
            const int XBUTTON1 = 0x0001;
            const int XBUTTON2 = 0x0002;
            if (button == VirtualKeyCode.XBUTTON1)
                return XBUTTON1;
            else if (button == VirtualKeyCode.XBUTTON2)
                return XBUTTON2;
            else throw new NotImplementedException();
        }
    }
       
    public enum Hook_target { Mouse, Keyboard }

    public delegate void action();
}
