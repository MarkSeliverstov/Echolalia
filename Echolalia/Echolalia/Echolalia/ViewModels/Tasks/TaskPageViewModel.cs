using System;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;
using Echolalia.Views.Tasks;
using Echolalia.Models;
using System.Threading.Tasks;

namespace Echolalia.ViewModels.Tasks
{
    /// <summary>
    /// Abstract base ViewModel for managing task-related pages in the application.
    /// </summary>
    public abstract class TaskPageViewModel: BaseViewModel
    {
        // Properties for commands and visibility control
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

        /// <summary>
        /// Initializes a new instance of the TaskPageViewModel class.
        /// </summary>
        /// <param name="questionContext">The question ViewModel for the task.</param>
        public TaskPageViewModel( BaseQuestionViewModel questionContext){

            rightAnswersCount = 0;
            IsVisibleAnswers = true;
            IsVisibleShowAnswerBtn = true;
            IsVisibleSubmitAnswerBtn = false;

            QuestionContext = questionContext;
            ShowAnswerCmd = new Command(ShowAnswer);
            SubmitAnswerCmd = new Command(SubmitAnswer);

            // Generate the context for the task, which may involve generating random questions
            _ = GenerateContext(); // The underscore is used to discard the task, as it is run asynchronously
        }

        /// <summary>
        /// Updates the title showing the count of questions.
        /// </summary>s
        void UpdateTitle()
        {
            this.Title = QuestionContext.GetCurrentQuestionNumber().ToString() + " / " +
                         QuestionContext.GetQuestionCount().ToString();
        }

        /// <summary>
        /// Generates the context for the task by generating random questions.
        /// You can override this method in child classes if needed.
        /// </summary>
        public virtual async Task GenerateContext()
        {
            await QuestionContext.GenerateRandomQuestions();
            UpdateTitle();
        }

        /// <summary>
        /// Creates the next question in the task.
        /// You can override this method in child classes if needed.
        /// </summary>
        public virtual void CreateNextQuestion()
        {
            QuestionContext.NextQuestion();
            UpdateControlls();
            UpdateTitle();
        }

        /// <summary>
        /// Updates the visibility controls for submitting and showing answers.
        /// </summary>
        protected void UpdateControlls()
        {
            IsVisibleSubmitAnswerBtn = IsVisibleShowAnswerBtn;
            IsVisibleShowAnswerBtn = !IsVisibleSubmitAnswerBtn;
            IsVisibleAnswers = IsVisibleShowAnswerBtn;
        }

        /// <summary>
        /// Submits the user's answer for the current question.
        /// </summary>
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
                FinishTask();
                return;
            }

            CreateNextQuestion();
        }

        /// <summary>
        /// Finishes the task and shows the results.
        /// </summary>
        async void FinishTask()
        {
            await Shell.Current.Navigation.PushAsync(new ResultsPage(
                rightAnswersCount,
                QuestionContext.GetQuestionCount())
            );
        }

        /// <summary>
        /// Checks the user's answer and shows the correct answer.
        /// </summary>
        public void CheckAnswer(string userAnswer)
        {
            UserAnswer = userAnswer;
            bool isCorrect = (QuestionContext.Answer.ToLower() == userAnswer.ToLower());
            QuestionContext.ShowAnswer(isCorrect);
            UpdateControlls();
        }

        /// <summary>
        /// Implements the show answer button action.
        /// </summary>
        public virtual void ShowAnswer()
        {
            QuestionContext.ShowAnswer();
            UpdateControlls();
        }
    }
}