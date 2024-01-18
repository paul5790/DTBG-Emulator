using DTBGEmulator.Classes;
using DTBGEmulator.Classes.DTO;
using DTBGEmulator.Forms;
using DTBGEmulator.UserControls;
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
        DataDTO dto;


        Setting settingForm; // setting 폼

        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치

        string ipAddress;
        string currTime;
        string TotalTime;

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

            //List<string> filePackets = fileData.GetFilePackets();
            //string startTime = fileData.GetStartTime();
            //string endTime = fileData.GetEndTime();
            //string storageSize = fileData.GetStorageSize();
            // fileData.SetDataDTOValues(dto);
            dto = fileData.GenerateDataDTO();
            List<string> filePackets = dto.FilePackets;
            Console.WriteLine($"fileContentJson: {filePackets}");
            string startTime = dto.StartTimeStr;
            string endTime = dto.EndTimeStr;
            string storageSize = dto.Storage;
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
            currTime = "00:00:00";
            TotalTime = "00:10:01";

            timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
            timeController.CurrTime = ChangeTimeToStrSec(currTime);
            timer_update.Start();

            speedComboBox.SelectedIndex = 0;
        }

        // 시간(문자) -> 시간초(숫자)
        private string ChangeTimeToStrSec(string dateTime)
        {
            int sec_HH = Convert.ToInt32(dateTime.Substring(0, 2)) * 3600;
            int sec_mm = Convert.ToInt32(dateTime.Substring(3, 2)) * 60;
            int sec_ss = Convert.ToInt32(dateTime.Substring(6, 2));

            string strSec = (sec_HH + sec_mm + sec_ss).ToString();

            return strSec;
        }

        // 시간초(숫자) -> 시간(문자)
        private string ChangeSecToTime(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            string timeString = $"{hours:D2}:{minutes:D2}:{seconds:D2}";

            return timeString;
        }

        // 시작버튼 클릭시
        private void runBtn_Click(object sender, EventArgs e)
        {
            timeController.TotalTime = ChangeTimeToStrSec(endTimeText.Text);
            timer_progress.Start();
        }

        // 타이머_프로그래스바 GUI 업데이트
        private void timer_update_Tick(object sender, EventArgs e)
        {
            startTimeText.Text = ChangeSecToTime(timeController.StartTime);
            endTimeText.Text = ChangeSecToTime(timeController.EndTime);
            currTimeText.Text = ChangeSecToTime(Convert.ToInt32(timeController.CurrTime));
        }

        // 타이머_1초마다 프로그래스바 업데이트
        private void timer_progress_Tick(object sender, EventArgs e)
        {
            int getStart = timeController.StartTime;
            int getEnd = timeController.EndTime;
            int getCurr = Convert.ToInt32(timeController.CurrTime);
            getCurr++;
            Console.WriteLine(getStart);
            Console.WriteLine(getEnd);
            Console.WriteLine(getCurr);
            if (getCurr > getEnd)
            {
                getCurr = getStart;
            }
            Console.WriteLine(ChangeSecToTime(getCurr));
            timeController.CurrTime = getCurr.ToString();
        }

        // 속도 설정
        private void speedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (speedComboBox.SelectedIndex)
            {
                case 0:
                    speedBindText.Text = "속도 (1.0x)";
                    timer_progress.Interval = 1000 / 1;
                    break;

                case 1:
                    speedBindText.Text = "속도 (2.0x)";

                    timer_progress.Interval = 1000 / 2;
                    break;

                case 2:
                    speedBindText.Text = "속도 (4.0x)";
                    timer_progress.Interval = 1000 / 4;
                    break;

                case 3:
                    speedBindText.Text = "속도 (8.0x)";
                    timer_progress.Interval = 1000 / 8;
                    break;

                case 4:
                    speedBindText.Text = "속도 (16.0x)";
                    timer_progress.Interval = 1000 / 16;
                    break;

                case 5:
                    speedBindText.Text = "속도 (32.0x)";
                    timer_progress.Interval = 1000 / 32 ;
                    break;

                default:
                    break;
            }
        }
    }
}
