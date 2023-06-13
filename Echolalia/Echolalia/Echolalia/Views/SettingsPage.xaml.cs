﻿using System;
using System.Collections.Generic;
using Xamarin.Essentials;

using Xamarin.Forms;

using Echolalia.ViewModels;

namespace Echolalia.Views
{	
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
			this.BindingContext = new SettingsViewModel();
		}
    }
}

