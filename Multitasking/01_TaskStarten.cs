namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Task anlegen mit Methodenzeiger (Action)
		t.Start(); //WICHTIG: Task starten

		//Ab hier parallel

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		////////////////////////////

		//Task direkt starten
		Task.Factory.StartNew(Run); //Ab .NET Framework 4.0

		Task.Run(Run); //Ab .NET Framework 4.5
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
