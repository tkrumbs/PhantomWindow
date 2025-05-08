namespace PhantomWindow.ViewModels;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

using PhantomWindow.Models;
using PhantomWindow.Services;


/// <summary>
/// The view model associated with the MainWindow view.
/// </summary>
public class MainViewModel : INotifyPropertyChanged
{
	private WindowState? _selectedAvailable;

	/// <summary>
	/// Creates the MainViewModel object.
	/// </summary>
	public MainViewModel()
	{
		AvailableWindows = [];
		TargetWindows = [];
		TargetWindows.CollectionChanged += OnTargetsChanged;

		RefreshAvailable();
	}

	/// <summary>
	/// Property changed event.
	/// </summary>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>
	/// Collection of currently available window.
	/// </summary>
	public ObservableCollection<WindowState> AvailableWindows { get; }

	/// <summary>
	/// Collection of current windows targeted for modification.
	/// </summary>
	public ObservableCollection<TargetWindowViewModel> TargetWindows { get; }

	public WindowState? SelectedAvailable
	{
		get => _selectedAvailable;
		set
		{
			if (_selectedAvailable != value)
			{
				_selectedAvailable = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAvailable)));
			}
		}
	}

	/// <summary>
	/// Refreshes the list of available WindowState objects with current windows.
	/// </summary>
	public void RefreshAvailable()
	{
		AvailableWindows.Clear();
		foreach (var w in WindowManager.EnumerateWindows())
			AvailableWindows.Add(w);
	}

	/// <summary>
	/// Handle the doubleclick of an available WindowState.
	/// </summary>
	public void HandleAvailableWindowDoubleClick()
	{
		if (SelectedAvailable is null) return;

		if (StateService.Add(SelectedAvailable))
		{
			TargetWindows.Add(new TargetWindowViewModel(SelectedAvailable));
		}
	}

	/// <summary>
	/// Removes the target WindowState.
	/// </summary>
	/// <param name="o"></param>
	/// <param name="eventArgs"></param>
	public void RemoveTargetWindowState(object o, System.Windows.RoutedEventArgs eventArgs)
	{
		if (eventArgs is System.Windows.RoutedEventArgs e &&
			e.OriginalSource is System.Windows.FrameworkElement fe &&
			fe.DataContext is TargetWindowViewModel vm)
		{
			StateService.Remove(vm.Handle);
			TargetWindows.Remove(vm);
		}
	}

	private void OnTargetsChanged(object? s, NotifyCollectionChangedEventArgs e)
	{
		// TODO: Remove if not going to use.
	}
}
