// This file is copied from:
// Microsoft WPF Samples - https://github.com/microsoft/WPF-Samples/
// Copyright (c) 2015 Microsoft
// Licensed under the MIT License.

namespace PhantomWindow.Config;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Original Source: https://github.com/microsoft/wpf-samples/tree/main/Windows/SaveWindowState
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct WindowPlacement
{
	public int length;
	public int flags;
	public int showCmd;
	public Point minPosition;
	public Point maxPosition;
	public Rect normalPosition;
}
