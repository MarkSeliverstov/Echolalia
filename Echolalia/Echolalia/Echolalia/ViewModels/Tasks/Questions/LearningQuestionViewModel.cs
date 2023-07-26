using System.Threading.Tasks;
using System.Linq;
using Echolalia.Models;
using System.Collections.Generic;

namespace Echolalia.ViewModels.Tasks.Questions
{
	public class LearningQuestionViewModel: BaseQuestionViewModel
	{
        public LearningQuestionViewModel() { }

        public override async Task<List<Item>> GetListForGeneratingQuestion()
        {
            var response = await App.localDB.GetItemsAsync();
            var unknownedWords = response.Where(
                (item) => item.progress == LearningProgress.unknown
            ).ToList();

            return unknownedWords;
        }
    }
}

