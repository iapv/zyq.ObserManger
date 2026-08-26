using zyq.ObserManger.Models;

namespace zyq.ObserManger.Services;

/// <summary>
/// 监控数据取数接口。ViewModel 只依赖此接口，不依赖具体协议。
/// </summary>
public interface IMonitoringDataSource
{
    Task<MonitoringSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default);

    Task<List<PanelAdapterModel>> GetPanelAdapterModelAsync(CancellationToken cancellationToken = default);


}
