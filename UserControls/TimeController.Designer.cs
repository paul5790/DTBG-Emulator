namespace DTBGEmulator.UserControls
{
    partial class TimeController
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Middle = new System.Windows.Forms.Panel();
            this.label_Time_Total = new System.Windows.Forms.Label();
            this.label_Time_Zero = new System.Windows.Forms.Label();
            this.txtBox_EndTime_HH = new System.Windows.Forms.TextBox();
            this.txtBox_EndTime_mm = new System.Windows.Forms.TextBox();
            this.txtBox_EndTime_ss = new System.Windows.Forms.TextBox();
            this.label_EndTime_Colon1 = new System.Windows.Forms.Label();
            this.label_EndTime_Colon2 = new System.Windows.Forms.Label();
            this.panel_Right = new System.Windows.Forms.Panel();
            this.txtBox_StartTime_HH = new System.Windows.Forms.TextBox();
            this.txtBox_StartTime_mm = new System.Windows.Forms.TextBox();
            this.txtBox_StartTime_ss = new System.Windows.Forms.TextBox();
            this.label_StartTime_Colon1 = new System.Windows.Forms.Label();
            this.label_StartTime_Colon2 = new System.Windows.Forms.Label();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.panel_Middle.SuspendLayout();
            this.panel_Right.SuspendLayout();
            this.panel_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Middle
            // 
            this.panel_Middle.BackColor = System.Drawing.Color.White;
            this.panel_Middle.Controls.Add(this.label_Time_Total);
            this.panel_Middle.Controls.Add(this.label_Time_Zero);
            this.panel_Middle.Location = new System.Drawing.Point(109, 0);
            this.panel_Middle.Name = "panel_Middle";
            this.panel_Middle.Size = new System.Drawing.Size(306, 44);
            this.panel_Middle.TabIndex = 0;
            this.panel_Middle.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Middle_Paint);
            this.panel_Middle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Middle_MouseDown);
            this.panel_Middle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Middle_MouseMove);
            this.panel_Middle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Middle_MouseUp);
            // 
            // label_Time_Total
            // 
            this.label_Time_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Time_Total.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Time_Total.ForeColor = System.Drawing.Color.Black;
            this.label_Time_Total.Location = new System.Drawing.Point(238, 30);
            this.label_Time_Total.Name = "label_Time_Total";
            this.label_Time_Total.Size = new System.Drawing.Size(67, 11);
            this.label_Time_Total.TabIndex = 8;
            this.label_Time_Total.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Time_Zero
            // 
            this.label_Time_Zero.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Time_Zero.ForeColor = System.Drawing.Color.Black;
            this.label_Time_Zero.Location = new System.Drawing.Point(0, 30);
            this.label_Time_Zero.Name = "label_Time_Zero";
            this.label_Time_Zero.Size = new System.Drawing.Size(70, 11);
            this.label_Time_Zero.TabIndex = 7;
            this.label_Time_Zero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBox_EndTime_HH
            // 
            this.txtBox_EndTime_HH.BackColor = System.Drawing.Color.White;
            this.txtBox_EndTime_HH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_EndTime_HH.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_EndTime_HH.ForeColor = System.Drawing.Color.Black;
            this.txtBox_EndTime_HH.Location = new System.Drawing.Point(1, 3);
            this.txtBox_EndTime_HH.MaxLength = 2;
            this.txtBox_EndTime_HH.Name = "txtBox_EndTime_HH";
            this.txtBox_EndTime_HH.Size = new System.Drawing.Size(20, 15);
            this.txtBox_EndTime_HH.TabIndex = 3;
            this.txtBox_EndTime_HH.Text = "00";
            this.txtBox_EndTime_HH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_EndTime_HH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_EndTime_KeyPress);
            this.txtBox_EndTime_HH.Leave += new System.EventHandler(this.txtBox_Leave);
            // 
            // txtBox_EndTime_mm
            // 
            this.txtBox_EndTime_mm.BackColor = System.Drawing.Color.White;
            this.txtBox_EndTime_mm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_EndTime_mm.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_EndTime_mm.ForeColor = System.Drawing.Color.Black;
            this.txtBox_EndTime_mm.Location = new System.Drawing.Point(33, 3);
            this.txtBox_EndTime_mm.MaxLength = 2;
            this.txtBox_EndTime_mm.Name = "txtBox_EndTime_mm";
            this.txtBox_EndTime_mm.Size = new System.Drawing.Size(20, 15);
            this.txtBox_EndTime_mm.TabIndex = 4;
            this.txtBox_EndTime_mm.Text = "00";
            this.txtBox_EndTime_mm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_EndTime_mm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_EndTime_KeyPress);
            this.txtBox_EndTime_mm.Leave += new System.EventHandler(this.txtBox_Leave);
            // 
            // txtBox_EndTime_ss
            // 
            this.txtBox_EndTime_ss.BackColor = System.Drawing.Color.White;
            this.txtBox_EndTime_ss.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_EndTime_ss.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_EndTime_ss.ForeColor = System.Drawing.Color.Black;
            this.txtBox_EndTime_ss.Location = new System.Drawing.Point(65, 3);
            this.txtBox_EndTime_ss.MaxLength = 2;
            this.txtBox_EndTime_ss.Name = "txtBox_EndTime_ss";
            this.txtBox_EndTime_ss.Size = new System.Drawing.Size(20, 15);
            this.txtBox_EndTime_ss.TabIndex = 5;
            this.txtBox_EndTime_ss.Text = "00";
            this.txtBox_EndTime_ss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_EndTime_ss.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_EndTime_KeyPress);
            this.txtBox_EndTime_ss.Leave += new System.EventHandler(this.txtBox_Leave);
            // 
            // label_EndTime_Colon1
            // 
            this.label_EndTime_Colon1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EndTime_Colon1.ForeColor = System.Drawing.Color.Black;
            this.label_EndTime_Colon1.Location = new System.Drawing.Point(21, 3);
            this.label_EndTime_Colon1.Name = "label_EndTime_Colon1";
            this.label_EndTime_Colon1.Size = new System.Drawing.Size(12, 15);
            this.label_EndTime_Colon1.TabIndex = 3;
            this.label_EndTime_Colon1.Text = ":";
            this.label_EndTime_Colon1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_EndTime_Colon2
            // 
            this.label_EndTime_Colon2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EndTime_Colon2.ForeColor = System.Drawing.Color.Black;
            this.label_EndTime_Colon2.Location = new System.Drawing.Point(53, 3);
            this.label_EndTime_Colon2.Name = "label_EndTime_Colon2";
            this.label_EndTime_Colon2.Size = new System.Drawing.Size(12, 15);
            this.label_EndTime_Colon2.TabIndex = 6;
            this.label_EndTime_Colon2.Text = ":";
            this.label_EndTime_Colon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Right
            // 
            this.panel_Right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Right.BackColor = System.Drawing.Color.White;
            this.panel_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Right.Controls.Add(this.label_EndTime_Colon2);
            this.panel_Right.Controls.Add(this.label_EndTime_Colon1);
            this.panel_Right.Controls.Add(this.txtBox_EndTime_ss);
            this.panel_Right.Controls.Add(this.txtBox_EndTime_mm);
            this.panel_Right.Controls.Add(this.txtBox_EndTime_HH);
            this.panel_Right.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_Right.Location = new System.Drawing.Point(421, 14);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(88, 20);
            this.panel_Right.TabIndex = 8;
            // 
            // txtBox_StartTime_HH
            // 
            this.txtBox_StartTime_HH.BackColor = System.Drawing.Color.White;
            this.txtBox_StartTime_HH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_StartTime_HH.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_StartTime_HH.ForeColor = System.Drawing.Color.Black;
            this.txtBox_StartTime_HH.Location = new System.Drawing.Point(1, 3);
            this.txtBox_StartTime_HH.MaxLength = 2;
            this.txtBox_StartTime_HH.Name = "txtBox_StartTime_HH";
            this.txtBox_StartTime_HH.Size = new System.Drawing.Size(20, 15);
            this.txtBox_StartTime_HH.TabIndex = 3;
            this.txtBox_StartTime_HH.Text = "00";
            this.txtBox_StartTime_HH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_StartTime_HH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_StartTime_KeyPress);
            this.txtBox_StartTime_HH.Leave += new System.EventHandler(this.txtBox_Leave);
            // 
            // txtBox_StartTime_mm
            // 
            this.txtBox_StartTime_mm.BackColor = System.Drawing.Color.White;
            this.txtBox_StartTime_mm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_StartTime_mm.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_StartTime_mm.ForeColor = System.Drawing.Color.Black;
            this.txtBox_StartTime_mm.Location = new System.Drawing.Point(33, 3);
            this.txtBox_StartTime_mm.MaxLength = 2;
            this.txtBox_StartTime_mm.Name = "txtBox_StartTime_mm";
            this.txtBox_StartTime_mm.Size = new System.Drawing.Size(20, 15);
            this.txtBox_StartTime_mm.TabIndex = 4;
            this.txtBox_StartTime_mm.Text = "00";
            this.txtBox_StartTime_mm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_StartTime_mm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_StartTime_KeyPress);
            this.txtBox_StartTime_mm.Leave += new System.EventHandler(this.txtBox_Leave);
            // 
            // txtBox_StartTime_ss
            // 
            this.txtBox_StartTime_ss.BackColor = System.Drawing.Color.White;
            this.txtBox_StartTime_ss.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_StartTime_ss.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_StartTime_ss.ForeColor = System.Drawing.Color.Black;
            this.txtBox_StartTime_ss.Location = new System.Drawing.Point(65, 3);
            this.txtBox_StartTime_ss.MaxLength = 2;
            this.txtBox_StartTime_ss.Name = "txtBox_StartTime_ss";
            this.txtBox_StartTime_ss.Size = new System.Drawing.Size(20, 15);
            this.txtBox_StartTime_ss.TabIndex = 5;
            this.txtBox_StartTime_ss.Text = "00";
            this.txtBox_StartTime_ss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_StartTime_ss.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_StartTime_KeyPress);
            this.txtBox_StartTime_ss.Leave += new System.EventHandler(this.txtBox_Leave);
            // 
            // label_StartTime_Colon1
            // 
            this.label_StartTime_Colon1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_StartTime_Colon1.ForeColor = System.Drawing.Color.Black;
            this.label_StartTime_Colon1.Location = new System.Drawing.Point(21, 3);
            this.label_StartTime_Colon1.Name = "label_StartTime_Colon1";
            this.label_StartTime_Colon1.Size = new System.Drawing.Size(12, 15);
            this.label_StartTime_Colon1.TabIndex = 3;
            this.label_StartTime_Colon1.Text = ":";
            this.label_StartTime_Colon1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_StartTime_Colon2
            // 
            this.label_StartTime_Colon2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_StartTime_Colon2.ForeColor = System.Drawing.Color.Black;
            this.label_StartTime_Colon2.Location = new System.Drawing.Point(53, 3);
            this.label_StartTime_Colon2.Name = "label_StartTime_Colon2";
            this.label_StartTime_Colon2.Size = new System.Drawing.Size(12, 15);
            this.label_StartTime_Colon2.TabIndex = 6;
            this.label_StartTime_Colon2.Text = ":";
            this.label_StartTime_Colon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Left
            // 
            this.panel_Left.BackColor = System.Drawing.Color.White;
            this.panel_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Left.Controls.Add(this.label_StartTime_Colon2);
            this.panel_Left.Controls.Add(this.label_StartTime_Colon1);
            this.panel_Left.Controls.Add(this.txtBox_StartTime_ss);
            this.panel_Left.Controls.Add(this.txtBox_StartTime_mm);
            this.panel_Left.Controls.Add(this.txtBox_StartTime_HH);
            this.panel_Left.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_Left.Location = new System.Drawing.Point(3, 14);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(88, 20);
            this.panel_Left.TabIndex = 2;
            // 
            // TimeController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Right);
            this.Controls.Add(this.panel_Left);
            this.Controls.Add(this.panel_Middle);
            this.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TimeController";
            this.Size = new System.Drawing.Size(510, 46);
            this.Load += new System.EventHandler(this.UC_TimeController_Load);
            this.SizeChanged += new System.EventHandler(this.UC_TimeController_SizeChanged);
            this.panel_Middle.ResumeLayout(false);
            this.panel_Right.ResumeLayout(false);
            this.panel_Right.PerformLayout();
            this.panel_Left.ResumeLayout(false);
            this.panel_Left.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Middle;
        private System.Windows.Forms.Label label_Time_Zero;
        private System.Windows.Forms.Label label_Time_Total;
        private System.Windows.Forms.TextBox txtBox_EndTime_HH;
        private System.Windows.Forms.TextBox txtBox_EndTime_mm;
        private System.Windows.Forms.TextBox txtBox_EndTime_ss;
        private System.Windows.Forms.Label label_EndTime_Colon1;
        private System.Windows.Forms.Label label_EndTime_Colon2;
        private System.Windows.Forms.Panel panel_Right;
        private System.Windows.Forms.TextBox txtBox_StartTime_HH;
        private System.Windows.Forms.TextBox txtBox_StartTime_mm;
        private System.Windows.Forms.TextBox txtBox_StartTime_ss;
        private System.Windows.Forms.Label label_StartTime_Colon1;
        private System.Windows.Forms.Label label_StartTime_Colon2;
        private System.Windows.Forms.Panel panel_Left;
    }
}
