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
		public string Title { get; }
        public string ThisMouth { get; }

        BarChart _barStatsChart;
        public BarChart BarStatsChart
        {
            get => _barStatsChart;
            private set => SetProperty(ref _barStatsChart, value);
        }

        DonutChart _donutLearnedChart;
        public DonutChart DonutLearnedChart
        {
            get => _donutLearnedChart;
            private set => SetProperty(ref _donutLearnedChart, value);
        }

        public StatisticsViewModel()
		{
			Title = "Stats";
            ThisMouth = DateTime.Today.ToString("MMMM");
        }

        public async Task CreateDonutLearnedChart()
        {
            List<ChartEntry> entries = await GetLearnedStats();
            DonutLearnedChart = new DonutChart()
            {
                Entries = entries,
                LabelTextSize = 50,
                LabelMode = LabelMode.RightOnly,
            };
        }

        private async Task<List<ChartEntry>> GetLearnedStats()
        {
            var items = await App.localDB.GetItemsAsync();
            int learnedCount = items.Where((item) => item.progress == LearningProgress.learned).Count();
            int inProcessCount = items.Where((item) => item.progress == LearningProgress.inProcess).Count();
            int notLearnedCount = items.Count() - inProcessCount - learnedCount;

            return new List<ChartEntry>()
            {
                new ChartEntry(learnedCount) {
                    Label="Learned",
                    ValueLabel=learnedCount.ToString(),
                    Color = SKColor.Parse("#46C931"),
                    ValueLabelColor = SKColor.Parse("#46C931") },
                new ChartEntry(inProcessCount) {
                    Label="InProcess",
                    ValueLabel=inProcessCount.ToString(),
                    Color = SKColor.Parse("#FFB74A"),
                    ValueLabelColor = SKColor.Parse("#FFB74A") },
                new ChartEntry(notLearnedCount) {
                    Label="Not learned",
                    ValueLabel=notLearnedCount.ToString(),
                    Color = SKColor.Parse("#FB5656"),
                    ValueLabelColor = SKColor.Parse("#FB5656")}
            };
        }
    }
}

