using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using zyq.ObserManger.Services;

namespace zyq.ObserManger.ViewModel
{
    public class QualityHistoryVm : INotifyPropertyChanged
    {

        private readonly QualitySeries _qualitySeries = new QualitySeries();

        private SeriesCollection _chartSeriesData;
        public SeriesCollection ChartSeriesData
        {
            get => _chartSeriesData;
            set { _chartSeriesData = value; OnPropertyChanged(); }
        }

        private string[] _axisXLabels;
        public string[] AxisXLabels
        {
            get => _axisXLabels;
            set { _axisXLabels = value; OnPropertyChanged(); }
        }

        public QualityHistoryVm()
        {
            var end = DateTime.Today;
            var start = end.AddDays(-6);          // 最近 7 天

            ChartSeriesData = _qualitySeries.Build(start, end);
            AxisXLabels = Enumerable.Range(0, 7)
                                    .Select(i => start.AddDays(i).ToString("MM-dd"))
                                    .ToArray();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}
