using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echolalia.Models;

namespace Echolalia.ViewModels.Tasks.Questions
{
    public class CommonQuestionViewModel : BaseQuestionViewModel
    {
		public CommonQuestionViewModel(){ }

        public async override Task<List<Item>> GetListForGeneratingQuestion()
        {
            var response = await App.localDB.GetItemsAsync();
            var wordsInProcess = response.Where(
                (item) => item.progress >= LearningProgress.unknown
            ).ToList();

            return wordsInProcess;
        }
    }
}

