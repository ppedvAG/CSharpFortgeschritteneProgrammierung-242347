using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
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

		//Pfad + Folder anlegen
		//Path, Directory, File
		string folderPath = "Test";
		string filePath = Path.Combine(folderPath, "Test.csv");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//CSV

		//Innerhalb von C#

		List<string> csv = [];
		foreach (Fahrzeug fzg in fahrzeuge)
		{
			csv.Add(string.Join(',', [fzg.MaxV, fzg.Marke]));
		}
		File.WriteAllLines(filePath, csv);


		List<Fahrzeug> readFzg = [];
		TextFieldParser tfp = new TextFieldParser(filePath);
		tfp.Delimiters = [","];
		while (!tfp.EndOfData)
		{
			string[] line =	tfp.ReadFields();
			Fahrzeug fzg = new Fahrzeug(int.Parse(line[0]), Enum.Parse<FahrzeugMarke>(line[1]));
			readFzg.Add(fzg);
		}


		//Externes Paket: CsvHelper

		using (StreamWriter sw = new StreamWriter(filePath))
		{
			CsvWriter writer = new CsvWriter(sw, CultureInfo.CurrentCulture);
			writer.WriteRecords(fahrzeuge);
		}

		using StreamReader sr = new StreamReader(filePath);
		CsvReader reader = new CsvReader(sr, CultureInfo.CurrentCulture);
		IEnumerable<Fahrzeug> readFzg2 = reader.GetRecords<Fahrzeug>();
	}

	static void NewtonsoftJson()
	{
		/*
		
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

		//Pfad + Folder anlegen
		//Path, Directory, File
		string folderPath = "Test";
		string filePath = Path.Combine(folderPath, "Test.json");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//Newtonsoft.Json

		//1. De-/Serialisieren
		string json = JsonConvert.SerializeObject(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson); //Als Generic den Zieltyp angeben

		//2. Settings
		JsonSerializerSettings settings = new(); //Diese Settings müssen beim De-/Serialisieren als Parameter mitgegeben werden
		settings.Formatting = Formatting.Indented;
		settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung mitserialisieren

		File.WriteAllText(filePath, JsonConvert.SerializeObject(fahrzeuge, settings));

		//3. Attribute
		//JsonIgnore: Ignoriert das Feld/Property
		//JsonExtensionData: Fängt Felder auf, welche im C# Code kein entsprechendes Property haben
		//JsonRequired: Feld benötigt einen Wert beim De-/Serialisieren (Exception wenn der gegebene Wert null ist)
		//JsonConverter: Wandelt Werte vom Json beim einlesen in einen anderen Wert um

		//4. Json per Hand
		JToken doc = JToken.Parse(readJson); //Array zu einem JToken konvertiert
		foreach (JToken token in doc) //Schleife die die einzelnen Json Objekte (Fahrzeuge) durchgeht
		{
			Console.WriteLine(token["MaxV"]); //Einzelne Felder per Name mit [] ansprechen
			Console.WriteLine(token["Marke"]);

			JToken hallo = token["Hallo"];
			if (hallo != null)
				Console.WriteLine(hallo);

			Console.WriteLine("----------------------");
		}

		*/
	}

	static void SystemJson()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
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

		//Pfad + Folder anlegen
		//Path, Directory, File
		string folderPath = "Test";
		string filePath = Path.Combine(folderPath, "Test.json");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//System.Text.Json

		//1. De-/Serialisieren
		string json = JsonSerializer.Serialize(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson);

		//2. Options
		JsonSerializerOptions options = new();
		options.WriteIndented = true;

		File.WriteAllText(filePath, JsonSerializer.Serialize(fahrzeuge, options));

		//3. Attribute
		//JsonIgnore, JsonRequired, JsonExtensionData, JsonConstructor, ...
		//JsonDerivedType: Vererbung mitserialisieren

		//4. Json per Hand
		JsonDocument doc = JsonDocument.Parse(readJson);
		foreach (JsonElement element in doc.RootElement.EnumerateArray()) //JsonElement == JToken
		{
			int v = element.GetProperty("MaxV").GetInt32(); //element["MaxV"].Value<int>()
			FahrzeugMarke m = (FahrzeugMarke) element.GetProperty("Marke").GetInt32(); //element["Marke"].Value<FahrzeugMarke>()
			
			Console.WriteLine(v);
			Console.WriteLine(m);
			Console.WriteLine("-----------------");
		}
	}

	static void XML()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
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

		//Pfad + Folder anlegen
		//Path, Directory, File
		string folderPath = "Test";
		string filePath = Path.Combine(folderPath, "Test.xml");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//XML

		//1. De-/Serialisieren
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
			xml.Serialize(sw, fahrzeuge);

		using StreamReader sr = new StreamReader(filePath);
		List<Fahrzeug> readFzg = (List<Fahrzeug>) xml.Deserialize(sr);

		//2. Attribute
		//XmlIgnore
		//XmlAttribute: Definiert, das ein Feld in der Attributschreibweise geschrieben wird
		//XmlInclude: Vererbung

		//3. XML per Hand
		XmlDocument doc = new XmlDocument();
		sr.BaseStream.Position = 0;
		doc.Load(sr);
		foreach (XmlNode node in doc.DocumentElement)
		{
			Console.WriteLine(node.Attributes["MaxV"].Value);
			Console.WriteLine(node.Attributes["Marke"].InnerText); //== Value

			//Console.WriteLine(node["MaxV"].InnerText);
			//Console.WriteLine(node["Marke"].InnerText);

			Console.WriteLine("---------------------");
		}
	}
}

[JsonDerivedType(typeof(Fahrzeug), "F")]
[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]

[Serializable]
public class Fahrzeug
{
	[XmlAttribute]
	public int MaxV { get; set; }

	[JsonIgnore]
	//[JsonRequired]
	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//[JsonExtensionData]
	//[JsonIgnore]
	//public Dictionary<string, object> AdditionalData { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }