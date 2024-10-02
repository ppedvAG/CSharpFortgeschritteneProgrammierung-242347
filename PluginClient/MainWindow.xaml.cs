using Microsoft.Win32;
using PluginBase;
using System.Reflection;
using System.Windows;

namespace PluginClient;

public partial class MainWindow : Window
{
	public MainWindow() => InitializeComponent();

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog ofd = new OpenFileDialog();
		ofd.Filter = "Plugins|*.dll";
		ofd.ShowDialog();

		//////////////////////////////////////////

		Assembly a = Assembly.LoadFrom(ofd.FileName);
		IPlugin plugin = (IPlugin) Activator.CreateInstance(a.GetTypes().First(e => e.GetInterface(nameof(IPlugin)) != null));
		if (plugin != null)
		{
			MethodInfo[] methods = plugin.GetType()
				.GetMethods()
				.Where(e => e.GetCustomAttribute<ReflectionVisible>() != null)
				.ToArray();

			foreach (MethodInfo method in methods)
			{
				TB.Text += method.GetCustomAttribute<ReflectionVisible>().Name + "\n";
			}
		}
	}
}