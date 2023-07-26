using System;
using Echolalia.ViewModels.Tasks.Questions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Echolalia.ViewModels.Tasks
{
	public class WritingTaskViewModel : TaskPageViewModel
	{
        public override Command CheckAnswerCmd { get; }
        public Command SubmitEntry { get; }


        private string entryAnsweText;
        public string EntryAnsweText { get => entryAnsweText; set => SetProperty(ref entryAnsweText, value); }


        public WritingTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext)
        {
            CheckAnswerCmd = new Command(CheckAnswer);
            SubmitEntry = new Command(PerformSubmitEntry);
        }

        private void PerformSubmitEntry()
        {
            if (EntryAnsweText != null && EntryAnsweText != "")
            {
                UserAnswer = EntryAnsweText;
                EntryAnsweText = "";
                CheckAnswer();
            }
        }
    }
}

