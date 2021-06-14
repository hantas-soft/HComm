using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HComm;
using HComm.Common;
using HComm.Device;

namespace HCommUnit
{
    public partial class FormHComm : Form
    {
        public FormHComm()
        {
            InitializeComponent();
        }
        private static HCommInterface _hComm;
        private const int Timeout = 1000;
        private enum MonitorType
        {
            None,
            RealTime,
            RealTimeAd,
            Graph,
            GraphAd,
            State
        };
        private StringBuilder Log { get; } = new StringBuilder();
        private bool StateMor { get; set; }
        private bool StateGraph { get; set; }
        private MonitorType MorType { get; set; }

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
            tbLog.BeginInvoke(new Action(() =>
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
            // check raw data
            if (cbHexLog.Checked)
                return;
            // check command
            switch (cmd)
            {
                case Command.Read:
                    //AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.Mor:
                    //AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.Write:
                    //AddLog($@"{cmd} / {addr} / {values[1]}");
                    break;
                case Command.Info:
                    //AddLog($@"{cmd} / {addr} / {values.Length}");
                    break;
                case Command.Graph:
                    //AddLog($@"{cmd} / {addr} / {values.Length}");
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
        private void ChangedState(bool state)
        {
            // debug
            Console.WriteLine($@"connection changed: {state}");
            Invoke(new EventHandler(delegate
            {
                // set button state
                btConnect.Text = state ? $@"Disconnect" : $@"Connect";
            }));            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // check state
            if (StateMor)
            {
                // check type
                switch (MorType)
                {
                    case MonitorType.None:
                        break;
                    case MonitorType.RealTime:
                        _hComm.SetRealTime();
                        break;
                    case MonitorType.RealTimeAd:
                        _hComm.GetState(3200);
                        break;
                    case MonitorType.Graph:
                        break;
                    case MonitorType.GraphAd:
                        break;
                    case MonitorType.State:
                        // debug
                        Invoke(new Action(() =>
                                _hComm.GetState(3300, 21)
                        ));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (StateGraph)
            {
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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
            _hComm = new HCommInterface {ReceivedMsg = ReceivedMsg, ReceivedRawMsg = ReceivedRawMsg, ChangedConnection = ChangedState };
            // start application
            AddLog(@"Start application");
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
                //_hComm.AutoRequestInfo = false;
                // connect
                if (!_hComm.Connect(target, option, id))
                    return;
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
                // debug
                Invoke(new Action(() =>
                    // get param
                    _hComm.GetParam(addr, value, true)
                ));
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
                //_hComm.GetState(3300, 13);

                // check state
                if(!StateMor)
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
                _hComm.GetInfo();
        }
        private void btActionMor_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State != ConnectionState.Connected)
                return;
            // check sender
            if ((sender == btMorStart || sender == btMorStartAd) && !StateMor)
            {
                // set monitor type
                MorType = sender == btMorStart ? MonitorType.RealTime : MonitorType.RealTimeAd;
                // set state
                StateMor = true;
                // start timer
                timer.Start();
            }
            else if (sender == btMorStop && StateMor)
            {
                // reset monitor type
                MorType = MonitorType.None;
                // reset state
                StateMor = !_hComm.SetRealTime(4002, 0);
                // stop event real-time monitoring
                timer.Stop();
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
            var ch1 = (ushort) cbCh1.SelectedIndex;
            var ch2 = (ushort) cbCh2.SelectedIndex;
            var sampling = (ushort) cbSampling.SelectedIndex;
            var option = (ushort) cbOption.SelectedIndex;
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
    }
}