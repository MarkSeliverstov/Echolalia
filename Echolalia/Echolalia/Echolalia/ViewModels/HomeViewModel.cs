﻿using System;
using Xamarin.Forms;
using Echolalia.ViewModels.Tasks.Questions;
using Echolalia.Views.Tasks;
using System.Linq;

namespace Echolalia.ViewModels
{
    /// <summary>
    /// ViewModel for managing the home page of the application.
    /// </summary>
    public class HomeViewModel: BaseViewModel
    {
		public Command LearnNewWordsCmd { get;  }
		public Command ChoosingWordsCmd { get;  }
		public Command WritingWordsCmd { get;  }

        private string countPracticeWordToday;
        public string CountPracticeWordToday {
            get => countPracticeWordToday;
            set => SetProperty(ref countPracticeWordToday, value);
        }

        public HomeViewModel ()
		{
			Title = "Home";
            CountPracticeWordToday = "0";

            LearnNewWordsCmd = new Command(LearnNewWordsAsync);
            ChoosingWordsCmd = new Command(ChoosingWordsAsync);
            WritingWordsCmd = new Command(WritingWordsAsync);
		}


        /// <summary>
        /// Retrieves the count of words practiced today
        /// and updates the CountPracticeWordToday property
        ///
        /// (for OnAppearing method to invoked when the page
        /// is about to be displayed).
        /// </summary>
        public async void GetCountOfWordsPracticedTodayAsync()
        {
            var data = await App.localDB.GetItemsAsync();
            var result = data.Where(
                (item) => item.LastPracticed == DateTime.Today
            );

            CountPracticeWordToday = result.Count().ToString();
        }

        async void LearnNewWordsAsync() =>
            await Shell.Current.Navigation.PushAsync(
                new LearningTaskPage(new LearningQuestionViewModel())
            );

        async void ChoosingWordsAsync() =>
            await Shell.Current.Navigation.PushAsync(
                new ChoosingTaskPage(new CommonQuestionViewModel())
            );

        async void WritingWordsAsync() =>
            await Shell.Current.Navigation.PushAsync(
                new WritingTaskPage(new CommonQuestionViewModel())
            );
    }
}