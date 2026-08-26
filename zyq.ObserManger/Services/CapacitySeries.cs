using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zyq.ObserManger.Data;
namespace zyq.ObserManger.Services
{
    public class CapacitySeries
    {

        public SeriesCollection Build(DateTime date)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();


            var rows = db.ProductionHourlies.Where(_=> _.RecordDate.Date == date.Date)
                .OrderBy(_=>_.Hour).ToList();
            var production = Enumerable.Range(8,9)
                .Select(h => (double)(rows.FirstOrDefault(r=>r.Hour==h)?.Quantity ?? 50)).ToList();
            // 不良计数：同样 9 个小时
            var defect = Enumerable.Range(8, 9)
                .Select(h => (double)(rows.FirstOrDefault(r => r.Hour == h)?.DefectCount ?? 50))
                .ToList();

            return new SeriesCollection
        {
            new ColumnSeries { Title = "生产计数", Values = new ChartValues<double>(production) },
            new ColumnSeries { Title = "不良计数", Values = new ChartValues<double>(defect) }
        };
        }
    }
}
