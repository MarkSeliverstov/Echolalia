using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echolalia.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Echolalia.Views.Tasks;

namespace Echolalia.ViewModels.Tasks.Questions
{
	public abstract class BaseQuestionViewModel: BaseViewModel
    {
        public abstract Task<List<Word>> GetListForGeneratingQuestion();

        /*
         * Propetries
         */
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

        // Base DAta
        int questionCount = App.preferencesDB.WordsCountPerTrain;
        readonly Color greenColor = Color.FromHex("#46C931");
        readonly Color redColor = Color.FromHex("#FB5656");
        readonly Color yellowColor = Color.FromHex("#FFB74A");

        List<Word> generatedQuestions = new List<Word>();
        private Word CurrentItem;
        int currentQuestionIndex = 0;

        // Constructor
        public BaseQuestionViewModel(){
            IsVisibleAnswer = false;
        }

        private void ChangeQuestionAndAnswer()
        {
            CurrentItem = generatedQuestions[currentQuestionIndex];
            Question = CurrentItem.Translation;
            Answer = CurrentItem.Original;
        }

        // Public Question context API
        public Word GetCurrentQuestionWord() => CurrentItem;
        public int GetCurrentQustionIndex() => currentQuestionIndex;
        public int GetCurrentQuestionNumber() => currentQuestionIndex + 1;
        public int GetQuestionCount() => questionCount;

        public async Task GenerateRandomQuestions()
        {
            List<Word> from = await GetListForGeneratingQuestion();

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

                while (generatedQuestions.Count() < questionCount)
                {
                    int randIndex = rnd.Next(from.Count());
                    Word question = from[randIndex];
                    generatedQuestions.Add(question);
                    from.Remove(question);
                }
            }

            ChangeQuestionAndAnswer();
        }


        public void NextQuestion()
        {
            currentQuestionIndex++;
            IsVisibleAnswer = false;
            ChangeQuestionAndAnswer();
        }

        public void ShowAnswer(bool isCorrect)
        {
            AnswerColor = isCorrect ? greenColor : redColor;
            IsVisibleAnswer = true;
        }

        public void ShowAnswer()
        {
            AnswerColor = yellowColor;
            IsVisibleAnswer = true;
        }
    }
}

