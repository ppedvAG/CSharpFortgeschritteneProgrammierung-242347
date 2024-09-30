using DelegatesEvents;
using System.Windows;

namespace DelegatesWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		Component comp = new();
		comp.Start += Comp_Start;
		comp.Stop += Comp_Stop;
		comp.Progress += Comp_Progress;
		comp.DoWork();
	}

	private void Comp_Start()
	{
		TB.Text += "Prozess gestartet\n";
	}

	private void Comp_Stop()
	{
		TB.Text += "Prozess fertig\n";
	}

	private void Comp_Progress(int e)
	{
		TB.Text += $"Fortschritt: {e}\n";
	}
}