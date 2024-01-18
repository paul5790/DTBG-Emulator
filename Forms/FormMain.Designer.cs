namespace DTBGEmulator
{
    partial class MainForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.settingBtn = new System.Windows.Forms.Button();
            this.addFolderBtn = new System.Windows.Forms.Button();
            this.addFileBtn = new System.Windows.Forms.Button();
            this.dataInfoText = new System.Windows.Forms.Label();
            this.dataInfoTextbox = new System.Windows.Forms.TextBox();
            this.dataInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.currTimeText = new System.Windows.Forms.Label();
            this.timeController = new DTBGEmulator.UserControls.TimeController();
            this.stopBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.runBtn = new System.Windows.Forms.Button();
            this.speedBindText = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.playSpeed = new System.Windows.Forms.Button();
            this.startTimeText = new System.Windows.Forms.TextBox();
            this.speedComboBox = new System.Windows.Forms.ComboBox();
            this.endTimeText = new System.Windows.Forms.TextBox();
            this.timeSetBtn = new System.Windows.Forms.Button();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.timer_progress = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.DimGray;
            this.panelTop.Controls.Add(this.pictureBox_Close);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(560, 30);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_Close.Image = global::DTBGEmulator.Properties.Resources.close;
            this.pictureBox_Close.Location = new System.Drawing.Point(525, 0);
            this.pictureBox_Close.Name = "pictureBox_Close";
            this.pictureBox_Close.Size = new System.Drawing.Size(35, 30);
            this.pictureBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Close.TabIndex = 5;
            this.pictureBox_Close.TabStop = false;
            this.pictureBox_Close.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "DTBG Emulator";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.settingBtn);
            this.panel2.Controls.Add(this.addFolderBtn);
            this.panel2.Controls.Add(this.addFileBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 50);
            this.panel2.TabIndex = 1;
            // 
            // settingBtn
            // 
            this.settingBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.cog;
            this.settingBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingBtn.FlatAppearance.BorderSize = 0;
            this.settingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingBtn.Location = new System.Drawing.Point(510, 5);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(40, 40);
            this.settingBtn.TabIndex = 12;
            this.settingBtn.UseVisualStyleBackColor = true;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // addFolderBtn
            // 
            this.addFolderBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.folderplus_outline;
            this.addFolderBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addFolderBtn.FlatAppearance.BorderSize = 0;
            this.addFolderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFolderBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addFolderBtn.Location = new System.Drawing.Point(62, 5);
            this.addFolderBtn.Name = "addFolderBtn";
            this.addFolderBtn.Size = new System.Drawing.Size(40, 40);
            this.addFolderBtn.TabIndex = 11;
            this.addFolderBtn.UseVisualStyleBackColor = true;
            this.addFolderBtn.Click += new System.EventHandler(this.addFolderBtn_Click);
            // 
            // addFileBtn
            // 
            this.addFileBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.fileplus_outline;
            this.addFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addFileBtn.FlatAppearance.BorderSize = 0;
            this.addFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFileBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addFileBtn.Location = new System.Drawing.Point(12, 5);
            this.addFileBtn.Name = "addFileBtn";
            this.addFileBtn.Size = new System.Drawing.Size(40, 40);
            this.addFileBtn.TabIndex = 10;
            this.addFileBtn.UseVisualStyleBackColor = true;
            this.addFileBtn.Click += new System.EventHandler(this.addFileBtn_Click);
            // 
            // dataInfoText
            // 
            this.dataInfoText.AutoSize = true;
            this.dataInfoText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataInfoText.Location = new System.Drawing.Point(15, 20);
            this.dataInfoText.Name = "dataInfoText";
            this.dataInfoText.Size = new System.Drawing.Size(69, 12);
            this.dataInfoText.TabIndex = 2;
            this.dataInfoText.Text = "데이터 정보";
            // 
            // dataInfoTextbox
            // 
            this.dataInfoTextbox.Location = new System.Drawing.Point(15, 40);
            this.dataInfoTextbox.Multiline = true;
            this.dataInfoTextbox.Name = "dataInfoTextbox";
            this.dataInfoTextbox.Size = new System.Drawing.Size(530, 120);
            this.dataInfoTextbox.TabIndex = 3;
            this.dataInfoTextbox.Text = "데이터 선택 대기...";
            // 
            // dataInfo
            // 
            this.dataInfo.AutoSize = true;
            this.dataInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataInfo.Location = new System.Drawing.Point(15, 10);
            this.dataInfo.Name = "dataInfo";
            this.dataInfo.Size = new System.Drawing.Size(69, 12);
            this.dataInfo.TabIndex = 4;
            this.dataInfo.Text = "데이터 정보";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataInfoTextbox);
            this.panel1.Controls.Add(this.dataInfoText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 170);
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.dataInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 250);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(560, 230);
            this.panel3.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.currTimeText);
            this.panel4.Controls.Add(this.timeController);
            this.panel4.Controls.Add(this.stopBtn);
            this.panel4.Controls.Add(this.pauseBtn);
            this.panel4.Controls.Add(this.runBtn);
            this.panel4.Controls.Add(this.speedBindText);
            this.panel4.Controls.Add(this.labelSpeed);
            this.panel4.Controls.Add(this.playSpeed);
            this.panel4.Controls.Add(this.startTimeText);
            this.panel4.Controls.Add(this.speedComboBox);
            this.panel4.Controls.Add(this.endTimeText);
            this.panel4.Controls.Add(this.timeSetBtn);
            this.panel4.Location = new System.Drawing.Point(15, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(530, 190);
            this.panel4.TabIndex = 5;
            // 
            // currTimeText
            // 
            this.currTimeText.AutoSize = true;
            this.currTimeText.ForeColor = System.Drawing.Color.Black;
            this.currTimeText.Location = new System.Drawing.Point(452, 143);
            this.currTimeText.Name = "currTimeText";
            this.currTimeText.Size = new System.Drawing.Size(49, 12);
            this.currTimeText.TabIndex = 19;
            this.currTimeText.Text = "88:88:88";
            // 
            // timeController
            // 
            this.timeController.BackColor = System.Drawing.Color.White;
            this.timeController.CurrTime = "0";
            this.timeController.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeController.Location = new System.Drawing.Point(3, 13);
            this.timeController.Name = "timeController";
            this.timeController.Size = new System.Drawing.Size(505, 44);
            this.timeController.TabIndex = 18;
            // 
            // stopBtn
            // 
            this.stopBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.stop;
            this.stopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.stopBtn.FlatAppearance.BorderSize = 0;
            this.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopBtn.Location = new System.Drawing.Point(468, 78);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(40, 40);
            this.stopBtn.TabIndex = 17;
            this.stopBtn.UseVisualStyleBackColor = true;
            // 
            // pauseBtn
            // 
            this.pauseBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.pause;
            this.pauseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pauseBtn.FlatAppearance.BorderSize = 0;
            this.pauseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseBtn.Location = new System.Drawing.Point(422, 78);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(40, 40);
            this.pauseBtn.TabIndex = 16;
            this.pauseBtn.UseVisualStyleBackColor = true;
            // 
            // runBtn
            // 
            this.runBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.play;
            this.runBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.runBtn.FlatAppearance.BorderSize = 0;
            this.runBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBtn.Location = new System.Drawing.Point(376, 78);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(40, 40);
            this.runBtn.TabIndex = 15;
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // speedBindText
            // 
            this.speedBindText.AutoSize = true;
            this.speedBindText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.speedBindText.Location = new System.Drawing.Point(260, 143);
            this.speedBindText.Name = "speedBindText";
            this.speedBindText.Size = new System.Drawing.Size(66, 12);
            this.speedBindText.TabIndex = 14;
            this.speedBindText.Text = "속도 (1.0x)";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelSpeed.Location = new System.Drawing.Point(182, 143);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(65, 12);
            this.labelSpeed.TabIndex = 13;
            this.labelSpeed.Text = "전송 속도 :";
            // 
            // playSpeed
            // 
            this.playSpeed.BackgroundImage = global::DTBGEmulator.Properties.Resources.play_speed;
            this.playSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.playSpeed.FlatAppearance.BorderSize = 0;
            this.playSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playSpeed.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playSpeed.Location = new System.Drawing.Point(148, 135);
            this.playSpeed.Name = "playSpeed";
            this.playSpeed.Size = new System.Drawing.Size(28, 25);
            this.playSpeed.TabIndex = 12;
            this.playSpeed.UseVisualStyleBackColor = true;
            // 
            // startTimeText
            // 
            this.startTimeText.Location = new System.Drawing.Point(27, 87);
            this.startTimeText.Name = "startTimeText";
            this.startTimeText.Size = new System.Drawing.Size(100, 21);
            this.startTimeText.TabIndex = 3;
            // 
            // speedComboBox
            // 
            this.speedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedComboBox.FormattingEnabled = true;
            this.speedComboBox.Items.AddRange(new object[] {
            "속도 (1.0x)",
            "속도 (2.0x)",
            "속도 (4.0x)",
            "속도 (8.0x)",
            "속도 (16.0x)",
            "속도 (32.0x)"});
            this.speedComboBox.Location = new System.Drawing.Point(27, 138);
            this.speedComboBox.Name = "speedComboBox";
            this.speedComboBox.Size = new System.Drawing.Size(101, 20);
            this.speedComboBox.TabIndex = 2;
            this.speedComboBox.SelectedIndexChanged += new System.EventHandler(this.speedComboBox_SelectedIndexChanged);
            // 
            // endTimeText
            // 
            this.endTimeText.Location = new System.Drawing.Point(150, 87);
            this.endTimeText.Name = "endTimeText";
            this.endTimeText.Size = new System.Drawing.Size(100, 21);
            this.endTimeText.TabIndex = 1;
            this.endTimeText.Text = "01:05:00";
            // 
            // timeSetBtn
            // 
            this.timeSetBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.timeSetBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.timeSetBtn.Location = new System.Drawing.Point(274, 87);
            this.timeSetBtn.Name = "timeSetBtn";
            this.timeSetBtn.Size = new System.Drawing.Size(75, 23);
            this.timeSetBtn.TabIndex = 0;
            this.timeSetBtn.Text = "적용하기";
            this.timeSetBtn.UseVisualStyleBackColor = false;
            this.timeSetBtn.Click += new System.EventHandler(this.timeSetBtn_Click);
            // 
            // timer_update
            // 
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // timer_progress
            // 
            this.timer_progress.Interval = 1000;
            this.timer_progress.Tick += new System.EventHandler(this.timer_progress_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(386, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "현재시간 :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(560, 510);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelTop);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addFileBtn;
        private System.Windows.Forms.Button addFolderBtn;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.Label dataInfoText;
        private System.Windows.Forms.TextBox dataInfoTextbox;
        private System.Windows.Forms.Label dataInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button timeSetBtn;
        private System.Windows.Forms.TextBox endTimeText;
        private System.Windows.Forms.ComboBox speedComboBox;
        private System.Windows.Forms.TextBox startTimeText;
        private System.Windows.Forms.Button playSpeed;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Label speedBindText;
        private UserControls.TimeController timeController1;
        private UserControls.TimeController timeController;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.Label currTimeText;
        private System.Windows.Forms.Timer timer_progress;
        private System.Windows.Forms.Label label2;
    }
}

