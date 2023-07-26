using System;
using static SQLite.SQLite3;
using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
	public class ZeroWordsErrorViewModel
	{
        public Command HomeCmd { get; }

		public ZeroWordsErrorViewModel()
		{
            HomeCmd = new Command(PerformHomeCmd);
        }

        private async void PerformHomeCmd()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}

