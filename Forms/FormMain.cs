using DTBGEmulator.Classes;
using DTBGEmulator.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DTBGEmulator
{
    public partial class MainForm : Form
    {
        SettingDTO settingDTO = new SettingDTO(); // SettingDTO 인스턴스
        private FileData fileData = new FileData(); // FileData 인스턴스
        private FolderData folderData = new FolderData(); // FolderData 인스턴스

        Setting settingForm; // setting 폼

        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치

        string ipAddress;
        public MainForm()
        {
            InitializeComponent();
        }


        private void pictureBox_Close_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("ss", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialog == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            // 버튼을 클릭했을 때 모달로 표시될 SettingForm 인스턴스 생성
            settingForm = new Setting(settingDTO);

            // ShowDialog 메서드를 사용하여 모달로 표시
            settingForm.ShowDialog();
        }

        #region 상단바 드래그
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
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

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (mDragForm)
            {
                this.SetDesktopLocation(MousePosition.X - mMousePosition.X, MousePosition.Y - mMousePosition.Y);
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            mDragForm = false;
            mMousePosition = Point.Empty;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
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

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mDragForm)
            {
                this.SetDesktopLocation(MousePosition.X - mMousePosition.X, MousePosition.Y - mMousePosition.Y);
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            mDragForm = false;
            mMousePosition = Point.Empty;
        }
        #endregion 상단바 드래그

        // 시간 설정
        private void timeSetBtn_Click(object sender, EventArgs e)
        {
            ipAddress = settingDTO.shipIPAddress;

            // DTO 데이터 확인 콘솔
            Console.WriteLine($"settingDTO.IPAddress: {settingDTO.shipIPAddress}");
            Console.WriteLine($"settingDTO.IPAddress: {settingDTO.controlIPAddress}");
            Console.WriteLine($"settingDTO.IPAddress: {settingDTO.shipPort}");
            Console.WriteLine($"settingDTO.IPAddress: {settingDTO.controlPort}");
        }

        private void addFileBtn_Click(object sender, EventArgs e)
        {
            // addFileBtn 클릭 시 FileData 클래스의 SelectFile 메서드 호출
            fileData.SelectFile();
            // 선택된 파일의 데이터를 읽어와서 사용하거나 저장할 수 있음
            //string fileContent = fileData.GetFileContent();
            ////Console.WriteLine($"fileContent: {fileContent}");

            //string fileContentJson = fileData.GetFileContentJson();
            //Console.WriteLine($"fileContentJson: {fileContentJson}");

            List<string> filePackets = fileData.GetFilePackets();
            Console.WriteLine('1' + filePackets[0]);
            //Console.WriteLine('2' + filePackets[1]);
            //Console.WriteLine('3' + filePackets[2]);
            //Console.WriteLine('4' + filePackets[3]);
            string startTime = fileData.GetStartTime();
            string endTime = fileData.GetEndTime();
            string storageSize = fileData.GetStorageSize();
            dataInfoTextbox.Text = $"데이터 정보\r\n시작 시간 : {startTime}\r\n종료시간 : {endTime}\r\n용량 : {storageSize}";
        }

        private void addFolderBtn_Click(object sender, EventArgs e)
        {
            // addFolderBtn 클릭 시 FileData 클래스의 SelectFile 메서드 호출
            folderData.SelectFolder();

            Dictionary<string, string> fileContents = folderData.GetFileContents();
            Console.WriteLine($"fileContents: {fileContents}");

            // 개별 파일 내용 출력
            foreach (var entry in fileContents)
            {
                Console.WriteLine($"File Name: {entry.Key}");
                Console.WriteLine($"File Content: {entry.Value}");
                Console.WriteLine("-----------------------");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string currTime = "00:00:00";
            string TotalTime = "01:00:00";

            timeController1.TotalTime = ChangeTimeToStrSec(TotalTime);
            timeController1.CurrTime = ChangeTimeToStrSec(currTime); ;
        }

        private string ChangeTimeToStrSec(string dateTime)
        {
            int sec_HH = Convert.ToInt32(dateTime.Substring(0, 2)) * 3600;
            int sec_mm = Convert.ToInt32(dateTime.Substring(3, 2)) * 60;
            int sec_ss = Convert.ToInt32(dateTime.Substring(6, 2));

            string strSec = (sec_HH + sec_mm + sec_ss).ToString();

            return strSec;
        }
    }
}
