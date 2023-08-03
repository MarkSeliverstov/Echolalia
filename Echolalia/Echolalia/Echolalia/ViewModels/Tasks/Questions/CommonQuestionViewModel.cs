using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echolalia.Models;

namespace Echolalia.ViewModels.Tasks.Questions
{
    public class CommonQuestionViewModel : BaseQuestionViewModel
    {
        // Returns all of words
        public async override Task<List<Word>> GetListForGeneratingQuestion()
        {
            var response = await App.localDB.GetItemsAsync();
            var wordsInProcess = response.Where(
                (item) => item.Progress >= LearningProgress.unknown
            ).ToList();

            return wordsInProcess;
        }
    }
}

