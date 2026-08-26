using System;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;

namespace UserControlLibrary.Commons
{
    /// <summary>
    /// LivePanelControl.xaml 的交互逻辑
    /// </summary>
    public partial class LivePanelControl : UserControl
    {
        // 接口型控件的标准插槽：父级通过 ChartSeries 把图表数据传进来
        public static readonly DependencyProperty ChartSeriesProperty =
            DependencyProperty.Register(
                nameof(ChartSeries),
                typeof(SeriesCollection),
                typeof(LivePanelControl),
                new PropertyMetadata(null));

        public SeriesCollection ChartSeries
        {
            get => (SeriesCollection)GetValue(ChartSeriesProperty);
            set => SetValue(ChartSeriesProperty, value);
        }

        public LivePanelControl()
        {
            InitializeComponent();
        }
    }
}
