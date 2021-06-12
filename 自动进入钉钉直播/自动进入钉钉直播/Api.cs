﻿using System;
using System.Runtime.InteropServices;

namespace 自动进入钉钉直播
{
    class Api
    {
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;

        [DllImport("user32.dll")]
        public extern static IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        public extern static int GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public extern static IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        public const int HORZRES = 8;
        public const int VERTRES = 10;
        public const int DESKTOPVERTRES = 117;
        public const int DESKTOPHORZRES = 118;
        public const int LOGPIXELSX = 88;


        /// <summary>  
        /// 获取宽度缩放百分比  
        /// </summary>  
        public static float ScaleX()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            float scaleX = GetDeviceCaps(hdc, DESKTOPHORZRES) / GetDeviceCaps(hdc, HORZRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return scaleX;
        }

        /// <summary>  
        /// 获取高度缩放百分比  
        /// </summary>  
        public static float ScaleY()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            float scaleY = GetDeviceCaps(hdc, DESKTOPVERTRES) / GetDeviceCaps(hdc, VERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return scaleY;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]// 查找窗口句柄
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, uint flags);// 始终保持窗口在最前端
        public const int HWND_TOPMOST = -1;
        public const int HWND_NOTOPMOST = -2;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOSIZE = 0x0001;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, out POINT pt);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);// 阻止休眠
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
        public const uint ES_CONTINUOUS = 0x80000000;

        [DllImport("user32.dll")]
       public static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        public const uint SC_CLOSE = 0xF060;   //关闭
        public const uint MF_BYCOMMAND = 0x00; 
        public const uint MF_GRAYED = 0x01;    //变灰
        public const uint MF_DISABLED = 0x02;  //不可用

    }
}
