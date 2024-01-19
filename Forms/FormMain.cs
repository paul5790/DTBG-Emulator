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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DTBGEmulator
{
    public partial class MainForm : Form
    {
        #region 변수 정의
        SettingDTO settingDTO = new SettingDTO(); // SettingDTO 인스턴스
       
        private FileData fileData = new FileData(); // FileData 인스턴스
        private FolderData folderData = new FolderData(); // FolderData 인스턴스

        private ManualResetEvent pauseEvent = new ManualResetEvent(true); // 초기 상태는 신호가 올라가 있음

        DataDTO dto;

        // 쓰레드 관련
        Thread udpSenderThread;

        Setting settingForm; // setting 폼

        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치

        // 
        string ipAddress;

        // 속도
        int dataSpeed;

        // timecotroller 데이터
        string currTime;
        string TotalTime;

        // 파일, 폴더 데이터
        List<string> filePackets;
        int packetCount;
        string startTime;
        string endTime;
        string storageSize;

        // 데이터 재생
        string runState = "stop";

        #endregion 변수 정의
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            currTime = "00:00:00";
            TotalTime = "01:00:00";

            timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
            timeController.CurrTime = ChangeTimeToStrSec(currTime);
            timer_update.Start();

            speedComboBox.SelectedIndex = 0;

            // 버튼 설정
            UpdateButtonState("stop");
        }

        private void udpSender(string data)
        {
            try
            {
                // UDP 클라이언트 생성 (임의의 IP 주소와 포트 사용)
                UdpClient udpClient = new UdpClient();

                // 전송할 데이터를 바이트 배열로 변환
                byte[] byteData = Encoding.UTF8.GetBytes(data);

                // 서버의 IP 주소와 포트 번호
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                int serverPort = 12345;

                // 데이터 전송
                udpClient.Send(byteData, byteData.Length, new IPEndPoint(serverIP, serverPort));

                // UDP 클라이언트 종료
                udpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending UDP data: {ex.Message}");
            }
        }

        private void UdpSenderThread()
        {
            // 예시: 10초에 한 번씩 데이터를 전송하는 무한 루프
            int idx = 0;
            while (true)
            {
                int sleepTime;
                if (packetCount > 60)
                {
                    sleepTime = dataSpeed / (packetCount / 60);
                }
                else
                {
                    // packetCount가 60 이하인 경우에는 packetCount를 기반으로 60까지의 시간으로 변환하여 계산
                    sleepTime = dataSpeed * (60 / packetCount);
                }
                if (idx == packetCount)
                {
                    idx = 0;
                }

                // 일시정지 여부 확인
                pauseEvent.WaitOne();

                // 예시 데이터 생성 (실제로는 필요한 데이터를 가져와서 사용)
                string dataToSend = $"{filePackets[idx]}";

                // Console.WriteLine($"확인{dataToSend}");
                Console.WriteLine($"확인{sleepTime}");
                Console.WriteLine($"확인{Convert.ToInt32(sleepTime)}");

                // UDP 데이터 전송
                udpSender(dataToSend);

                idx++;

                // 1초 대기
                Thread.Sleep(Convert.ToInt32(sleepTime));
            }
        }


        private void pictureBox_Close_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("ss", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialog == DialogResult.OK)
            {
                if (udpSenderThread != null && udpSenderThread.IsAlive)
                {
                    udpSenderThread.Abort();
                }
                Application.Exit();
            }
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            // 버튼을 클릭했을 때 모달로 표시될 SettingForm 인스턴스 생성
            settingForm = new Setting(settingDTO);

            // MainForm의 정 가운데 계산
            int mainFormCenterX = this.Left + (this.Width - settingForm.Width) / 2;
            int mainFormCenterY = this.Top + (this.Height - settingForm.Height) / 2 - 20;

            // SettingForm 위치 설정
            settingForm.StartPosition = FormStartPosition.Manual;
            settingForm.Location = new Point(mainFormCenterX, mainFormCenterY);

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


        // 파일 데이터 처리
        private void addFileBtn_Click(object sender, EventArgs e)
        {
            // 쓰레드 실행중이면 정지
            if (udpSenderThread != null && udpSenderThread.IsAlive)
            {
                udpSenderThread.Abort();
            }
            // addFileBtn 클릭 시 FileData 클래스의 SelectFile 메서드 호출
            fileData.SelectFile();
            // 선택된 파일의 데이터를 읽어와서 사용하거나 저장할 수 있음
            dto = fileData.GenerateDataDTO();
            // null 체크 추가
            if (dto != null)
            {
                filePackets = dto.FilePackets;
                packetCount = filePackets.Count;
                Console.WriteLine("패킷메인" + packetCount);

                startTime = dto.StartDateStr;
                endTime = dto.EndDateStr;
                storageSize = dto.Storage;
                dataInfoTextbox.Text = $"데이터 정보\r\n시작 시간 : {startTime}\r\n종료시간 : {endTime}\r\n용량 : {storageSize}";

                currTime = "00:00:00";
                TotalTime = "00:10:00";

                timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
                timeController.CurrTime = ChangeTimeToStrSec(currTime);
            }
            else
            {
                // null일 때의 처리
                Console.WriteLine("파일을 선택해주세요.");
            }
        }

        private void addFolderBtn_Click(object sender, EventArgs e)
        {
            // 쓰레드 실행중이면 정지
            if (udpSenderThread != null && udpSenderThread.IsAlive)
            {
                udpSenderThread.Abort();
            }
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

        #region 동작 버튼 이벤트 (시작, 일시정지, 정지)
        private void UpdateButtonState(string newState)
        {
            string runImageKey, pauseImageKey, stopImageKey;
            bool runEnabled, pauseEnabled, stopEnabled;

            switch (newState)
            {
                case "run":
                    runImageKey = "run_c";
                    pauseImageKey = "pause_n";
                    stopImageKey = "stop_n";
                    runEnabled = false;
                    pauseEnabled = true;
                    stopEnabled = true;
                    break;
                case "pause":
                    runImageKey = "run_n";
                    pauseImageKey = "pause_c";
                    stopImageKey = "stop_n";
                    runEnabled = true;
                    pauseEnabled = false;
                    stopEnabled = true;
                    break;
                case "stop":
                    runImageKey = "run_n";
                    pauseImageKey = "pause_c";
                    stopImageKey = "stop_c";
                    runEnabled = true;
                    pauseEnabled = false;
                    stopEnabled = false;
                    break;
                default:
                    return;
            }

            // 버튼 이미지 변경
            runBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(runImageKey);
            pauseBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(pauseImageKey);
            stopBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(stopImageKey);

            // 버튼 상태
            runState = newState;
            runBtn.Enabled = runEnabled;
            pauseBtn.Enabled = pauseEnabled;
            stopBtn.Enabled = stopEnabled;
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            if (packetCount > 0)
            {
                timer_progress.Start();
                // 쓰레드가 실행 중이 아니라면 시작
                if (udpSenderThread == null || !udpSenderThread.IsAlive)
                {
                    udpSenderThread = new Thread(UdpSenderThread);
                    udpSenderThread.Start();
                }
                else
                {
                    // 일시정지 중이라면 다시 시작
                    pauseEvent.Set();
                }
                UpdateButtonState("run");
            }
            else
            {
                MessageBox.Show("형식에 맞는 파일을 선택해주세요");
            }
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            if (runState == "run")
            {
                timer_progress.Stop();
                pauseEvent.Reset();
                UpdateButtonState("pause");
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (runState == "run" || runState == "pause")
            {
                // 쓰레드 종료
                if (udpSenderThread != null && udpSenderThread.IsAlive)
                {
                    udpSenderThread.Abort();
                    timer_progress.Stop();
                    TotalTime = "01:00:00";
                    currTime = "00:00:00";
                    timeController.CurrTime = ChangeTimeToStrSec(currTime);
                    timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
                    UpdateButtonState("stop");
                }
            }
        }

        #endregion 동작 버튼 이벤트 (시작, 일시정지, 정지)


        // 타이머_프로그래스바 GUI 업데이트
        private void timer_update_Tick(object sender, EventArgs e)
        {
            // 변환 시도
            currTimeText.Text = ChangeSecToTime(Convert.ToInt32(timeController.CurrTime));
        }

        // 타이머_1초마다 프로그래스바 업데이트
        private void timer_progress_Tick(object sender, EventArgs e)
        {
            int getStart = timeController.StartTime;
            int getEnd = timeController.EndTime;
            int getCurr = Convert.ToInt32(timeController.CurrTime);
            getCurr++;
            if (getCurr > getEnd)
            {
                getCurr = getStart;
            }
            timeController.CurrTime = getCurr.ToString();
        }

        // 속도 설정
        private void speedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 각 인덱스에 대응하는 데이터 속도 배열
            int[] speedValues = { 1, 2, 4, 8, 16, 32 };

            // 선택된 인덱스를 이용하여 데이터 속도 설정
            if (speedComboBox.SelectedIndex >= 0 && speedComboBox.SelectedIndex < speedValues.Length)
            {
                dataSpeed = 1000 / speedValues[speedComboBox.SelectedIndex];
                timer_progress.Interval = dataSpeed;
            }
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void currTimeText_Click(object sender, EventArgs e)
        {

        }
    }
}
