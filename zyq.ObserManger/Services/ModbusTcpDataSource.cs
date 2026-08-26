using System.Net;
using System.Net.Sockets;
using NModbus;
using zyq.ObserManger.Models;

namespace zyq.ObserManger.Services;

/// <summary>
/// 通过 Modbus TCP 读取保持寄存器的真实数据源。
/// 对应 Modbus Slave 模拟器配置：从站 ID=1，功能码 03，寄存器地址 0 开始。
/// </summary>
public class ModbusTcpDataSource 
{
    private readonly string _host;
    private readonly int _port;
    private readonly byte _slaveId;

    public ModbusTcpDataSource(string host = "127.0.0.1", int port = 502, byte slaveId = 1)
    {
        _host = host;
        _port = port;
        _slaveId = slaveId;
    }

    public async Task<MonitoringSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(IPAddress.Parse(_host), _port);

        var factory = new ModbusFactory();
        using var master = factory.CreateMaster(client);

        // 从地址 0 开始读 3 个保持寄存器（功能码 03）。
        // Modbus Slave 截图里：0=298, 1=1643, 2=23。
        ushort[] registers = await master.ReadHoldingRegistersAsync(_slaveId, startAddress: 0, numberOfPoints: 3);

        return new MonitoringSnapshot(
            MachineTotal: registers[0],
            ProductionCount: registers[1],
            DefectCount: registers[2]);
    }


}
