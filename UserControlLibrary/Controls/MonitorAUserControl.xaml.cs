using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UserControlLibrary.Commons;

namespace UserControlLibrary.Controls
{
    /// <summary>
    /// MonitorAUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorAUserControl : UserControl
    {
        public MonitorAUserControl()
        {
            InitializeComponent();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => UpdateTime();
            _timer.Start();
            UpdateTime(); // 立即显示一次，避免开局空白
        }

        public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register(
            nameof(CurrentTime), typeof(string), typeof(MonitorAUserControl));
        public string CurrentTime
        {
            get => (string)GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }
        public static readonly DependencyProperty CurrentDateProperty =
    DependencyProperty.Register(nameof(CurrentDate), typeof(string), typeof(MonitorAUserControl));
        public string CurrentDate
        {
            get => (string)GetValue(CurrentDateProperty);
            set => SetValue(CurrentDateProperty, value);
        }

        public static readonly DependencyProperty CurrentDayProperty =
            DependencyProperty.Register(nameof(CurrentDay), typeof(string), typeof(MonitorAUserControl));
        public string CurrentDay
        {
            get => (string)GetValue(CurrentDayProperty);
            set => SetValue(CurrentDayProperty, value);
        }

        public static readonly DependencyProperty MachineTotalProperty =
            DependencyProperty.Register(nameof(MachineTotal), typeof(string), typeof(MonitorAUserControl));
        public string MachineTotal
        {
            get => (string)GetValue(MachineTotalProperty);
            set => SetValue(MachineTotalProperty, value);
        }
        public string ProductionCount
        {
            get => (string)GetValue(ProductionCountProperty);
            set => SetValue(ProductionCountProperty, value);
        }

        public static readonly DependencyProperty ProductionCountProperty =
            DependencyProperty.Register(
                nameof(ProductionCount),
                typeof(string),
                typeof(MonitorAUserControl),
                new PropertyMetadata("0"));


        public string DefectCount
        {
            get => (string)GetValue(DefectCountProperty);
            set => SetValue(DefectCountProperty, value);
        }

        public static readonly DependencyProperty DefectCountProperty =
            DependencyProperty.Register(
                nameof(DefectCount),
                typeof(string),
                typeof(MonitorAUserControl),
                new PropertyMetadata("0"));
 
        
        private readonly DispatcherTimer _timer;


        private void UpdateTime()
        {
            var now = DateTime.Now;
            CurrentTime = now.ToString("HH:mm");
            CurrentDate = now.ToString("yyyy-MM-dd");
            CurrentDay = now.ToString("dddd", new CultureInfo("zh-CN")); // 星期六
        }
    }
}
