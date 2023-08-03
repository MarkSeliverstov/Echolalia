using System;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Echolalia.Models;

namespace Echolalia.ViewModels
{
	public class StatisticsViewModel: BaseViewModel
    {
        DonutChart _donutLearnedChart;
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

        readonly SKColor learned = SKColor.Parse("#46C931");
        readonly SKColor inProcess = SKColor.Parse("#FFB74A");
        readonly SKColor notLearned = SKColor.Parse("#FB5656");

        private async Task<List<ChartEntry>> GetStatsAsync()
        {
            var items = await App.localDB.GetItemsAsync();

            int learnedWordsCount = items.Where(
                (item) => item.Progress == LearningProgress.learned
            ).Count();

            int inProcessWordsCount = items.Where(
                (item) => item.Progress == LearningProgress.inProcess
            ).Count();

            int notLearnedCount = items.Count() - inProcessWordsCount - learnedWordsCount;

            return new List<ChartEntry>()
            {
                new ChartEntry(learnedWordsCount) {
                    Label="Learned",
                    ValueLabel=learnedWordsCount.ToString(),
                    Color = learned,
                    ValueLabelColor = learned
                },
                new ChartEntry(inProcessWordsCount) {
                    Label="InProcess",
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
