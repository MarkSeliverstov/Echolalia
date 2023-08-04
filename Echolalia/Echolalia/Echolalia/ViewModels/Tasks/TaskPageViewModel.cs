using System;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;
using Echolalia.Views.Tasks;
using Echolalia.Models;
using System.Threading.Tasks;

namespace Echolalia.ViewModels.Tasks
{
	public abstract class TaskPageViewModel: BaseViewModel
    {
        // Elements that must implemented by each task
        public abstract Task GenerateRandomAnswers();

        // Properties
        public Command ShowAnswerCmd { get; }
        public Command SubmitAnswerCmd { get; }
        public BaseQuestionViewModel QuestionContext { get; set; }

        private bool isVisibleSubmitAnswerBtn;
        public bool IsVisibleSubmitAnswerBtn
        {
            get => isVisibleSubmitAnswerBtn;
            set => SetProperty(ref isVisibleSubmitAnswerBtn, value);
        }

        private bool isVisibleAnswers;
        public bool IsVisibleAnswers
        {
            get => isVisibleAnswers;
            set => SetProperty(ref isVisibleAnswers, value);
        }

        private bool isVisibleShowAnswerBtn;
        public bool IsVisibleShowAnswerBtn {
            get => isVisibleShowAnswerBtn;
            set => SetProperty(ref isVisibleShowAnswerBtn, value);
        }

        // Base data
        protected string UserAnswer;
        protected int rightAnswersCount;

        // Constructor
        public TaskPageViewModel( BaseQuestionViewModel questionContext){

            rightAnswersCount = 0;
            IsVisibleAnswers = true;
            IsVisibleShowAnswerBtn = true;
            IsVisibleSubmitAnswerBtn = false;

            QuestionContext = questionContext;
            ShowAnswerCmd = new Command(ShowAnswer);
            SubmitAnswerCmd = new Command(SubmitAnswer);

            GenerateContext();
        }

        // Updates the title showing the count of questions
        void UpdateTitle()
        {
            this.Title = QuestionContext.GetCurrentQuestionNumber().ToString() + " / " +
                         QuestionContext.GetQuestionCount().ToString();
        }

        async void GenerateContext()
        {
            await QuestionContext.GenerateRandomQuestions();
            // Generates random answer if necessary
            await GenerateRandomAnswers();
            UpdateTitle();
        }

        // From answered view to submit answer view and and on the contrary.
        protected void UpdateControlls()
        {
            IsVisibleSubmitAnswerBtn = IsVisibleShowAnswerBtn;
            IsVisibleShowAnswerBtn = !IsVisibleSubmitAnswerBtn;
            IsVisibleAnswers = IsVisibleShowAnswerBtn;
        }

        // Submit one answered question
        public async void SubmitAnswer()
        {
            Word currentQuestionWord = QuestionContext.GetCurrentQuestionWord();

            if (currentQuestionWord.Original == UserAnswer)
            {
                rightAnswersCount++;
                if (currentQuestionWord.Progress < LearningProgress.learned)
                {
                    currentQuestionWord.Progress++;
                }
            }
            else if (currentQuestionWord.Progress > LearningProgress.unknown){
                    currentQuestionWord.Progress--;
            }

            currentQuestionWord.LastPracticed = DateTime.Today;
            await App.localDB.EditItemAsync(currentQuestionWord);

            if (QuestionContext.GetCurrentQuestionNumber() == QuestionContext.GetQuestionCount())
            {
                SubmitResult();
                return;
            }

            QuestionContext.NextQuestion();
            await GenerateRandomAnswers();
            UpdateControlls();
            UpdateTitle();
        }

        // Submit all answered question
        async void SubmitResult()
        {
            await Shell.Current.Navigation.PushAsync(new ResultsPage(
                rightAnswersCount,
                QuestionContext.GetQuestionCount())
            );
        }

        // Checks user answers and you can rewrite it in child classes
        public void CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            bool isCorrect = (QuestionContext.Answer.ToLower() == userAnswer.ToLower());
            QuestionContext.ShowAnswer(isCorrect);
            UpdateControlls();
        }

        // Implements show button action
        public virtual void ShowAnswer()
        {
            QuestionContext.ShowAnswer();
            UpdateControlls();
        }
    }
}