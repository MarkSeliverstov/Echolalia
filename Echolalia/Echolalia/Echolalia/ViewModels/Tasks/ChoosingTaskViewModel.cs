using System;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;
using System.Collections.Generic;
using Echolalia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Echolalia.ViewModels.Tasks
{
    /// <summary>
    /// ViewModel for managing the choosing task page.
    /// </summary>
    public class ChoosingTaskViewModel : TaskPageViewModel
    {
        private List<Word> answersPool;
        private int BtnsCount = 4;
        List<string> buttonsTextList;

        public List<string> ButtonsTextList
        {
            get => buttonsTextList;
            set => SetProperty(ref buttonsTextList, value);
        }

        public Command AnswerCmd { get; }

        /// <summary>
        /// Initializes a new instance of the ChoosingTaskViewModel class.
        /// </summary>
        /// <param name="questionContext">The question ViewModel for the choosing task.</param>
        public ChoosingTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext)
        {
            AnswerCmd = new Command(PerformAnswerCmd);
            ButtonsTextList = new List<string>(BtnsCount);
        }

        // Overrides the GenerateContext method to generate random answers for the choosing task.
        public override async Task GenerateContext()
        {
            await base.GenerateContext();
            await GenerateRandomAnswers();
        }

        // Overrides the CreateNextQuestion method to generate random answers for the next question.
        public override async void CreateNextQuestion()
        {
            base.CreateNextQuestion();
            await GenerateRandomAnswers();
        }

        /// <summary>
        /// Generates random answers for the choosing task.
        /// </summary>
        public async Task GenerateRandomAnswers()
        {
            ButtonsTextList.Clear();
            List<string> result = new();

            var response = await App.localDB.GetItemsAsync();
            answersPool = response.Where((item) => item.Original != QuestionContext.Answer).ToList();

            Random rnd = new Random();
            int rndIndexOfRightAnswer = rnd.Next(BtnsCount);
            Console.WriteLine(rndIndexOfRightAnswer);

            for (int i = 0; i < BtnsCount; i++)
            {
                if (rndIndexOfRightAnswer == i)
                {
                    result.Add(QuestionContext.Answer);
                }
                else if (answersPool.Count() <= 0)
                {
                    result.Add("None");
                }
                else
                {
                    while (true)
                    {
                        int randIndex = rnd.Next(answersPool.Count());
                        string wordToAdd = answersPool[randIndex].Original;
                        if (!ButtonsTextList.Contains(wordToAdd))
                        {
                            result.Add(answersPool[randIndex].Original);
                            answersPool.Remove(answersPool[randIndex]);
                            break;
                        }
                    }
                }
            }

            ButtonsTextList = result;
        }

        /// <summary>
        /// Executes when the user selects an answer and checks the answer.
        /// </summary>
        /// <param name="userAnswer">The button that was clicked</param>
        private void PerformAnswerCmd(object userAnswer)
        {
            CheckAnswer(userAnswer.ToString());
        }
    }
}
