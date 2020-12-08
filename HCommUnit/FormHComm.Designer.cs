
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.cbType = new System.Windows.Forms.ComboBox();
            this.gbSerial = new System.Windows.Forms.GroupBox();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.gbEthernet = new System.Windows.Forms.GroupBox();
            this.nmPort = new System.Windows.Forms.NumericUpDown();
            this.gbUsb = new System.Windows.Forms.GroupBox();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.slVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbGetSet = new System.Windows.Forms.GroupBox();
            this.btSetParam = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btGetParam = new System.Windows.Forms.Button();
            this.gbRealTime = new System.Windows.Forms.GroupBox();
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
            this.tbIp = new System.Windows.Forms.TextBox();
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
            this.gbSerial.SuspendLayout();
            this.gbEthernet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).BeginInit();
            this.gbUsb.SuspendLayout();
            this.ssInfo.SuspendLayout();
            this.gbGetSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gbRealTime.SuspendLayout();
            this.gbGraph.SuspendLayout();
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
            lbCh2.Location = new System.Drawing.Point(6, 52);
            lbCh2.Name = "lbCh2";
            lbCh2.Size = new System.Drawing.Size(62, 12);
            lbCh2.TabIndex = 8;
            lbCh2.Text = "Channel 2";
            // 
            // lbSampling
            // 
            lbSampling.AutoSize = true;
            lbSampling.Location = new System.Drawing.Point(6, 104);
            lbSampling.Name = "lbSampling";
            lbSampling.Size = new System.Drawing.Size(58, 12);
            lbSampling.TabIndex = 11;
            lbSampling.Text = "Sampling";
            // 
            // lbGOption
            // 
            lbGOption.AutoSize = true;
            lbGOption.Location = new System.Drawing.Point(6, 78);
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
            // timer
            // 
            this.timer.Interval = 10;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Serial",
            "Ethernet",
            "USB"});
            this.cbType.Location = new System.Drawing.Point(14, 24);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.TabIndex = 0;
            this.cbType.Text = "Serial";
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
            this.btConnect.Location = new System.Drawing.Point(229, 23);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(307, 23);
            this.btConnect.TabIndex = 5;
            this.btConnect.Text = "CONNECT";
            this.btConnect.UseVisualStyleBackColor = true;
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
            this.gbGetSet.Controls.Add(this.numericUpDown2);
            this.gbGetSet.Controls.Add(lbOption);
            this.gbGetSet.Controls.Add(lbAddr);
            this.gbGetSet.Controls.Add(this.numericUpDown1);
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
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(95, 48);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(93, 21);
            this.numericUpDown2.TabIndex = 8;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(95, 20);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(93, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
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
            // 
            // gbRealTime
            // 
            this.gbRealTime.Controls.Add(this.btMorStop);
            this.gbRealTime.Controls.Add(this.btMorStart);
            this.gbRealTime.Location = new System.Drawing.Point(366, 134);
            this.gbRealTime.Name = "gbRealTime";
            this.gbRealTime.Size = new System.Drawing.Size(170, 82);
            this.gbRealTime.TabIndex = 8;
            this.gbRealTime.TabStop = false;
            this.gbRealTime.Text = "Real-Time monitor";
            // 
            // btMorStop
            // 
            this.btMorStop.Location = new System.Drawing.Point(28, 47);
            this.btMorStop.Name = "btMorStop";
            this.btMorStop.Size = new System.Drawing.Size(115, 23);
            this.btMorStop.TabIndex = 2;
            this.btMorStop.Text = "Stop";
            this.btMorStop.UseVisualStyleBackColor = true;
            // 
            // btMorStart
            // 
            this.btMorStart.Location = new System.Drawing.Point(28, 20);
            this.btMorStart.Name = "btMorStart";
            this.btMorStart.Size = new System.Drawing.Size(115, 23);
            this.btMorStart.TabIndex = 1;
            this.btMorStart.Text = "Start";
            this.btMorStart.UseVisualStyleBackColor = true;
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
            this.gbGraph.Size = new System.Drawing.Size(522, 135);
            this.gbGraph.TabIndex = 9;
            this.gbGraph.TabStop = false;
            this.gbGraph.Text = "Graph monitoring";
            // 
            // btGraphSet
            // 
            this.btGraphSet.Location = new System.Drawing.Point(225, 23);
            this.btGraphSet.Name = "btGraphSet";
            this.btGraphSet.Size = new System.Drawing.Size(270, 23);
            this.btGraphSet.TabIndex = 18;
            this.btGraphSet.Text = "Set graph setting";
            this.btGraphSet.UseVisualStyleBackColor = true;
            // 
            // btGraphStop
            // 
            this.btGraphStop.Location = new System.Drawing.Point(225, 98);
            this.btGraphStop.Name = "btGraphStop";
            this.btGraphStop.Size = new System.Drawing.Size(270, 23);
            this.btGraphStop.TabIndex = 17;
            this.btGraphStop.Text = "Stop";
            this.btGraphStop.UseVisualStyleBackColor = true;
            // 
            // btGraphStartAd
            // 
            this.btGraphStartAd.Location = new System.Drawing.Point(380, 69);
            this.btGraphStartAd.Name = "btGraphStartAd";
            this.btGraphStartAd.Size = new System.Drawing.Size(115, 23);
            this.btGraphStartAd.TabIndex = 16;
            this.btGraphStartAd.Text = "Start (AD Only)";
            this.btGraphStartAd.UseVisualStyleBackColor = true;
            // 
            // btGraphStart
            // 
            this.btGraphStart.Location = new System.Drawing.Point(225, 69);
            this.btGraphStart.Name = "btGraphStart";
            this.btGraphStart.Size = new System.Drawing.Size(115, 23);
            this.btGraphStart.TabIndex = 15;
            this.btGraphStart.Text = "Start";
            this.btGraphStart.UseVisualStyleBackColor = true;
            // 
            // cbOption
            // 
            this.cbOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOption.FormattingEnabled = true;
            this.cbOption.Items.AddRange(new object[] {
            "Fastening",
            "Lossening",
            "Both"});
            this.cbOption.Location = new System.Drawing.Point(95, 75);
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
            this.cbSampling.Location = new System.Drawing.Point(95, 101);
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
            this.cbCh2.Location = new System.Drawing.Point(95, 49);
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
            this.tbLog.Size = new System.Drawing.Size(230, 333);
            this.tbLog.TabIndex = 10;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(141, 23);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(82, 23);
            this.btRefresh.TabIndex = 12;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(39, 16);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(125, 21);
            this.tbIp.TabIndex = 8;
            this.tbIp.Text = "192.168.0.100";
            // 
            // FormHComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 386);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gbRealTime.ResumeLayout(false);
            this.gbGraph.ResumeLayout(false);
            this.gbGraph.PerformLayout();
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
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
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
    }
}

