
namespace HCommUnit
{
    partial class FormHComm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lbType;
            System.Windows.Forms.Label lbPort;
            System.Windows.Forms.Label lbBaudrate;
            System.Windows.Forms.Label lbIp;
            System.Windows.Forms.Label lbEPort;
            System.Windows.Forms.Label lbDevice;
            System.Windows.Forms.Label lbAddr;
            System.Windows.Forms.Label lbOption;
            System.Windows.Forms.Label lbCh1;
            System.Windows.Forms.Label lbCh2;
            System.Windows.Forms.Label lbSampling;
            System.Windows.Forms.Label lbGOption;
            System.Windows.Forms.Label lbLog;
            System.Windows.Forms.Label lbId;
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.cbType = new System.Windows.Forms.ComboBox();
            this.gbSerial = new System.Windows.Forms.GroupBox();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.gbEthernet = new System.Windows.Forms.GroupBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.nmPort = new System.Windows.Forms.NumericUpDown();
            this.gbUsb = new System.Windows.Forms.GroupBox();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.slVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbGetSet = new System.Windows.Forms.GroupBox();
            this.btSetParam = new System.Windows.Forms.Button();
            this.nmValue = new System.Windows.Forms.NumericUpDown();
            this.nmAddr = new System.Windows.Forms.NumericUpDown();
            this.btGetParam = new System.Windows.Forms.Button();
            this.gbRealTime = new System.Windows.Forms.GroupBox();
            this.btMorStartAd = new System.Windows.Forms.Button();
            this.btMorStop = new System.Windows.Forms.Button();
            this.btMorStart = new System.Windows.Forms.Button();
            this.gbGraph = new System.Windows.Forms.GroupBox();
            this.btGraphSet = new System.Windows.Forms.Button();
            this.btGraphStop = new System.Windows.Forms.Button();
            this.btGraphStartAd = new System.Windows.Forms.Button();
            this.btGraphStart = new System.Windows.Forms.Button();
            this.cbOption = new System.Windows.Forms.ComboBox();
            this.cbSampling = new System.Windows.Forms.ComboBox();
            this.cbCh2 = new System.Windows.Forms.ComboBox();
            this.cbCh1 = new System.Windows.Forms.ComboBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btRefresh = new System.Windows.Forms.Button();
            this.nmID = new System.Windows.Forms.NumericUpDown();
            this.cbHexLog = new System.Windows.Forms.CheckBox();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.btGetInfo = new System.Windows.Forms.Button();
            this.btGetStatus = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            lbType = new System.Windows.Forms.Label();
            lbPort = new System.Windows.Forms.Label();
            lbBaudrate = new System.Windows.Forms.Label();
            lbIp = new System.Windows.Forms.Label();
            lbEPort = new System.Windows.Forms.Label();
            lbDevice = new System.Windows.Forms.Label();
            lbAddr = new System.Windows.Forms.Label();
            lbOption = new System.Windows.Forms.Label();
            lbCh1 = new System.Windows.Forms.Label();
            lbCh2 = new System.Windows.Forms.Label();
            lbSampling = new System.Windows.Forms.Label();
            lbGOption = new System.Windows.Forms.Label();
            lbLog = new System.Windows.Forms.Label();
            lbId = new System.Windows.Forms.Label();
            this.gbSerial.SuspendLayout();
            this.gbEthernet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).BeginInit();
            this.gbUsb.SuspendLayout();
            this.ssInfo.SuspendLayout();
            this.gbGetSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAddr)).BeginInit();
            this.gbRealTime.SuspendLayout();
            this.gbGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmID)).BeginInit();
            this.gbStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbType
            // 
            lbType.AutoSize = true;
            lbType.Location = new System.Drawing.Point(12, 9);
            lbType.Name = "lbType";
            lbType.Size = new System.Drawing.Size(34, 12);
            lbType.TabIndex = 1;
            lbType.Text = "Type";
            // 
            // lbPort
            // 
            lbPort.AutoSize = true;
            lbPort.Location = new System.Drawing.Point(6, 23);
            lbPort.Name = "lbPort";
            lbPort.Size = new System.Drawing.Size(27, 12);
            lbPort.TabIndex = 3;
            lbPort.Text = "Port";
            // 
            // lbBaudrate
            // 
            lbBaudrate.AutoSize = true;
            lbBaudrate.Location = new System.Drawing.Point(6, 49);
            lbBaudrate.Name = "lbBaudrate";
            lbBaudrate.Size = new System.Drawing.Size(55, 12);
            lbBaudrate.TabIndex = 5;
            lbBaudrate.Text = "Baudrate";
            // 
            // lbIp
            // 
            lbIp.AutoSize = true;
            lbIp.Location = new System.Drawing.Point(6, 23);
            lbIp.Name = "lbIp";
            lbIp.Size = new System.Drawing.Size(16, 12);
            lbIp.TabIndex = 4;
            lbIp.Text = "IP";
            // 
            // lbEPort
            // 
            lbEPort.AutoSize = true;
            lbEPort.Location = new System.Drawing.Point(6, 49);
            lbEPort.Name = "lbEPort";
            lbEPort.Size = new System.Drawing.Size(27, 12);
            lbEPort.TabIndex = 6;
            lbEPort.Text = "Port";
            // 
            // lbDevice
            // 
            lbDevice.AutoSize = true;
            lbDevice.Location = new System.Drawing.Point(6, 23);
            lbDevice.Name = "lbDevice";
            lbDevice.Size = new System.Drawing.Size(43, 12);
            lbDevice.TabIndex = 4;
            lbDevice.Text = "Device";
            // 
            // lbAddr
            // 
            lbAddr.AutoSize = true;
            lbAddr.Location = new System.Drawing.Point(6, 25);
            lbAddr.Name = "lbAddr";
            lbAddr.Size = new System.Drawing.Size(52, 12);
            lbAddr.TabIndex = 6;
            lbAddr.Text = "Address";
            // 
            // lbOption
            // 
            lbOption.AutoSize = true;
            lbOption.Location = new System.Drawing.Point(6, 52);
            lbOption.Name = "lbOption";
            lbOption.Size = new System.Drawing.Size(80, 12);
            lbOption.TabIndex = 7;
            lbOption.Text = "Value(Count)";
            // 
            // lbCh1
            // 
            lbCh1.AutoSize = true;
            lbCh1.Location = new System.Drawing.Point(6, 26);
            lbCh1.Name = "lbCh1";
            lbCh1.Size = new System.Drawing.Size(62, 12);
            lbCh1.TabIndex = 7;
            lbCh1.Text = "Channel 1";
            // 
            // lbCh2
            // 
            lbCh2.AutoSize = true;
            lbCh2.Location = new System.Drawing.Point(6, 54);
            lbCh2.Name = "lbCh2";
            lbCh2.Size = new System.Drawing.Size(62, 12);
            lbCh2.TabIndex = 8;
            lbCh2.Text = "Channel 2";
            // 
            // lbSampling
            // 
            lbSampling.AutoSize = true;
            lbSampling.Location = new System.Drawing.Point(6, 106);
            lbSampling.Name = "lbSampling";
            lbSampling.Size = new System.Drawing.Size(58, 12);
            lbSampling.TabIndex = 11;
            lbSampling.Text = "Sampling";
            // 
            // lbGOption
            // 
            lbGOption.AutoSize = true;
            lbGOption.Location = new System.Drawing.Point(6, 80);
            lbGOption.Name = "lbGOption";
            lbGOption.Size = new System.Drawing.Size(41, 12);
            lbGOption.TabIndex = 13;
            lbGOption.Text = "Option";
            // 
            // lbLog
            // 
            lbLog.AutoSize = true;
            lbLog.Location = new System.Drawing.Point(542, 9);
            lbLog.Name = "lbLog";
            lbLog.Size = new System.Drawing.Size(26, 12);
            lbLog.TabIndex = 11;
            lbLog.Text = "Log";
            // 
            // lbId
            // 
            lbId.AutoSize = true;
            lbId.Location = new System.Drawing.Point(141, 9);
            lbId.Name = "lbId";
            lbId.Size = new System.Drawing.Size(16, 12);
            lbId.TabIndex = 14;
            lbId.Text = "ID";
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Serial",
            "Ethernet",
            "USB"});
            this.cbType.Location = new System.Drawing.Point(14, 24);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.TabIndex = 0;
            // 
            // gbSerial
            // 
            this.gbSerial.Controls.Add(lbBaudrate);
            this.gbSerial.Controls.Add(this.cbBaudrate);
            this.gbSerial.Controls.Add(lbPort);
            this.gbSerial.Controls.Add(this.cbPort);
            this.gbSerial.Location = new System.Drawing.Point(14, 50);
            this.gbSerial.Name = "gbSerial";
            this.gbSerial.Size = new System.Drawing.Size(170, 78);
            this.gbSerial.TabIndex = 2;
            this.gbSerial.TabStop = false;
            this.gbSerial.Text = "Serial";
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "19200",
            "9600"});
            this.cbBaudrate.Location = new System.Drawing.Point(67, 46);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.Size = new System.Drawing.Size(93, 20);
            this.cbBaudrate.TabIndex = 4;
            // 
            // cbPort
            // 
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(67, 20);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(93, 20);
            this.cbPort.TabIndex = 0;
            // 
            // gbEthernet
            // 
            this.gbEthernet.Controls.Add(this.tbIp);
            this.gbEthernet.Controls.Add(this.nmPort);
            this.gbEthernet.Controls.Add(lbEPort);
            this.gbEthernet.Controls.Add(lbIp);
            this.gbEthernet.Location = new System.Drawing.Point(190, 50);
            this.gbEthernet.Name = "gbEthernet";
            this.gbEthernet.Size = new System.Drawing.Size(170, 78);
            this.gbEthernet.TabIndex = 3;
            this.gbEthernet.TabStop = false;
            this.gbEthernet.Text = "Ethernet";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(39, 16);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(125, 21);
            this.tbIp.TabIndex = 8;
            this.tbIp.Text = "192.168.0.100";
            // 
            // nmPort
            // 
            this.nmPort.Location = new System.Drawing.Point(39, 43);
            this.nmPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmPort.Name = "nmPort";
            this.nmPort.Size = new System.Drawing.Size(125, 21);
            this.nmPort.TabIndex = 7;
            this.nmPort.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // gbUsb
            // 
            this.gbUsb.Controls.Add(this.cbDevice);
            this.gbUsb.Controls.Add(lbDevice);
            this.gbUsb.Location = new System.Drawing.Point(366, 50);
            this.gbUsb.Name = "gbUsb";
            this.gbUsb.Size = new System.Drawing.Size(170, 77);
            this.gbUsb.TabIndex = 4;
            this.gbUsb.TabStop = false;
            this.gbUsb.Text = "USB";
            // 
            // cbDevice
            // 
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(6, 42);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(158, 20);
            this.cbDevice.TabIndex = 5;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(366, 21);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(164, 23);
            this.btConnect.TabIndex = 5;
            this.btConnect.Text = "CONNECT";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // ssInfo
            // 
            this.ssInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slVersion});
            this.ssInfo.Location = new System.Drawing.Point(0, 364);
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Size = new System.Drawing.Size(784, 22);
            this.ssInfo.TabIndex = 6;
            // 
            // slVersion
            // 
            this.slVersion.Name = "slVersion";
            this.slVersion.Size = new System.Drawing.Size(47, 17);
            this.slVersion.Text = "v1.00.0";
            // 
            // gbGetSet
            // 
            this.gbGetSet.Controls.Add(this.btSetParam);
            this.gbGetSet.Controls.Add(this.nmValue);
            this.gbGetSet.Controls.Add(lbOption);
            this.gbGetSet.Controls.Add(lbAddr);
            this.gbGetSet.Controls.Add(this.nmAddr);
            this.gbGetSet.Controls.Add(this.btGetParam);
            this.gbGetSet.Location = new System.Drawing.Point(14, 134);
            this.gbGetSet.Name = "gbGetSet";
            this.gbGetSet.Size = new System.Drawing.Size(346, 82);
            this.gbGetSet.TabIndex = 7;
            this.gbGetSet.TabStop = false;
            this.gbGetSet.Text = "Get / Set parameter";
            // 
            // btSetParam
            // 
            this.btSetParam.Location = new System.Drawing.Point(225, 47);
            this.btSetParam.Name = "btSetParam";
            this.btSetParam.Size = new System.Drawing.Size(115, 23);
            this.btSetParam.TabIndex = 9;
            this.btSetParam.Text = "Set Parameter";
            this.btSetParam.UseVisualStyleBackColor = true;
            this.btSetParam.Click += new System.EventHandler(this.btActionParam_Click);
            // 
            // nmValue
            // 
            this.nmValue.Location = new System.Drawing.Point(95, 48);
            this.nmValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmValue.Name = "nmValue";
            this.nmValue.Size = new System.Drawing.Size(93, 21);
            this.nmValue.TabIndex = 8;
            // 
            // nmAddr
            // 
            this.nmAddr.Location = new System.Drawing.Point(95, 20);
            this.nmAddr.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmAddr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmAddr.Name = "nmAddr";
            this.nmAddr.Size = new System.Drawing.Size(93, 21);
            this.nmAddr.TabIndex = 1;
            this.nmAddr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btGetParam
            // 
            this.btGetParam.Location = new System.Drawing.Point(225, 20);
            this.btGetParam.Name = "btGetParam";
            this.btGetParam.Size = new System.Drawing.Size(115, 23);
            this.btGetParam.TabIndex = 0;
            this.btGetParam.Text = "Get Parameter";
            this.btGetParam.UseVisualStyleBackColor = true;
            this.btGetParam.Click += new System.EventHandler(this.btActionParam_Click);
            // 
            // gbRealTime
            // 
            this.gbRealTime.Controls.Add(this.btMorStartAd);
            this.gbRealTime.Controls.Add(this.btMorStop);
            this.gbRealTime.Controls.Add(this.btMorStart);
            this.gbRealTime.Location = new System.Drawing.Point(366, 222);
            this.gbRealTime.Name = "gbRealTime";
            this.gbRealTime.Size = new System.Drawing.Size(170, 135);
            this.gbRealTime.TabIndex = 8;
            this.gbRealTime.TabStop = false;
            this.gbRealTime.Text = "Real-Time monitor";
            // 
            // btMorStartAd
            // 
            this.btMorStartAd.Location = new System.Drawing.Point(28, 51);
            this.btMorStartAd.Name = "btMorStartAd";
            this.btMorStartAd.Size = new System.Drawing.Size(115, 23);
            this.btMorStartAd.TabIndex = 3;
            this.btMorStartAd.Text = "Start (AD only)";
            this.btMorStartAd.UseVisualStyleBackColor = true;
            this.btMorStartAd.Click += new System.EventHandler(this.btActionMor_Click);
            // 
            // btMorStop
            // 
            this.btMorStop.Location = new System.Drawing.Point(28, 77);
            this.btMorStop.Name = "btMorStop";
            this.btMorStop.Size = new System.Drawing.Size(115, 23);
            this.btMorStop.TabIndex = 2;
            this.btMorStop.Text = "Stop";
            this.btMorStop.UseVisualStyleBackColor = true;
            this.btMorStop.Click += new System.EventHandler(this.btActionMor_Click);
            // 
            // btMorStart
            // 
            this.btMorStart.Location = new System.Drawing.Point(28, 24);
            this.btMorStart.Name = "btMorStart";
            this.btMorStart.Size = new System.Drawing.Size(115, 23);
            this.btMorStart.TabIndex = 1;
            this.btMorStart.Text = "Start";
            this.btMorStart.UseVisualStyleBackColor = true;
            this.btMorStart.Click += new System.EventHandler(this.btActionMor_Click);
            // 
            // gbGraph
            // 
            this.gbGraph.Controls.Add(this.btGraphSet);
            this.gbGraph.Controls.Add(this.btGraphStop);
            this.gbGraph.Controls.Add(this.btGraphStartAd);
            this.gbGraph.Controls.Add(this.btGraphStart);
            this.gbGraph.Controls.Add(this.cbOption);
            this.gbGraph.Controls.Add(lbGOption);
            this.gbGraph.Controls.Add(this.cbSampling);
            this.gbGraph.Controls.Add(lbSampling);
            this.gbGraph.Controls.Add(this.cbCh2);
            this.gbGraph.Controls.Add(this.cbCh1);
            this.gbGraph.Controls.Add(lbCh2);
            this.gbGraph.Controls.Add(lbCh1);
            this.gbGraph.Location = new System.Drawing.Point(14, 222);
            this.gbGraph.Name = "gbGraph";
            this.gbGraph.Size = new System.Drawing.Size(346, 135);
            this.gbGraph.TabIndex = 9;
            this.gbGraph.TabStop = false;
            this.gbGraph.Text = "Graph monitoring";
            // 
            // btGraphSet
            // 
            this.btGraphSet.Location = new System.Drawing.Point(225, 23);
            this.btGraphSet.Name = "btGraphSet";
            this.btGraphSet.Size = new System.Drawing.Size(115, 23);
            this.btGraphSet.TabIndex = 18;
            this.btGraphSet.Text = "Set graph setting";
            this.btGraphSet.UseVisualStyleBackColor = true;
            this.btGraphSet.Click += new System.EventHandler(this.btGraphSet_Click);
            // 
            // btGraphStop
            // 
            this.btGraphStop.Location = new System.Drawing.Point(225, 101);
            this.btGraphStop.Name = "btGraphStop";
            this.btGraphStop.Size = new System.Drawing.Size(115, 23);
            this.btGraphStop.TabIndex = 17;
            this.btGraphStop.Text = "Stop";
            this.btGraphStop.UseVisualStyleBackColor = true;
            this.btGraphStop.Click += new System.EventHandler(this.btActionGraph_Click);
            // 
            // btGraphStartAd
            // 
            this.btGraphStartAd.Location = new System.Drawing.Point(225, 75);
            this.btGraphStartAd.Name = "btGraphStartAd";
            this.btGraphStartAd.Size = new System.Drawing.Size(115, 23);
            this.btGraphStartAd.TabIndex = 16;
            this.btGraphStartAd.Text = "Start (AD Only)";
            this.btGraphStartAd.UseVisualStyleBackColor = true;
            this.btGraphStartAd.Click += new System.EventHandler(this.btActionGraph_Click);
            // 
            // btGraphStart
            // 
            this.btGraphStart.Location = new System.Drawing.Point(225, 49);
            this.btGraphStart.Name = "btGraphStart";
            this.btGraphStart.Size = new System.Drawing.Size(115, 23);
            this.btGraphStart.TabIndex = 15;
            this.btGraphStart.Text = "Start";
            this.btGraphStart.UseVisualStyleBackColor = true;
            this.btGraphStart.Click += new System.EventHandler(this.btActionGraph_Click);
            // 
            // cbOption
            // 
            this.cbOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOption.FormattingEnabled = true;
            this.cbOption.Items.AddRange(new object[] {
            "Fastening",
            "Lossening",
            "Both"});
            this.cbOption.Location = new System.Drawing.Point(95, 77);
            this.cbOption.Name = "cbOption";
            this.cbOption.Size = new System.Drawing.Size(93, 20);
            this.cbOption.TabIndex = 14;
            // 
            // cbSampling
            // 
            this.cbSampling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSampling.FormattingEnabled = true;
            this.cbSampling.Items.AddRange(new object[] {
            "5 ms",
            "10 ms",
            "15 ms",
            "30 ms"});
            this.cbSampling.Location = new System.Drawing.Point(95, 104);
            this.cbSampling.Name = "cbSampling";
            this.cbSampling.Size = new System.Drawing.Size(93, 20);
            this.cbSampling.TabIndex = 12;
            // 
            // cbCh2
            // 
            this.cbCh2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCh2.FormattingEnabled = true;
            this.cbCh2.Items.AddRange(new object[] {
            "None",
            "Torque",
            "Current",
            "Speed",
            "Angle",
            "Speed command",
            "Current command",
            "Snug angle",
            "Torque/Angle"});
            this.cbCh2.Location = new System.Drawing.Point(95, 51);
            this.cbCh2.Name = "cbCh2";
            this.cbCh2.Size = new System.Drawing.Size(93, 20);
            this.cbCh2.TabIndex = 10;
            // 
            // cbCh1
            // 
            this.cbCh1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCh1.FormattingEnabled = true;
            this.cbCh1.Items.AddRange(new object[] {
            "None",
            "Torque",
            "Current",
            "Speed",
            "Angle",
            "Speed command",
            "Current command",
            "Snug angle",
            "Torque/Angle"});
            this.cbCh1.Location = new System.Drawing.Point(95, 23);
            this.cbCh1.Name = "cbCh1";
            this.cbCh1.Size = new System.Drawing.Size(93, 20);
            this.cbCh1.TabIndex = 9;
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.BackColor = System.Drawing.Color.White;
            this.tbLog.Location = new System.Drawing.Point(542, 24);
            this.tbLog.MaxLength = 65535;
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(230, 304);
            this.tbLog.TabIndex = 10;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(278, 21);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(82, 23);
            this.btRefresh.TabIndex = 12;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // nmID
            // 
            this.nmID.Location = new System.Drawing.Point(141, 23);
            this.nmID.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nmID.Name = "nmID";
            this.nmID.Size = new System.Drawing.Size(120, 21);
            this.nmID.TabIndex = 13;
            this.nmID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbHexLog
            // 
            this.cbHexLog.AutoSize = true;
            this.cbHexLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbHexLog.Location = new System.Drawing.Point(694, 8);
            this.cbHexLog.Name = "cbHexLog";
            this.cbHexLog.Size = new System.Drawing.Size(78, 16);
            this.cbHexLog.TabIndex = 15;
            this.cbHexLog.Text = "RAW data";
            this.cbHexLog.UseVisualStyleBackColor = true;
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.btGetInfo);
            this.gbStatus.Controls.Add(this.btGetStatus);
            this.gbStatus.Location = new System.Drawing.Point(366, 134);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(170, 82);
            this.gbStatus.TabIndex = 16;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "Status / Information";
            // 
            // btGetInfo
            // 
            this.btGetInfo.Location = new System.Drawing.Point(28, 48);
            this.btGetInfo.Name = "btGetInfo";
            this.btGetInfo.Size = new System.Drawing.Size(115, 23);
            this.btGetInfo.TabIndex = 2;
            this.btGetInfo.Text = "Get Info";
            this.btGetInfo.UseVisualStyleBackColor = true;
            this.btGetInfo.Click += new System.EventHandler(this.btActionStatus_Click);
            // 
            // btGetStatus
            // 
            this.btGetStatus.Location = new System.Drawing.Point(28, 20);
            this.btGetStatus.Name = "btGetStatus";
            this.btGetStatus.Size = new System.Drawing.Size(115, 23);
            this.btGetStatus.TabIndex = 1;
            this.btGetStatus.Text = "Get Status";
            this.btGetStatus.UseVisualStyleBackColor = true;
            this.btGetStatus.Click += new System.EventHandler(this.btActionStatus_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.Location = new System.Drawing.Point(542, 334);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(230, 23);
            this.btClear.TabIndex = 17;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // FormHComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 386);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.gbStatus);
            this.Controls.Add(this.cbHexLog);
            this.Controls.Add(lbId);
            this.Controls.Add(this.nmID);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(lbLog);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.gbGraph);
            this.Controls.Add(this.gbRealTime);
            this.Controls.Add(this.gbGetSet);
            this.Controls.Add(this.ssInfo);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.gbUsb);
            this.Controls.Add(this.gbEthernet);
            this.Controls.Add(this.gbSerial);
            this.Controls.Add(lbType);
            this.Controls.Add(this.cbType);
            this.Name = "FormHComm";
            this.Text = "Hantas tool device communication example software";
            this.Load += new System.EventHandler(this.FormHComm_Load);
            this.gbSerial.ResumeLayout(false);
            this.gbSerial.PerformLayout();
            this.gbEthernet.ResumeLayout(false);
            this.gbEthernet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).EndInit();
            this.gbUsb.ResumeLayout(false);
            this.gbUsb.PerformLayout();
            this.ssInfo.ResumeLayout(false);
            this.ssInfo.PerformLayout();
            this.gbGetSet.ResumeLayout(false);
            this.gbGetSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAddr)).EndInit();
            this.gbRealTime.ResumeLayout(false);
            this.gbGraph.ResumeLayout(false);
            this.gbGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmID)).EndInit();
            this.gbStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.GroupBox gbSerial;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.GroupBox gbEthernet;
        private System.Windows.Forms.NumericUpDown nmPort;
        private System.Windows.Forms.GroupBox gbUsb;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.StatusStrip ssInfo;
        private System.Windows.Forms.ToolStripStatusLabel slVersion;
        private System.Windows.Forms.GroupBox gbGetSet;
        private System.Windows.Forms.Button btGetParam;
        private System.Windows.Forms.NumericUpDown nmValue;
        private System.Windows.Forms.NumericUpDown nmAddr;
        private System.Windows.Forms.Button btSetParam;
        private System.Windows.Forms.GroupBox gbRealTime;
        private System.Windows.Forms.Button btMorStop;
        private System.Windows.Forms.GroupBox gbGraph;
        private System.Windows.Forms.ComboBox cbCh2;
        private System.Windows.Forms.ComboBox cbCh1;
        private System.Windows.Forms.ComboBox cbOption;
        private System.Windows.Forms.ComboBox cbSampling;
        private System.Windows.Forms.Button btGraphStart;
        private System.Windows.Forms.Button btGraphStop;
        private System.Windows.Forms.Button btGraphStartAd;
        private System.Windows.Forms.Button btGraphSet;
        private System.Windows.Forms.Button btMorStart;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.NumericUpDown nmID;
        private System.Windows.Forms.CheckBox cbHexLog;
        private System.Windows.Forms.Button btMorStartAd;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Button btGetInfo;
        private System.Windows.Forms.Button btGetStatus;
        private System.Windows.Forms.Button btClear;
    }
}

