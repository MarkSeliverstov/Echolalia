using System;
using Xamarin.Forms;
using Echolalia.Views;
using Echolalia.ViewModels.Tasks.Questions;
using Echolalia.Views.Tasks;

namespace Echolalia.ViewModels
{
	public class HomeViewModel: BaseViewModel
    {

		public string Title { get;  }
		public Command LearnNewWordsCmd { get;  }
		public Command ChoosingWordsCmd { get;  }
		public Command WritingWordsCmd { get;  }

        public HomeViewModel ()
		{
			Title = "Home";
            LearnNewWordsCmd = new Command(LearnNewWords);
            ChoosingWordsCmd = new Command(ChoosingWords);
            WritingWordsCmd = new Command(WritingWords);
		}

        private async void WritingWords()
        {
            await Shell.Current.Navigation.PushAsync(
                new EntryTaskPage(new CommonQuestionViewModel())
            );
        }

        async void LearnNewWords()
		{
            await Shell.Current.Navigation.PushAsync(
                new LearningTaskPage(new LearningQuestionViewModel())
            );
        }

        async void ChoosingWords()
        {
            await Shell.Current.Navigation.PushAsync(
                new ChoosingTaskPage(new CommonQuestionViewModel())
            );
        }
    }
}


