namespace Multitasking;

public class _06_ExceptionsImTask
{
	static void Main(string[] args)
	{
		Task<int> t1 = Task<int>.Run(Run);
		Task<int> t2 = Task<int>.Run(Run);
		Task<int> t3 = Task<int>.Run(Run);
	
		try
		{
			//Wait, WaitAll, Result werfen die AggregateException
			//t.Wait();
			Task.WaitAll(t1, t2, t3);
			//Console.WriteLine(t.Result);
		}
		catch (AggregateException e)
		{
			//AggregateException ist eine Sammelexception für mehrere Fehler
			foreach (Exception ex in e.InnerExceptions)
				Console.WriteLine(ex.Message);
		}

		//Problem: Wenn ein Task vor allen anderen fehlschlägt, kommt die Fehlermeldung erst wenn alle Tasks fertig sind
		//Lösung: ContinueWith

		Console.ReadKey();
	}

	static int Run()
	{
		int timeout = Random.Shared.Next() % 10000;
		Thread.Sleep(timeout);
		throw new Exception($"Exception aufgetreten nach {timeout / 1000.0}s");
	}
}