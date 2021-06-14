
namespace HCommThread
{
    partial class FormThread
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
            DevExpress.XtraEditors.LabelControl labelControl1;
            DevExpress.XtraEditors.LabelControl labelControl2;
            DevExpress.XtraEditors.LabelControl labelControl3;
            this.tbThread = new DevExpress.XtraEditors.SpinEdit();
            this.btStart = new DevExpress.XtraEditors.SimpleButton();
            this.cbPorts = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbBaud = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btOpen = new DevExpress.XtraEditors.SimpleButton();
            this.workTimer = new System.Windows.Forms.Timer(this.components);
            this.btWrite = new DevExpress.XtraEditors.SimpleButton();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tbThread.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPorts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbBaud.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(14, 125);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(39, 14);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "Thread";
            // 
            // labelControl2
            // 
            labelControl2.Location = new System.Drawing.Point(14, 15);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(23, 14);
            labelControl2.TabIndex = 4;
            labelControl2.Text = "Port";
            // 
            // labelControl3
            // 
            labelControl3.Location = new System.Drawing.Point(14, 44);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(49, 14);
            labelControl3.TabIndex = 5;
            labelControl3.Text = "Baudrate";
            // 
            // tbThread
            // 
            this.tbThread.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbThread.Location = new System.Drawing.Point(75, 122);
            this.tbThread.Name = "tbThread";
            this.tbThread.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tbThread.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.tbThread.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbThread.Size = new System.Drawing.Size(100, 20);
            this.tbThread.TabIndex = 0;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(12, 148);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(163, 23);
            this.btStart.TabIndex = 2;
            this.btStart.Text = "Start";
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // cbPorts
            // 
            this.cbPorts.Location = new System.Drawing.Point(75, 12);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Properties.Appearance.Options.UseTextOptions = true;
            this.cbPorts.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbPorts.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cbPorts.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbPorts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPorts.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbPorts.Size = new System.Drawing.Size(100, 20);
            this.cbPorts.TabIndex = 3;
            // 
            // cbBaud
            // 
            this.cbBaud.EditValue = "115200";
            this.cbBaud.Location = new System.Drawing.Point(75, 41);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Properties.Appearance.Options.UseTextOptions = true;
            this.cbBaud.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbBaud.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cbBaud.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbBaud.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbBaud.Properties.Items.AddRange(new object[] {
            "115200"});
            this.cbBaud.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbBaud.Size = new System.Drawing.Size(100, 20);
            this.cbBaud.TabIndex = 6;
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(14, 67);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(161, 23);
            this.btOpen.TabIndex = 7;
            this.btOpen.Text = "Open";
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // workTimer
            // 
            this.workTimer.Interval = 500;
            this.workTimer.Tick += new System.EventHandler(this.workTimer_Tick);
            // 
            // btWrite
            // 
            this.btWrite.Location = new System.Drawing.Point(12, 210);
            this.btWrite.Name = "btWrite";
            this.btWrite.Size = new System.Drawing.Size(163, 23);
            this.btWrite.TabIndex = 8;
            this.btWrite.Text = "Write";
            this.btWrite.Click += new System.EventHandler(this.btWrite_Click);
            // 
            // FormThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 368);
            this.Controls.Add(this.btWrite);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.cbBaud);
            this.Controls.Add(labelControl3);
            this.Controls.Add(labelControl2);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.btStart);
            this.Controls.Add(labelControl1);
            this.Controls.Add(this.tbThread);
            this.Name = "FormThread";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormThread_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbThread.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPorts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbBaud.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit tbThread;
        private DevExpress.XtraEditors.SimpleButton btStart;
        private DevExpress.XtraEditors.ComboBoxEdit cbPorts;
        private DevExpress.XtraEditors.ComboBoxEdit cbBaud;
        private DevExpress.XtraEditors.SimpleButton btOpen;
        private System.Windows.Forms.Timer workTimer;
        private DevExpress.XtraEditors.SimpleButton btWrite;
    }
}

