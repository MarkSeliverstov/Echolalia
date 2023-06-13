using System;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Echolalia.Models;
using Xamarin.Forms;

namespace Echolalia.ViewModels
{
	public class StatisticsViewModel: BindableObject
    {
		public string Title { get; }
        public string ThisMouth { get; }

        BarChart _barStatsChart;
        public BarChart BarStatsChart
        {
            get => _barStatsChart;
            private set
            {
                _barStatsChart = value;
                OnPropertyChanged();
            }
        }

        DonutChart _donutLearnedChart;
        public DonutChart DonutLearnedChart
        {
            get => _donutLearnedChart;
            private set
            {
                _donutLearnedChart = value;
                OnPropertyChanged();
            }
        }

        public StatisticsViewModel()
		{
			Title = "Stats";
            ThisMouth = DateTime.Today.Month.ToString();
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
            int allWordsCount = items.Count();

            return new List<ChartEntry>()
            {
                new ChartEntry(learnedCount) {
                    Label="Learned",
                    ValueLabel=learnedCount.ToString(),
                    Color = SKColor.Parse("#46C931"),
                    ValueLabelColor = SKColor.Parse("#46C931") },
                new ChartEntry(allWordsCount) {
                    Label="Not learned",
                    ValueLabel=allWordsCount.ToString(),
                    Color = SKColor.Parse("#FB5656"),
                    ValueLabelColor = SKColor.Parse("#FB5656")}
            };
        }

        public async Task CreateBarStatsChart()
        {
            List<ChartEntry> entries = await GetMouthStats();
            BarStatsChart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 40,
                Margin = 2,
                LabelColor = SKColor.Parse("#5C5C5C"),
                LabelOrientation = Orientation.Vertical,
                ValueLabelOrientation = Orientation.Vertical
            };
        }

        private async Task<List<ChartEntry>> GetMouthStats()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            var response = await App.localDB.GetMouthStats();
            var stats = response.Where((item) =>
                item.date.Month == DateTime.Today.Month &&
                item.date.Year == DateTime.Today.Year);


            DateTime now = DateTime.Today;
            int daysInMouth = DateTime.DaysInMonth(now.Year, now.Month);

            for (int i = 1; i <= daysInMouth; i++)
            {
                var thisDaysInDB = stats.Where((item) => item.date.Day == i).ToArray();
                if (thisDaysInDB != null && thisDaysInDB.Count() >= 1)
                {
                    entries.Add(
                        new ChartEntry(thisDaysInDB[0].count) {
                            Label= i.ToString(),
                            ValueLabel = thisDaysInDB[0].count.ToString(),
                            Color = SKColor.Parse("#46C931") }
                    );
                    continue;
                }
                entries.Add(
                    new ChartEntry(0) {
                        Label= i.ToString(),
                        ValueLabel = "",
                        Color = SKColor.Parse("#FFB74A") }
                );
            }

            return entries;
        }
    }
}

