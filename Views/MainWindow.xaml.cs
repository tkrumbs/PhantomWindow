namespace PhantomWindow.Views;

using System.ComponentModel;
using System.Windows;

using PhantomWindow.Config;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		ApplicationWindowManager.Load(this);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		ApplicationWindowManager.Save(this);
	}
}
