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

using SourceRconLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mcrcon
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();

			// to make port box accept only numeric characters
			tbPort.TextChanged += tbPort_TextChanged;

			// allow enter key on all form inputs!
			tbHost.KeyDown		+= FormInput_KeyDown;
			tbPort.KeyDown		+= FormInput_KeyDown;
			tbPassword.KeyDown	+= FormInput_KeyDown;
		}

		void FormInput_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
				AttemptLogin();
		}

		void tbPort_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			int val = -1;

			if(Int32.TryParse(tb.Text, out val) == false)
			{
				TextChange textChange = e.Changes.ElementAt<TextChange>(0);
				int len = textChange.AddedLength;
				int offset = textChange.Offset;

				tb.Text = tb.Text.Remove(offset, len);
			}
		}

		private void bCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void bLogin_Click(object sender, RoutedEventArgs e)
		{
			AttemptLogin();
		}

		private void AttemptLogin()
		{
			if (!tbHost.Text.Equals(""))
			{
				if (!tbPort.Text.Equals(""))
				{
					if (!tbPassword.Password.Equals(""))
					{
						// good, let's attempt a connection!
						IPAddress ip = null;
						IPEndPoint endPoint;

						if(IPAddress.TryParse(tbHost.Text, out ip) == false)
						{
							// text was not an IP address, let's try to get that from the hostname!
							IPHostEntry hostname;

							try
							{
								hostname = Dns.GetHostEntry(tbHost.Text);
							}
							catch(SocketException ex)
							{
								MessageBox.Show("Invalid hostname!\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
								return;
							}

							if(IPAddress.TryParse(hostname.AddressList[0].ToString(), out ip) == false)
							{
								MessageBox.Show("Error attempting to probe DNS for address!\n\nAddress Received:"
									+ hostname.AddressList[0].ToString(),
									"DNS Probe Error", MessageBoxButton.OK, MessageBoxImage.Error);

								return;
							}
						}

						if (ip != null)
							endPoint = new IPEndPoint(ip, int.Parse(tbPort.Text));
						else
							endPoint = new IPEndPoint(IPAddress.Parse(tbHost.Text), int.Parse(tbPort.Text));

						// Attempt RCON connection!
						Rcon rcon = new Rcon();
						bool connected = false;

						try
						{
							connected = rcon.ConnectBlocking(endPoint, tbPassword.Password);
						}
						catch
						{
							MessageBox.Show("Error connecting to RCON server. Please try again later!", "RCON Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}

						// Connection established! We'll start the new mcrcon window with new RCON object!
						if(connected)
						{
							new MainWindow(rcon).Show();
							Close();
						}
						else
							MessageBox.Show("Error connecting to RCON server. Please try again later!", "RCON Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else
						MessageBox.Show("You must enter your RCON password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else
					MessageBox.Show("You must enter in the port number of the server!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
				MessageBox.Show("You must enter in the hostname of the server!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
