using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Echolalia.Models;

namespace Echolalia.ViewModels
{
    /// <summary>
    /// ViewModel for displaying statistics related to learning progress.
    /// </summary>
    public class StatisticsViewModel: BaseViewModel
    {
        DonutChart _donutLearnedChart;
        /// <summary>
        /// Gets or sets the DonutChart representing the learned word statistics.
        /// </summary>
        public DonutChart DonutLearnedChart
        {
            get => _donutLearnedChart;
            private set => SetProperty(ref _donutLearnedChart, value);
        }

        public StatisticsViewModel()
		{
            this.Title = "Stats";
        }

        public async Task CreateDonutChartAsync()
        {
            List<ChartEntry> entries = await GetStatsAsync();
            DonutLearnedChart = new DonutChart()
            {
                Entries = entries,
                LabelTextSize = 50,
                LabelMode = LabelMode.RightOnly,
            };
        }

        // Colors for different learning progress states
        readonly SKColor learned = SKColor.Parse("#46C931");
        readonly SKColor inProcess = SKColor.Parse("#FFB74A");
        readonly SKColor notLearned = SKColor.Parse("#FB5656");

        /// <summary>
        /// Asynchronously retrieves the learning progress statistics and returns a list of ChartEntry objects.
        /// </summary>
        /// <returns>List of ChartEntry objects representing learning progress statistics.</returns>
        private async Task<List<ChartEntry>> GetStatsAsync()
        {
            var items = await App.localDB.GetItemsAsync();

            // Calculate the number of learned, in-process, and not learned words
            int learnedWordsCount = items.Where((item) => item.Progress == LearningProgress.learned).Count();
            int inProcessWordsCount = items.Where((item) => item.Progress == LearningProgress.inProcess).Count();
            int notLearnedCount = items.Count() - inProcessWordsCount - learnedWordsCount;

            // Create a list of ChartEntry objects to represent the learning progress data
            return new List<ChartEntry>()
            {
                new ChartEntry(learnedWordsCount) {
                    Label="Learned",
                    ValueLabel=learnedWordsCount.ToString(),
                    Color = learned,
                    ValueLabelColor = learned
                },
                new ChartEntry(inProcessWordsCount) {
                    Label="In process",
                    ValueLabel=inProcessWordsCount.ToString(),
                    Color = inProcess,
                    ValueLabelColor = inProcess
                },
                new ChartEntry(notLearnedCount) {
                    Label="Not learned",
                    ValueLabel=notLearnedCount.ToString(),
                    Color = notLearned,
                    ValueLabelColor = notLearned
                }
            };
        }
    }
}
