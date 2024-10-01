namespace Multitasking;

public class _02_TaskMitParameter
{
    static void Main(string[] args)
    {
		Task t = new Task(Run, 200); //Daten mitgeben über Action<object>
		t.Start();

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		//Task wird hier vorzeitig abgebrochen
		//Der Main Thread ist ein Vordergrundthread -> Hält das Programm vom Beenden ab bis er fertig ist
		//Alle Tasks sind Hintergrundthreads -> Werden abgebrochen, wenn alle Vordergrundthreads fertig sind

		//Main Thread aufhalten
		Console.ReadKey();
	}

	static void Run(object o) //Die Daten als object Parameter
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
			{
                Console.WriteLine($"Task: {i}");
            }
		}
	}
}