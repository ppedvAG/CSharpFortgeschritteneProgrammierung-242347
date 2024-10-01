namespace Multitasking;

public class _09_Lock
{
	public static int Counter = 0;

	public static object Lock = new object();

    static void Main(string[] args)
    {
        for (int i = 0; i < 50; i++)
		{
			Task.Run(CounterIncrement);
		}

		Console.ReadKey();
    }

	public static void CounterIncrement()
	{
		for (int i = 0; i < 100; i++)
		{
			//Wenn ein Task hier steht, muss er warten bis das Lock wieder freigegeben wird
			//Wenn ein Task gerade diesen Block ausführt, müssen alle anderen Tasks warten, bis dieser Task fertig ist
			//lock (Lock)
			//{
			//	Counter++;
			//	Console.WriteLine(Counter);
			//}

			//Monitor.Enter/Exit: 1:1 gleicher Code wie lock-Block
			//Vorteil: Kann mit ifs kombiniert werden | Nachteil: Kann Exit vergessen
			Monitor.Enter(Lock);
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(Lock);

			//////////////////////////////////////////////

			//Interlocked: Stellt Operationen bereit, welche automatisch gelockt sind
			Interlocked.Add(ref Counter, 1); //Interlocked hat den Lock-Block intern
		}
	}
}