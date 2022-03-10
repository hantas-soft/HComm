using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HComm.Common;
using UsbHid;
using UsbHid.USB.Classes;

namespace HComm.Device
{
    public class HcUsb : IHComm
    {
        private const int ProcessTime = 10;
        private const int TimeoutTime = 100;
        private const int Vid = 0x0483;
        private const int Pid = 0x5710;
        private UsbHidDevice UsbDevice { get; set; }
        private Timer ProcessTimer { get; set; }
        private List<byte> ReceiveBuf { get; } = new List<byte>();
        private List<byte> AnalyzeBuf { get; } = new List<byte>();
        private byte Id { get; set; }

        /// <summary>
        ///     HComm usb connection state
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        ///     HComm usb received data event
        /// </summary>
        public AckData AckReceived { get; set; }

        /// <summary>
        ///     HCommInterface usb raw acknowledge event
        /// </summary>
        public AckRawData AckRawReceived { get; set; }

        /// <summary>
        ///     HCommInterface usb monitor acknowledge event
        /// </summary>
        public AckMorData AckMorReceived { get; set; }

        /// <summary>
        ///     HCommInterface connection state changed
        /// </summary>
        public ChangedConnection ConnectionChanged { get; set; }

        /// <summary>
        ///     HComm usb connect
        /// </summary>
        /// <param name="target">not use: target</param>
        /// <param name="option">not use: option</param>
        /// <param name="id">id</param>
        /// <returns>result</returns>
        public bool Connect(string target, int option, byte id = 2)
        {
            // check id
            if (id > 0x0F)
                return false;

            try
            {
                // open
                var devices = DeviceDiscovery.FindHidDevices(new VidPidMatcher(Vid, Pid));
                // check device
                if (devices.Count == 0)
                    return false;
                // get device
                UsbDevice = new UsbHidDevice(devices[0].Key);

                // set id
                Id = id;
                // clear buffer
                ReceiveBuf.Clear();
                AnalyzeBuf.Clear();
                // set event
                UsbDevice.DataReceived += UsbDevice_DataReceived;
                // check timer
                if (ProcessTimer == null)
                    // new timer
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
        ///     HComm usb close
        /// </summary>
        /// <returns>result</returns>
        public bool Close()
        {
            // stop timer
            ProcessTimer.Change(Timeout.Infinite, Timeout.Infinite);
            // clear buffer
            ReceiveBuf.Clear();
            AnalyzeBuf.Clear();
            // reset event
            UsbDevice.DataReceived -= UsbDevice_DataReceived;
            // disconnect
            UsbDevice.Disconnect();
            // clear
            UsbDevice = null;
            // clear state
            if (IsConnected)
                // update event
                ConnectionChanged?.Invoke(IsConnected = false);

            // result
            return true;
        }

        /// <summary>
        ///     HComm usb packet write
        /// </summary>
        /// <param name="packet">packet</param>
        /// <param name="length">length</param>
        /// <returns>result</returns>
        public bool Write(byte[] packet, int length)
        {
            // check connection
            if (!UsbDevice.IsDeviceConnected)
                return false;

            try
            {
                // write packet
                var res = UsbDevice.SendMessage(packet);

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($@"{e.Message}");
            }

            return false;
        }

        /// <summary>
        ///     HComm usb get parameter packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>result</returns>
        public IEnumerable<byte> PacketGetParam(ushort addr, ushort count)
        {
            var packet = new List<byte>
            {
                Id, (byte)Command.Read,
                (byte)((addr >> 8) & 0xFF),
                (byte)(addr & 0xFF),
                (byte)((count >> 8) & 0xFF),
                (byte)(count & 0xFF)
            };
            // add dummy
            packet.AddRange(new byte[65 - packet.Count]);
            // packet
            return packet.ToArray();
        }

        /// <summary>
        ///     HComm usb set parameter packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="value">value</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketSetParam(ushort addr, ushort value)
        {
            var packet = new List<byte>
            {
                Id, (byte)Command.Write,
                (byte)((addr >> 8) & 0xFF),
                (byte)(addr & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF)
            };
            // add dummy
            packet.AddRange(new byte[65 - packet.Count]);
            // packet
            return packet.ToArray();
        }

        /// <summary>
        ///     HComm usb get state packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetState(ushort addr, ushort count)
        {
            var packet = new List<byte>
            {
                Id, (byte)Command.Mor,
                (byte)((addr >> 8) & 0xFF),
                (byte)(addr & 0xFF),
                (byte)((count >> 8) & 0xFF),
                (byte)(count & 0xFF)
            };
            // add dummy
            packet.AddRange(new byte[65 - packet.Count]);
            // packet
            return packet.ToArray();
        }

        /// <summary>
        ///     HComm usb get info packet make
        /// </summary>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetInfo()
        {
            var packet = new List<byte>
            {
                Id, (byte)Command.Info
            };
            // add dummy
            packet.AddRange(new byte[65 - packet.Count]);
            // packet
            return packet.ToArray();
        }

        /// <summary>
        ///     HComm usb get graph packet make
        /// </summary>
        /// <param name="addr">not use: address</param>
        /// <param name="count">not use: count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetGraph(ushort addr, ushort count)
        {
            var packet = new List<byte>
            {
                Id, (byte)Command.GraphAd, 0x00
            };
            // add dummy
            packet.AddRange(new byte[65 - packet.Count]);
            // packet
            return packet.ToArray();
        }

        /// <summary>
        ///     HComm usb get device list
        /// </summary>
        /// <returns>list</returns>
        public static List<string> GetDeviceNames()
        {
            // get devices
            var devices = DeviceDiscovery.FindHidDevices(new VidPidMatcher(Vid, Pid));
            // convert name list
            return devices.Select(device => device.Value.Product).ToList();
        }

        private void UsbDevice_DataReceived(byte[] data)
        {
            // lock receive buffer
            if (!Monitor.TryEnter(ReceiveBuf, TimeoutTime))
                return;
            try
            {
                // add receive data
                ReceiveBuf.AddRange(data);
                // update raw event
                AckRawReceived?.Invoke(data);
            }
            finally
            {
                // unlock
                Monitor.Exit(ReceiveBuf);
            }
        }

        private void ProcessTimerCallback(object state)
        {
            // lock receive buffer
            if (!Monitor.TryEnter(ReceiveBuf, TimeoutTime))
                return;
            try
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

                    // check frame length
                    if (AnalyzeBuf.Count < UsbDevice.InputReportByteLength)
                        break;
                    // set command
                    var cmd = (Command)AnalyzeBuf[1];
                    var error = (byte)cmd & 0x80;
                    // check error
                    if (error == 0x80 && cmd != Command.GraphAd)
                    {
                        // error
                        AckReceived?.Invoke(Command.Error, AnalyzeBuf.Skip(2).Take(1).ToArray());
                        // remove analyze buffer
                        AnalyzeBuf.RemoveRange(0, UsbDevice.InputReportByteLength);
                        // break
                        break;
                    }

                    var length = 0;
                    // check command
                    switch (cmd)
                    {
                        case Command.Write:
                            // set length
                            length = 4;
                            break;
                        case Command.Graph:
                        case Command.GraphRes:
                        case Command.GraphAd:
                        case Command.Read:
                        case Command.Mor:
                        case Command.Info:
                            // set length
                            length = AnalyzeBuf[2] + 1;
                            break;
                        case Command.Error:
                            break;
                        case Command.None:
                        default:
                            // clear analyze buffer
                            AnalyzeBuf.Clear();
                            // exit
                            return;
                    }

                    // check body length
                    if (length > AnalyzeBuf.Count)
                        break;
                    // check state
                    if (!IsConnected)
                        // update event
                        ConnectionChanged?.Invoke(IsConnected = true);
                    // get packet
                    var packet = AnalyzeBuf.Take(UsbDevice.InputReportByteLength).ToArray();
                    // process acknowledge
                    AckReceived?.Invoke(cmd, packet.Skip(2).Take(length).ToArray());
                    // remove analyze buffer
                    AnalyzeBuf.RemoveRange(0, UsbDevice.InputReportByteLength);
                }
            }
            finally
            {
                // unlock
                Monitor.Exit(ReceiveBuf);
            }
        }
    }
}