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
public struct Rect
{
	public Rect(int left, int top, int right, int bottom)
	{
		Left = left;
		Top = top;
		Right = right;
		Bottom = bottom;
	}

	public int Left;
	public int Top;
	public int Right;
	public int Bottom;
}