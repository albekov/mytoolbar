using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyToolbar.Services
{
    /// <summary>
    /// Setting global keyboard hook here.
    /// </summary>
    internal sealed class KeyboardHookHelper : IDisposable
    {
        private readonly ICharsHandler _handler;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private readonly IntPtr _hookId;

        public KeyboardHookHelper(ICharsHandler handler)
        {
            _handler = handler;
            _hookId = RegisterHook(HookCallback);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }

        private IntPtr RegisterHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam == (IntPtr) WM_KEYDOWN)
            {
                var s = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                var c = GetAsciiCharacter((int) s.vkCode, s.scanCode);
                _handler.HandleChar(c);
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private static char GetAsciiCharacter(int uVirtKey, uint uScanCode)
        {
            var lpKeyState = new byte[256];
            GetKeyboardState(lpKeyState);
            var lpChar = new byte[2];
            if (ToAscii(uVirtKey, uScanCode, lpKeyState, lpChar, 0) == 1)
                return (char)lpChar[0];
            return new char();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImportAttribute("user32.dll")]
        public static extern int ToAscii(int uVirtKey, uint uScanCode, byte[] lpbKeyState, byte[] lpChar, int uFlags);

        [DllImportAttribute("user32.dll")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        public enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }
    }
}