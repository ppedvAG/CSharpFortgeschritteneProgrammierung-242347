using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection: Zur Laufzeit alle möglichen Dinge über ein Objekt ermittelt
		//Nicht das Objekt selbst, sondern der Aufbau des Objekts

		//Reflection geht immer von Type aus
		
		//1. GetType()
		Program p = new Program();
		Type gt = p.GetType();

		//2. typeof(...)
		Type pt = typeof(Program);

		//Aufgabe: Methode von Program indirekt (per Reflection) ausführen
		gt.GetMethod("Test").Invoke(p, null);

        Console.WriteLine(gt.GetMethod("ToString").Invoke(p, null)); //ToString ausführen

		//Property beschreiben
		gt.GetProperty("Text").SetValue(p, "Hallo");
        Console.WriteLine(gt.GetProperty("Text").GetValue(p));

        //Objekt per Reflection erstellen
        object o = Activator.CreateInstance(pt);
		pt.GetMethod("Test").Invoke(o, null);

		//Assembly
		//Eine Projektumgebung
		//Eine Sammlung von Typen, Klassen, Dependencies, DLLs, ...
		Assembly a = Assembly.GetExecutingAssembly(); //Das derzeitige Projekt
		a.GetTypes(); //Alle Typen aus dem Assembly holen

		///////////////////////////////////////////////////////////

		//Aufgabenstellung: Component aus DelegatesEvents per Reflection einbinden
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2024_09_30\DelegatesEvents\bin\Debug\net8.0\DelegatesEvents.dll");
		Type compType = loaded.GetType("DelegatesEvents.Component");
		object comp = Activator.CreateInstance(compType);
		compType.GetEvent("Start").AddEventHandler(comp, () => Console.WriteLine("Start Reflection"));
		compType.GetEvent("Stop").AddEventHandler(comp, () => Console.WriteLine("Stop Reflection"));
		compType.GetEvent("Progress").AddEventHandler(comp, (int x) => Console.WriteLine($"Fortschritt Reflection: {x}"));
		compType.GetMethod("DoWork").Invoke(comp, null);
	}

	public string Text { get; set; }

	public void Test()
	{
        Console.WriteLine("Hallo Reflection");
    }
}
