using System.Text;
using System.Text.Json;

namespace Reflection;

public static class JsonGetPropertyGenerator
{
	public static void Main()
	{
		//Aufgabenstellung: Bei JsonElement GetInt32, GetInt64, GetDouble, ... zu einer einzelnen Methode konsolidieren
		Type eType = typeof(JsonElement);
		StringBuilder sb = new("T t = new T();\nswitch (t)\n{\n");
		foreach (string s in eType.GetMethods().Where(e => !e.Name.Contains("Try")).Select(e => $"\tcase {e.ReturnType.Name}: return element.{e.Name}();"))
			sb.AppendLine(s);
		sb.AppendLine("\tdefault: throw new Exception(\"Invalid Type\");\n}");
        Console.WriteLine(sb.ToString());
    }

	public static T GetProperty<T>(this JsonElement element, string propertyName) where T : new()
	{
		T t = new T();
		switch (t)
		{
		//	case Boolean: return element.GetBoolean();
		//	case String: return element.GetString();
		//	case Byte[]: return element.GetBytesFromBase64();
		//	case SByte: return element.GetSByte();
		//	case Byte: return element.GetByte();
		//	case Int16: return element.GetInt16();
		//	case UInt16: return element.GetUInt16();
		//	case Int32: return element.GetInt32();
		//	case UInt32: return element.GetUInt32();
		//	case Int64: return element.GetInt64();
		//	case UInt64: return element.GetUInt64();
		//	case Double: return element.GetDouble();
		//	case Single: return element.GetSingle();
		//	case Decimal: return element.GetDecimal();
		//	case DateTime: return element.GetDateTime();
		//	case DateTimeOffset: return element.GetDateTimeOffset();
		//	case Guid: return element.GetGuid();
			default: throw new Exception("Invalid Type");
		}
	}
}