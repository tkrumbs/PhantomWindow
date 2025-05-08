// This file is derived from:
// Microsoft WPF Samples - https://github.com/microsoft/WPF-Samples/
// Copyright (c) 2015 Microsoft
// Licensed under the MIT License.

namespace PhantomWindow.Config;

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

using PhantomWindow.Properties;

public static partial class ApplicationWindowManager
{
	public const int SwShownormal = 1;
	public const int SwShowminimized = 2;

	[LibraryImport("user32.dll", EntryPoint = "SetWindowPlacement", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool SetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

	[LibraryImport("user32.dll", EntryPoint = "GetWindowPlacement", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

	public static void Load(Window window)
	{
		try
		{
			var wp = Settings.Default.WindowPlacement;
			if (!(wp.normalPosition.Bottom == 0 && wp.normalPosition.Top == 0 && wp.normalPosition.Left == 0 && wp.normalPosition.Right == 0))
			{
				wp.length = Marshal.SizeOf<WindowPlacement>();
				wp.flags = 0;
				wp.showCmd = (wp.showCmd == SwShowminimized ? SwShownormal : wp.showCmd);
				var hwnd = new WindowInteropHelper(window).Handle;
				SetWindowPlacement(hwnd, ref wp);
			}
		}
		catch { }
	}

	public static void Save(Window window)
	{
		// Persist window placement details to application settings
		var hwnd = new WindowInteropHelper(window).Handle;
		GetWindowPlacement(hwnd, out WindowPlacement wp);
		Settings.Default.WindowPlacement = wp;
		Settings.Default.Save();
	}
}
