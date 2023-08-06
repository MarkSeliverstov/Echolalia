using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
    /// <summary>
    /// ViewModel for handling the scenario when there are no words available
    /// for a particular task in the application.
    /// </summary>
	public class ZeroWordsErrorViewModel
	{
        public Command HomeCmd { get; }

		public ZeroWordsErrorViewModel()
		{
            HomeCmd = new Command(PerformHomeCmd);
        }

        /// <summary>
        /// Performs the command to navigate back to the home page.
        /// </summary>
        private async void PerformHomeCmd()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}

