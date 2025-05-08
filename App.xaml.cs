namespace PhantomWindow;

using System.Windows;
using PhantomWindow.Services;

/// <summary>
/// Base application.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// Constructor for base application.
	/// </summary>
	public App()
	{
		AppDomain.CurrentDomain.UnhandledException += (_, _) => StateService.RestoreAll();
		this.Exit += (_, _) => StateService.RestoreAll();
	}
}
