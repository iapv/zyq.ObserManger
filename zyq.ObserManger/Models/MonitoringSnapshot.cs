namespace zyq.ObserManger.Models;

/// <summary>
/// 一次从数据源取到的监控快照。
/// 数值由 Modbus 寄存器或 Mock 数据源映射而来。
/// </summary>
public record MonitoringSnapshot(
    int MachineTotal,
    int ProductionCount,
    int DefectCount);
