namespace PluginBase;

/// <summary>
/// Attribute: In [] über Member (Klassen, Felder, Methoden, ...) extra Eigenschaften anhängen
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ReflectionVisible : Attribute
{
	public string Name { get; private set; }

	public ReflectionVisible(string name) => Name = name;
}