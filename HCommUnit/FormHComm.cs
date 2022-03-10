using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HComm;
using HComm.Common;
using HComm.Device;

namespace HCommExample
{
    public partial class FormHComm : Form
    {
        /// <summary>
        ///     Direction types
        /// </summary>
        public enum DirectionTypes
        {
            Fastening,
            Loosening
        }

        /// <summary>
        ///     Status types
        /// </summary>
        public enum StatusTypes
        {
            Other,
            FasteningOk,
            LooseningNg,
            DirectionChange,
            PresetChange,
            AlarmReset,
            Error,
            Barcode,
            ScrewCountReset
        }

        private static HCommInterface _hComm;

        public FormHComm()
        {
            InitializeComponent();
        }

        private StringBuilder Log { get; } = new StringBuilder();
        private bool StateMor { get; set; }
        private bool StateGraph { get; set; }
        private MonitorType MorType { get; set; }
        
        private List<Item> Items { get; } = new List<Item>();

        private void AddLog(string log, bool lineFeed = false)
        {
            // add time
            Log.Append($@"[{DateTime.Now:HH:mm:ss.fff}] ");
            // check line feed
            if (lineFeed)
                // add line feed
                Log.AppendLine();
            // add log
            Log.AppendLine($@"{log}");
            // print
            tbLog.BeginInvoke(new EventHandler(delegate
            {
                // set text
                tbLog.Text = Log.ToString();
                // set scroll position end
                tbLog.SelectionStart = Log.Length;
                tbLog.ScrollToCaret();
            }));
        }

