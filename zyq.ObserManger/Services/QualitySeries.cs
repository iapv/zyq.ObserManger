using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using zyq.ObserManger.Data;
using zyq.ObserManger.Models;

namespace zyq.ObserManger.Services;

public class QualitySeries
{
    public SeriesCollection Build(DateTime startDate, DateTime endDate)
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();

        var rows = db.QualityHistories
                     .Where(q => q.RecordDate.Date >= startDate.Date &&
                                 q.RecordDate.Date <= endDate.Date)
                     .OrderBy(q => q.RecordDate)
                     .ToList();

        var values = new List<double>();
        for (var d = startDate.Date; d <= endDate.Date; d = d.AddDays(1))
        {
            var row = rows.FirstOrDefault(r => r.RecordDate.Date == d);
            values.Add(row?.DefectCount ?? 0);   // 不良计数，范围 0~15
        }

        // 青色面积填充
        var areaFill = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0, 1),
            GradientStops = new GradientStopCollection
            {
                new GradientStop(Color.FromArgb(0x66, 0x2b, 0xed, 0xf1), 0.0),
                new GradientStop(Color.FromArgb(0x00, 0x2b, 0xed, 0xf1), 1.0)
            }
        };

        return new SeriesCollection
        {
            new LineSeries
            {
                Title = "不良计数",
                Values = new ChartValues<double>(values),
                Stroke = new SolidColorBrush(Color.FromRgb(0x2b, 0xed, 0xf1)),   // 青色线
                Fill = areaFill,                                                   // 下方渐变面积
                StrokeThickness = 2,
                PointGeometrySize = 0,    // 截图没显示圆点
                LineSmoothness = 0.5      // 0 = 折线，1 = 很圆的曲线，0.3 略带弧度
            }
        };
    }
}
