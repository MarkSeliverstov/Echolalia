using System;
using System.Collections.ObjectModel;
using Echolalia.ViewModels.Tasks.Questions;
using Xamarin.Forms;
using System.Collections.Generic;
using Echolalia.Models;
using System.Linq;

namespace Echolalia.ViewModels.Tasks
{
	public class ChoosingTaskViewModel: TaskPageViewModel
    {
        private List<Item> Data;

        private int BtnsCount = 4;
        List<string> buttonsTextList;
        public List<string> ButtonsTextList
        {
            get => buttonsTextList;
            set => SetProperty(ref buttonsTextList, value);
        }

        public override Command CheckAnswerCmd { get; }
        public Command AnswerCmd { get; }

        public ChoosingTaskViewModel(BaseQuestionViewModel questionContext) : base(questionContext)
        {
            CheckAnswerCmd = new Command(CheckAnswer);
            AnswerCmd = new Command(PerformAnswerCmd);
            ButtonsTextList = new List<string>(BtnsCount);
            GenerateAnswers();
        }

        public async void GenerateAnswers()
        {
            List<string> result = new List<string>();

            var response = await App.localDB.GetItemsAsync();
            Data = response.Where((item) => item.Original != QuestionContext.Answer).ToList();

            Random rnd = new Random();
            int RndAndexOfRightAnswer = rnd.Next(BtnsCount);

            
            for (int i = 0; i < BtnsCount; i++)
            {
                if (RndAndexOfRightAnswer == i)
                {
                    result.Add(QuestionContext.Answer);
                }
                else if (Data.Count() <= 0)
                {
                    result.Add("None");
                }
                else
                {
                    while (true){
                        int randIndex = rnd.Next(Data.Count());
                        string itemToAdd = Data[randIndex].Original;
                        if (!ButtonsTextList.Contains(itemToAdd))
                        {
                            result.Add(Data[randIndex].Original);
                            Data.Remove(Data[randIndex]);
                            break;
                        }
                    }
                }
            }

            ButtonsTextList = result;
        }

        private void PerformAnswerCmd(object arg)
        {
            UserAnswer = arg.ToString();
            CheckAnswer();
        }
    }
}
