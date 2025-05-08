namespace PhantomWindow.Services;

using System;
using System.Collections.Generic;
using System.Diagnostics;

using PhantomWindow.Interop;
using PhantomWindow.Models;

/// <summary>
/// Contains methods to generate and apply WindowState object to active windows.
/// </summary>
internal static class WindowManager
{
	/// <summary>
	/// Retrives active windows as WindowState objects.
	/// </summary>
	/// <returns>An enumerable collection of WindowState objects.</returns>
	public static IEnumerable<WindowState> EnumerateWindows()
	{
		List<WindowState> list = [];
		IntPtr currentHandle = Process.GetCurrentProcess().MainWindowHandle;
		IntPtr shellWnd = NativeMethods.GetShellWindow();

		NativeMethods.EnumWindows((hWnd, _) =>
		{
			if (hWnd == shellWnd) return true;
			if (hWnd == currentHandle) return true;
			if (!NativeMethods.IsWindowVisible(hWnd)) return true;
			if (NativeMethods.GetWindow(hWnd, NativeMethods.GW_OWNER) != IntPtr.Zero) return true;

			int textLength = NativeMethods.GetWindowTextLength(hWnd);
			if (textLength == 0)
				return true;

			if (NativeMethods.DwmGetWindowAttribute(hWnd, NativeMethods.DWMWA_CLOAKED, out int cloaked, sizeof(int)) == 0 && cloaked != 0)
				return true;

			char[] buffer = new char[textLength + 1];
			int bufferLength = NativeMethods.GetWindowText(hWnd, buffer, buffer.Length);
			string title = new(buffer, 0, bufferLength);

			int exStyle = NativeMethods.GetWindowLongPtr(hWnd, NativeMethods.GWL_EXSTYLE).ToInt32();
			var state = new WindowState(hWnd, title, exStyle, 255);
			list.Add(state);

			return true;
		}, IntPtr.Zero);

		return list;
	}

	/// <summary>
	/// Applies WindowState values to the window matching the handle.
	/// </summary>
	/// <param name="state">A WindowState object.</param>
	public static void Apply(WindowState state)
	{
		int style = state.OriginalExStyle | NativeMethods.WS_EX_LAYERED;
		if (state.ClickThrough) style |= NativeMethods.WS_EX_TRANSPARENT;
		if (state.Topmost) style |= NativeMethods.WS_EX_TOPMOST;
		NativeMethods.SetWindowLongPtr(state.Handle, NativeMethods.GWL_EXSTYLE, new IntPtr(style));
		NativeMethods.SetLayeredWindowAttributes(state.Handle, 0, state.Opacity, NativeMethods.LWA_ALPHA);
		NativeMethods.SetWindowPos(
			state.Handle,
			state.Topmost ? NativeMethods.HWND_TOPMOST : NativeMethods.HWND_NOTOPMOST,
			0, 0, 0, 0,
			NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
	}

	/// <summary>
	/// Restores original WindowState values to the mwindow matching the handle.
	/// </summary>
	/// <param name="state">A WindowState object.</param>
	public static void Restore(WindowState state)
	{
		state.Opacity = state.OriginalOpacity;
		state.ClickThrough = state.OriginalClickThrough;
		state.Topmost = state.OriginalTopmost;

		NativeMethods.SetWindowLongPtr(state.Handle, NativeMethods.GWL_EXSTYLE, new IntPtr(state.OriginalExStyle));
		NativeMethods.SetLayeredWindowAttributes(state.Handle, 0, state.OriginalOpacity, NativeMethods.LWA_ALPHA);
		NativeMethods.SetWindowPos(
			state.Handle,
			state.OriginalTopmost ? NativeMethods.HWND_TOPMOST : NativeMethods.HWND_NOTOPMOST,
			0, 0, 0, 0,
			NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
	}
}
