using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echolalia.Models;

namespace Echolalia.ViewModels.Tasks.Questions
{
    /// <summary>
    /// ViewModel for generating common questions based on words in the learning process.
    /// </summary>
    public class CommonQuestionViewModel : BaseQuestionViewModel
    {
        /// <summary>
        /// Retrieves a list of words in the learning process from the local database.
        /// </summary>
        /// <returns>A list of words that are in the learning process.</returns>
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

