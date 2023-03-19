using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Echolalia.Views;

namespace Echolalia
{	
	public partial class AppShell : Shell
	{	
		public AppShell ()
		{
			InitializeComponent ();

			Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
			Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));

		}
	}
}

