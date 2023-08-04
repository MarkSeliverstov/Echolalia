using System.Threading.Tasks;
using Echolalia.ViewModels.Tasks.Questions;

namespace Echolalia.ViewModels.Tasks
{
    public class LearningTaskViewModel : TaskPageViewModel
    {
        public LearningTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext){}

        // This page doesn't have answers but
        public override Task GenerateRandomAnswers()
        {
            return Task.CompletedTask;
        }

        // All of answers must be right to count the learning
        public override void ShowAnswer()
        {
            UserAnswer = QuestionContext.Answer;
            QuestionContext.ShowAnswer(true);
            UpdateControlls();
        }
    }
}

