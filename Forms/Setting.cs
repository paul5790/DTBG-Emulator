using DTBGEmulator.Classes;
using DTBGEmulator.Classes.DTO;
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
        SettingDTO dto = new SettingDTO();
        SettingDTO settingDTO = new SettingDTO(); // SettingDTO 인스턴스
        private string ipAddress = "192.168.0.11";

        // ip 설정 주소
        private string shipIPAddress;
        private string controlIPAddress;
        private string shipPortAddress;
        private string controlPortAddress;

        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치

        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            shipIP.Text = dto.shipIPAddress;
            shipPort.Text = dto.shipPort;
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
            dto.shipIPAddress = shipIP.Text;
            controlIPAddress = controlIP.Text;
            dto.shipPort = shipPort.Text;
            controlPortAddress = controlPort.Text;
            MessageBox.Show("설정이 완료되었습니다.");
            shipIP.Text = dto.shipIPAddress;
            shipPort.Text = dto.shipPort;
        }

        public SettingDTO GenerateSettingDTO()
        {
            return dto;
        }

    }
}
