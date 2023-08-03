using System;
using System.Collections.ObjectModel;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;
using System.Collections.Generic;
using Echolalia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Echolalia.ViewModels.Tasks
{
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

        public ChoosingTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext)
        {
            AnswerCmd = new Command(PerformAnswerCmd);
            ButtonsTextList = new List<string>(BtnsCount);
        }

        public override async Task GenerateRandomAnswers()
        {
            ButtonsTextList.Clear();
            List<string> result = new();

            var response = await App.localDB.GetItemsAsync();
            answersPool = response.Where((item) => item.Original != QuestionContext.Answer).ToList();
            // Deleting right answer word from answersPool
            //Item answerWord = answersPool.Where((word) => word.Translation == QuestionContext.Answer).First();
            //answersPool.Remove(answerWord);

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

        private void PerformAnswerCmd(object userAnswer)
        {
            CheckAnswer(userAnswer.ToString());
        }
    }
}
