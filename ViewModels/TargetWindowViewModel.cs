namespace PhantomWindow.ViewModels;

using System;
using System.ComponentModel;

using PhantomWindow.Models;
using PhantomWindow.Services;

/// <summary>
/// ViewModel for the Target Window UI.
/// </summary>
public class TargetWindowViewModel : INotifyPropertyChanged
{
	private byte _opacity;
	private bool _clickThrough;
	private bool _topmost;

	/// <summary>
	/// Creates a TargetWindowViewModel object.
	/// </summary>
	/// <param name="model"></param>
	public TargetWindowViewModel(WindowState model)
	{
		Model = model;
		_opacity = model.Opacity;
		_clickThrough = model.ClickThrough;
		_topmost = model.Topmost;
	}

	/// <summary>
	/// Property changed event.
	/// </summary>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>
	/// Associated WindowState for the TargetWindow object.
	/// </summary>
	public WindowState Model { get; }

	/// <summary>
	/// The window handle of the WindowState.
	/// </summary>
	public IntPtr Handle => Model.Handle;

	/// <summary>
	/// The title of the WindowState.
	/// </summary>
	public string Title => Model.Title;

	/// <summary>
	/// The window opacity state.
	/// </summary>
	public byte Opacity
	{
		get => _opacity;
		set
		{
			if (_opacity == value) return;
			_opacity = value;
			Model.Opacity = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Opacity)));
			WindowManager.Apply(Model);
		}
	}

	/// <summary>
	/// The window clickthrough state.
	/// </summary>
	public bool ClickThrough
	{
		get => _clickThrough;
		set
		{
			if (_clickThrough == value) return;
			_clickThrough = value;
			Model.ClickThrough = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClickThrough)));
			WindowManager.Apply(Model);
		}
	}

	/// <summary>
	/// The window topmost state.
	/// </summary>
	public bool Topmost
	{
		get => _topmost;
		set
		{
			if (_topmost == value) return;
			_topmost = value;
			Model.Topmost = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Topmost)));
			WindowManager.Apply(Model);
		}
	}
}
