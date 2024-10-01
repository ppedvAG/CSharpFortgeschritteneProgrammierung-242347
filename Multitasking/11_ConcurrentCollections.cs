using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Multitasking;

public class _11_ConcurrentCollections
{
    static void Main(string[] args)
    {
		ConcurrentDictionary<int, string> dict = [];
		dict.TryAdd(1, "Eins");
		dict.AddOrUpdate(1, "Eins", (key, value) => value);
		//Func, welche ein Update durchführt, falls der Key bereits existiert
		//Diese Func überschreibt den Value, falls er bereits existiert

		//Benötigt ein Paket namens System.ServiceModel.Primitives
		//Äquivalent zur normalen List, aber Threadsicher
		SynchronizedCollection<int> list = [];
		list.Add(1);
		list.Add(2);
		list.Add(3);

		foreach (int x in list)
		{
			Console.WriteLine(x);
		}

		Console.WriteLine(list[0]);
		Console.WriteLine(list[1]);
		Console.WriteLine(list[2]);

		//Bag: Unsortierte Liste, hat keinen Index
		//Für viele Anwendungszwecke nutzbar, aber nicht für alle
        ConcurrentBag<int> bag = [];
	}
}