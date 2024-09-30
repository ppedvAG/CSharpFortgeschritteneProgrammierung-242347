namespace DelegatesEvents;

public class User
{
    static void Main(string[] args)
    {
		Component comp = new();
		comp.Start += Comp_Start;
		comp.Stop += Comp_Stop;
		comp.Progress += Comp_Progress;
		comp.DoWork();
    }

	/// <summary>
	/// Über Events kann der Benutzer jetzt frei entscheiden, was bei Start passieren soll
	/// </summary>
	private static void Comp_Start()
	{
		Console.WriteLine("Prozess gestartet");
	}

	private static void Comp_Stop()
	{
		Console.WriteLine("Prozess fertig");
	}

	private static void Comp_Progress(int e)
	{
        Console.WriteLine($"Fortschritt: {e}");
    }
}