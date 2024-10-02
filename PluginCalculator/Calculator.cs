using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Rechnerplugin";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Addition")]
	public double Add(double x, double y) => x + y;

	[ReflectionVisible("Subtraktion")]
	public double Sub(double x, double y) => x - y;
}
