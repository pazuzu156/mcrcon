using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Globalization;
using System.Reflection;

namespace mcrcon
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		Mutex _mutex;

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var assembly = Assembly.GetExecutingAssembly();
			string mutexID = string.Format(CultureInfo.InvariantCulture, "Local\\{{{0}}}{{{1}}}",
				assembly.GetType().GUID, assembly.GetName().Name);
			bool mutexCreated;

			_mutex = new Mutex(true, mutexID, out mutexCreated);

			if(!mutexCreated)
			{
				_mutex = null;
				MessageBox.Show("You can only have one instance of mcrcon running at a time!", "Multiple Instance Error", MessageBoxButton.OK, MessageBoxImage.Error);
				Current.Shutdown(-2);
				return;
			}
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			_mutex = null;
			Current.Shutdown(0);
		}
	}
}