        private void ReceivedMsg(Command cmd, int addr, int[] values)
        {
            // debug
            //Console.WriteLine($@"COMMAND: {cmd}, ADDRESS: {addr}, LENGTH: {values.Length}");
            // check raw data
            if (cbHexLog.Checked)
                return;
            // check command
            switch (cmd)
            {
                case Command.Read:
                    // check address
                    if (addr > 1000)
                        break;
                    // get count
                    for (var i = 0; i < values.Length; i++)
                    {
                        // get address
                        var address = addr + i - 650;
                        // get preset / index
                        var preset = address / 20;
                        var index = address % 20;
                        // check preset / index
                        if (preset < 0 || preset >= 20 || index < 0 || index >= 20)
                            return;
                        // check index
                        if (index != 0)
                            continue;
                        // check mode
                        if (Items[preset].Mode != values[i])
                            // debug
                            Debug.WriteLine(
                                $@"ADDR: {preset + 1}, MODE: {Items[preset].Mode} -> {values[i]}");
                        // set value
                        Items[preset].Mode = values[i];
                    }
                    
                    // AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.Mor:
                    AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.Write:
                    AddLog($@"{cmd} / {addr} / {values[1]}");
                    break;
                case Command.Info:
                    AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.Graph:
                    AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.GraphRes:
                    AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.GraphAd:
                    // check null data
                    if (values == null || values.Length < 2)
                        break;
                    AddLog($@"GRAPH: {values[0] & 0xFF} / {(values[0] >> 8) & 0xFF} / {values.Length}");
                    break;
                case Command.Error:
                    AddLog($@"{cmd} / {addr} / {values[0]}");
                    break;
                case Command.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cmd), cmd, null);
            }
        }

        private void ReceivedRawMsg(byte[] packet)
        {
            // check raw data
            if (!cbHexLog.Checked)
                return;
            // hex to string
            var hex = string.Concat(packet.Select(d => $@"0x{d:X2} "));
            // add log
            AddLog(hex, true);
        }

        private void ReceivedMonitor(MonitorCommand cmd, byte[] packet)
        {
            // check command
            switch (cmd)
            {
                case MonitorCommand.Backup:
                    // new backup monitor item
                    var backup = new BackupMonitorInfo(packet);
                    // acknowledge
                    _hComm.SetParam(4016, 1);
                    // add log
                    AddLog($@"{backup.FastenTime}", true);
                    break;
                case MonitorCommand.Report:
                    // new backup monitor item
                    var report = new ReportMonitorItem(packet);
                    // add log
                    AddLog($@"{report.Index}", true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cmd), cmd, null);
            }
        }

        private void ChangedState(bool state)
        {
            // debug
            Console.WriteLine($@"connection changed: {state}");
            Invoke(new EventHandler(delegate
            {
                // set button state
                btConnect.Text = state ? @"Disconnect" : @"Connect";
            }));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // check state
            if (StateMor)
                // check type
                switch (MorType)
                {
                    case MonitorType.None:
                        break;
                    case MonitorType.RealTime:
                        _hComm.SetRealTime();
                        break;
                    case MonitorType.RealTimeAd:
                        // _hComm.GetState(3200);
                        // debug
                        _hComm.GetParam(650, 300);
                        _hComm.GetParam(1650, 300);
                        _hComm.GetParam(2650, 300);
                        break;
                    case MonitorType.Graph:
                        break;
                    case MonitorType.GraphAd:
                        break;
                    case MonitorType.State:
                        _hComm.GetState(3300, 21);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            else if (StateGraph)
                // check type
                switch (MorType)
                {
                    case MonitorType.None:
                        break;
                    case MonitorType.RealTime:
                        break;
                    case MonitorType.RealTimeAd:
                        break;
                    case MonitorType.Graph:
                        _hComm.SetGraph();
                        break;
                    case MonitorType.GraphAd:
                        _hComm.GetGraph();
                        break;
                    case MonitorType.State:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        private void FormHComm_Load(object sender, EventArgs e)
        {
            // refresh item list
            btRefresh.PerformClick();
            // check port count
            if (cbPort.Items.Count > 0)
                // select port item
                cbPort.SelectedIndex = 0;
            // check device count
            if (cbDevice.Items.Count > 0)
                // select device item
                cbDevice.SelectedIndex = 0;
            // select baud-rate item
            cbBaudrate.SelectedIndex = 0;
            // select graph channel 1 item
            cbCh1.SelectedIndex = 1;
            // select graph channel 2 item
            cbCh2.SelectedIndex = 0;
            // select graph option item
            cbOption.SelectedIndex = 0;
            // select graph sampling item
            cbSampling.SelectedIndex = 1;
            // select communication type
            cbType.SelectedIndex = 0;

            // check HComm interface
            _hComm = new HCommInterface
            {
                ReceivedMsg = ReceivedMsg,
                ReceivedRawMsg = ReceivedRawMsg,
                ReceivedMorMsg = ReceivedMonitor,
                ChangedConnection = ChangedState
            };
            // start application
            AddLog(@"Start application");
            
            // debug
            for (var i = 0; i < 20; i++)
                Items.Add(new Item { Preset = i + 1 });
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            // clear
            cbPort.Items.Clear();
            cbDevice.Items.Clear();
            // get serial port list
            var ports = HcSerial.GetPortNames();
            // check port list
            foreach (var port in ports)
                // add port
                cbPort.Items.Add(port);
            // get device list
            var devices = HcUsb.GetDeviceNames();
            // check device list
            foreach (var device in devices)
                // add device
                cbDevice.Items.Add(device);
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State == ConnectionState.Disconnected)
            {
                // get type
                var type = (CommType)(cbType.SelectedIndex + 1);
                // get target and option
                var target = string.Empty;
                var option = -1;
                var id = Convert.ToByte(nmID.Value);
                // check type
                switch (type)
                {
                    case CommType.None:
                        break;
                    case CommType.Serial:
                        // check selected item
                        if (cbPort.SelectedIndex < 0)
                            return;
                        // target / option
                        target = cbPort.SelectedItem.ToString();
                        option = Convert.ToInt32(cbBaudrate.SelectedItem.ToString());
                        break;
                    case CommType.Ethernet:
                        // target / option
                        target = tbIp.Text;
                        option = Convert.ToInt32(nmPort.Value);
                        break;
                    case CommType.Usb:
                        // check selected item
                        if (cbDevice.SelectedIndex < 0)
                            return;
                        // target / option
                        target = cbDevice.SelectedItem.ToString();
                        option = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // check target / option
                if (string.IsNullOrWhiteSpace(target))
                    return;
                if (option < 0)
                    return;
                // setup
                _hComm.SetUp(type);
                // set option
                // _hComm.AutoDisconnect = false;
                // _hComm.AutoRequestInfo = false;
                // try connect
                _hComm.Connect(target, option, id);
            }
            else
            {
                // close
                _hComm.Close();
            }
        }

        private void btActionParam_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State != ConnectionState.Connected)
                return;
            // get address / value
            var addr = Convert.ToUInt16(nmAddr.Value);
            var value = Convert.ToUInt16(nmValue.Value);
            // check sender
            if (sender == btGetParam)
                // get param
                _hComm.GetParam(addr, value);
            else if (sender == btSetParam)
                // set param
                _hComm.SetParam(addr, value);
        }

        private void btActionStatus_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State != ConnectionState.Connected)
                return;
            // check sender
            if (sender == btGetStatus)
            {
                // get status
                _hComm.GetState(3200, 30);

                // check state
                if (!StateMor)
                {
                    // set monitor type
                    MorType = MonitorType.State;
                    // set state
                    StateMor = true;
                    // start timer
                    timer.Start();
                }
                else
                {
                    // reset monitor type
                    MorType = MonitorType.None;
                    // reset state
                    StateMor = false;
                    // stop timer
                    timer.Stop();
                }
            }
            else if (sender == btGetInfo)
                // get information
            {
                _hComm.GetInfo();
            }
        }

        private void btActionMor_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State != ConnectionState.Connected)
                return;
            // check sender
            if ((sender == btMorStart || sender == btMorStartAd) && !StateMor)
            {
                // _hComm.SetParam(4025, 0x0195);
                // set monitor type
                MorType = sender == btMorStart ? MonitorType.RealTime : MonitorType.RealTimeAd;
                // set state
                StateMor = true;
                // check ad
                if (sender == btMorStartAd)
                    // change interval
                    timer.Interval = 50;
                // start timer
                timer.Start();
            }
            else if (sender == btMorStop && StateMor)
            {
                // _hComm.SetParam(4025, 0);
                // reset monitor type
                MorType = MonitorType.None;
                // reset state
                // StateMor = !_hComm.SetRealTime(4002, 0);
                // stop event real-time monitoring
                timer.Stop();
                // change interval
                timer.Interval = 5000;
            }
        }

        private void btGraphSet_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State != ConnectionState.Connected)
                return;
            // check state
            if (StateGraph)
                return;
            // get graph setting
            var ch1 = (ushort)cbCh1.SelectedIndex;
            var ch2 = (ushort)cbCh2.SelectedIndex;
            var sampling = (ushort)cbSampling.SelectedIndex;
            var option = (ushort)cbOption.SelectedIndex;
            // set graph setting
            _hComm.SetParam(4101, ch1);
            _hComm.SetParam(4102, ch2);
            _hComm.SetParam(4103, sampling);
            _hComm.SetParam(4104, option);
        }

        private void btActionGraph_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State != ConnectionState.Connected)
                return;
            // check sender
            if ((sender == btGraphStart || sender == btGraphStartAd) && !StateGraph)
            {
                // set monitor type
                MorType = sender == btGraphStart ? MonitorType.Graph : MonitorType.GraphAd;
                // set state
                StateGraph = true;
                // set state
                timer.Start();
            }
            else if (sender == btGraphStop && StateGraph)
            {
                // reset monitor type
                MorType = MonitorType.None;
                // stop event graph monitoring
                StateGraph = !_hComm.SetGraph(4100, 0);
                // stop timer
                timer.Stop();
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            // clear log
            Log.Clear();
            tbLog.Text = string.Empty;
        }

        private void cbGetInfo_CheckedChanged(object sender, EventArgs e)
        {
            _hComm.AutoRequestInfo = cbGetInfo.Checked;
            _hComm.AutoDisconnect = cbGetInfo.Checked;
        }

        private enum MonitorType
        {
            None,
            RealTime,
            RealTimeAd,
            Graph,
            GraphAd,
            State
        }

        /// <summary>
        ///     Tool backup event information class
        /// </summary>
        public class BackupMonitorInfo
        {
            private const int Size = 69;

            private BackupMonitorInfo()
            {
                // add dummy values
                Values.AddRange(new byte[Size]);
            }

            /// <summary>
            ///     Constructor
            /// </summary>
            /// <param name="values">values</param>
            public BackupMonitorInfo(IReadOnlyList<byte> values) : this()
            {
                // check values length
                if (values.Count < Size - 32)
                    return;
                // check size
                for (var i = 0; i < values.Count; i++)
                    // change value
                    Values[i] = values[i];
            }

            private List<byte> Values { get; } = new List<byte>();

            public int Length => Values[0];
            public int Count => (Values[1] << 8) | Values[2];
            public int FastenTime => (Values[3] << 8) | Values[4];
            public int Preset => (Values[5] << 8) | Values[6];
            public float TargetTorque => (float)(((Values[7] << 8) | Values[9]) / 100.0);
            public float ConvertedTorque => (float)(((Values[10] << 8) | Values[11]) / 100.0);
            public int Speed => (Values[12] << 8) | Values[13];
            public int Angle1 => (Values[14] << 8) | Values[15];
            public int Angle2 => (Values[16] << 8) | Values[17];
            public int Angle3 => (Values[18] << 8) | Values[19];
            public int Screws => (Values[20] << 8) | Values[21];
            public int Error => (Values[22] << 8) | Values[23];
            public DirectionTypes Direction => (DirectionTypes)((Values[24] << 8) | Values[25]);
            public StatusTypes Status => (StatusTypes)((Values[26] << 8) | Values[27]);
            public int SnugAngle => (Values[28] << 8) | Values[29];
            public float SeatingTorque => (float)(((Values[30] << 8) | Values[31]) / 100.0);
            public float ClampTorque => (float)(((Values[32] << 8) | Values[33]) / 100.0);
            public float PrevailingTorque => (float)(((Values[34] << 8) | Values[35]) / 100.0);
            public float CompensationTorque => (float)(((Values[36] << 8) | Values[37]) / 100.0);
            public string Barcode => Encoding.ASCII.GetString(Values.Skip(30).Take(32).ToArray()).Replace("\0", "");

            public string ShiftBarcode =>
                Encoding.ASCII.GetString(Values.Skip(38).Take(32).ToArray()).Replace("\0", "");

            /// <summary>
            ///     Set data information
            /// </summary>
            /// <param name="data">packet data</param>
            public void SetInfo(IReadOnlyList<byte> data)
            {
                // check length
                if (data.Count < Size - 32)
                    return;
                // clear values
                for (var i = 0; i < Size; i++)
                    Values[i] = 0;
                // set values
                for (var i = 0; i < data.Count; i++)
                    Values[i] = data[i];
            }
        }
        /// <summary>
        ///     Tool report event information class
        /// </summary>
        public class ReportMonitorItem
        {
            /// <summary>
            ///     Direction types
            /// </summary>
            public enum DirectionTypes
            {
                Fastening,
                Loosening
            }

            public enum GraphTypes
            {
                None,
                Torque,
                Speed,
                Angle,
                TorqueAngle
            }

            public enum SamplingTypes
            {
                [Description(@"2 ms")] Ms2,
                [Description(@"5 ms")] Ms5,
                [Description(@"10 ms")] Ms10
            }

            /// <summary>
            ///     Status types
            /// </summary>
            public enum StatusTypes
            {
                Other,
                FasteningOk,
                LooseningNg,
                DirectionChange,
                PresetChange,
                AlarmReset,
                Error,
                Barcode,
                ScrewCountReset
            }

            private const int Size = 4096;

            private ReportMonitorItem()
            {
                // add dummy values
                Values.AddRange(new byte[Size]);
            }

            /// <summary>
            ///     Constructor
            /// </summary>
            /// <param name="values">values</param>
            public ReportMonitorItem(IReadOnlyList<byte> values) : this()
            {
                // check values length
                if (values.Count >= Size)
                    return;
                // check size
                for (var i = 0; i < values.Count; i++)
                    // change value
                    Values[i] = values[i];
            }

            private List<byte> Values { get; } = new List<byte>();

            public int Length => (Values[0] << 8) | Values[1];
            public GraphTypes Channel1 => (GraphTypes)Values[2];
            public int CountOfChannel1 => (Values[3] << 8) | Values[4];
            public GraphTypes Channel2 => (GraphTypes)Values[5];
            public int CountOfChannel2 => (Values[6] << 8) | Values[7];
            public SamplingTypes Sampling => (SamplingTypes)Values[8];
            public int Revision => Values[9];
            private int Decimal => Values[10];
            public int Index => (Values[11] << 8) | Values[12];
            public int FastenTime => (Values[13] << 8) | Values[14];
            public int Preset => (Values[15] << 8) | Values[16];
            public float TargetTorque => (float)(((Values[17] << 8) | Values[18]) / Math.Pow(10, Decimal));
            public float ConvertedTorque => (float)(((Values[19] << 8) | Values[20]) / Math.Pow(10, Decimal));
            public int Speed => (Values[21] << 8) | Values[22];
            public int Angle1 => (Values[23] << 8) | Values[24];
            public int Angle2 => (Values[25] << 8) | Values[26];
            public int Angle3 => (Values[27] << 8) | Values[28];
            public int Screws => (Values[29] << 8) | Values[30];
            public int Error => (Values[31] << 8) | Values[32];
            public DirectionTypes Direction => (DirectionTypes)((Values[33] << 8) | Values[34]);
            public StatusTypes Status => (StatusTypes)((Values[35] << 8) | Values[36]);
            public int SnugAngle => (Values[37] << 8) | Values[38];
            public float SeatingTorque => (float)(((Values[39] << 8) | Values[40]) / Math.Pow(10, Decimal));
            public float ClampTorque => (float)(((Values[41] << 8) | Values[42]) / Math.Pow(10, Decimal));
            public float PrevailingTorque => (float)(((Values[43] << 8) | Values[44]) / Math.Pow(10, Decimal));
            public float CompensationTorque => (float)(((Values[45] << 8) | Values[46]) / Math.Pow(10, Decimal));
            public string Barcode => Encoding.ASCII.GetString(Values.Skip(64).Take(32).ToArray()).Replace("\0", "");

            public List<int> GraphOfChannel1
            {
                get
                {
                    var list = new List<int>();
                    // check count
                    for (var i = 0; i < CountOfChannel1; i++)
                        // add item
                        list.Add((Values[96 + i * 2] << 8) | Values[97 + i * 2]);
                    return list;
                }
            }

            public List<int> GraphOfChannel2
            {
                get
                {
                    var list = new List<int>();
                    // check count
                    for (var i = 0; i < CountOfChannel2; i++)
                        // add item
                        list.Add((Values[2096 + i * 2] << 8) | Values[2097 + i * 2]);
                    return list;
                }
            }

            /// <summary>
            ///     Set data information
            /// </summary>
            /// <param name="data">packet data</param>
            public void SetInfo(IReadOnlyList<byte> data)
            {
                // check length
                if (data.Count >= Size)
                    return;
                // clear values
                for (var i = 0; i < Size; i++)
                    Values[i] = 0;
                // set values
                for (var i = 0; i < data.Count; i++)
                    Values[i] = data[i];
            }
        }
        
        // debug item class
        private class Item
        {
            public int Preset { get; set; }
            public int Mode { get; set; }
        }
    }
}