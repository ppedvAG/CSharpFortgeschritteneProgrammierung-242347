using System.Text;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	/// <summary>
	/// Über den this Parameter wird festgelegt, auf welchen Typen sich diese Erweiterungsmethode bezieht
	/// </summary>
	public static int Quersumme(this int x)
	{
		//int summe = 0;
		//string zahlAlsString = x.ToString();
		//for (int i = 0; i < zahlAlsString.Length; i++)
		//{
		//	summe += (int) char.GetNumericValue(zahlAlsString[i]);
		//}
		//return summe;

		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static T GetProperty<T>(this JsonElement element, string propertyName)
	{
		//switch (nameof(T))
		//{
		//	case "int":
		//		return (T) element.GetProperty(propertyName).GetInt32();

		//}
		//return element.GetProperty(propertyName).
		return default;
	}

	//Aufgabe: Liste als String ausgeben
	public static string AsString<T>(this IEnumerable<T> list)
	{
		StringBuilder sb = new("[");
		foreach (var item in list)
		{
			sb.Append(item);
			sb.Append(", ");
		}
		string output = sb.ToString().TrimEnd(',', ' ') + "]";
		return output;
	}
}