namespace PhantomWindow.Interop;

using System;
using System.Runtime.InteropServices;

using PhantomWindow.Config;

/// <summary>
/// Contains static methods to invoke Windows API (user32.dll)
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/)"/>
internal static partial class NativeMethods
{
	/// <summary>
	/// Enables extended window styles.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptrw"/>
	/// </summary>
	public const int GWL_EXSTYLE = -20;

	/// <summary>
	/// The retrieved handle identifies the specified window's owner window, if any.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow"/>
	/// </summary>
	public const uint GW_OWNER = 4;

	/// <summary>
	/// The window is a layered window. This style cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles"/>
	/// </summary>
	public const int WS_EX_LAYERED = 0x00080000;

	/// <summary>
	/// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles"/>
	/// </summary>
	public const int WS_EX_TRANSPARENT = 0x00000020;

	/// <summary>
	/// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles"/>
	/// </summary>
	public const int WS_EX_TOPMOST = 0x00000008;

	/// <summary>
	/// Use bAlpha to determine the opacity of the layered window.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setlayeredwindowattributes"/>
	/// </summary>
	public const uint LWA_ALPHA = 0x02;

	/// <summary>
	/// Retains the current size (ignores the cx and cy parameters).
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/>
	/// </summary>
	public const uint SWP_NOSIZE = 0x0001;

	/// <summary>
	/// Retains the current position (ignores X and Y parameters).
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/>
	/// </summary>
	public const uint SWP_NOMOVE = 0x0002;

	/// <summary>
	/// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/>
	/// </summary>
	public const uint SWP_NOACTIVATE = 0x0010;

	/// <summary>
	/// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/>
	/// </summary>
	public static readonly IntPtr HWND_TOPMOST = new(-1);

	/// <summary>
	/// Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window. 
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/>
	/// </summary>
	public static readonly IntPtr HWND_NOTOPMOST = new(-2);

	/// <summary>
	/// DWMWA_CLOAKED: window is cloaked (e.g. UWP/system host).
	/// </summary>
	public const int DWMWA_CLOAKED = 14;

	/// <summary>
	/// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongptrw"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
	/// <param name="nIndex">The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of a LONG_PTR.</param>
	/// <returns>If the function succeeds, the return value is the requested value.	If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
	public static partial IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

	/// <summary>
	/// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptrw"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs. The SetWindowLongPtr function fails if the process that owns the window specified by the hWnd parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.</param>
	/// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of a LONG_PTR.</param>
	/// <param name="dwNewLong">The replacement value.</param>
	/// <returns>If the function succeeds, the return value is the previous value of the specified offset.	If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
	public static partial IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	/// <summary>
	/// Retrieves a handle to a window that has the specified relationship (Z-Order or owner) to the specified window.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow""/>
	/// </summary>
	/// <param name="hWnd">A handle to a window. The window handle retrieved is relative to this window, based on the value of the uCmd parameter.</param>
	/// <param name="uCmd">The relationship between the specified window and the window whose handle is to be retrieved.</param>
	/// <returns></returns>
	[LibraryImport("user32.dll", EntryPoint = "GetWindow", SetLastError = false)]
	public static partial IntPtr GetWindow(IntPtr hWnd, uint uCmd);

	/// <summary>
	/// Sets the opacity and transparency color key of a layered window.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setlayeredwindowattributes"/>
	/// </summary>
	/// <param name="hwnd">A handle to the layered window. A layered window is created by specifying WS_EX_LAYERED when creating the window with the CreateWindowEx function or by setting WS_EX_LAYERED via SetWindowLong after the window has been created.</param>
	/// <param name="crKey">A COLORREF structure that specifies the transparency color key to be used when composing the layered window. All pixels painted by the window in this color will be transparent.</param>
	/// <param name="bAlpha">Alpha value used to describe the opacity of the layered window. Similar to the SourceConstantAlpha member of the BLENDFUNCTION structure. When bAlpha is 0, the window is completely transparent. When bAlpha is 255, the window is opaque.</param>
	/// <param name="dwFlags">An action to be taken.</param>
	/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static partial bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

	/// <summary>
	/// Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered according to their appearance on the screen. The topmost window receives the highest rank and is the first window in the Z order.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window.</param>
	/// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order.</param>
	/// <param name="X">The new position of the left side of the window, in client coordinates.</param>
	/// <param name="Y">The new position of the top of the window, in client coordinates.</param>
	/// <param name="cx">The new width of the window, in pixels.</param>
	/// <param name="cy">The new height of the window, in pixels.</param>
	/// <param name="uFlags">The window sizing and positioning flags.</param>
	/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static partial bool SetWindowPos(
		IntPtr hWnd,
		IntPtr hWndInsertAfter,
		int X,
		int Y,
		int cx,
		int cy,
		uint uFlags);

