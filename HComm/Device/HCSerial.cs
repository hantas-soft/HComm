using System;
using System.Linq;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using HComm.Common;

namespace HComm.Device
{
    public class HcSerial : IHComm
    {
        private const int ProcessTime = 10;
        private static readonly int[] BaudRates = {9600, 19200, 38400, 57600, 115200};
        private SerialPort Port { get; } = new SerialPort();
        private Timer ProcessTimer { get; set; }
        private List<byte> ReceiveBuf { get; } = new List<byte>();
        private List<byte> AnalyzeBuf { get; } = new List<byte>();
        private byte Id { get; set; }

        /// <summary>
        /// HCommInterface serial connection state
        /// </summary>
        public bool IsConnected => Port.IsOpen;

        /// <summary>
        /// HCommInterface serial acknowledge event
        /// </summary>
        public AckData AckReceived { get; set; }
        /// <summary>
        /// HCommInterface serial raw acknowledge event
        /// </summary>
        public AckRawData AckRawReceived { get; set; }

        /// <summary>
        /// HCommInterface serial connect
        /// </summary>
        /// <param name="target">target port</param>
        /// <param name="option">baud-rate</param>
        /// <param name="id">id</param>
        /// <returns>result</returns>
        public bool Connect(string target, int option, byte id = 1)
        {
            // check target
            if (string.IsNullOrWhiteSpace(target))
                return false;
            // check option
            if (!BaudRates.Contains(option))
                return false;
            // check id
            if (id < 1 || id > 0x0F)
                return false;

            try
            {
                // check connection
                if (IsConnected)
                    // close
                    Close();
                // set com port
                Port.PortName = target;
                Port.BaudRate = option;
                Port.Encoding = Encoding.GetEncoding(28591);
                // open
                Port.Open();
                // set id
                Id = id;
                // clear buffer
                ReceiveBuf.Clear();
                AnalyzeBuf.Clear();
                // set event
                Port.DataReceived += Port_DataReceived;
                // check timer
                if (ProcessTimer == null)
                    ProcessTimer = new Timer(ProcessTimerCallback);
                // start timer
                ProcessTimer.Change(ProcessTime, ProcessTime);
                // result
                return true;
            }
            catch (Exception e)
            {
                // error
                Console.WriteLine($@"{this}_Connect: {e.Message}");
            }

            return false;
        }
        /// <summary>
        /// HCommInterface serial close
        /// </summary>
        /// <returns>result</returns>
        public bool Close()
        {
            // stop timer
            ProcessTimer.Change(Timeout.Infinite, Timeout.Infinite);
            // close port
            Port.Close();
            // reset event
            Port.DataReceived -= Port_DataReceived;
            // result
            return true;
        }
        /// <summary>
        /// HCommInterface serial write
        /// </summary>
        /// <param name="packet">packet</param>
        /// <param name="length">packet length</param>
        /// <returns>result</returns>
        public bool Write(byte[] packet, int length)
        {
            // check connection
            if (!IsConnected)
                return false;

            try
            {
                // write packet
                Port.Write(packet, 0, length);
                // result
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// HCommInterface serial get parameter packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetParam(ushort addr, ushort count)
        {
            var packet = new List<byte>
            {
                Id, (byte) Command.Read,
                (byte) ((addr >> 8) & 0xFF),
                (byte) (addr & 0xFF),
                (byte) ((count >> 8) & 0xFF),
                (byte) (count & 0xFF),
            };
            // crc
            packet.AddRange(GetCrc(packet.ToArray()));
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HCommInterface serial set parameter packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="value">value</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketSetParam(ushort addr, ushort value)
        {
            var packet = new List<byte>
            {
                Id, (byte) Command.Write,
                (byte) ((addr >> 8) & 0xFF),
                (byte) (addr & 0xFF),
                (byte) ((value >> 8) & 0xFF),
                (byte) (value & 0xFF),
            };
            // crc
            packet.AddRange(GetCrc(packet.ToArray()));
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HCommInterface serial get state packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetState(ushort addr, ushort count)
        {
            var packet = new List<byte>
            {
                Id, (byte) Command.Mor,
                (byte) ((addr >> 8) & 0xFF),
                (byte) (addr & 0xFF),
                (byte) ((count >> 8) & 0xFF),
                (byte) (count & 0xFF),
            };
            // crc
            packet.AddRange(GetCrc(packet.ToArray()));
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HCommInterface serial get info packet make
        /// </summary>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetInfo()
        {
            var packet = new List<byte>
            {
                Id, (byte) Command.Info,
            };
            // crc
            packet.AddRange(GetCrc(packet.ToArray()));
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HCommInterface serial get graph packet make
        /// </summary>
        /// <param name="addr">not use: address</param>
        /// <param name="count">not use: count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetGraph(ushort addr, ushort count)
        {
            var packet = new List<byte>
            {
                Id, (byte) Command.GraphAd, 0x00
            };
            // crc
            packet.AddRange(GetCrc(packet.ToArray()));
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HComm serial get port names
        /// </summary>
        /// <returns>port list</returns>
        public static IEnumerable<string> GetPortNames() => SerialPort.GetPortNames();
        /// <summary>
        /// HComm serial get port baud-rates
        /// </summary>
        /// <returns>baud-rate list</returns>
        public static IEnumerable<int> GetBaudRates() => BaudRates;

        private static IEnumerable<byte> GetCrc(IEnumerable<byte> packet)
        {
            var crc = new byte[] {0xFF, 0xFF};
            ushort crcFull = 0xFFFF;

            foreach (var data in packet)
            {
                crcFull = (ushort) (crcFull ^ data);
                for (var j = 0; j < 8; j++)
                {
                    var lsb = (ushort) (crcFull & 0x0001);
                    crcFull = (ushort) ((crcFull >> 1) & 0x7FFF);
                    if (lsb == 0x01)
                        crcFull = (ushort) (crcFull ^ 0xA001);
                }
            }

            crc[1] = (byte) ((crcFull >> 8) & 0xFF);
            crc[0] = (byte) (crcFull & 0xFF);

            return crc;
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // lock receive buffer
            lock (ReceiveBuf)
            {
                // get data
                var data = Port.Encoding.GetBytes(Port.ReadExisting());
                // add receive data
                ReceiveBuf.AddRange(data);
                // update raw event
                AckRawReceived?.Invoke(data);
            }
        }
        private void ProcessTimerCallback(object state)
        {
            // lock receive buffer
            lock (ReceiveBuf)
            {
                // check receive buffer count
                if (ReceiveBuf.Count > 0)
                {
                    // add analyze buffer
                    AnalyzeBuf.AddRange(ReceiveBuf);
                    // clear receive buffer
                    ReceiveBuf.Clear();
                }

                // timeout
                var timeout = DateTime.Now;
                // check analyze buffer count
                while (AnalyzeBuf.Count > 0)
                {
                    // get laps
                    var laps = DateTime.Now - timeout;
                    // check timeout
                    if (laps.TotalMilliseconds > 500)
                        // clear analyze buffer
                        AnalyzeBuf.Clear();

                    // check header length
                    if (AnalyzeBuf.Count < 3)
                        break;
                    // set command
                    var cmd = (Command) AnalyzeBuf[1];
                    var error = (byte) cmd & 0x80;
                    // check error
                    if (error == 0x80 && cmd != Command.GraphAd)
                        // check header length
                        if (AnalyzeBuf.Count >= 5)
                            cmd = (Command) error;

                    int length;
                    // check header length
                    if (AnalyzeBuf.Count < 4)
                        break;
                    // check command
                    switch (cmd)
                    {
                        case Command.Write:
                            // set length
                            length = 8;
                            break;
                        case Command.Graph:
                        case Command.GraphRes:
                            // set length
                            length = AnalyzeBuf[2] << 8 | AnalyzeBuf[3] + 6;
                            break;
                        case Command.GraphAd:
                        case Command.Read:
                        case Command.Mor:
                        case Command.Info:
                            // set length
                            length = AnalyzeBuf[2] + 5;
                            break;
                        case Command.Error:
                            // set length
                            length = 5;
                            break;
                        default:
                            // clear analyze buffer
                            AnalyzeBuf.Clear();
                            // exit
                            return;
                    }

                    // check body length
                    if (length > AnalyzeBuf.Count)
                        break;
                    // get packet
                    var packet = AnalyzeBuf.Take(length).ToArray();
                    // get crc
                    var crc = GetCrc(packet.Take(length - 2).ToArray()).ToArray();
                    // check crc
                    if (crc[0] != packet[packet.Length - 2] || crc[1] != packet[packet.Length - 1])
                        // error
                        AckReceived?.Invoke(Command.Error, new byte[] {0xFF});
                    else
                        // process acknowledge
                        AckReceived?.Invoke(cmd, packet.Skip(2).Take(length - 4).ToArray());
                    // remove analyze buffer
                    AnalyzeBuf.RemoveRange(0, length);
                }
            }
        }
    }
}