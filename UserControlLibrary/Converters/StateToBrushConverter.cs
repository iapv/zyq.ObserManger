using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UserControlLibrary.Converters
{
    public class StateToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "处理中" => new SolidColorBrush(Color.FromRgb(255, 204, 0)),   // 黄
                "运行" or "运行中" => new SolidColorBrush(Color.FromRgb(0, 255, 136)), // 绿
                "待机" or "停止" => new SolidColorBrush(Color.FromRgb(120, 144, 156)), // 灰
                "故障" or "报警" => new SolidColorBrush(Color.FromRgb(255, 68, 68)),   // 红
                _ => new SolidColorBrush(Colors.Gray),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
