using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
    /// <summary>
    /// ViewModel for the results page that displays the user's performance after completing a task.
    /// </summary>
	public class ResultsPageViewModel: BaseViewModel
	{
        private string labelOfResult;
        public string LabelOfResult { get => labelOfResult; set => SetProperty(ref labelOfResult, value); }

        private string result;
        public string Result { get => result; set => SetProperty(ref result, value); }

        public Command HomeCmd { get; }

        /// <summary>
        /// Initializes a new instance of the ResultsPageViewModel class.
        /// </summary>
        /// <param name="rightAnswersCount">The number of right answers.</param>
        /// <param name="questionCount">The total number of questions attempted.</param>
		public ResultsPageViewModel(int rightAnswersCount, int questionCount)
		{
            LabelOfResult = $"Good Job, {App.preferencesDB.UserName}!";
            Result = $"{rightAnswersCount} / {questionCount}";
            HomeCmd = new Command(PerformHomeCmdAsync);
		}

        /// <summary>
        /// Method executed when the "Home" command is invoked to navigate back to the root page.
        /// </summary>
        private async void PerformHomeCmdAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }

    }
}

