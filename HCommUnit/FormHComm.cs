using System;
using System.Linq;
using System.Windows.Forms;
using HComm;
using HComm.Common;
using HComm.Device;

namespace HCommUnit
{
    public partial class FormHComm : Form
    {
        private HCommInterface HComm => new HCommInterface();
        public FormHComm()
        {
            InitializeComponent();
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
    }
}