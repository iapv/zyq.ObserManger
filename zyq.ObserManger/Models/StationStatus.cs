using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace zyq.ObserManger.Models;

public class StationStatus : INotifyPropertyChanged
{
    public string Name { get; }
    public double PositionX { get; }      // 在 Canvas 里的 X 坐标，用于定位
    private string _state;

    public string State
    {
        get => _state;
        set { _state = value; OnPropertyChanged(); }
    }

    public StationStatus(string name, double positionX, string state)
    {
        Name = name; PositionX = positionX; _state = state;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? n = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
