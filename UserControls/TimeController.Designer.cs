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
            this.tableLayoutPanel_Main = new System.Windows.Forms.Panel();
            this.panel_Middle = new System.Windows.Forms.Panel();
            this.panel_Right = new System.Windows.Forms.Panel();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.Controls.Add(this.panel_Middle);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_Right);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_Left);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(800, 40);
            this.tableLayoutPanel_Main.TabIndex = 0;
            // 
            // panel_Middle
            // 
            this.panel_Middle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Middle.Location = new System.Drawing.Point(90, 0);
            this.panel_Middle.Name = "panel_Middle";
            this.panel_Middle.Size = new System.Drawing.Size(620, 40);
            this.panel_Middle.TabIndex = 2;
            // 
            // panel_Right
            // 
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Right.Location = new System.Drawing.Point(710, 0);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(90, 40);
            this.panel_Right.TabIndex = 1;
            // 
            // panel_Left
            // 
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Left.Location = new System.Drawing.Point(0, 0);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(90, 40);
            this.panel_Left.TabIndex = 0;
            // 
            // TimeController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Name = "TimeController";
            this.Size = new System.Drawing.Size(800, 40);
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tableLayoutPanel_Main;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.Panel panel_Middle;
        private System.Windows.Forms.Panel panel_Right;
    }
}
