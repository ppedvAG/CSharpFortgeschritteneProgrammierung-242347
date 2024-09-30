using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DelegatesEvents;

/// <summary>
/// Events
/// 
/// Statischer Punkt, an welchen eine Methode angehängt werden kann
/// Wird mit dem event-Keyword und einem Delegate definiert
/// 
/// ////////////////////////////////////////// 
/// 
/// Beispiel: Click-Event in WPF/WinForms
/// Entwicklerseite (Entwickler bei MS): Definiert, wann das Event ausgeführt wird, und führt dieses aus
/// - Button muss aktiviert sein, Maus muss auf dem Button sein, kein anderes UI-Element darf über dem Button sein, ...
/// Benutzerseite (Wir selbst): Definieren die Methode, welche ausgeführt wird, wenn der Button geklickt wird
/// </summary>
public class Events
{
	/// <summary>
	/// Definition von einem Event (Entwicklerseite)
	/// 
	/// EventHandler
	/// Standarddelegate für Events
	/// Stellt einen Sender (das Objekt, welches das Event auslöst) und EventArgs bereit (Daten des Events)
	/// </summary>
	public event EventHandler TestEvent;

	/// <summary>
	/// Bei EventHandler können auch bestimmte Daten mitgegeben werden
	/// Diese werden über eine eigene Klasse mit der Oberklasse EventArgs definiert
	/// </summary>
	public event EventHandler<CustomEventArgs> ArgsEvent;

	public event EventHandler<int> IntEvent;

	static void Main(string[] args) => new Events().Start();

	public void Start()
	{
		TestEvent += Events_TestEvent; //Benutzerseite
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent?.Invoke(this, new CustomEventArgs() { Status = "Erfolg" });

		IntEvent += Events_IntEvent;
		IntEvent?.Invoke(this, 10);

		//////////////////////////////////////////////

		ObservableCollection<int> list = [];
		list.CollectionChanged += List_CollectionChanged;
		list.Add(10); //Methode wird ausgeführt
		list.Remove(10); //Methode wird ausgeführt
	}

	private void List_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add:
                Console.WriteLine($"Element hinzugefügt: {e.NewItems[0]}");
                break;
			case NotifyCollectionChangedAction.Remove:
				Console.WriteLine($"Element entfernt: {e.OldItems[0]}");
				break;
		}
	}

	/// <summary>
	/// Event 1
	/// </summary>
	private void Events_TestEvent(object? sender, EventArgs e)
	{
        Console.WriteLine("TestEvent ausgeführt");
	}

	/// <summary>
	/// Event 2
	/// </summary>
	private void Events_ArgsEvent(object? sender, CustomEventArgs e)
	{
		Console.WriteLine(e.Status);
	}

	/// <summary>
	/// Event 3
	/// </summary>
	private void Events_IntEvent(object? sender, int e)
	{
		Console.WriteLine(e);
	}
}

public class CustomEventArgs : EventArgs
{
	public string Status { get; set; }
}