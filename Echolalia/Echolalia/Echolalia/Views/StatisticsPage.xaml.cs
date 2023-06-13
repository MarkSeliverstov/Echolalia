using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Echolalia.ViewModels;

namespace Echolalia.Views
{	
	public partial class StatisticsPage : ContentPage
	{
        public StatisticsPage ()
		{
			InitializeComponent ();
            this.BindingContext = new StatisticsViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as StatisticsViewModel).CreateBarStatsChart();
            await (BindingContext as StatisticsViewModel).CreateDonutLearnedChart();
        }

        async void btnSettings_Clicked(System.Object sender, System.EventArgs e)
        {
			await Navigation.PushAsync(new SettingsPage());
        }
    }
}

