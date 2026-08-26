using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zyq.ObserManger.Models
{
    public class ProductionHourly
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }   // 哪一天
        public int Hour { get; set; }              // 8~16
        public int Quantity { get; set; }          // 该小时产量
        public DateTime CreatedAt { get; set; }

        public double DefectCount { get; set; }     // 不良计数（新增）
    }
}
