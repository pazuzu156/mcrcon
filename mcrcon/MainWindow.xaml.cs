using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SourceRconLib;

namespace mcrcon
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Rcon rcon;

		public MainWindow(Rcon rcon)
		{
			InitializeComponent();

			this.rcon = rcon;

			rcon.ServerCommand("say RCON has entered the world!");
		}

		private void bSend_Click(object sender, RoutedEventArgs e)
		{
			rcon.ServerCommandBlocking("say \"Hello World!\"");
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			rcon.ServerCommand("say RCON has left the world!");
		}

		private void bStop_Click(object sender, RoutedEventArgs e)
		{
			rcon.ServerCommand("stop");
			rcon.Disconnect();
			Close();
		}
	}
}
