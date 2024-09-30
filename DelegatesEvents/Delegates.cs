namespace DelegatesEvents;

internal class Delegates
{
	/// <summary>
	/// Definition eines Delegates
	/// </summary>
	public delegate void Vorstellung(string name);

	static void Main(string[] args)
	{
		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit einer Initialmethode (Methodenzeiger)
		v("Max"); //Delegate ausführen

		v += VorstellungDE; //Weiteren Methodenzeiger anhängen
		v("Udo");

		v += VorstellungEN;
		v += VorstellungEN;
		v += VorstellungEN;
		v("Tom");

		v -= VorstellungDE; //Methodenzeiger abnehmen
		v -= VorstellungDE;
		v -= VorstellungDE; //Kein Effekt
		v("Max");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v("Udo"); //Wenn das Delegate null ist, gibt es eine Exception

		if (v is not null)
			v("Udo");

		//Null Propagation: Führe den Code nach dem Fragezeichen nur aus, wenn die Variable davor nicht null ist
		v?.Invoke("Udo");

		foreach (Delegate dg in v.GetInvocationList()) //Delegate iterieren
		{

		}
	}

	static void VorstellungDE(string name) => Console.WriteLine($"Hallo mein Name ist {name}");

	static void VorstellungEN(string name) => Console.WriteLine($"Hello my name is {name}");
}
