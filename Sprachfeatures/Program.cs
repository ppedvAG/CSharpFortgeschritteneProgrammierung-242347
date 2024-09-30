using System.Collections;
using System.Reflection.Metadata.Ecma335;
using IntList = System.Collections.Generic.List<int>;

namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		object o = 10;
		if (o is int)
		{
			int i = (int) o;
			//Vererbungshierarchietypvergleich
		}

		if (o is object x) //Automatischer Cast
		{
			//true
		}

		if (o is IComparable)
		{
			//Hat o das Interface?
		}

		if (o.GetType() == typeof(int))
		{
			//Genauer Typvergleich
		}

		if (o.GetType() == typeof(object))
		{
			//false
		}

		(int, string) t;
		t.Item1 = 1;
		t.Item2 = "1";

		object[] werte = [1, "1"];
		Console.WriteLine(werte[0]);
		Console.WriteLine(werte[1]);

		double d = 32_328_523.123_498;
		
		//class und struct

		//class
		//Referenztyp
		//Wenn ein Objekt einer Klasse auf eine Variable zugewiesen wird, wird eine Referenz erzeugt
		//Wenn zwei Objekte einer Klasse verglichen werden, werden die Speicheradressen verglichen
		Person p = new Person("Max");
		Person p2 = p;
		p.Name = "Udo";

		//Zeiger auf das selbe unterliegende Objekt im Arbeitsspeicher
        Console.WriteLine(p.Name);
        Console.WriteLine(p2.Name);

        Console.WriteLine(p == p2);
        Console.WriteLine(p.GetHashCode() == p2.GetHashCode());
        Console.WriteLine(p.GetHashCode());
        Console.WriteLine(p2.GetHashCode());

        //struct
        //Wertetyp
        //Wenn ein Objekt eines Structs auf eine Variable zugewiesen wird, wird eine Kopie erzeugt
        //Wenn zwei Objekte eines Structs verglichen werden, werden die Inhalte verglichen
        int original = 10;
		int neu = original;
		original = 20;

		//In neu befindet sich eine Kopie ohne Bezug auf original
        Console.WriteLine(original);
        Console.WriteLine(neu);

        Console.WriteLine(original == neu);

		//struct per Referenz übergeben
		int original2 = 10;
		ref int neu2 = ref original2; //Referenz herstellen mittels ref-Keyword
		original2 = 20;
	
		Test(ref neu2);

		//Null-Coalescing Operator (??-Operator): Wenn die Linke Seite nicht null ist, nimm die Linke Seite, sonst die Rechte Seite
		string str = "Hallo";
		if (str != null)
            Console.WriteLine(str);
		else
            Console.WriteLine("str ist leer");

		Console.WriteLine(str != null ? str : "str ist leer");

        Console.WriteLine(str ?? "str ist leer");

		//Funktion konfigurieren (Nur die Werte übergeben, welche auch wirklich interessant sind)
		Test2(y: 5, z: 1);
		Test2(x: 5, z: 1);
		Test2(z: 1);

		unsafe
		{

		}

		int zahl = 3;
		string zahlAlsText = zahl switch
		{
			0 => "Null",
			1 => "Eins",
			2 => "Zwei",
			3 => "Drei",
			_ => "Andere Zahl"
		};

		//Strg + .: Schnelloptionen anzeigen
		switch (zahl)
		{
			case 0:
				zahlAlsText = "Null";
				break;
			case 1:
				zahlAlsText = "Eins";
				break;
			case 2:
				zahlAlsText = "Zwei";
				break;
			case 3:
				zahlAlsText = "Drei";
				break;
			default:
				zahlAlsText = "Andere Zahl";
				break;
		}

		using (StreamWriter sw = new StreamWriter("Test.txt"))
		{

		} //.Dispose()

		using StreamWriter sw2 = new StreamWriter("Test.txt");
		//.Dispose() am Ende der Funktion

		//Wenn eine externe Resource angesprochen wird (z.B. File, DB, Webschnittstelle, ...) IMMER using benutzen

		void Inner()
		{
			zahl++;
		}

		//Casefehler beheben (Anfangsbuchstabe groß, Rest klein)
		string name = "MaX";
		string nameFixed = char.ToUpper(name[0]) + name[1..].ToLower();
        Console.WriteLine(nameFixed);

        List<int> liste = new List<int>();
		liste ??= new List<int>(); //Wenn die Liste null ist, erstelle sie

		if (liste == null)
			liste = new List<int>();

		//String-Interpolation ($-String): Code in einen String einbetten
		int a = 10;
		string b = "Hallo";
		bool c = false;

		string kombi = "Die Zahl ist: " + a + ", der Text ist: " + b + ", der Boolean ist: " + c;
        Console.WriteLine(kombi);

		string inter = $"Die Zahl ist: {a}, der Text ist: {b}, der Boolean ist: {c}";
        Console.WriteLine(inter);

        Console.WriteLine($"Die Zahl mal 2 ist: {a * 2}");
        Console.WriteLine($"Die Zahl ist: {(a % 2 == 0 ? "durch 2 teilbar" : "nicht durch 2 teilbar")}");
        Console.WriteLine($"Die Zahl ist: {zahl switch
		{
			0 => "Null",
			1 => "Eins",
			2 => "Zwei",
			3 => "Drei",
			_ => "Andere Zahl"
		}}");

		//Verbatim-String (@-String): String, welcher Escape-Sequenzen ignoriert
		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.1\System.Runtime.InteropServices.dll";
		string pfad2 = "C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\8.0.1\\System.Runtime.InteropServices.dll";

		Mitarbeiter m = new Mitarbeiter(1, "Max", 30);
        Console.WriteLine(m.ID);
        Console.WriteLine(m.Name);
        Console.WriteLine(m.Alter);
        Console.WriteLine(m);

		(int id, string n, int alter) = m;

		StringBuilder sb;

        Console.WriteLine("Das ist ein \"Text\"");
        Console.WriteLine($"""Das ist ein "Text""");

		Console.WriteLine("Das ist ein {Text}");
		Console.WriteLine($$"""Das ist ein {Text} {{m}}""");
		Console.WriteLine($"Das ist ein {{Text}} {m}");

		int[] v = { 1, 2, 3 };
		v = new int[] { 1, 2, 3 };
		v = new[] { 1, 2, 3 };
		v = [1, 2, 3]; //Ab C# 12

		List<int> u = new List<int>();
		u = new();
		u = [];

		Dictionary<int, string> dict = [];

		IntList l; //Alias für List<int>

		DateTime dt = DateTime.Now;
		dt += TimeSpan.FromDays(3);

        Console.WriteLine(dt >= DateTime.Now);

		double g = 10;
		int h = (int) g;

		foreach (int i in u)
		{

		}

		Person k = new Person("");
		foreach (object q in k)
		{

		}

        Console.WriteLine(k[10]);
		Console.WriteLine(k["Hallo"]);

		string s1 = "Hallo";
		string s2 = "Welt";
		s1 += s2; //Kopie wird erzeugt mit HalloWelt -> 3 Strings im RAM

		StringBuilder builder = new();
		builder.Append(s1);
		builder.Append(s2);
        Console.WriteLine(builder.ToString());

		string zusammenbauen = "";
		for (int i = 0; i < 100; i++)
		{
			zusammenbauen += i.ToString();
            Console.WriteLine(zusammenbauen);
        }
    }

	static void Test(ref int x) => x = 100;

	static unsafe void Test2(int x = 0, int y = 0, int z = 0)
	{
		//...
	}

	/// <summary>
	/// https://github.com/dotnet/csharplang/blob/main/meetings/2022/LDM-2022-04-06.md#parameter-null-checking
	/// </summary>
	/// <param name="s"></param>
	//static void Test3(string s!!)
	//{

	//}
}

unsafe class Person(string Name) : IEnumerable
{
	public string Name { get; set; } = Name;

	public IEnumerator GetEnumerator()
	{
		return new List<int>().GetEnumerator();
		//throw new NotImplementedException();
	}

	public int this[int x]
	{
		get => 0;
	}

	public int this[string x]
	{
		get => 0;
	}

	//  public Person(string Name)
	//  {
	//this.Name = Name;
	//  }

	public static bool operator ==(Person a, Person b)
	{
		return a.Name == b.Name;
	}

	public static bool operator !=(Person a, Person b)
	{
		return !(a == b);
	}
}

public record Mitarbeiter(int ID, string Name, int Alter);