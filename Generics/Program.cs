using System.Collections;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<int> ints = new List<int>();
		ints.Add(1); //T wird durch int ersetzt

		List<string> strings = new List<string>();
		strings.Add("A"); //T wird durch string ersetzt

		Dictionary<int, string> dict = [];
		dict.Add(1, "1");

		DataStore<int> zahlen = [];
		zahlen[0] = 5;
		zahlen.FindItem(5);
		foreach (int x in zahlen)
		{
            Console.WriteLine(x);
        }

		Test<int>();

		Enum.Parse(typeof(DayOfWeek), "Monday"); //Rückgabetyp object, Cast notwendig
		Enum.Parse<DayOfWeek>("Monday"); //Rückgabetyp DayOfWeek
	}

	/// <summary>
	/// Wird verwendet, wenn eine Methode einen Typen als Parameter bekommen soll
	/// </summary>
	static T Test<T>()
	{
		T x = default(T); //Standardwert von T
        Console.WriteLine(nameof(T)); //Typ hinter T als string
        Console.WriteLine(typeof(T)); //Typ von T als Type Objekt (für Typvergleiche/Reflection)

		if (x != null)
			return x;
		else
			return default;
    }
}

public class DataStore<T> : IEnumerable<T>, IProgress<int>
{
	private T[] data;

	public List<T> Data => data.ToList();

	public void Add(T item, int index)
	{
		data[index] = item;
	}

	public T FindItem(T item)
	{
		return data.First(e => e.Equals(item));
	}

	public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();

	public void Report(int value)
	{
		throw new NotImplementedException();
	}

	public T this[int index]
	{
		get => data[index];
		set => data[index] = value;
	}
}
