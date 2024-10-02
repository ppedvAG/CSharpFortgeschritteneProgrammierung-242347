using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow() => InitializeComponent();

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Info.Text += i + "\n";
			Thread.Sleep(25); //GUI Updates werden blockiert
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Dispatcher.Invoke(() => Info.Text += i + "\n");
				Dispatcher.Invoke(() => Scroll.ScrollToEnd());
				Thread.Sleep(25);
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Info.Text += i + "\n";
			Scroll.ScrollToEnd();
			await Task.Delay(25);
		}
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		AsyncDataSource ds = new();
		await foreach (int x in ds.GetNumbers()) //Wenn eine Zahl per yield return zurückgegeben wird, wird diese hier in x eingefangen
		{
			Info.Text += x + "\n";
		}
	}

	private async void Button_Click_Parallel_ForEachAsync(object sender, RoutedEventArgs e)
	{
		//Aufgabe: 100 Files parallel schreiben
		List<string> fileContents = [];
		for (int i = 0; i < 100; i++)
		{
			StringBuilder sb = new();
			for (int j = 0; j < 1_000_000; j++)
			{
				sb.Append(j);
			}
			fileContents.Add(sb.ToString());
		}

		if (Directory.Exists("Output"))
			Directory.Delete("Output", true);
		Directory.CreateDirectory("Output");

		Stopwatch sw = Stopwatch.StartNew();
		//for (int i = 0; i < fileContents.Count; i++)
		//{
		//	File.WriteAllText($"Output\\{i}.txt", fileContents[i]);
		//}

		//////////////////////////////////////////////////////////////////////
		
		//Paralleles Schreiben der Files

		List<Task> writeTasks = [];
		for (int i = 0; i < fileContents.Count; i++)
		{
			writeTasks.Add(File.WriteAllTextAsync($"Output\\{i}.txt", fileContents[i])); //Starte 100 Aufgaben
		}
		await Task.WhenAll(writeTasks); //Warte auf 100 Aufgaben
		Info.Text = sw.ElapsedMilliseconds.ToString();
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		//Aufgabe: Buchtext herunterladen und den User informieren, bei welchem Schritt wir gerade sind

		//Aufbau
		//- Start der Aufgabe
		//- Zwischenschritte (optional)
		//- Warten auf die Aufgabe

		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		//Aufgabe starten
		using HttpClient client = new();
		Task<HttpResponseMessage> get = client.GetAsync(url);

		//Zwischenschritte
		Info.Text = "Request gestartet";
		ReqButton.IsEnabled = false;

		//Warten
		HttpResponseMessage response = await get;
		if (response.IsSuccessStatusCode)
		{
			//Aufgabe starten
			Task<string> buchtext = response.Content.ReadAsStringAsync();

			//Zwischenschritte
			Info.Text = "Text wird ausgelesen...";

			//Warten
			string text = await buchtext;

			Info.Text = text;
			ReqButton.IsEnabled = true;
		}
		else
		{
			Info.Text = $"Request fehlgeschlagen: {response.StatusCode}";
		}
	}
}