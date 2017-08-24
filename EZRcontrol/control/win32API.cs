using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace EZRcontrol
{
    //http://ehpub.co.kr/%EC%9B%90%EA%B2%A9-%EC%A0%9C%EC%96%B4-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-15-%ED%82%A4%EB%B3%B4%EB%93%9C-%EB%A7%88%EC%9A%B0%EC%8A%A4-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EB%9E%98%ED%8D%BC/

    [Flags]
    public enum KeyFlag
    {
        KE_DOWN = 0, KE__EXTENDEDKEY = 1, KE_UP = 2
    }

    [Flags]
    public enum MouseFlag
    {
        ME_MOVE = 1, ME_LEFTDOWN = 2, ME_LEFTUP = 4, ME_RIGHTDOWN = 8,
        ME_RIGHTUP = 0x10, ME_MIDDLEDOWN = 0x20, ME_MIDDLEUP = 0x40, ME_WHEEL = 0x800,
        ME_ABSOULUTE = 8000
    }

    public static class win32API
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int flag, int dx, int dy, int buttons, int extras);
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point point);
        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);
        [DllImport("User32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int extra);

        #region native rapping
        public static void Key_Down(int kc)
        {
            keybd_event((byte)kc, 0, (int)KeyFlag.KE_DOWN, 0);
        }
        public static void Key_Up(int kc)
        {
            keybd_event((byte)kc, 0, (int)KeyFlag.KE_UP , 0);
        }
        public static void Mouse_Move(int x, int y)
        {
            SetCursorPos(x, y);
        }
        public static void Mouse_LDown()
        {
            mouse_event((int)MouseFlag.ME_LEFTDOWN, 0, 0, 0, 0);
        }
        public static void Mouse_LUp()
        {
            mouse_event((int)MouseFlag.ME_LEFTUP, 0, 0, 0, 0);
        }
        public static void Mouse_RDown()
        {
            mouse_event((int)MouseFlag.ME_RIGHTDOWN, 0, 0, 0, 0);
        }
        public static void Mouse_RUp()
        {
            mouse_event((int)MouseFlag.ME_RIGHTUP, 0, 0, 0, 0);
        }
        public static void Mouse_MDown()
        {
            mouse_event((int)MouseFlag.ME_MIDDLEDOWN, 0, 0, 0, 0);
        }
        public static void Mouse_MUp()
        {
            mouse_event((int)MouseFlag.ME_MIDDLEUP, 0, 0, 0, 0);
        }
        #endregion

    }
}
