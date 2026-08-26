using NModbus;
using NModbus.Extensions.Enron;
using NModbus.IO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zyq.ObserManger.Models;
namespace zyq.ObserManger.Services;

public class ModbusRtuDataSource : IMonitoringDataSource
{
    private readonly string _portName;   // "COM3"
    private readonly int _baudRate;      // 9600
    private readonly Parity _parity;     // None / Even / Odd  ← 你说的"校验位"
    private readonly int _dataBits;      // 8
    private readonly StopBits _stopBits; // One
    private readonly byte _slaveId;      // 1

    private SerialPort? _serialPort;
    private IModbusSerialMaster? _modbusMaster;

    private readonly object _lock = new();
    public ModbusRtuDataSource(string portName = "COM13", int baudRate = 9600,
    Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One,
    byte slaveId = 1)
    {
        _portName = portName; _baudRate = baudRate; _parity = parity;
        _dataBits = dataBits; _stopBits = stopBits; _slaveId = slaveId;
    }


    private IModbusMaster EnsureConnected()
    {
        lock (_lock)
        {
            if (_modbusMaster != null && _serialPort?.IsOpen == true)
                return _modbusMaster;

            _modbusMaster?.Dispose();
            _serialPort?.Dispose();

            _serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits);
            _serialPort.Open();
            var factory = new ModbusFactory();
            var adapter = new SerialPortAdapter(_serialPort);
            _modbusMaster = factory.CreateRtuMaster(adapter);

            return _modbusMaster;
        }
    }
    public async Task<MonitoringSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default)
    {
         var master = EnsureConnected();

        ushort[] regs = await master.ReadHoldingRegistersAsync(_slaveId, 0, 3);
        return new MonitoringSnapshot(regs[0], regs[1], regs[2]);
    }

    public async Task<List<PanelAdapterModel>> GetPanelAdapterModelAsync(CancellationToken cancellationToken = default)
    {
        Dictionary<string, int> data = new Dictionary<string, int>();
         var master = EnsureConnected();

        ushort[] regs = await master.ReadHoldingRegistersAsync(_slaveId, 3, 7);
        List<string> regsStr = new List<string>();
        regsStr.Add("光照(Lux)");
        regsStr.Add("噪音(db)");
        regsStr.Add("温度(C)");
        regsStr.Add("适度(%)");
        regsStr.Add("PM2.5(m)");
        regsStr.Add("硫化氢(ppm)");
        regsStr.Add("氮气(ppm)");
        return regsStr.Zip(regs, (x, y) => new PanelAdapterModel(x, y)).ToList();



    }

    public void Dispose()
    {
        _modbusMaster?.Dispose();
        _serialPort?.Dispose();
    }
}
