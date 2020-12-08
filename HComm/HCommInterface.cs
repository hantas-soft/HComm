using System;
using System.Collections.Generic;
using System.Threading;
using HComm.Common;
using HComm.Device;

namespace HComm
{
    public class HCommInterface
    {
        private const int ProcessTime = 10;
        private const int MaxQueueSize = 20;
        private const int MaxParamBlock = 30;
        private IHComm Comm { get; set; }
        private Timer MsgTimer { get; }
        private List<HCommMsg> MsgQueue { get; } = new List<HCommMsg>();
        
        /// <summary>
        /// HComm communicator connected state
        /// </summary>
        public bool IsConnected => Comm != null && Comm.IsConnected;
        /// <summary>
        /// HComm communicator type
        /// </summary>
        public CommType Type { get; set; } = CommType.None;
        /// <summary>
        /// HComm data received delegate
        /// </summary>
        /// <param name="cmd">command</param>
        /// <param name="addr">address</param>
        /// <param name="values">values</param>
        public delegate void ReceivedData(Command cmd, int addr, int[] values);
        /// <summary>
        /// HComm data received event
        /// </summary>
        public ReceivedData ReceivedMsg { get; set; }
        /// <summary>
        /// HComm interface constructor
        /// </summary>
        public HCommInterface()
        {
            // message process timer
            MsgTimer = new Timer(ProcessTimer);
        }

