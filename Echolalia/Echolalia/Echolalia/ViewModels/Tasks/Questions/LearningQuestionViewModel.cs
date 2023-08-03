using System.Threading.Tasks;
using System.Linq;
using Echolalia.Models;
using System.Collections.Generic;

namespace Echolalia.ViewModels.Tasks.Questions
{
	public class LearningQuestionViewModel: BaseQuestionViewModel
	{
        // Just returns only words to be learned
        public override async Task<List<Word>> GetListForGeneratingQuestion()
        {
            var response = await App.localDB.GetItemsAsync();
            var unknownedWords = response.Where(
                (item) => item.Progress == LearningProgress.unknown
            ).ToList();

            return unknownedWords;
        }
    }
}

