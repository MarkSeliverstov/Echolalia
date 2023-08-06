using System.Threading.Tasks;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
    /// <summary>
    /// ViewModel for managing the writing task in the application.
    /// The writing task allows users to submit their written answers for evaluation.
    /// </summary>
	public class WritingTaskViewModel : TaskPageViewModel
	{
        public Command SubmitEntry { get; }

        string _entryAnswerText;
        public string EntryUserAnswer
        {
            get => _entryAnswerText;
            set => SetProperty(ref _entryAnswerText, value);
        }

        /// <summary>
        /// Initializes a new instance of the WritingTaskViewModel class.
        /// </summary>
        /// <param name="questionContext">The question ViewModel for the writing task.</param>
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
    }
}

