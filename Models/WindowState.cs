namespace PhantomWindow.Models;

using System;

using PhantomWindow.Interop;

/// <summary>
/// contains values related to the state of a window.
/// </summary>
public class WindowState
{
	/// <summary>
	/// Creates a new WindowState.
	/// </summary>
	/// <param name="handle">The pointer value of the window handle.</param>
	/// <param name="title">The title text of the window.</param>
	/// <param name="originalExStyle">The original window style values.</param>
	/// <param name="originalOpacity">The original opacity value.</param>
	public WindowState(IntPtr handle, string title, int originalExStyle, byte originalOpacity)
	{
		Handle = handle;
		Title = title;
		OriginalExStyle = originalExStyle;
		OriginalOpacity = originalOpacity;
		OriginalClickThrough = (originalExStyle & NativeMethods.WS_EX_TRANSPARENT) != 0;
		OriginalTopmost = (originalExStyle & NativeMethods.WS_EX_TOPMOST) != 0;
		Opacity = originalOpacity;
		ClickThrough = OriginalClickThrough;
		Topmost = OriginalTopmost;
	}

	/// <summary>
	/// The pointer value of the window handle.
	/// </summary>
	public IntPtr Handle { get; }

	/// <summary>
	/// The title text of the window.
	/// </summary>
	public string Title { get; }

	/// <summary>
	/// The original window style values.
	/// </summary>
	public int OriginalExStyle { get; }

	/// <summary>
	/// The original opacity value.
	/// </summary>
	public byte OriginalOpacity { get; }

	/// <summary>
	/// The original click-through setting value.
	/// </summary>
	public bool OriginalClickThrough { get; }

	/// <summary>
	/// The original topmost setting value.
	/// </summary>
	public bool OriginalTopmost { get; }

	/// <summary>
	/// The current opacity value.
	/// </summary>
	public byte Opacity { get; set; }

	/// <summary>
	/// The current clickthrough state.
	/// </summary>
	public bool ClickThrough { get; set; }

	/// <summary>
	/// The current topmost state.
	/// </summary>
	public bool Topmost { get; set; }
}
