using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using HComm.Common;
using HComm.Device;

namespace HComm
{
    public class HCommInterface
    {
        private const int ProcessTime = 10;
        private IHComm Comm { get; set; }
        private Timer MsgTimer { get; }
        private List<HCommMsg> MsgQueue { get; } = new List<HCommMsg>();
        private DateTime ConnectionTime { get; set; }
        private DateTime InfoTime { get; set; }
        
        /*
        /// <summary>
        /// HComm communicator connected state
        /// </summary>
        public bool IsConnected { get; set; }
        */
        public ConnectionState State { get; private set; } = ConnectionState.Disconnected;
        /// <summary>
        /// HComm communicator type
        /// </summary>
        public CommType Type { get; private set; } = CommType.None;
        /// <summary>
        /// HComm communicator message queue size
        /// </summary>
        public int MaxQueueSize { get; set; } = 30;
        /// <summary>
        /// HComm communicator waiting queue count
        /// </summary>
        public int QueueCount
        {
            get
            {
                // lock message queue
                if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100)))
                    return 0;
                try
                {
                    return MsgQueue.Count;
                }
                finally
                {
                    // unlock
                    Monitor.Exit(MsgQueue);
                }
            }
        }
        /// <summary>
        /// HComm communicator message block size (USB > 30 not working)
        /// </summary>
        public int MaxParamBlock { get; set; } = 100;
        /// <summary>
        /// HComm communicator automatic request information command
        /// </summary>
        public bool AutoRequestInfo { get; set; } = true;
        /// <summary>
        /// Device information
        /// </summary>
        public DeviceInfo Information { get; } = new DeviceInfo();

        /// <summary>
        /// HComm data received delegate
        /// </summary>
        /// <param name="cmd">command</param>
        /// <param name="addr">address</param>
        /// <param name="values">values</param>
        public delegate void ReceivedData(Command cmd, int addr, int[] values);
        /// <summary>
        /// HComm raw data received delegate
        /// </summary>
        /// <param name="packet">packet</param>
        public delegate void ReceivedRawData(byte[] packet);
        /// <summary>
        /// HComm connection state changed delegate
        /// </summary>
        /// <param name="state"></param>
        public delegate void ChangedConnectState(bool state);
        /// <summary>
        /// HComm data received event
        /// </summary>
        public ReceivedData ReceivedMsg { get; set; }
        /// <summary>
        /// HComm raw data received event
        /// </summary>
        public ReceivedRawData ReceivedRawMsg { get; set; }
        /// <summary>
        /// HComm connection state changed event
        /// </summary>
        public ChangedConnectState ChangedConnection { get; set; }
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
        /// <param name="type">communication type</param>
        /// <returns>result</returns>
        public bool SetUp(CommType type)
        {
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
            // set connection event
            Comm.ConnectionChanged = ConnectionChanged;
            // set state
            State = ConnectionState.Disconnected;
            // result
            return Comm != null;
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
            // check state
            if (State != ConnectionState.Disconnected)
                return false;
            // connect request
            if (!Comm.Connect(target, option, id))
                return false;
            // set time state
            ConnectionTime = DateTime.Now;
            InfoTime = DateTime.Now;
            // set state
            State = ConnectionState.Connecting;
            // clear message queue
            MsgQueue.Clear();
            // start process timer
            MsgTimer.Change(ProcessTime, ProcessTime);

            return true;
        }
        /// <summary>
        /// HComm interface close
        /// </summary>
        /// <returns>result</returns>
        public bool Close()
        {
            // set state
            State = ConnectionState.Disconnecting;
            // set connection time
            ConnectionTime = DateTime.Now;
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
        public bool GetParam(ushort addr, ushort count, bool merge = false)
        {
            // lock message queue
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100))) 
                return false;
            try
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                var blockSize = MaxParamBlock;
                // check usb type
                if (Type == CommType.Usb)
                    // fixed size
                    blockSize = 30;
                // block / remain
                var block = (ushort) (count / blockSize);
                var remain = (ushort) (count % blockSize);
                var limit = block + (remain > 0 || block == 0 && remain == 0 ? 1 : 0);
                // check merge block
                if (!merge)
                {
                    // check block
                    for (var i = 0; i < limit; i++)
                    {
                        // set address
                        var start = (ushort) (addr + i * blockSize);
                        // check block num
                        MsgQueue.Add(i == limit - 1 && remain != 0 || limit == 1 && block == 0 && remain == 0
                            ? new HCommMsg(start, Comm.PacketGetParam(start, remain))
                            : new HCommMsg(start, Comm.PacketGetParam(start, (ushort) blockSize)));
                    }
                }
                else
                    MsgQueue.Add(new HCommMsg(addr, Comm.PacketGetParam(addr, count)));
                // result
                return true;
            }
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
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
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100))) 
                return false;
            try
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check queue count
                if (MsgQueue.Count > 1)
                    // inser queue
                    MsgQueue.Insert(1, new HCommMsg(addr, Comm.PacketSetParam(addr, value)));
                else
                    // add queue
                    MsgQueue.Add(new HCommMsg(addr, Comm.PacketSetParam(addr, value)));
                // result
                return true;
            }
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
            }
        }
        /// <summary>
        /// HComm device get information
        /// </summary>
        /// <returns>result</returns>
        public bool GetInfo()
        {
            // lock message queue
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100)))
                return false;
            try
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // add queue
                MsgQueue.Add(new HCommMsg(0, Comm.PacketGetInfo()));
                // result
                return true;
            }
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
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
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100)))
                return false;
            try
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
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
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
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100))) 
                return false;
            try
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
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
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
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100))) 
                return false;
            try
            {
                // check max queue size
                if (MsgQueue.Count >= MaxQueueSize)
                    return false;
                // check duplicate message
                if (MsgQueue.Find(x => x.Address == addr) != null)
                    return false;
                // check queue count
                if (MsgQueue.Count > 1)
                    // inser queue
                    MsgQueue.Insert(1, new HCommMsg(addr, Comm.PacketGetState(addr, count)));
                else
                    // add queue
                    MsgQueue.Add(new HCommMsg(addr, Comm.PacketGetState(addr, count)));
                // result
                return true;
            }
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
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
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100))) 
                return false;
            try
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
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
            }
        }

        private void ProcessTimer(object state)
        {
            // check connection state
            if (AutoRequestInfo && (DateTime.Now - ConnectionTime).TotalSeconds > 3)
            {
                // stop process timer
                MsgTimer.Change(Timeout.Infinite, Timeout.Infinite);
                // disconnect
                Comm.Close();
                // reset event
                Comm.AckReceived = null;
                Comm.AckRawReceived = null;
                // clear communicator
                Comm = null;
                // change state
                State = ConnectionState.Disconnected;
                // update event
                ChangedConnection?.Invoke(false);
                // exit
                return;
            }
            // lock message queue
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100)))
                return;
            try
            {
                // check message queue
                if (MsgQueue.Count > 0)
                {
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
                        if (msg.Retry > 0)
                            return;
                        // remove message
                        MsgQueue.Remove(msg);
                        // error
                        ReceivedMsg?.Invoke(Command.Error, 0, new int[] { 0x00 });
                    }
                }
                else if (AutoRequestInfo && (DateTime.Now - InfoTime).TotalSeconds > 1)
                    // get information
                    GetInfo();
            }
            finally
            {
                // unlock msg queue
                Monitor.Exit(MsgQueue);
            }
        }
        private void AckReceivedCallback(Command cmd, byte[] packet)
        {
            int[] values = null;
            var length = 0;
            int count = 0;

            // reset connection time
            ConnectionTime = DateTime.Now;

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
                        count = length;
                        values = new int[count];
                        // set values
                        for (var i = 0; i < count; i++)
                            values[i] = packet[i + 1];
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
                        count = (length - 2) / 2 + 1;
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
            if (!Monitor.TryEnter(MsgQueue, new TimeSpan(0, 0, 0, 0, 100)))
                return;
            try
            {
                // get message
                var msg = MsgQueue.Count > 0 ? MsgQueue[0] : null;
                // get address
                var addr = msg == null ||
                           cmd == Command.Graph || cmd == Command.GraphRes ||
                           cmd == Command.Mor && msg.Address < 3200 && msg.Address > 3237
                    ? 0
                    : msg.Address;
                // check information
                if (cmd == Command.Info && (count == 12 || count == 13))
                    // set info
                    Information.SetInfo(values);
                // update message
                ReceivedMsg?.Invoke(cmd, addr, values);
                // set info time
                InfoTime = DateTime.Now;
                // check passing
                if (msg == null || MsgQueue.Count == 0 ||
                    cmd == Command.Graph || cmd == Command.GraphRes ||
                    cmd == Command.Mor && msg.Address < 3200 && msg.Address > 3237)
                    return;
                // clear first queue
                MsgQueue.RemoveAt(0);
            }
            finally
            {
                // unlock
                Monitor.Exit(MsgQueue);
            }
        }
        private void AckRawReceived(byte[] packet)
        {
            // update event
            ReceivedRawMsg?.Invoke(packet);
        }
        private void ConnectionChanged(bool state)
        {
            // check state
            switch (State)
            {
                case ConnectionState.None:
                    break;
                case ConnectionState.Connecting:
                    // check state
                    if (state)
                    {
                        // change state
                        State = ConnectionState.Connected;
                        // set event
                        Comm.AckReceived = AckReceivedCallback;
                        Comm.AckRawReceived = AckRawReceived;
                        // update event
                        ChangedConnection?.Invoke(true);
                    }
                    break;
                case ConnectionState.Connected:
                    break;
                case ConnectionState.Disconnecting:
                    // check state
                    if (!state)
                    {
                        // stop process timer
                        MsgTimer.Change(Timeout.Infinite, Timeout.Infinite);
                        // reset event
                        Comm.AckReceived = null;
                        Comm.AckRawReceived = null;
                        // clear communicator
                        Comm = null;
                        // change state
                        State = ConnectionState.Disconnected;
                        // update event
                        ChangedConnection?.Invoke(false);
                    }
                    break;
                case ConnectionState.Disconnected:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public class DeviceInfo
        {
            public List<byte> Values { get; } = new List<byte>();
            /// <summary>
            /// Driver id
            /// </summary>
            public int DriverId => (Values[0] << 8) | Values[1];
            /// <summary>
            /// Controller model number
            /// </summary>
            public int Controller => (Values[2] << 8) | Values[3];
            /// <summary>
            /// Driver model number
            /// </summary>
            public int Driver => (Values[4] << 8) | Values[5];
            /// <summary>
            /// Device version
            /// </summary>
            public string Version => ((Values[6] << 8) | Values[7]).ToString("D4").Insert(3, ".").Insert(1, ".");
            /// <summary>
            /// Driver serial number
            /// </summary>
            public string Serial =>
                string.Join("", Values.Skip(8).Take(Values.Count - 8).Reverse().Select(x => $@"{x:D2}").ToArray());
                
            /// <summary>
            /// Set data information
            /// </summary>
            /// <param name="data">packet data</param>
            public void SetInfo(IEnumerable<int> data)
            {
                // clear values
                Values.Clear();
                // check data
                foreach (var value in data)
                    // add dat
                    Values.Add((byte) value);
            }
        }
    }
}