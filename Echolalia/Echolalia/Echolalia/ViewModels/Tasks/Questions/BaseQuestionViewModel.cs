using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echolalia.Helpers;
using Echolalia.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Echolalia.Views.Tasks;

namespace Echolalia.ViewModels.Tasks.Questions
{
	public abstract class BaseQuestionViewModel: BaseViewModel
    {
        public abstract Task<List<Item>> GetListForGeneratingQuestion();

        /*
         * Propetries
         */
        private Item CurrentItem;
        public Item GetCurrentItem() => CurrentItem;

        private string _question;
        public string Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
        }

        private string _answer;
        public string Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        Color _answerColor;
        public Color AnswerColor
        {
            get => _answerColor;
            set => SetProperty(ref _answerColor, value);
        }

        private bool _isVisibleAnswer;
        public bool IsVisibleAnswer
        {
            get => _isVisibleAnswer;
            set => SetProperty(ref _isVisibleAnswer, value);
        }

        /*
         * Base Data
         */
        Color greenColor = Color.FromHex("#46C931");
        Color redcolor = Color.FromHex("#FB5656");

        public List<Item> generatedQuestions = new List<Item>();
        protected int currentQuestionIndex = 0;
        int questionCount = Preferences.Get(
                SettingKeys.wordsCountPerTrain_Key, 10
        );

        public int GetCurrentQustion() => currentQuestionIndex;
        public int GetQuestionCount() => questionCount;

       /*
        * Constructor
        */
        public BaseQuestionViewModel(){
            IsVisibleAnswer = false;
        }

        public async Task GenerateRandomQuestions()
        {
            List<Item> from = await GetListForGeneratingQuestion();

            if (from.Count() <= 0)
            {
                await Shell.Current.Navigation.PushAsync(new ZeroWordsErrorPage());
                return;
            }

            if (from.Count() <= questionCount)
            {
                generatedQuestions = from;
                questionCount = generatedQuestions.Count();
            }
            else
            {
                Random rnd = new Random();

                while (generatedQuestions.Count() < 20)
                {
                    int randIndex = rnd.Next(from.Count());
                    Item question = from[randIndex];
                    generatedQuestions.Add(question);
                    from.Remove(question);
                }
            }

            ChangeQuestionAndAnswer();
        }

        private void ChangeQuestionAndAnswer()
        {
            CurrentItem = generatedQuestions[currentQuestionIndex];
            Question = CurrentItem.Translation;
            Answer = CurrentItem.Original;
        }

        public void NextQuestion()
        {
            currentQuestionIndex++;
            IsVisibleAnswer = false;
            ChangeQuestionAndAnswer();
        }

        public void ShowAnswer(bool isCorrect)
        {
            AnswerColor = isCorrect ? greenColor : redcolor;
            IsVisibleAnswer = true;
        }
    }
}

