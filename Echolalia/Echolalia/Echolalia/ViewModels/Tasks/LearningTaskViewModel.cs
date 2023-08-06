using Echolalia.ViewModels.Tasks.Questions;

namespace Echolalia.ViewModels.Tasks
{ 
    /// <summary>
    /// ViewModel for managing the learning task page.
    /// </summary>
    public class LearningTaskViewModel : TaskPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the LearningTaskViewModel class.
        /// </summary>
        /// <param name="questionContext">The question ViewModel for the learning task.</param>
        public LearningTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext){}

        /// <summary>
        /// Overrides the ShowAnswer method to show the correct answer and update the controls.
        /// </summary>
        public override void ShowAnswer()
        {
            UserAnswer = QuestionContext.Answer;
            QuestionContext.ShowAnswer(true);
            UpdateControlls();
        }
    }
}

