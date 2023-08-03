using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
	public class ResultsPageViewModel: BaseViewModel
	{
        private string labelOfResult;
        public string LabelOfResult { get => labelOfResult; set => SetProperty(ref labelOfResult, value); }

        private string result;
        public string Result { get => result; set => SetProperty(ref result, value); }

        public Command HomeCmd { get; }

		public ResultsPageViewModel(int rightAnswersCount, int questionCount)
		{
            LabelOfResult = $"Good Job, {App.preferencesDB.UserName}!";
            Result = $"{rightAnswersCount} / {questionCount}";
            HomeCmd = new Command(PerformHomeCmdAsync);
		}

        private async void PerformHomeCmdAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }

    }
}

