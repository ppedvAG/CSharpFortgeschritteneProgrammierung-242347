namespace DelegatesEvents;

/// <summary>
/// Hier wird eine Komponente implementiert, welche eine länger andauernde Arbeit ausführen soll
/// 
/// Entwicklerseite -> Definition von Events (event Keyword), ausführen der Events
/// </summary>
public class Component
{
	//public event EventHandler Start;

	//public event EventHandler Stop;

	//public event EventHandler<int> Progress;

	public event Action Start;

	public event Action Stop;

	public event Action<int> Progress;

	public void DoWork()
	{
		Start?.Invoke();
        for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i);
		}
		Stop?.Invoke();
	}
}