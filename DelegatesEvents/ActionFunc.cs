namespace DelegatesEvents;

public class ActionFunc
{
    static void Main(string[] args)
    {
		//Action, Func
		//Delegates, welche an vielen Stellen in C# vorkommen
		//z.B.: Linq, Multitasking/async, await, Reflection, ...
		//Essentiell für die fortgeschrittene Programmierung mit C#

		////////////////////////////////////////////////////////////////////////

		//Action
		//Vorgegebenes Delegate, welches einen Methodenzeiger halten kann, der void zurückgibt und bis zu 16 Parameter haben kann
		//Die Parameter werden mittels Generics mitgegeben
		
		//Action<int, int> action = new Action<int, int>(Addiere); //Erstellung der Action mit einer Initialmethode (Methodenzeiger)
		
		Action<int, int> action = Addiere;
		action?.Invoke(4, 6);

		List<int> ints = [1, 2, 3, 4, 5];
		//Aufgabenstellung: Alle Elemente der Liste in der Konsole ausgeben mittels ForEach-Funktion
		ints.ForEach(PrintZahl); //Funktionszeiger zu PrintZahl hier übergeben (Action-Parameter)
		ints.ForEach(Console.WriteLine);

		//Eigene Funktion mit Action
		DoAction(4, 9, Addiere); //Über den Action-Parameter steuern, was die Funktion machen soll

		////////////////////////////////////////////////////////////////////////

		//Func
		//Vorgegebenes Delegate, welches einen Methodenzeiger halten kann, der einen beliebigen Rückgabetyp zurückgibt und bis zu 16 Parameter haben kann
		//Die Parameter werden mittels Generics mitgegeben

		Func<int, int, double> func = Dividiere;
		double? d = func?.Invoke(5, 2); //Wenn die func selbst null ist, kommt hier null zurück -> Nullable double (double?)
		double d2 = func?.Invoke(7, 3) ?? double.NaN; //Normalen double verwenden

		//Aufgabenstellung: Alle Zahlen finden, welche durch 2 teilbar sind
		ints.Where(TeilbarDurch2);

		//Eigene Funktion mit Func
		DoFunc(8, 2, Dividiere); //Über den Func-Parameter steuern, was die Funktion machen soll

		//Anonyme Methoden: Methoden, die keine separate Implementation haben (nur einmal verwendet werden)
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anonyme Methoden in Verwendung
		Func<int, bool> teilbarDurch2 = e => e % 2 == 0; //Anonyme Funktion zwischenspeichern
		ints.Where(teilbarDurch2); //Anonyme Funktion verwenden
		
		//Anonyme Funktion ohne Zwischenspeicher verwenden
		ints.Where(delegate (int x) { return x % 2 == 0; });
		ints.Where((int x) => { return x % 2 == 0; });
		ints.Where(x => { return x % 2 == 0; });
		ints.Where(x => x % 2 == 0);
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");

	static void PrintZahl(int x) => Console.WriteLine($"Die Zahl ist: {x}");

	static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region	Func
	static double Dividiere(int x, int y) => (double) x / y;

	static bool TeilbarDurch2(int x) => x % 2 == 0;

	static void DoFunc(int x, int y, Func<int, int, double> func) => Console.WriteLine(func?.Invoke(x, y));
	#endregion
}