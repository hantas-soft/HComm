using System;
using System.Threading;
using System.Windows.Forms;
using HComm;
using HComm.Common;
using HComm.Device;

namespace HCommThread
{
    public partial class FormThread : Form
    {
        private static HCommInterface _hComm;

        public FormThread()
        {
            InitializeComponent();
        }

        private void FormThread_Load(object sender, EventArgs e)
        {
            // check HComm interface
            _hComm = new HCommInterface { ReceivedMsg = ReceivedMsg, ChangedConnection = ChangedState };
            // get serial port list
            var ports = HcSerial.GetPortNames();
            // check port list
            foreach (var port in ports)
                // add port
                cbPorts.Properties.Items.Add(port);
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            // check connection state
            if (_hComm.State == ConnectionState.Disconnected)
            {
                // check selected item
                if (cbPorts.SelectedIndex < 0)
                    return;
                // get target and option
                var target = $@"{cbPorts.SelectedItem}";
                var option = Convert.ToInt32($@"{cbBaud.SelectedItem}");
                // setup
                _hComm.SetUp(CommType.Serial);
                // change queue count
                _hComm.MaxQueueSize = 300;
                // connect
                if (!_hComm.Connect(target, option))
                    return;
            }
            else
            {
                // close
                _hComm.Close();
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            workTimer.Enabled = !workTimer.Enabled;
        }

        private void btWrite_Click(object sender, EventArgs e)
        {
            // check connected
            if (_hComm.State != ConnectionState.Connected)
                return;

            for (var i = 0; i < tbThread.Value; i++)
            {
                var i1 = i;
                new Thread(delegate()
                {
                    while (true)
                    {
                        // write
                        if (!_hComm.SetParam(292, 127))
                            Console.WriteLine(@"Write failed");
                        // sleep
                        Thread.Sleep(500 + i1 * 25);
                    }
                }).Start();
            }
        }

        private void ChangedState(bool state)
        {
            Invoke(new EventHandler(delegate
            {
                // set button state
                btOpen.Text = state ? @"Close" : @"Open";
            }));
        }

        private static void ReceivedMsg(Command cmd, int addr, int[] values)
        {
            switch (cmd)
            {
                case Command.Read:
                    Console.WriteLine($@"{cmd}: {addr} / {values.Length}");
                    break;
                case Command.Mor:
                    Console.WriteLine($@"{cmd}: {addr} / {values.Length}");
                    break;
                case Command.Write:
                    Console.WriteLine($@"{cmd}: {addr} / {values[1]}");
                    break;
                case Command.Info:
                    break;
                case Command.Graph:
                    Console.WriteLine($@"{cmd}: {addr} / {values.Length}");
                    break;
                case Command.GraphRes:
                    break;
                case Command.GraphAd:
                    break;
                case Command.Error:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cmd), cmd, null);
            }
        }

        private void workTimer_Tick(object sender, EventArgs e)
        {
            // check connected
            if (_hComm.State != ConnectionState.Connected)
                return;
            var rand = new Random(DateTime.Now.Millisecond);
            // request parameter
            if (!_hComm.GetParam((ushort)rand.Next(1, 500), 50))
                // error
                Console.WriteLine(@"Read failed");
        }
    }
}