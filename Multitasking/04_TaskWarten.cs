namespace Multitasking;

public class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //Ab hier auf den Task warten

		//Ab hier sequentiell

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		///////////////////////////////////////////
		
		Task t1 = Task.Run(Run);
		Task t2 = Task.Run(Run);
		Task t3 = Task.Run(Run);

		Task.WaitAll(t1, t2, t3); //Warte auf alle Tasks

		int schnellster = Task.WaitAny(t1, t2, t3); //Warte auf den ersten Task, welcher fertig wird (Gibt zurück, welcher Task am schnellsten war)
        Console.WriteLine($"Schnellster Task: {schnellster}");
    }

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Task: {i}");
        }
	}
}