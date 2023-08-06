using System.Threading.Tasks;
using System.Linq;
using Echolalia.Models;
using System.Collections.Generic;

namespace Echolalia.ViewModels.Tasks.Questions
{
    /// <summary>
    /// ViewModel for generating questions related to learning new words.
    /// </summary>
	public class LearningQuestionViewModel: BaseQuestionViewModel
	{
        /// <summary>
        /// Gets the list of words to be used for generating learning questions.
        /// Only includes words with LearningProgress set to unknown.
        /// </summary>
        /// <returns>The list of words to be used for generating questions.</returns>
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

