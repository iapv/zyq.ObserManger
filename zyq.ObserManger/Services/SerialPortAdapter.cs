using NModbus.IO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zyq.ObserManger.Services
{
    public  class SerialPortAdapter : IStreamResource
    {
        private readonly SerialPort _serialPort;

        public SerialPortAdapter(SerialPort serialPort)
        {
               _serialPort = serialPort;
        }

        public int InfiniteTimeout => SerialPort.InfiniteTimeout;

        public int ReadTimeout
        {
            get => _serialPort.ReadTimeout;
            set => _serialPort.ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => _serialPort.WriteTimeout;
            set => _serialPort.WriteTimeout = value;
        }

        public void DiscardInBuffer() => _serialPort.DiscardInBuffer();

        public int Read(byte[] buffer, int offset, int count)
            => _serialPort.Read(buffer, offset, count);

        public void Write(byte[] buffer, int offset, int count)
            => _serialPort.Write(buffer, offset, count);

        public void Dispose() => _serialPort.Dispose();

    }
}
