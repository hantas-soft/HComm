using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using SuperSocket.ClientEngine;
using HComm.Common;

namespace HComm.Device
{
    public class HcEthernet : IHComm
    {
        private const int ProcessTime = 10;
        private AsyncTcpSession Session { get; } = new AsyncTcpSession();
        private Timer ProcessTimer { get; set; }
        private List<byte> ReceiveBuf { get; } = new List<byte>();
        private List<byte> AnalyzeBuf { get; } = new List<byte>();
        private ushort Transaction { get; set; }
        private ushort Id { get; set; }

        /// <summary>
        /// HComm ethernet session connection state
        /// </summary>
        public bool IsConnected { get; private set; }
        /// <summary>
        /// HComm ethernet received data event
        /// </summary>
        public AckData AckReceived { get; set; }
        /// <summary>
        /// HCommInterface ethernet raw acknowledge event
        /// </summary>
        public AckRawData AckRawReceived { get; set; }
        /// <summary>
        /// HCommInterface connection state changed
        /// </summary>
        public ChangedConnection ConnectionChanged { get; set; } 
        /// <summary>
        /// HComm ethernet constructor
        /// </summary>
        public HcEthernet()
        {
            // set session general event
            Session.Connected += Session_Connected;
            Session.Closed += Session_Closed;
            Session.Error += Session_Error;
        }

