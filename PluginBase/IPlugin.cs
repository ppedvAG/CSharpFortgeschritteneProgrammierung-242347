namespace PluginBase;

/// <summary>
/// Wird per Dependency in PluginCalculator (in alle Plugins) und in den PluginClient eingefügt
/// </summary>
public interface IPlugin
{
	public string Name { get; }

	public string Description { get; }

	public string Version { get; }

	public string Author { get; }
}