	/// <summary>
	/// n application-defined callback function used with the EnumWindows or EnumDesktopWindows function. It receives top-level window handles. The WNDENUMPROC type defines a pointer to this callback function. EnumWindowsProc is a placeholder for the application-defined function name.
	/// <see href="https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633498(v=vs.85)"/>
	/// </summary>
	/// <param name="hWnd">A handle to a top-level window.</param>
	/// <param name="lParam">The application-defined value given in EnumWindows or EnumDesktopWindows.</param>
	/// <returns>To continue enumeration, the callback function must return TRUE; to stop enumeration, it must return FALSE.</returns>
	public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	/// <summary>
	/// Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an application-defined callback function. EnumWindows continues until the last top-level window is enumerated or the callback function returns FALSE.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumwindows"/>
	/// </summary>
	/// <param name="lpEnumFunc">A pointer to an application-defined callback function.</param>
	/// <param name="lParam">An application-defined value to be passed to the callback function.</param>
	/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "EnumWindows")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static partial bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

	/// <summary>
	/// Determines the visibility state of the specified window.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window to be tested.</param>
	/// <returns>If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style, the return value is nonzero. Otherwise, the return value is zero.</returns>
	[LibraryImport("user32.dll", EntryPoint = "IsWindowVisible")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static partial bool IsWindowVisible(IntPtr hWnd);

	/// <summary>
	/// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). If the specified window is a control, the function retrieves the length of the text within the control. However, GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengthw"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window or control.</param>
	/// <returns>If the function succeeds, the return value is the length, in characters, of the text. Under certain conditions, this value might be greater than the length of the text (see Remarks).
	/// If the window has no text, the return value is zero.
	///	Function failure is indicated by a return value of zero and a GetLastError result that is nonzero.
	///	</returns>
	[LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW", SetLastError = true)]
	public static partial int GetWindowTextLength(IntPtr hWnd);

	/// <summary>
	/// Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another application.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window or control containing the text.</param>
	/// <param name="lpString">The buffer that will receive the text. If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
	/// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character. If the text exceeds this limit, it is truncated.</param>
	/// <returns>If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window or control handle is invalid, the return value is zero. To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "GetWindowTextW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
	public static partial int GetWindowText(IntPtr hWnd, Span<char> lpString, int nMaxCount);


	/// <summary>
	/// Retrieves a handle to the Shell's desktop window.
	/// </summary>
	/// <returns>The return value is the handle of the Shell's desktop window. If no Shell process is present, the return value is NULL.</returns>
	[LibraryImport("user32.dll", EntryPoint = "GetShellWindow", SetLastError = true)]
	public static partial IntPtr GetShellWindow();

	/// <summary>
	/// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid"/>
	/// </summary>
	/// <param name="hWnd">A handle to the window.</param>
	/// <param name="lpdwProcessId">A pointer to a variable that receives the process identifier. If this parameter is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not. If the function fails, the value of the variable is unchanged.</param>
	/// <returns>If the function succeeds, the return value is the identifier of the thread that created the window. If the window handle is invalid, the return value is zero. To get extended error information, call GetLastError.</returns>
	[LibraryImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
	public static partial uint GetWindowThreadProcessId(
		IntPtr hWnd,
		out uint lpdwProcessId);

	/// <summary>
	/// Retrieves the current value of a specified Desktop Window Manager (DWM) attribute applied to a window.
	/// <see href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute"/>
	/// </summary>
	/// <param name="hWnd">The handle to the window from which the attribute value is to be retrieved.</param>
	/// <param name="dwAttribute">A flag describing which value to retrieve, specified as a value of the DWMWINDOWATTRIBUTE enumeration. This parameter specifies which attribute to retrieve, and the pvAttribute parameter points to an object into which the attribute value is retrieved.</param>
	/// <param name="pvAttribute">A pointer to a value which, when this function returns successfully, receives the current value of the attribute. The type of the retrieved value depends on the value of the dwAttribute parameter. The DWMWINDOWATTRIBUTE enumeration topic indicates, in the row for each flag, what type of value you should pass a pointer to in the pvAttribute parameter.</param>
	/// <param name="cbAttribute">The size, in bytes, of the attribute value being received via the pvAttribute parameter. The type of the retrieved value, and therefore its size in bytes, depends on the value of the dwAttribute parameter.</param>
	/// <returns>If the function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
	[LibraryImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute", SetLastError = true)]
	public static partial int DwmGetWindowAttribute(
		IntPtr hWnd,
		int dwAttribute,
		out int pvAttribute,
		int cbAttribute);
}