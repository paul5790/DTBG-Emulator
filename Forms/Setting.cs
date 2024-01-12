using DTBGEmulator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTBGEmulator.Forms
{
    public partial class Setting : Form
    {
        private MainForm mainForm; // MainForm을 참조하기 위한 변수

        SettingDTO settingDTO = new SettingDTO(); // SettingDTO 인스턴스
        private string ipAddress = "192.168.0.11";

        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치

        public Setting(SettingDTO mainFormSettingDTO)
        {
            InitializeComponent();
            settingDTO = mainFormSettingDTO;
        }

        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            // Setting 폼을 닫음
            this.Close();
        }

        #region 상단바 드래그
        private void panelTop_Setting_MouseDown(object sender, MouseEventArgs e)
        {
            mDragForm = true;

            Control control = sender as Control;

            if (control.GetType() == typeof(Panel))
            {
                mMousePosition = e.Location;
            }
            else
            {
                Point point = new Point(e.Location.X + control.Location.X, e.Location.Y + control.Location.Y);
                mMousePosition = point;
            }
        }

        private void panelTop_Setting_MouseMove(object sender, MouseEventArgs e)
        {
            if (mDragForm)
            {
                this.SetDesktopLocation(MousePosition.X - mMousePosition.X, MousePosition.Y - mMousePosition.Y);
            }
        }

        private void panelTop_Setting_MouseUp(object sender, MouseEventArgs e)
        {
            mDragForm = false;
            mMousePosition = Point.Empty;
        }

        private void settings_MouseDown(object sender, MouseEventArgs e)
        {
            mDragForm = true;

            Control control = sender as Control;

            if (control.GetType() == typeof(Panel))
            {
                mMousePosition = e.Location;
            }
            else
            {
                Point point = new Point(e.Location.X + control.Location.X, e.Location.Y + control.Location.Y);
                mMousePosition = point;
            }
        }

        private void settings_MouseMove(object sender, MouseEventArgs e)
        {
            if (mDragForm)
            {
                this.SetDesktopLocation(MousePosition.X - mMousePosition.X, MousePosition.Y - mMousePosition.Y);
            }
        }

        private void settings_MouseUp(object sender, MouseEventArgs e)
        {
            mDragForm = false;
            mMousePosition = Point.Empty;
        }
        #endregion 상단바 드래그

        private void button_Ok_Click(object sender, EventArgs e)
        {

            settingDTO.shipIPAddress = shipIP.Text;
            settingDTO.controlIPAddress = controlIP.Text;
            settingDTO.shipPort = shipPort.Text;
            settingDTO.controlPort = controlPort.Text;

            Console.WriteLine($"IPAddress: {ipAddress}");
            Console.WriteLine($"settingDTO.IPAddress: {settingDTO.shipIPAddress}");
        }
    }
}
