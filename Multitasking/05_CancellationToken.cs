namespace Multitasking;

public class _05_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new(); //Quelle von CancellationTokens (Sender von Cancel Signalen)
		CancellationToken token = cts.Token; //Token aus der Source erzeugen (Struct -> Kopie)

		Task t = new Task(Run, token);
		t.Start();

		Thread.Sleep(500);

		//cts.Cancel(); //Task wird abgebrochen

		/////////////////////////////////////

		Task t2 = new Task(Run2, token); //Hier Token per Parameter mitgeben, um den Token im Task angreifbar zu machen
		t2.Start();

		Thread.Sleep(500);

		cts.Cancel(); //Task wird abgebrochen
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
			Thread.Sleep(25);
		}
	}

	static void Run2(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Task per Hand abbrechen
				//Exception ist hier nicht sichtbar

				Console.WriteLine($"Task: {i}");
				Thread.Sleep(25);
			}
		}
	}
}