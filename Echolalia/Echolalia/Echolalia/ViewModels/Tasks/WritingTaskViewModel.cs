using System.Threading.Tasks;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
	public class WritingTaskViewModel : TaskPageViewModel
	{
        public Command SubmitEntry { get; }

        string _entryAnswerText;
        public string EntryUserAnswer
        {
            get => _entryAnswerText;
            set => SetProperty(ref _entryAnswerText, value);
        }


        public WritingTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext)
        {
            SubmitEntry = new Command(PerformSubmitEntry);
        }

        private void PerformSubmitEntry()
        {
            if (!string.IsNullOrWhiteSpace(EntryUserAnswer))
            {
                CheckAnswer(EntryUserAnswer);
                EntryUserAnswer = "";
            }
        }

        public override Task GenerateRandomAnswers()
        {
            return Task.CompletedTask;
        }
    }
}

