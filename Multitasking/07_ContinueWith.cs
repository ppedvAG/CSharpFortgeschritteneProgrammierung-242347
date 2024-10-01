namespace Multitasking;

public class _07_ContinueWith
{
    static void Main(string[] args)
    {
		//ContinueWith: Taskketten erzeugen, wenn der originale Task fertig ist, werden weitere Tasks losgetreten

		//Lösung zu t.Result
		//Aufgabe: Ergebnis soll mittendrin erscheinen, ohne den Code darunter anpassen zu müssen
		Task<int> t = new Task<int>(Berechne);

		//Hier kann der Task konfiguriert werden (vor Start)
		//In ContinueWith gibt es immer den Zugriff auf den vorherigen Task
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result));
		//t.ContinueWith(PrintResult); //Mit dedizierter Methode

		//Wenn t fertig ist, wird direkt im Anschluss ein neuer Task gestartet, welcher das Ergebnis ausgibt
		t.Start();

		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Main Thread: {i}");
        }

		//////////////////////////////////////////////////////////////

		//Options
		//Folgetasks werden allgemein immer gestartet
		//Über Options kann konfiguriert werden, das ein Folgetask nur unter bestimmten Bedingungen gestartet wird

		for (int i = 0; i < 20; i++)
		{
			Task<int> t1 = new Task<int>(Run);
			t1.ContinueWith(x => Console.WriteLine(x.Result), TaskContinuationOptions.OnlyOnRanToCompletion); //Nur wenn der Task erfolgreich fertig gelaufen ist, soll das Ergebnis ausgegeben werden
			t1.ContinueWith(x => Console.WriteLine(x.Exception.InnerException.Message), TaskContinuationOptions.OnlyOnFaulted); //Nur wenn der Task abgestürzt ist, soll die Exception ausgegeben werden
			t1.Start();
		}

		Console.ReadKey();
	}

	static int Berechne() //Längere Berechnung
	{
		Thread.Sleep(50);
		return Random.Shared.Next();
	}

	static void PrintResult(Task<int> vorherigerTask)
	{
        Console.WriteLine(vorherigerTask.Result);
    }

	static int Run()
	{
		int timeout = Random.Shared.Next() % 10000;
		Thread.Sleep(timeout);

		if (Random.Shared.Next() % 2 == 0)
			throw new Exception($"Exception aufgetreten nach {timeout / 1000.0}s");
		else
			return Random.Shared.Next();
	}
}