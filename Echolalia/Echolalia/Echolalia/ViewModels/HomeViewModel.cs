using System;
using Xamarin.Forms;
using Echolalia.Views;
using Echolalia.ViewModels.Tasks.Questions;
using Echolalia.Views.Tasks;
using System.Linq;

namespace Echolalia.ViewModels
{
	public class HomeViewModel: BaseViewModel
    {

		public string Title { get;  }
		public Command LearnNewWordsCmd { get;  }
		public Command ChoosingWordsCmd { get;  }
		public Command WritingWordsCmd { get;  }

        private string countPracticeWordToday;
        public string CountPracticeWordToday { get => countPracticeWordToday; set => SetProperty(ref countPracticeWordToday, value); }

        public HomeViewModel ()
		{
			Title = "Home";
            LearnNewWordsCmd = new Command(LearnNewWords);
            ChoosingWordsCmd = new Command(ChoosingWords);
            WritingWordsCmd = new Command(WritingWords);
            CountPracticeWordToday = "0";
            GetCountOfWordsPracticedToday();
		}

        private async void GetCountOfWordsPracticedToday()
        {
            var data = await App.localDB.GetItemsAsync();
            var result = data.Where((item) => item.LastPracticed == DateTime.Today);
            CountPracticeWordToday = result.Count().ToString();
        }

        private async void WritingWords()
        {
            GetCountOfWordsPracticedToday();
            await Shell.Current.Navigation.PushAsync(
                new EntryTaskPage(new CommonQuestionViewModel())
            );
        }

        async void LearnNewWords()
		{
            GetCountOfWordsPracticedToday();
            await Shell.Current.Navigation.PushAsync(
                new LearningTaskPage(new LearningQuestionViewModel())
            );
        }

        async void ChoosingWords()
        {
            GetCountOfWordsPracticedToday();
            await Shell.Current.Navigation.PushAsync(
                new ChoosingTaskPage(new CommonQuestionViewModel())
            );
        }

    }
}