        /// <summary>
        /// HComm interface setup
        /// </summary>
        /// <param name="type"></param>
        public void SetUp(CommType type)
        {
            // check communicator
            if (IsConnected)
            {
                // close
                Close();
                // clear
                Comm = null;
            }
            // check type
            switch (type)
            {
                case CommType.None:
                    break;
                case CommType.Serial:
                    // new serial
                    Comm = new HcSerial();
                    break;
                case CommType.Ethernet:
                    // new ethernet
                    Comm = new HcEthernet();
                    break;
                case CommType.Usb:
                    // new usb
                    Comm = new HcUsb();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            // set type
            Type = type;
        }
        /// <summary>
        /// HComm interface connect
        /// </summary>
        /// <param name="target">target</param>
        /// <param name="option">option</param>
        /// <param name="id">id</param>
        /// <returns>result</returns>
        public bool Connect(string target, int option, byte id = 0x01)
        {
            // check communicator type
            if (Type == CommType.None)
                return false;
            // connect
            var res = Comm.Connect(target, option, id);
            // check res
            if (!res)
                return false;
            // set event
            Comm.AckReceived = AckReceivedCallback;
            // start process timer
            MsgTimer.Change(ProcessTime, ProcessTime);
            // result
            return true;
        }
        /// <summary>
        /// HComm interface close
        /// </summary>
        /// <returns>result</returns>
        public bool Close()
        {
            // stop process timer
            MsgTimer.Change(Timeout.Infinite, Timeout.Infinite);
            // reset event
            Comm.AckReceived = null;
            // clear queue
            MsgQueue.Clear();
            // close
            Comm.Close();
            // result
            return true;
        }

        /// <summary>
        /// HComm device get parameter
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>result</returns>
        public bool GetParam(ushort addr, ushort count)
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                // check type
                if (Type == CommType.Usb)
                {
                    // block / remain
                    var block = (ushort) (count / MaxParamBlock);
                    var remain = (ushort) (count % MaxParamBlock);
                    var limit = block + (remain > 0 ? 1 : 0);
                    // check block
                    for (var i = 0; i < limit; i++)
                    {
                        // set address
                        var start = (ushort) (addr + i * MaxParamBlock);
                        // check block num
                        MsgQueue.Add(i == limit - 1 && remain > 0
                            ? new HCommMsg(start, Comm.PacketGetParam(start, remain))
                            : new HCommMsg(start, Comm.PacketGetParam(start, MaxParamBlock)));
                    }
                }
                else
                    // add queue
                    MsgQueue.Add(new HCommMsg(addr, Comm.PacketGetParam(addr, count)));
                // result
                return true;
            }
        }
        /// <summary>
        /// HComm device set parameter
        /// </summary>
        /// <param name="addr">addr</param>
        /// <param name="value">value</param>
        /// <returns>result</returns>
        public bool SetParam(ushort addr, ushort value)
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(addr, Comm.PacketSetParam(addr, value)));
                // result
                return true;
            }
        }
        /// <summary>
        /// HComm device get information
        /// </summary>
        /// <returns>result</returns>
        public bool GetInfo()
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(0, Comm.PacketGetInfo()));
                // result
                return true;
            }
        }
        /// <summary>
        /// HComm device set real-time monitoring state
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="state">state</param>
        /// <returns>result</returns>
        public bool SetRealTime(ushort addr = 4002, ushort state = 1)
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(addr, Comm.PacketSetParam(addr, state)));
                // result
                return true;
            }
        }
        /// <summary>
        /// HComm device set graph monitoring state
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="state">state</param>
        /// <returns>result</returns>
        public bool SetGraph(ushort addr = 4100, ushort state = 1)
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(addr, Comm.PacketSetParam(addr, state)));
                // result
                return true;
            }
        }
        /// <summary>
        /// HComm device get current state
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="count">count</param>
        /// <returns>result</returns>
        public bool GetState(ushort addr = 3300, ushort count = 14)
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(addr, Comm.PacketGetState(addr, count)));
                // result
                return true;
            }
        }
        /// <summary>
        /// HComm device get graph monitoring data
        /// </summary>
        /// <param name="addr">not use: address</param>
        /// <param name="count">not use: count</param>
        /// <returns>result</returns>
        public bool GetGraph(ushort addr = 4200, ushort count = 1)
        {
            // lock message queue
            lock (MsgQueue)
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(addr, Comm.PacketGetGraph(addr, count)));
                // result
                return true;
            }
        }

        private void ProcessTimer(object state)
        {
            // check connection state
            if (!Comm.IsConnected)
                return;
            // lock queue
            lock (MsgQueue)
            {
                // check queue count
                if (MsgQueue.Count == 0)
                    return;
                // get first message
                var msg = MsgQueue[0];
                // check queue active
                if (!msg.Active)
                {
                    // write
                    if (!Comm.Write(msg.Packet.ToArray(), msg.Packet.Count))
                        return;
                    // active waiting
                    msg.Time = DateTime.Now;
                    msg.Active = true;
                }
                else
                {
                    // laps
                    var laps = DateTime.Now - msg.Time;
                    // check time
                    if (laps.TotalMilliseconds < 500)
                        return;
                    // reset timer
                    msg.Active = false;
                    msg.Retry -= 1;
                    msg.Time = DateTime.Now;
                    // check retry count
                    if (msg.Retry >= 1) 
                        return;
                    // error
                    Console.WriteLine(@"============= Communication error ===");
                    // clear
                    MsgQueue.Remove(msg);
                }
            }
        }
        private void AckReceivedCallback(Command cmd, byte[] packet)
        {
            int[] values = null;
            var length = 0;
            int count;

            // check command
            switch (cmd)
            {
                case Command.Read:
                    // check packet length
                    if (packet.Length > 0)
                        // set length
                        length = packet[0];
                    // check length
                    if (length == packet.Length - 1)
                    {
                        count = length / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                            values[i] = (packet[i * 2 + 1] << 8) | packet[i * 2 + 2];
                    }
                    break;
                case Command.Mor:
                    // check packet length
                    if (packet.Length > 0)
                        // set length
                        length = packet[0];
                    // check length
                    if (length == packet.Length - 1)
                    {
                        count = length / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                            values[i] = (packet[i * 2 + 1] << 8) | packet[i * 2 + 2];
                    }
                    break;
                case Command.Write:
                    // check packet length
                    if (packet.Length > 0)
                        // set length
                        length = 4;
                    // check length
                    if (length == packet.Length)
                    {
                        count = length / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                            values[i] = (packet[i * 2] << 8) | packet[i * 2 + 1];
                    }
                    break;
                case Command.Info:
                    // check packet length
                    if (packet.Length > 0)
                        // set length
                        length = packet[0];
                    // check length
                    if (length == packet.Length - 1)
                    {
                        count = length / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                            values[i] = (packet[i * 2 + 1] << 8) | packet[i * 2 + 2];
                    }
                    break;
                case Command.Graph:
                    // check packet length
                    if (packet.Length > 1)
                        // set length
                        length = (packet[0] << 8) | packet[1];
                    // check length
                    if (length == packet.Length - 2)
                    {
                        count = length / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                        {
                            values[i] = (packet[i * 2 + 2] << 8) | packet[i * 2 + 3];
                            // check MSB
                            if ((values[i] & 0x8000) == 0x8000)
                                // convert short type
                                values[i] = (short) values[i];
                        }
                    }
                    break;
                case Command.GraphRes:
                    // check packet length
                    if (packet.Length > 1)
                        // set length
                        length = (packet[0] << 8) | packet[1];
                    // check length
                    if (length == packet.Length - 2)
                    {
                        count = length / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                        {
                            values[i] = (packet[i * 2 + 2] << 8) | packet[i * 2 + 3];
                            // check MSB
                            if ((values[i] & 0x8000) == 0x8000)
                                // convert short type
                                values[i] = (short)values[i];
                        }
                    }
                    break;
                case Command.GraphAd:
                    // check packet length
                    if (packet.Length > 1)
                        // set length
                        length = packet[0];
                    // check length
                    if (length > 1 && length == packet.Length - 1)
                    {
                        count = (length - 2) / 2;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                        {
                            values[i] = (packet[i * 2 + 1] << 8) | packet[i * 2 + 2];
                            // check MSB
                            if ((values[i] & 0x8000) == 0x8000)
                                // convert short type
                                values[i] = (short)values[i];
                        }
                    }
                    break;
                case Command.Error:
                    // check packet length
                    if (packet.Length > 0)
                        length = 1;
                    // check length
                    if (length == packet.Length)
                    {
                        count = 1;
                        values = new int[count];
                        // set values
                        values[0] = packet[0];
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cmd), cmd, null);
            }
            // lock message queue
            lock (MsgQueue)
            {
                // get message
                var msg = MsgQueue.Count > 0 ? MsgQueue[0] : null;
                // get address
                var addr = msg == null ||
                           cmd == Command.Graph || cmd == Command.GraphRes ||
                           cmd == Command.Mor && msg.Address < 3200 && msg.Address > 3237
                    ? 0
                    : msg.Address;
                // update message
                ReceivedMsg?.Invoke(cmd, addr, values);
                // check passing
                if (msg == null ||
                    cmd == Command.Graph || cmd == Command.GraphRes ||
                    cmd == Command.Mor && msg.Address < 3200 && msg.Address > 3237)
                    return;
                // clear first queue
                MsgQueue.RemoveAt(0);
            }
        }
    }
}