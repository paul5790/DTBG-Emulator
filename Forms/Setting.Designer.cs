namespace DTBGEmulator.Forms
{
    partial class Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.shipPort = new System.Windows.Forms.TextBox();
            this.shipIP = new System.Windows.Forms.TextBox();
            this.shipPortText = new System.Windows.Forms.Label();
            this.shipIPText = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.controlPort = new System.Windows.Forms.TextBox();
            this.controlIP = new System.Windows.Forms.TextBox();
            this.controlPortText = new System.Windows.Forms.Label();
            this.controlIPText = new System.Windows.Forms.Label();
            this.panelTop_Setting = new System.Windows.Forms.Panel();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.button_Ok = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelTop_Setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 35);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(320, 130);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.shipPort);
            this.tabPage1.Controls.Add(this.shipIP);
            this.tabPage1.Controls.Add(this.shipPortText);
            this.tabPage1.Controls.Add(this.shipIPText);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(312, 104);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "선내 데이터";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // shipPort
            // 
            this.shipPort.Location = new System.Drawing.Point(160, 55);
            this.shipPort.Name = "shipPort";
            this.shipPort.Size = new System.Drawing.Size(60, 21);
            this.shipPort.TabIndex = 7;
            this.shipPort.Text = "1234";
            // 
            // shipIP
            // 
            this.shipIP.Location = new System.Drawing.Point(160, 25);
            this.shipIP.Name = "shipIP";
            this.shipIP.Size = new System.Drawing.Size(100, 21);
            this.shipIP.TabIndex = 6;
            this.shipIP.Text = "192.168.0.0";
            // 
            // shipPortText
            // 
            this.shipPortText.AutoSize = true;
            this.shipPortText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.shipPortText.Location = new System.Drawing.Point(10, 60);
            this.shipPortText.Name = "shipPortText";
            this.shipPortText.Size = new System.Drawing.Size(116, 12);
            this.shipPortText.TabIndex = 5;
            this.shipPortText.Text = "Target Port Number";
            // 
            // shipIPText
            // 
            this.shipIPText.AutoSize = true;
            this.shipIPText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.shipIPText.Location = new System.Drawing.Point(10, 30);
            this.shipIPText.Name = "shipIPText";
            this.shipIPText.Size = new System.Drawing.Size(107, 12);
            this.shipIPText.TabIndex = 4;
            this.shipIPText.Text = "Target IP Address";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.controlPort);
            this.tabPage2.Controls.Add(this.controlIP);
            this.tabPage2.Controls.Add(this.controlPortText);
            this.tabPage2.Controls.Add(this.controlIPText);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(312, 104);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "관제 데이터";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // controlPort
            // 
            this.controlPort.Location = new System.Drawing.Point(160, 55);
            this.controlPort.Name = "controlPort";
            this.controlPort.Size = new System.Drawing.Size(60, 21);
            this.controlPort.TabIndex = 11;
            this.controlPort.Text = "8878";
            // 
            // controlIP
            // 
            this.controlIP.Location = new System.Drawing.Point(160, 25);
            this.controlIP.Name = "controlIP";
            this.controlIP.Size = new System.Drawing.Size(100, 21);
            this.controlIP.TabIndex = 10;
            this.controlIP.Text = "192.168.0.0";
            // 
            // controlPortText
            // 
            this.controlPortText.AutoSize = true;
            this.controlPortText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.controlPortText.Location = new System.Drawing.Point(10, 60);
            this.controlPortText.Name = "controlPortText";
            this.controlPortText.Size = new System.Drawing.Size(116, 12);
            this.controlPortText.TabIndex = 9;
            this.controlPortText.Text = "Target Port Number";
            // 
            // controlIPText
            // 
            this.controlIPText.AutoSize = true;
            this.controlIPText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.controlIPText.Location = new System.Drawing.Point(10, 30);
            this.controlIPText.Name = "controlIPText";
            this.controlIPText.Size = new System.Drawing.Size(107, 12);
            this.controlIPText.TabIndex = 8;
            this.controlIPText.Text = "Target IP Address";
            // 
            // panelTop_Setting
            // 
            this.panelTop_Setting.BackColor = System.Drawing.Color.DimGray;
            this.panelTop_Setting.Controls.Add(this.pictureBox_Close);
            this.panelTop_Setting.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop_Setting.Location = new System.Drawing.Point(0, 0);
            this.panelTop_Setting.Name = "panelTop_Setting";
            this.panelTop_Setting.Size = new System.Drawing.Size(350, 20);
            this.panelTop_Setting.TabIndex = 5;
            this.panelTop_Setting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_Setting_MouseDown);
            this.panelTop_Setting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_Setting_MouseMove);
            this.panelTop_Setting.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_Setting_MouseUp);
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_Close.Image = global::DTBGEmulator.Properties.Resources.close;
            this.pictureBox_Close.Location = new System.Drawing.Point(330, 0);
            this.pictureBox_Close.Name = "pictureBox_Close";
            this.pictureBox_Close.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Close.TabIndex = 6;
            this.pictureBox_Close.TabStop = false;
            this.pictureBox_Close.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // button_Ok
            // 
            this.button_Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.button_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Ok.ForeColor = System.Drawing.Color.White;
            this.button_Ok.Location = new System.Drawing.Point(261, 168);
            this.button_Ok.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(70, 25);
            this.button_Ok.TabIndex = 6;
            this.button_Ok.Text = "OK";
            this.button_Ok.UseVisualStyleBackColor = false;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 200);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.panelTop_Setting);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Setting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelTop_Setting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox shipIP;
        private System.Windows.Forms.Label shipPortText;
        private System.Windows.Forms.Label shipIPText;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox shipPort;
        private System.Windows.Forms.TextBox controlPort;
        private System.Windows.Forms.TextBox controlIP;
        private System.Windows.Forms.Label controlPortText;
        private System.Windows.Forms.Label controlIPText;
        private System.Windows.Forms.Panel panelTop_Setting;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.Button button_Ok;
    }
}