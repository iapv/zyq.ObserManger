using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using zyq.ObserManger.Services;

namespace zyq.ObserManger.ViewModel;

public class MonitorAViewModel : INotifyPropertyChanged
{
    private readonly IMonitoringDataSource _dataSource;
    private readonly DispatcherTimer _timer;
    private PanelAdapterVm _panelAdapterVm;
    public PanelAdapterVm PanelAdapterVm
    {
        get => _panelAdapterVm;
        set
        {
            if (_panelAdapterVm != value)
            {
                _panelAdapterVm = value;
                OnPropertyChanged();
            }
        }
    }


    private ProductionHourliesVm _productionHourliesVm;

    public ProductionHourliesVm ProductionHourliesVm
    {
        get => _productionHourliesVm;
        set
        {
            if (_productionHourliesVm != value)
            {
                _productionHourliesVm = value;
                OnPropertyChanged();
            }
        }
    }
    private QualityHistoryVm _qualityHistoryVm;
    public QualityHistoryVm QualityHistoryVm
    {
        get => _qualityHistoryVm;
        set
        {
            if (_qualityHistoryVm != value)
            {
                _qualityHistoryVm = value;
                OnPropertyChanged();
            }
        }
    }

    private ConveyorVm _conveyorVm;
    public ConveyorVm ConveyorVm
    {
        get => _conveyorVm;
        set { if (_conveyorVm != value) { _conveyorVm = value; OnPropertyChanged(); } }
    }
    private string _title = "监控A";
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private string _machineTotal = "000";
    public string MachineTotal
    {
        get => _machineTotal;
        set { _machineTotal = value; OnPropertyChanged(); }
    }

    private string _productionCount = "0";
    public string ProductionCount
    {
        get => _productionCount;
        set { _productionCount = value; OnPropertyChanged(); }
    }

    private string _defectCount = "0";
    public string DefectCount
    {
        get => _defectCount;
        set { _defectCount = value; OnPropertyChanged(); }
    }

    private string _status = "连接中...";
    public string Status
    {
        get => _status;
        set { _status = value; OnPropertyChanged(); }
    }

    /// <summary>
    /// 默认使用 Modbus TCP 数据源（供 XAML/DataTemplate 实例化）。
    /// </summary>


    public MonitorAViewModel(IMonitoringDataSource dataSource)
    {
        _dataSource = dataSource;
        _panelAdapterVm = new PanelAdapterVm(dataSource);
        _productionHourliesVm = new ProductionHourliesVm();
        _qualityHistoryVm = new QualityHistoryVm();
        _conveyorVm = new ConveyorVm();
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += async (s, e) => await UpdateAsync();
        _= UpdateAsync();
        _timer.Start();
    }

    private async Task UpdateAsync()
    {
        try
        {
            var snapshot = await _dataSource.GetSnapshotAsync();
            MachineTotal = snapshot.MachineTotal.ToString("D4");
            ProductionCount = snapshot.ProductionCount.ToString("D3");
            DefectCount = snapshot.DefectCount.ToString("D3");
            Status = $"已更新 {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex)
        {
            Status = $"读取失败: {ex.Message}";
            System.Diagnostics.Debug.WriteLine("Modbus错误: " + ex);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? n = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