        /// <summary>
        /// HComm session connect
        /// </summary>
        /// <param name="target">ip address</param>
        /// <param name="option">port</param>
        /// <param name="id">id</param>
        /// <returns>try connect result</returns>
        public bool Connect(string target, int option, byte id = 1)
        {
            var tryIp = IPAddress.TryParse(target, out var ip);
            // check target
            if (!tryIp)
                return false;
            // check option
            if (option < 1 || option > 65535)
                return false;
            // check id
            if (id < 1 || id > 0x0F)
                return false;

            try
            {
                // get remote point
                var client = new IPEndPoint(ip, option);
                // try connect
                Session.Connect(client);
                // set id
                Id = id;
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
        /// HComm session close
        /// </summary>
        /// <returns>session close result</returns>
        public bool Close()
        {
            // close
            Session.Close();
            // result
            return true;
        }
        /// <summary>
        /// HComm session write
        /// </summary>
        /// <param name="packet">packet</param>
        /// <param name="length">packet length</param>
        /// <returns>result</returns>
        public bool Write(byte[] packet, int length)
        {
            // check connection
            if (!Session.IsConnected)
                return false;

            try
            {
                // write packet
                Session.Send(packet, 0, length);
                // result
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// HComm device get parameter packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetParam(ushort addr, ushort count)
        {
            // transaction id
            Transaction += 1;
            // packet
            var packet = new List<byte>
            {
                // transaction id
                (byte) ((Transaction >> 8) & 0xFF), (byte) (Transaction & 0xFF),
                // protocol id
                0x00, (byte) Id,
                // length
                0x00, 0x06,
                // unit id
                0x00,
                // command
                (byte) Command.Read,
                // values
                (byte) ((addr >> 8) & 0xFF),
                (byte) (addr & 0xFF),
                (byte) ((count >> 8) & 0xFF),
                (byte) (count & 0xFF),
            };
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HComm device set parameter packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="value">value</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketSetParam(ushort addr, ushort value)
        {
            // transaction id
            Transaction += 1;
            // packet
            var packet = new List<byte>
            {
                // transaction id
                (byte) ((Transaction >> 8) & 0xFF), (byte) (Transaction & 0xFF),
                // protocol id
                0x00, (byte) Id,
                // length
                0x00, 0x06,
                // unit id
                0x00,
                // command
                (byte) Command.Write,
                // values
                (byte) ((addr >> 8) & 0xFF),
                (byte) (addr & 0xFF),
                (byte) ((value >> 8) & 0xFF),
                (byte) (value & 0xFF),
            };
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HComm device get state packet make
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetState(ushort addr, ushort count)
        {
            // transaction id
            Transaction += 1;
            // packet
            var packet = new List<byte>
            {
                // transaction id
                (byte) ((Transaction >> 8) & 0xFF), (byte) (Transaction & 0xFF),
                // protocol id
                0x00, (byte) Id,
                // length
                0x00, 0x06,
                // unit id
                0x00,
                // command
                (byte) Command.Mor,
                // values
                (byte) ((addr >> 8) & 0xFF),
                (byte) (addr & 0xFF),
                (byte) ((count >> 8) & 0xFF),
                (byte) (count & 0xFF),
            };
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HComm device get information packet make
        /// </summary>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetInfo()
        {
            // transaction id
            Transaction += 1;
            // packet
            var packet = new List<byte>
            {
                // transaction id
                (byte) ((Transaction >> 8) & 0xFF), (byte) (Transaction & 0xFF),
                // protocol id
                0x00, (byte) Id,
                // length
                0x00, 0x02,
                // unit id
                0x00,
                // command
                (byte) Command.Info,
            };
            // packet
            return packet.ToArray();
        }
        /// <summary>
        /// HComm device get graph monitoring data packet make
        /// </summary>
        /// <param name="addr">not use: address</param>
        /// <param name="count">not use: count</param>
        /// <returns>packet</returns>
        public IEnumerable<byte> PacketGetGraph(ushort addr, ushort count)
        {
            return null;
        }

        private void Session_Connected(object sender, EventArgs e)
        {
            // clear buffer
            ReceiveBuf.Clear();
            AnalyzeBuf.Clear();
            // set event
            Session.DataReceived += SessionDataReceived;
            // check timer
            if (ProcessTimer == null)
                ProcessTimer = new Timer(ProcessTimerCallback);
            // start timer
            ProcessTimer.Change(ProcessTime, ProcessTime);
            // update event
            ConnectionChanged?.Invoke(IsConnected = true);
        }
        private void Session_Closed(object sender, EventArgs e)
        {
            // stop timer
            ProcessTimer.Change(Timeout.Infinite, Timeout.Infinite);
            // reset event
            Session.DataReceived -= SessionDataReceived;
            // clear buffer
            ReceiveBuf.Clear();
            AnalyzeBuf.Clear();
            // update event
            ConnectionChanged?.Invoke(IsConnected = false);
        }
        private void Session_Error(object sender, ErrorEventArgs e)
        {
            // error
            AckReceived?.Invoke(Command.Error, new byte[] {0xFF});
        }
        private void SessionDataReceived(object sender, DataEventArgs e)
        {
            // lock receive buffer
            lock (ReceiveBuf)
            {
                // get data
                var data = e.Data.Take(e.Length).ToArray();
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
                    if (AnalyzeBuf.Count < 8)
                        break;
                    // set frame length
                    var frame = AnalyzeBuf[4] << 8 | AnalyzeBuf[5] + 6;
                    var length = frame - 8;
                    // check frame length
                    if(frame < 8)
                        // clear analyze buffer
                        AnalyzeBuf.Clear();
                    if (frame > AnalyzeBuf.Count)
                        break;

                    // get packet
                    var packet = AnalyzeBuf.Take(frame).ToArray();
                    // set command
                    var cmd = (Command) packet[7];
                    // check error
                    if (((byte) cmd & 0xF0) == 0x80)
                    {
                        // error
                        AckReceived?.Invoke(
                            Command.Error,
                            packet.Skip(frame - 1).Take(1).ToArray());
                        // remove analyze buffer
                        AnalyzeBuf.RemoveRange(0, frame);
                        // break
                        break;
                    }
                    // process message
                    AckReceived?.Invoke(cmd, packet.Skip(8).Take(length).ToArray());
                    // remove analyze buffer
                    AnalyzeBuf.RemoveRange(0, frame);
                }
            }
        }
    }
}