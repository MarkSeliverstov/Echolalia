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
        protected string UserAnswer;

        int rightAnswersCount;

        public abstract Command CheckAnswerCmd { get; }
        public Command SubmitAnswerCmd { get; }

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

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public BaseQuestionViewModel QuestionContext { get; set; }

        public TaskPageViewModel( BaseQuestionViewModel questionContext){
            rightAnswersCount = 0;
            //this.Title = Title;

            IsVisibleAnswers = true;
            IsVisibleShowAnswerBtn = true;
            IsVisibleSubmitAnswerBtn = false;

            QuestionContext = questionContext;
            SubmitAnswerCmd = new Command(SubmitAnswer);

            GenerateContext();
        }

        async void GenerateContext()
        {
            await QuestionContext.GenerateRandomQuestions();
        }

        protected void UpdateControlls()
        {
            IsVisibleSubmitAnswerBtn = IsVisibleShowAnswerBtn;
            IsVisibleShowAnswerBtn = !IsVisibleSubmitAnswerBtn;
            IsVisibleAnswers = IsVisibleShowAnswerBtn;
        }
        
        void SubmitAnswer()
        {
            if (QuestionContext.Answer == UserAnswer)
            {
                rightAnswersCount++;
                Item currentItem = QuestionContext.GetCurrentItem();
                if (currentItem.progress < Models.LearningProgress.learned)
                {
                    currentItem.progress++;
                }
                App.localDB.EditItem(currentItem);
            }

            if (QuestionContext.GetCurrentQustion()+1 == QuestionContext.GetQuestionCount())
            {
                SubmitResult();
                return;
            }


            QuestionContext.NextQuestion();
            UpdateControlls();
        }

        async void SubmitResult()
        {
            await Shell.Current.Navigation.PushAsync(new ResultsPage(
                rightAnswersCount,
                QuestionContext.GetQuestionCount())
            );
        }

        public virtual void CheckAnswer()
        {
            bool isCorrect = (QuestionContext.Answer.ToLower() == UserAnswer.ToLower());
            QuestionContext.ShowAnswer(isCorrect);
            UpdateControlls();
        }
    }
}

