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
        /// 
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataInfoText = new System.Windows.Forms.Label();
            this.dataInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.settingBtn = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.speedComboBox = new System.Windows.Forms.ComboBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.fullTimeData = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.endTimeData = new System.Windows.Forms.TextBox();
            this.startTimeData = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.fileLocation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lastFileName = new System.Windows.Forms.TextBox();
            this.firstFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addFolderBtn = new System.Windows.Forms.Button();
            this.addFileBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataViewCheckBox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.whiteSpaceCheckBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.currTimeText = new System.Windows.Forms.Label();
            this.timeController = new DTBGEmulator.UserControls.TimeController();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.timer_progress = new System.Windows.Forms.Timer(this.components);
            this.timer_udp = new System.Windows.Forms.Timer(this.components);
            this.dataViewTextBox = new System.Windows.Forms.TextBox();
            this.dataViewText = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(74)))), ((int)(((byte)(72)))));
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.pictureBox_Close);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(570, 30);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DTBGEmulator.Properties.Resources.KrisoLogo;
            this.pictureBox1.Location = new System.Drawing.Point(10, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_Close.Image = global::DTBGEmulator.Properties.Resources.close;
            this.pictureBox_Close.Location = new System.Drawing.Point(535, 0);
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
            this.label1.Location = new System.Drawing.Point(156, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ESCORT-DTBG 연동시험용 에뮬레이터";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // dataInfoText
            // 
            this.dataInfoText.AutoSize = true;
            this.dataInfoText.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataInfoText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataInfoText.Location = new System.Drawing.Point(296, 24);
            this.dataInfoText.Name = "dataInfoText";
            this.dataInfoText.Size = new System.Drawing.Size(73, 19);
            this.dataInfoText.TabIndex = 2;
            this.dataInfoText.Text = "데이터 정보";
            // 
            // dataInfo
            // 
            this.dataInfo.AutoSize = true;
            this.dataInfo.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataInfo.Location = new System.Drawing.Point(16, 8);
            this.dataInfo.Name = "dataInfo";
            this.dataInfo.Size = new System.Drawing.Size(101, 19);
            this.dataInfo.TabIndex = 4;
            this.dataInfo.Text = "데이터 재생 현황";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.settingBtn);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dataInfoText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 225);
            this.panel1.TabIndex = 5;
            // 
            // settingBtn
            // 
            this.settingBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.settings_ccc;
            this.settingBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingBtn.FlatAppearance.BorderSize = 0;
            this.settingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingBtn.Location = new System.Drawing.Point(525, 8);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(30, 30);
            this.settingBtn.TabIndex = 12;
            this.settingBtn.UseVisualStyleBackColor = true;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.speedComboBox);
            this.panel7.Controls.Add(this.runBtn);
            this.panel7.Controls.Add(this.pauseBtn);
            this.panel7.Controls.Add(this.stopBtn);
            this.panel7.Location = new System.Drawing.Point(295, 172);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(260, 44);
            this.panel7.TabIndex = 19;
            // 
            // speedComboBox
            // 
            this.speedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedComboBox.FormattingEnabled = true;
            this.speedComboBox.Items.AddRange(new object[] {
            "속도 (0.1x)",
            "속도 (0.25x)",
            "속도 (0.5x)",
            "속도 (1.0x)",
            "속도 (2.0x)",
            "속도 (4.0x)",
            "속도 (10.0x)",
            "속도 (20.0x)",
            "속도 (max)"});
            this.speedComboBox.Location = new System.Drawing.Point(7, 11);
            this.speedComboBox.Name = "speedComboBox";
            this.speedComboBox.Size = new System.Drawing.Size(93, 20);
            this.speedComboBox.TabIndex = 21;
            this.speedComboBox.SelectedIndexChanged += new System.EventHandler(this.speedComboBox_SelectedIndexChanged);
            // 
            // runBtn
            // 
            this.runBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.run_c;
            this.runBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.runBtn.FlatAppearance.BorderSize = 0;
            this.runBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBtn.Location = new System.Drawing.Point(108, -1);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(45, 45);
            this.runBtn.TabIndex = 15;
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // pauseBtn
            // 
            this.pauseBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.pause_c;
            this.pauseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pauseBtn.FlatAppearance.BorderSize = 0;
            this.pauseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseBtn.Location = new System.Drawing.Point(159, -1);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(45, 45);
            this.pauseBtn.TabIndex = 16;
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.BackgroundImage = global::DTBGEmulator.Properties.Resources.stop_c;
            this.stopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.stopBtn.FlatAppearance.BorderSize = 0;
            this.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBtn.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopBtn.Location = new System.Drawing.Point(210, -1);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(45, 45);
            this.stopBtn.TabIndex = 17;
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(296, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 19);
            this.label9.TabIndex = 9;
            this.label9.Text = "에뮬레이터 제어";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.fullTimeData);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.endTimeData);
            this.panel6.Controls.Add(this.startTimeData);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Location = new System.Drawing.Point(295, 46);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(260, 100);
            this.panel6.TabIndex = 8;
            // 
            // fullTimeData
            // 
            this.fullTimeData.Location = new System.Drawing.Point(84, 69);
            this.fullTimeData.Name = "fullTimeData";
            this.fullTimeData.ReadOnly = true;
            this.fullTimeData.Size = new System.Drawing.Size(150, 21);
            this.fullTimeData.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(16, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "전체 시간";
            // 
            // endTimeData
            // 
            this.endTimeData.Location = new System.Drawing.Point(84, 40);
            this.endTimeData.Name = "endTimeData";
            this.endTimeData.ReadOnly = true;
            this.endTimeData.Size = new System.Drawing.Size(150, 21);
            this.endTimeData.TabIndex = 15;
            // 
            // startTimeData
            // 
            this.startTimeData.Location = new System.Drawing.Point(84, 11);
            this.startTimeData.Name = "startTimeData";
            this.startTimeData.ReadOnly = true;
            this.startTimeData.Size = new System.Drawing.Size(150, 21);
            this.startTimeData.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(16, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "종료 시간";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(16, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "시작 시간";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.fileLocation);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.lastFileName);
            this.panel5.Controls.Add(this.firstFileName);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.addFolderBtn);
            this.panel5.Controls.Add(this.addFileBtn);
            this.panel5.Location = new System.Drawing.Point(15, 46);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(265, 170);
            this.panel5.TabIndex = 7;
            // 
            // fileLocation
            // 
            this.fileLocation.Location = new System.Drawing.Point(85, 137);
            this.fileLocation.Name = "fileLocation";
            this.fileLocation.ReadOnly = true;
            this.fileLocation.Size = new System.Drawing.Size(160, 21);
            this.fileLocation.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(17, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "폴더 경로";
            // 
            // lastFileName
            // 
            this.lastFileName.Location = new System.Drawing.Point(85, 70);
            this.lastFileName.Name = "lastFileName";
            this.lastFileName.ReadOnly = true;
            this.lastFileName.Size = new System.Drawing.Size(160, 21);
            this.lastFileName.TabIndex = 11;
            // 
            // firstFileName
            // 
            this.firstFileName.Location = new System.Drawing.Point(85, 42);
            this.firstFileName.Name = "firstFileName";
            this.firstFileName.ReadOnly = true;
            this.firstFileName.Size = new System.Drawing.Size(160, 21);
            this.firstFileName.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(17, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "종료 파일";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(17, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "시작 파일";
            // 
            // addFolderBtn
            // 
            this.addFolderBtn.ForeColor = System.Drawing.Color.Black;
            this.addFolderBtn.Location = new System.Drawing.Point(10, 105);
            this.addFolderBtn.Name = "addFolderBtn";
            this.addFolderBtn.Size = new System.Drawing.Size(75, 25);
            this.addFolderBtn.TabIndex = 7;
            this.addFolderBtn.Text = "폴더 선택";
            this.addFolderBtn.UseVisualStyleBackColor = true;
            this.addFolderBtn.Click += new System.EventHandler(this.addFolderBtn_Click);
            // 
            // addFileBtn
            // 
            this.addFileBtn.ForeColor = System.Drawing.Color.Black;
            this.addFileBtn.Location = new System.Drawing.Point(10, 10);
            this.addFileBtn.Name = "addFileBtn";
            this.addFileBtn.Size = new System.Drawing.Size(75, 25);
            this.addFileBtn.TabIndex = 6;
            this.addFileBtn.Text = "파일 선택";
            this.addFileBtn.UseVisualStyleBackColor = true;
            this.addFileBtn.Click += new System.EventHandler(this.addFileBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "데이터 선택";
            // 
            // dataViewCheckBox
            // 
            this.dataViewCheckBox.AutoSize = true;
            this.dataViewCheckBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataViewCheckBox.Location = new System.Drawing.Point(474, 6);
            this.dataViewCheckBox.Name = "dataViewCheckBox";
            this.dataViewCheckBox.Size = new System.Drawing.Size(88, 16);
            this.dataViewCheckBox.TabIndex = 20;
            this.dataViewCheckBox.Text = "데이터 표시";
            this.dataViewCheckBox.UseVisualStyleBackColor = true;
            this.dataViewCheckBox.CheckedChanged += new System.EventHandler(this.dataViewCheckBox_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataViewCheckBox);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.dataInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 255);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(570, 140);
            this.panel3.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.whiteSpaceCheckBox);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.currTimeText);
            this.panel4.Controls.Add(this.timeController);
            this.panel4.Location = new System.Drawing.Point(15, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(540, 100);
            this.panel4.TabIndex = 5;
            // 
            // whiteSpaceCheckBox
            // 
            this.whiteSpaceCheckBox.AutoSize = true;
            this.whiteSpaceCheckBox.Enabled = false;
            this.whiteSpaceCheckBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.whiteSpaceCheckBox.Location = new System.Drawing.Point(443, 77);
            this.whiteSpaceCheckBox.Name = "whiteSpaceCheckBox";
            this.whiteSpaceCheckBox.Size = new System.Drawing.Size(76, 16);
            this.whiteSpaceCheckBox.TabIndex = 28;
            this.whiteSpaceCheckBox.Text = "공백 제거";
            this.whiteSpaceCheckBox.UseVisualStyleBackColor = true;
            this.whiteSpaceCheckBox.CheckedChanged += new System.EventHandler(this.whiteSpaceCheckBox_CheckedChanged);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label16.Location = new System.Drawing.Point(423, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 13);
            this.label16.TabIndex = 26;
            this.label16.Text = "구간반복 종료시간";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label13.Location = new System.Drawing.Point(6, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "구간반복 시작시간";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label12.Location = new System.Drawing.Point(111, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 15);
            this.label12.TabIndex = 23;
            this.label12.Text = "시작시간";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(349, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 15);
            this.label11.TabIndex = 22;
            this.label11.Text = "종료시간";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.label2.Location = new System.Drawing.Point(182, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "현재 재생 시간 :";
            // 
            // currTimeText
            // 
            this.currTimeText.AutoSize = true;
            this.currTimeText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.currTimeText.Location = new System.Drawing.Point(276, 79);
            this.currTimeText.Name = "currTimeText";
            this.currTimeText.Size = new System.Drawing.Size(65, 12);
            this.currTimeText.TabIndex = 19;
            this.currTimeText.Text = "88 : 88 : 88";
            // 
            // timeController
            // 
            this.timeController.availableControl = false;
            this.timeController.BackColor = System.Drawing.Color.White;
            this.timeController.CurrTime = "0";
            this.timeController.EndFileTime = "00 : 00 : 00";
            this.timeController.EndRepeatTime = 0;
            this.timeController.FileCount = 0;
            this.timeController.First = false;
            this.timeController.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeController.Location = new System.Drawing.Point(10, 15);
            this.timeController.Name = "timeController";
            this.timeController.Size = new System.Drawing.Size(510, 44);
            this.timeController.StartFileTime = "00 : 00 : 00";
            this.timeController.StartRepeatTime = 0;
            this.timeController.TabIndex = 18;
            this.timeController.UseController = false;
            // 
            // timer_progress
            // 
            this.timer_progress.Interval = 1000;
            this.timer_progress.Tick += new System.EventHandler(this.timer_progress_Tick);
            // 
            // dataViewTextBox
            // 
            this.dataViewTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.dataViewTextBox.Location = new System.Drawing.Point(15, 419);
            this.dataViewTextBox.Multiline = true;
            this.dataViewTextBox.Name = "dataViewTextBox";
            this.dataViewTextBox.ReadOnly = true;
            this.dataViewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataViewTextBox.Size = new System.Drawing.Size(540, 364);
            this.dataViewTextBox.TabIndex = 7;
            this.dataViewTextBox.Visible = false;
            // 
            // dataViewText
            // 
            this.dataViewText.AutoSize = true;
            this.dataViewText.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataViewText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataViewText.Location = new System.Drawing.Point(16, 398);
            this.dataViewText.Name = "dataViewText";
            this.dataViewText.Size = new System.Drawing.Size(73, 19);
            this.dataViewText.TabIndex = 8;
            this.dataViewText.Text = "데이터 표시";
            this.dataViewText.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(570, 400);
            this.Controls.Add(this.dataViewText);
            this.Controls.Add(this.dataViewTextBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.Label dataInfoText;
        private System.Windows.Forms.Label dataInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.Button runBtn;
        private UserControls.TimeController timeController1;
        private System.Windows.Forms.Timer timer_update;
        public System.Windows.Forms.Label currTimeText;
        private System.Windows.Forms.Timer timer_progress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer_udp;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button addFileBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button addFolderBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox fullTimeData;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox endTimeData;
        private System.Windows.Forms.TextBox startTimeData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox fileLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lastFileName;
        private System.Windows.Forms.TextBox firstFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox speedComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox dataViewCheckBox;
        private System.Windows.Forms.TextBox dataViewTextBox;
        private UserControls.TimeController timeController;
        private System.Windows.Forms.CheckBox whiteSpaceCheckBox;
        private System.Windows.Forms.Label dataViewText;
    }
}

