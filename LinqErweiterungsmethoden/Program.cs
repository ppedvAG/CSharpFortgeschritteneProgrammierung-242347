using System.Diagnostics;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Listentheorie
		//IEnumerable
		//IEnumerable ist eine Anleitung zum Erstellen der fertigen Daten
		
		IEnumerable<int> zahlen = Enumerable.Range(0, 20); //Anleitung zum Erstellen der Daten von 0 bis 19

		//Anleitung kann ausgeführt werden durch eine Enumerierung (foreach, ToList, ToArray, ...)
		zahlen.ToList(); //Hier werden Daten erzeugt

		List<int> l = zahlen.ToList(); //ToList iteriert das IEnumerable
		l.AddRange(l); //AddRange iteriert das IEnumerable auch (2x iteriert)
					   //Ohne ToList wird ein kompletter Durchlauf gespart

		Enumerable.Range(0, 1_000_000_000); //1ms, hier wird nur die Anleitung angelegt

		//Enumerable.Range(0, 1_000_000_000).ToList(); //2.7s, hier werden Daten erzeugt (4GB im RAM werden belegt)

		/////////////////////////////////////////////////////////////

		//IEnumerator
		//Ermöglicht, eine Liste zu iterieren
		//Stellt einen Zeiger bereit, welcher immer auf ein bestimmtes Element zeigt
		//Kann mittels MoveNext() bewegt werden
		//Kann mittels Reset() an den Anfang gesetzt werden

		List<int> ints = Enumerable.Range(0, 100).ToList();
		foreach (int z in ints) //Hier wird der Enumerator angesprochen
		{
            Console.WriteLine(z);
        }

		//foreach ohne foreach
		IEnumerator<int> enumerator = ints.GetEnumerator();
		enumerator.MoveNext(); //Starte die Schleife

		start:
        Console.WriteLine(enumerator.Current);
		bool next = enumerator.MoveNext();
		if (next)
			goto start;
		enumerator.Reset(); //Enumerator an den Anfang zurücksetzen
        Console.WriteLine("Fertig");
		#endregion

		#region Einfaches Linq
		List<int> x = Enumerable.Range(1, 20).ToList();

        Console.WriteLine(x.Average());
        Console.WriteLine(x.Min());
        Console.WriteLine(x.Max());
        Console.WriteLine(x.Sum());

        Console.WriteLine(x.First()); //Gibt das Erste Element der Liste zurück, gibt eine Exception zurück wenn kein Element gefunden wird
        Console.WriteLine(x.FirstOrDefault()); //Gibt das Erste Element der Liste zurück, gibt null zurück wenn kein Element gefunden wird

		Console.WriteLine(x.Last());
        Console.WriteLine(x.LastOrDefault());

        //Console.WriteLine(x.First(e => e % 50 == 0)); //Exception
        Console.WriteLine(x.FirstOrDefault(e => e % 50 == 0)); //Exception
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Linq mit Objekten
		//Aufgabenstellung: Finde alle BMWs
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW);

		//Aufgabenstellung: Finde alle BMWs, die mindestens 250km/h fahren können
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW && e.MaxV >= 250);

		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.BMW) //12 Durchgänge
			.Where(e => e.MaxV >= 250); //4 Durchgänge

		//Fahrzeuge nach Marke sortieren
		fahrzeuge.OrderBy(e => e.Marke);

		//Fahrzeuge nach Marke und danach nach Geschwindigkeit sortieren
		fahrzeuge
			.OrderBy(e => e.Marke)
			.ThenBy(e => e.MaxV);

		//Absteigend sortieren
		fahrzeuge
			.OrderByDescending(e => e.Marke)
			.ThenByDescending(e => e.MaxV);

		//All und Any

		//All: Prüft, ob alle Elemente einer Liste eine Bedingung erfüllen

		//Aufgabe: Fahren alle Fahrzeuge mind. 250km/h?
		if (fahrzeuge.All(e => e.MaxV >= 250))
		{

		}

		//Aufgabe: Fährt mindestens eines unserer Fahrzeuge mind. 250km/h?
		if (fahrzeuge.Any(e => e.MaxV >= 250))
		{

		}

		//Sind alle Zeichen in einem Text Buchstaben?
		string text = "Hallo"; //Linq kann auch auf strings verwendet werden
		if (text.All(char.IsLetter))
		{

		}

		//Wieviele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW); //Ergebnis: int (4)

		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count(); //Mehr Aufwand

		//Avg, Sum, Min, Max, MinBy, MaxBy

		//Predicate vs. Selector
		//Predicate: Func, welche einen bool zurückgibt (z.B.: Where, Count, All, Any, ...)
		//Selector: Func, welche T zurückgibt (z.B.: OrderBy, Avg, Sum, Min, Max, MinBy, MaxBy, Select, ...)

		//Was ist die Durchschnittsgeschwindigkeit aller Fahrzeuge?
		fahrzeuge
			.Select(e => e.MaxV) //Mit Zwischenschritt
			.Average();

		fahrzeuge.Average(e => e.MaxV); //208.41666666666666
		fahrzeuge.Sum(e => e.MaxV);
		fahrzeuge.Min(e => e.MaxV); //Ergebnis: int
		fahrzeuge.Max(e => e.MaxV);

		fahrzeuge.MinBy(e => e.MaxV); //Ergebnis: Fahrzeug (Das Fahrzeug, mit der kleinsten Geschwindigkeit)
		fahrzeuge.MaxBy(e => e.MaxV);

		//Skip und Take

		//1. Beispiel: Webshop
		//10 Artikel auf der ersten Seite, nächste Seite nächsten Artikel

		int page = 0;
		fahrzeuge.Skip(page * 10).Take(10);
		//page 0: Skip 0, Take 10 (0-9)
		//page 1: Skip 10, Take 10 (10-19)

		//2. Top X Liste
		//Was sind die 3 schnellsten Fahrzeuge?
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(3);

		//Select
		//Formt eine Liste um
		//Select nimm eine Func, jedes Element wird in diese Func hineingeworfen
		//Am Ende kommt die Liste nach der Func heraus

		//1. Einzelnes Feld entnehmen
		//Finde alle Marken
		fahrzeuge.Select(e => e.Marke); //Liste mit nur Marken
		fahrzeuge
			.Select(e => e.Marke)
			.Distinct();

		fahrzeuge.Select(e => e.MaxV); //Liste von ints

		//2. Liste casten
		//List<int> x = Enumerable.Range(1, 20).ToList();
		x.Cast<double>(); //Funktioniert nicht, weil int und double nur über Umwege kompatibel sind
		x.Select(e => (double) e); //Funktioniert
		x.OfType<int>(); //Filterung nach einem Typen

		//3. Liste transformieren
		//Alle Dateien aus einem Ordner einlesen und nur den Dateinamen (ohne Pfad & Endung) ausgeben

		//Ohne Linq
		string[] files = Directory.GetFiles(@"C:\Windows");
		List<string> names = [];
		foreach (string file in files)
			names.Add(Path.GetFileNameWithoutExtension(file));

		//Mit Linq
		IEnumerable<string> pfade = Directory
			.GetFiles(@"C:\Windows")
			.Select(Path.GetFileNameWithoutExtension); //Select wendet den Term in der Klammer auf jedes Element der Liste an

        Console.WriteLine(names.SequenceEqual(pfade));

		//SelectMany
		//Glättet eine Liste
		List<int[]> a = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
		a.SelectMany(e => e); //Einzelne Liste mit allen Elementen

		//GroupBy
		//Gruppiert nach einem Kriterium
		//-> Erzeugt Gruppen anhand des Kriteriums und fügt jedes Element in seine Gruppe hinzu
		fahrzeuge.GroupBy(e => e.Marke); //Audi-Gruppe, BMW-Gruppe, VW-Gruppe

		//Gruppen angreifen

		//Per Lookup
		var lookup = fahrzeuge.GroupBy(e => e.Marke).ToLookup(e => e.Key);
		Console.WriteLine(lookup[FahrzeugMarke.Audi]);

		//Per Dictionary
		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, v => v.ToList()); //ToDictionary: Funktion mit 2 Lambda-Expressions (Key-Selector, Value-Selector)
		#endregion

		#region Erweiterungsmethoden
		//Methoden, welche an beliebige Typen angehängt werden können
		int zahl = 38529;
		zahl.Quersumme();

        Console.WriteLine(321957.Quersumme());

		Console.WriteLine(new int[] { 1, 2, 3 }.AsString());

		//Eigentlicher Code
		ExtensionMethods.Quersumme(321957);

		//Alle BMWs
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW);
		Enumerable.Where(fahrzeuge, e => e.Marke == FahrzeugMarke.BMW);
		#endregion
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
public class Fahrzeug
{
	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }
}

public enum FahrzeugMarke { Audi, BMW, VW }