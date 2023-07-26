using System;
using System.Threading.Tasks;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
    public class LearningTaskViewModel : TaskPageViewModel
    {
        public override Command CheckAnswerCmd { get; }

        public LearningTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext)
        {
            CheckAnswerCmd = new Command(CheckAnswer);
        }

        public override void CheckAnswer()
        {
            UserAnswer = QuestionContext.Answer;
            QuestionContext.ShowAnswer(true);

            UpdateControlls();
        }
    }
}

