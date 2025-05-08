// This file copied from:
// Microsoft WPF Samples - https://github.com/microsoft/WPF-Samples/
// Copyright (c) 2015 Microsoft
// Licensed under the MIT License.

namespace PhantomWindow.Config;

using System;
using System.Runtime.InteropServices;


/// <summary>
/// Original source: https://github.com/microsoft/wpf-samples/tree/main/Windows/SaveWindowState
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Point
{
	public int X;
	public int Y;

	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}
}
