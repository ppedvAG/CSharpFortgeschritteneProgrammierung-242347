using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();

		//Sequentiell
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//////////////////////////////////////////////////////////////

		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse);
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t2, t3);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Probleme: WaitAll, Tasse & Kaffee werden gleichzeitig fertig -> sollten nacheinander fertig werden

		//////////////////////////////////////////////////////////////

		//Task t1 = new Task(Toast);
		//Task t2 = new Task(Tasse);
		//Task t3 = t2.ContinueWith(x => Kaffee())
		//	.ContinueWith(x =>
		//	{
		//		if (t1.IsCompletedSuccessfully)
		//		{
		//			sw.Stop();
		//			Console.WriteLine(sw.ElapsedMilliseconds);
		//		}
		//	});
		//t1.ContinueWith(x =>
		//{
		//	if (t3.IsCompletedSuccessfully)
		//	{
		//		sw.Stop();
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//	}
		//});
		//t1.Start();
		//t2.Start();

		//Problem: Was ist, wenn Tasse + Kaffee länger dauern?
		//Äußerst umständlich

		//Console.ReadKey();

		//////////////////////////////////////////////////////////////

		//Bessere Lösung: Async/Await
		//Zwei Neuerungen: async Methoden, await Operator

		//async Methoden
		//Methoden, welche den async Modifier haben
		//Effekte: Methoden mit async können await benutzen
		//- async void: Kann nicht selbst awaited werden
		//- async Task: Kann selbst awaited werden
		//- async Task<T>: Kann selbst awaited werden und hat ein Ergebnis (return)

		//WICHTIG: Async-Methoden, welche normal aufgerufen werden, werden als Tasks ausgeführt (im Hintergrund)

		//await: Ähnlich wie t.Wait(), aber nicht blockierend
		//Task t1 = ToastAsync(); //Toast starten
		//Task t2 = TasseAsync(); //Tasse starten
		//await t2; //Warte hier darauf, das t2 fertig wird
		//Task t3 = KaffeeAsync(); //Kaffee starten
		//await t3; //Warte hier darauf, das t3 fertig wird
		//await t1; //Warte hier darauf, das t1 fertig wird
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Vereinfachung
		//Task t1 = ToastAsync();
		//await TasseAsync();
		//await KaffeeAsync();
		//await t1;
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//////////////////////////////////////////////////////////////

		//Frühstück mit Ergebnissen
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = TasseObjectAsync();
		//Tasse tasse = await t2; //await kann auch Objekte als Ergebnis aus Tasks herausholen
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse);
		//Kaffee kaffee = await t3;
		//Toast toast = await t1;
		//Fruehstueck f = new Fruehstueck(toast, kaffee);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Vereinfachung
		Task<Toast> t1 = ToastObjectAsync();
		Fruehstueck f = new Fruehstueck(await KaffeeObjectAsync(await TasseObjectAsync()), await t1);
		Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//////////////////////////////////////////////////////////////
		
		//Aufbau
		//- Start der Aufgabe
		//- Zwischenschritte (optional)
		//- Warten auf die Aufgabe
	}

	#region Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	static void Tasse()
	{
		Thread.Sleep(2500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(2500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Async
	static async Task ToastAsync()
	{
		await Task.Delay(4000); //Task.Delay == Thread.Sleep
		//await Task.Run(() => Thread.Sleep(4000)); //Selbiges wie darüber
		Console.WriteLine("Toast fertig");
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Async mit Objekten
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public record Toast();

public record Tasse();

public record Kaffee(Tasse t);

public record Fruehstueck(Kaffee k, Toast t);