/*
 * Copyright (C) 2016 Kaleb Klein
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

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
		LoginWindow _login;

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

			_login = new LoginWindow();
			_login.Show();
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			_mutex = null;
			Current.Shutdown(0);
		}
	}
}
