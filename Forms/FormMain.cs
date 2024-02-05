using DTBGEmulator.Classes;
using DTBGEmulator.Classes.DTO;
using DTBGEmulator.Forms;
using DTBGEmulator.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DTBGEmulator
{
    public partial class MainForm : Form
    {
        #region 변수 정의

        private Setting setting = new Setting();
        private SettingDTO settingDTO = new SettingDTO(); // SettingDTO 인스턴스
       
        private FileData fileData = new FileData(); // FileData 인스턴스
        private FolderData folderData = new FolderData(); // FolderData 인스턴스

        private ManualResetEvent pauseEvent = new ManualResetEvent(true); // 초기 상태는 신호가 올라가 있음

        private DataDTO dto;
        private SettingDTO sdto;

        // 쓰레드 관련
        private Thread udpSenderThread;

        private Setting settingForm; // setting 폼

        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치

        // 
        private string ipAddress;

        // 속도
        private int dataSpeed;

        // timecotroller 데이터
        private string currTime;
        private string TotalTime;

        // 파일, 폴더 데이터
        private List<string> filePackets;
        private int fileCount;
        private int packetCount;
        private string startTime;
        private string endTime;
        private string storageSize;

        // 데이터 재생
        private string runState = "stop";
        private bool isPaused = false;
        private bool stopRequested = false;

        #endregion 변수 정의
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            currTime = "00:00:00";
            TotalTime = "00:01:00";

            timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
            timeController.CurrTime = ChangeTimeToStrSec(currTime);
            timer_update.Start();

            speedComboBox.SelectedIndex = 0;

            // 버튼 설정
            UpdateButtonState("default");
        }

        private void udpSender(string data, string ip, string port)
        {
            try
            {
                // UDP 클라이언트 생성 (임의의 IP 주소와 포트 사용)
                UdpClient udpClient = new UdpClient();

                // 전송할 데이터를 바이트 배열로 변환
                byte[] byteData = Encoding.UTF8.GetBytes(data);

                // 서버의 IP 주소와 포트 번호
                IPAddress serverIP = IPAddress.Parse(ip);
                int serverPort = Convert.ToInt32(port);

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

        private System.Threading.Timer timer; // 클래스 멤버로 선언
        private int idx = 0;

        private void UdpSenderThread()
        {
            sdto = setting.GenerateSettingDTO();
            if (dto != null)
            {
                string setip = sdto.shipIPAddress;
                string setport = sdto.shipPort;

                int a = (packetCount / 60) + 1;
                int sleepTime = packetCount > 60 ? dataSpeed / (packetCount / 60) : dataSpeed * (60 / packetCount);
                int b = 0;
                

                // TimerCallback 함수를 1초 간격으로 호출하는 타이머 생성
                timer = new System.Threading.Timer(TimerCallback, new Tuple<string, string>(setip, setport), 0, 1000 - 12);
                b++;
            }
            else
            {
                MessageBox.Show("IP 설정을 해주세요");
            }
        }

        private void TimerCallback(object state)
        {
            // TimerCallback 함수에서는 짧은 시간 동안만 블록되어야 함
            Tuple<string, string> parameters = (Tuple<string, string>)state;
            string setip = parameters.Item1;
            string setport = parameters.Item2;

            // 일시정지 여부 확인
            pauseEvent.WaitOne();

            int getStart = timeController.StartTime;
            int getEnd = timeController.EndTime;
            int getCurr = Convert.ToInt32(timeController.CurrTime);
            getCurr++;
            if (getCurr > getEnd)
            {
                getCurr = getStart;
            }
            timeController.CurrTime = getCurr.ToString();

            if (idx == packetCount)
            {
                idx = 0;
            }
            Console.WriteLine($"TimerCallback executed at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            Console.WriteLine(packetCount);
            for (int i = 0; i < 87; i++)
            {
                if (idx == packetCount)
                {
                    idx = 0;
                }
                string dataToSend = filePackets[idx];
                udpSender(dataToSend, setip, setport);
                idx++;
            }
        }


        private void pictureBox_Close_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("프로그램을 종료하시겠습니까?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

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
            

            // MainForm의 정 가운데 계산
            int mainFormCenterX = this.Left + (this.Width - setting.Width) / 2;
            int mainFormCenterY = this.Top + (this.Height - setting.Height) / 2 - 20;

            // SettingForm 위치 설정
            setting.StartPosition = FormStartPosition.Manual;
            setting.Location = new Point(mainFormCenterX, mainFormCenterY);

            // ShowDialog 메서드를 사용하여 모달로 표시
            setting.ShowDialog();
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
        private async void addFileBtn_Click(object sender, EventArgs e)
        {
            // 쓰레드 실행중이면 정지
            if (udpSenderThread != null && udpSenderThread.IsAlive)
            {
                udpSenderThread.Abort();
            }
            // addFileBtn 클릭 시 FileData 클래스의 SelectFile 메서드 호출
            await fileData.SelectFile();
            // 선택된 파일의 데이터를 읽어와서 사용하거나 저장할 수 있음
            dto = fileData.GenerateDataDTO();
            // null 체크 추가
            if (dto != null)
            {
                //filePackets = dto.FilePackets;
                //packetCount = filePackets.Count;
                //Console.WriteLine("패킷메인" + packetCount);

                startTime = dto.FirstFileName;
                endTime = dto.LastFileName;
                Console.WriteLine("시간확인" + dto.FirstFileName + dto.LastFileName);
                firstFileName.Text = $"{startTime}";
                lastFileName.Text = $"{endTime}";
                fileCount = dto.FileCount;

                // "yyyyMMdd HHmm" 형식으로 변환
                string formattedStartDateTime = startTime.Substring(0, 4) + startTime.Substring(5, 2) + startTime.Substring(8, 2) + " " + startTime.Substring(11, 4);
                string formattedEndDateTime = endTime.Substring(0, 4) + endTime.Substring(5, 2) + endTime.Substring(8, 2) + " " + endTime.Substring(11, 4);

                // 주어진 형식의 문자열을 DateTime으로 파싱
                DateTime dateStartTime = DateTime.ParseExact(formattedStartDateTime, "yyyyMMdd HHmm", null);
                DateTime dateEndTime = DateTime.ParseExact(formattedEndDateTime, "yyyyMMdd HHmm", null);

                // 새로운 형식의 문자열로 변환
                string formattedStartTime = dateStartTime.ToString("yyyy.MM.dd. HH:mm:ss");
                string formattedEndTime = dateEndTime.ToString("yyyy.MM.dd. HH:mm");

                startTimeData.Text = formattedStartTime;
                endTimeData.Text = formattedEndTime+":59";

                int hours = fileCount / 60;
                int minutes = fileCount % 60;
                if (hours <= 0)
                {
                    fullTimeData.Text = $"{minutes}분";
                }
                else
                {
                    fullTimeData.Text = $"{hours}시간 {minutes}분";
                }

                //int fileNum = dto.FileCount;
                //currTime = "00:00:00";
                //string totalTimeNum = $"{fileNum * 60}";

                //timeController.TotalTime = totalTimeNum;
                //timeController.CurrTime = ChangeTimeToStrSec(currTime);

                // 버튼 설정
                UpdateButtonState("stop");


                await fileData.LoadFile();
                dto = fileData.GenerateDataDTO();

                if (dto != null)
                {
                    filePackets = dto.FilePackets;
                    packetCount = filePackets.Count;
                    Console.WriteLine("패킷메인" + packetCount);

                    int fileNum = dto.FileCount;
                    currTime = "00:00:00";
                    string totalTimeNum = $"{fileNum * 60}";

                    timeController.TotalTime = totalTimeNum;
                    timeController.CurrTime = ChangeTimeToStrSec(currTime);
                }
                else
                {
                    // null일 때의 처리
                    Console.WriteLine("파일을 선택해주세요.");
                }

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

            dto = folderData.GenerateDataDTO();
            if (dto != null)
            {
                filePackets = dto.FilePackets;
                packetCount = filePackets.Count;

                startTime = dto.StartDateStr;
                endTime = dto.EndDateStr;
                storageSize = dto.Storage;
                fileLocation.Text = $"데이터 정보\r\n시작 시간 : {startTime}\r\n종료시간 : {endTime}\r\n용량 : {storageSize}";


                int fileNum = dto.FileCount;
                currTime = "00:00:00";
                string totalTimeNum = $"{fileNum * 60}";

                timeController.TotalTime = totalTimeNum;
                timeController.CurrTime = ChangeTimeToStrSec(currTime);

                // 버튼 설정
                UpdateButtonState("stop");
            }
            else
            {
                // null일 때의 처리
                Console.WriteLine("파일을 선택해주세요.");
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
            bool runEnabled, pauseEnabled, stopEnabled, selectEnabled;

            switch (newState)
            {
                case "default":
                    runImageKey = "run_c";
                    pauseImageKey = "pause_c";
                    stopImageKey = "stop_c";
                    runEnabled = false;
                    pauseEnabled = false;
                    stopEnabled = false;
                    selectEnabled = true;
                    break;
                case "run":
                    runImageKey = "run_c";
                    pauseImageKey = "pause_n";
                    stopImageKey = "stop_n";
                    runEnabled = false;
                    pauseEnabled = true;
                    stopEnabled = true;
                    selectEnabled = false;
                    break;
                case "pause":
                    runImageKey = "run_n";
                    pauseImageKey = "pause_c";
                    stopImageKey = "stop_n";
                    runEnabled = true;
                    pauseEnabled = false;
                    stopEnabled = true;
                    selectEnabled = false;
                    break;
                case "stop":
                    runImageKey = "run_n";
                    pauseImageKey = "pause_c";
                    stopImageKey = "stop_c";
                    runEnabled = true;
                    pauseEnabled = false;
                    stopEnabled = false;
                    selectEnabled = true;
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
            addFileBtn.Enabled = selectEnabled;
            addFolderBtn.Enabled = selectEnabled;
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
                    TotalTime = "01:00:00";
                    currTime = "00:00:00";
                    timeController.CurrTime = ChangeTimeToStrSec(currTime);
                    timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
                    stopRequested = true;
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

        // 속도 설정
        private void speedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 각 인덱스에 대응하는 데이터 속도 배열
            int[] speedValues = { 1, 2, 4, 8, 16, 32 };

            // 선택된 인덱스를 이용하여 데이터 속도 설정
            if (speedComboBox.SelectedIndex >= 0 && speedComboBox.SelectedIndex < speedValues.Length)
            {
                dataSpeed = speedValues[speedComboBox.SelectedIndex];
            }
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void currTimeText_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        int seconds = 0;
        private void timer_progress_Tick(object sender, EventArgs e)
        {
            
            seconds++;

            // TimeSpan을 사용하여 초를 시:분:초 형태의 문자열로 변환
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            string formattedTime = timeSpan.ToString(@"hh\:mm\:ss");

            // 변환된 문자열을 UI에 표시
            realTime.Text = formattedTime;

        }
    }
}
