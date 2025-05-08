namespace PhantomWindow.Services;

using PhantomWindow.Models;

/// <summary>
/// Controls WindowState collection.
/// </summary>
internal static class StateService
{
	private static readonly Dictionary<IntPtr, WindowState> StatesDictionary = [];

	/// <summary>
	/// List of all WindowStates.
	/// </summary>
	public static IReadOnlyCollection<WindowState> States => StatesDictionary.Values;

	/// <summary>
	/// Add WindowState to collection.
	/// </summary>
	/// <param name="state">A WindowState.</param>
	/// <returns>Value of true if added; otherwise false.</returns>
	public static bool Add(WindowState state)
	{
		if (!StatesDictionary.ContainsKey(state.Handle))
		{
			StatesDictionary[state.Handle] = state;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Removes WindowState if it exists in the collection.
	/// </summary>
	/// <param name="handle">The window handle.</param>
	public static void Remove(IntPtr handle)
	{
		if (StatesDictionary.TryGetValue(handle, out var state))
		{
			WindowManager.Restore(state);
			StatesDictionary.Remove(handle);
		}
	}

	/// <summary>
	/// Restore all windows to original values.
	/// </summary>
	public static void RestoreAll()
	{
		foreach (var state in StatesDictionary.Values)
			WindowManager.Restore(state);
		StatesDictionary.Clear();
	}
}
