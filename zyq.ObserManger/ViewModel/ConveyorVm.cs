using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using zyq.ObserManger.Models;

namespace zyq.ObserManger.ViewModel;

public class ConveyorVm : INotifyPropertyChanged
{
    private double _materialX = 15;
    private readonly DispatcherTimer _timer;
    private readonly double _speed = 3.0;

    private int _output = 1247;
    public int Output
    {
        get => _output;
        set { _output = value; OnPropertyChanged(); }
    }

    private double _cycleTime = 15.0;
    public double CycleTime
    {
        get => _cycleTime;
        set { _cycleTime = value; OnPropertyChanged(); }
    }

    private double _passRate = 98.1;
    public double PassRate
    {
        get => _passRate;
        set { _passRate = value; OnPropertyChanged(); }
    }

    public double MaterialX
    {
        get => _materialX;
        set { _materialX = value; OnPropertyChanged(); }
    }

    public ObservableCollection<StationStatus> Stations { get; }

    public ConveyorVm()
    {
        Stations = new ObservableCollection<StationStatus>
        {
            new StationStatus("上料", 120, "运行"),
            new StationStatus("检测", 300, "运行"),
            new StationStatus("装配", 480, "运行"),
            new StationStatus("下料", 660, "运行"),
        };

        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(40) };
        _timer.Tick += (s, e) => Move();
        _timer.Start();
    }

    private void Move()
    {
        MaterialX += _speed;
        if (MaterialX > 680)
        {
            MaterialX = 15;
            Output++;          // 模拟完成一个产品
        }

        double center = MaterialX + 10;
        foreach (var st in Stations)
        {
            bool near = Math.Abs(center - st.PositionX) < 28;
            st.State = near ? "处理中" : "运行";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? n = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
