using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace zyq.ObserManger.Models;

public class QualityHistory
{
    public int Id { get; set; }

    public DateTime RecordDate { get; set; }  // 日期

    public double PassRate { get; set; }      // 合格率，如 98.5

    public double DefectRate { get; set; }    // 不良率，如 1.5

    public int TotalCount { get; set; }       // 当日总数

    public int DefectCount { get; set; }      // 当日不良数
}

