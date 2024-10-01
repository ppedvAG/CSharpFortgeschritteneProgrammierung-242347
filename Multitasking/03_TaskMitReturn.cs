namespace Multitasking;

public class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Calculate); //Hier wird eine Func benötigt
		t.Start();

		//Console.WriteLine(t.Result); //Problem: Blockiert die Schleife im Anschluss

		bool printed = false;
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");

			//Nicht immer möglich, umständlich
			if (t.IsCompletedSuccessfully && !printed)
			{
				Console.WriteLine(t.Result);
				printed = true;
			}
        }

		//Console.WriteLine(t.Result); //Problem: Ergebnis kommt immer danach, auch wenn das Ergebnis vorher fertig ist
		//Lösungen: ContinueWith, async/await
	}

	static int Calculate() //Längere Berechnung
	{
		Thread.Sleep(50);
		return Random.Shared.Next();
	}
}