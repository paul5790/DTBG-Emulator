﻿using DTBGEmulator.Classes;
using DTBGEmulator.Classes.DTO;
using DTBGEmulator.Forms;
using DTBGEmulator.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DTBGEmulator
{
    public partial class MainForm : Form
    {
        #region 변수 정의

        public int test = 0;

        private Setting setting = new Setting();
        private SettingDTO settingDTO = new SettingDTO(); // SettingDTO 인스턴스

        private FileData fileData = new FileData(); // FileData 인스턴스

        private ManualResetEvent pauseEvent = new ManualResetEvent(true); // 초기 상태는 신호가 올라가 있음

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
        private int dustTime;
        private int filecontrol;
        int invokeControl = 1;

        // timecotroller 데이터   // "00 : 00 : 00"
        private string currTime;  
        private string TotalTime; 
        private string timeControllerStartTime;
        private string timeControllerEndTime;
        private string timeControllerCurrText;

        // timecontroller와 데이터와 파일 데이터 연동
        public int lastHours; // 8
        public int firstHours; // 4
        public int lastMinutes; // 8
        public int firstMinutes; // 4
        public int lastSeconds; // 59
        public int firstSeconds; // 0
        public int currentHours; // 0
        public int currentMinutes; // 0
        public int currentSeconds; // 0
        private int immutablefirstMinutes; // 4
        private int immutablelastMinutes; // 4
        private int immutablefirstHours; // 4
        private int immutablelastHours; // 4

        // timecontroller 사용제어
        private bool available = false;
        public bool currEvent = false;


        // 파일, 폴더 데이터
        private SortedDictionary<string, List<string>> filePackets;
        private Dictionary<string, List<List<string>>> filePacketsData;
        private int fileCount;
        private int takenTime;
        private int packetCount;
        private string startTime;
        private string endTime;


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
            currTime = "00 : 00 : 00";
            TotalTime = "00 : 01 : 00";

            timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
            timeController.CurrTime = ChangeTimeToStrSec(currTime);
            timer_update.Start();

            speedComboBox.SelectedIndex = 3;

            // 버튼 설정
            UpdateButtonState("default");
        }

        private void UpdateDataViewTextBox(string data)
        {
            try
            {
                if (dataViewTextBox.InvokeRequired)
                {
                    dataViewTextBox.BeginInvoke((MethodInvoker)(() => { UpdateDataViewTextBox(data); }));
                }
                else
                {
                    dataViewTextBox.Text = data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating dataViewTextBox: {ex.Message}");
            }
        }

        private void udpSender(string data, string ip, string port)
        {
            try
            {
                using (UdpClient udpClient = new UdpClient())
                {
                    // 서버의 IP 주소와 포트 번호
                    IPAddress serverIP = IPAddress.Parse(ip);
                    int serverPort = Convert.ToInt32(port);

                    // 전송할 데이터를 바이트 배열로 변환
                    byte[] byteData = Encoding.UTF8.GetBytes(data);

                    // MTU(Maximum Transmission Unit) 크기를 고려하여 패킷을 나눔
                    int mtu = 1400; // 조절 가능한 MTU 크기

                    if (byteData.Length > mtu)
                    {
                        for (int i = 0; i < byteData.Length; i += mtu)
                        {
                            int remainingBytes = Math.Min(mtu, byteData.Length - i);
                            byte[] segment = new byte[remainingBytes];
                            Array.Copy(byteData, i, segment, 0, remainingBytes);

                            // 데이터 분할 전송
                            udpClient.Send(segment, segment.Length, new IPEndPoint(serverIP, serverPort));
                        }
                    }
                    else
                    {
                        // 데이터 크기가 MTU보다 작으면 전체 데이터를 한 번에 전송
                        udpClient.Send(byteData, byteData.Length, new IPEndPoint(serverIP, serverPort));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending UDP data: {ex.Message}");
            }
        }

        private void UdpSenderThread()
        {
            sdto = setting.GenerateSettingDTO();
            string setip = sdto.shipIPAddress;
            string setport = sdto.shipPort;

            int setTime = 1000;
            int sleepTime = setTime;
            // Stopwatch 객체 생성
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            while (true)
            {
                // 코드 실행 시작 시간 기록
                int sixty = 60;
                int keyCount = filePackets.Count;
                int keyIndex = 1; // 첫 번째 키부터 시작
                //foreach (var kvp in filePackets)
                //for (int i = 0; i < takenTime; i++)
                int defingI;
                int loopI;
                if (currEvent)
                {
                    defingI = (currentMinutes + (currentHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60));
                    loopI = (lastMinutes + (lastHours * 60)) - (firstMinutes + (firstHours * 60)) + 1 + ((firstMinutes + (firstHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60)));
                }
                else
                {
                    defingI = (firstMinutes + (firstHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60));
                    loopI = (lastMinutes + (lastHours * 60)) - (firstMinutes + (firstHours * 60)) + 1 + ((firstMinutes + (firstHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60)));
                }
                for (int i = defingI; i < loopI; i++)
                    {
                    var kvp = filePackets.ElementAt(i);
                    Console.WriteLine($"{keyIndex} 번째 키의 리스트 갯수: {kvp.Value.Count}");

                    int count = 0;
                    int count1 = 0;
                    int loopJ = sixty;
                    int defingK = 0;
                    int loopK = kvp.Value.Count / 60 + 1;
                    if (currEvent)
                    {
                        loopJ = sixty - currentSeconds;
                        count = currentSeconds * loopK;
                        currEvent = false;
                    }
                    else
                    {
                        if (keyIndex == 1)
                        {
                            // 첫번째 루프일때
                            count = firstSeconds * loopK;
                            if (keyCount == keyIndex)
                            {
                                loopJ = lastSeconds - firstSeconds + 1;
                            }
                            else
                            {
                                loopJ = sixty - firstSeconds;
                            }
                        }
                        else if (keyCount == keyIndex)
                        {
                            // 마지막 루프일때
                            loopJ = lastSeconds + 1;
                        }
                        else
                        {
                            // 중간 루프일때
                        }
                    }
                    for (int j = 0; j < loopJ; j++)
                    {
                        stopwatch1.Reset();
                        stopwatch1.Start();
                        stopwatch.Reset();
                        stopwatch.Start();


                        count1++;
                        if (filecontrol == count1)
                        {
                            int getStart = timeController.StartTime;
                            int getEnd = timeController.EndTime;
                            int getCurr = Convert.ToInt32(timeController.CurrTime);
                            getCurr = getCurr += 1;
                            if (getCurr > getEnd)
                            {
                                timeController.CurrTime = getStart.ToString();
                                // 여기~~~~~
                                break; // 루프를 중지하고 다음 while 루프로 이동
                            }
                            timeController.CurrTime = getCurr.ToString();
                            count1 = count1 - filecontrol;
                        }

                        int sendFileNumber = (kvp.Value.Count / 60 + 1) / filecontrol;
                        for (int k = defingK; k < sendFileNumber; k++)
                        {
                            try
                            {
                                // Console.WriteLine(kvp.Value[count].ToString());
                                udpSender(kvp.Value[count].ToString(), setip, setport);

                                count++;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                // 리스트의 인덱스가 범위를 벗어날 때 예외 처리
                                Console.WriteLine("리스트의 인덱스가 범위를 벗어났습니다.");
                                break; // for 루프를 중지하고 다음 키로 이동
                            }
                        }
                        Console.WriteLine(timeController.CurrTime);
                        UpdateDataViewTextBox(kvp.Value[count - 1].ToString());
                        UpdateCurrTimeText();

                        stopwatch.Stop();
                        sleepTime = (1000 / dataSpeed - dustTime) - Convert.ToInt32(stopwatch.ElapsedMilliseconds);
                        if (sleepTime <= 0) sleepTime = 0;
                        Thread.Sleep(sleepTime);
                        


                        // 일시정지 여부 확인
                        pauseEvent.WaitOne();
                        stopwatch.Stop();
                        //Console.WriteLine($"Code execution time: {stopwatch.ElapsedMilliseconds} ms");
                        Console.WriteLine($"Code execution time: {stopwatch1.ElapsedMilliseconds} ms");
                    }
                    keyIndex++;
                    if (keyIndex > keyCount) keyIndex = 1;
                }
            }
        }

        private void UpdateCurrTimeText()
        {
            // IncrementTimeByOneSecond 메서드를 사용하여 시간을 1초씩 증가시킴
            string incrementedTime = IncrementTimeByOneSecond(timeControllerStartTime);

            // dataSpeed에 따라서 Invoke 호출 빈도를 결정
            if (ShouldInvokeUpdate(dataSpeed))
            {
                currTimeText.Invoke((MethodInvoker)delegate
                {
                    // currTimeText에 증가된 시간을 할당
                    currTimeText.Text = incrementedTime;
                });
            }
            invokeControl++;
            // timeControllerCurrText = incrementedTime;
        }

        private bool ShouldInvokeUpdate(int speed)
        {
            // dataSpeed에 따라서 조건에 따른 Invoke 호출 빈도 결정
            if (speed >= 20)
            {
                return invokeControl % 4 == 0;
            }
            else if (speed >= 10 && speed < 20)
            {
                return invokeControl % 2 == 0;
            }
            else
            {
                return true;
            }
        }

        private string IncrementTimeByOneSecond(string time)
        {
            // 공백을 제거하여 시간 문자열 파싱
            string trimmedTime = time.Replace(" ", "");
            TimeSpan currentTime = TimeSpan.ParseExact(trimmedTime, "hh':'mm':'ss", CultureInfo.InvariantCulture);

            // 1초 추가
            currentTime = currentTime.Add(TimeSpan.FromSeconds(Convert.ToInt32(timeController.CurrTime)));

            // TimeSpan을 다시 문자열로 변환
            string incrementedTime = currentTime.ToString(@"hh' : 'mm' : 'ss");

            currentMinutes = Convert.ToInt32(currentTime.ToString(@"hh"));
            currentMinutes = Convert.ToInt32(currentTime.ToString(@"mm"));
            currentSeconds = Convert.ToInt32(currentTime.ToString(@"ss"));

            return incrementedTime;
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

        // 파일 데이터 처리
        private void addFileBtn_Click(object sender, EventArgs e)
        {
            // 쓰레드 실행중이면 정지
            if (udpSenderThread != null && udpSenderThread.IsAlive)
            {
                udpSenderThread.Abort();
            }
            timeController.First = true;
            FileDatatest.Instance.SelectFile();
            Thread dataLoadThread = new Thread(() => FileDatatest.Instance.LoadFile());
            dataLoadThread.Start();
            startTime = FileDatatest.Instance.FirstFileName;
            endTime = FileDatatest.Instance.LastFileName;
            firstFileName.Text = $"{startTime}";
            lastFileName.Text = $"{endTime}";
            // fileCount = dto.FileCount;
            takenTime = FileDatatest.Instance.TakenTime;

            // "yyyyMMdd HHmm" 형식으로 변환
            string formattedStartDateTime = startTime.Substring(0, 4) + startTime.Substring(5, 2) + startTime.Substring(8, 2) + " " + startTime.Substring(11, 4);
            string formattedEndDateTime = endTime.Substring(0, 4) + endTime.Substring(5, 2) + endTime.Substring(8, 2) + " " + endTime.Substring(11, 4);

            // 주어진 형식의 문자열을 DateTime으로 파싱
            DateTime dateStartTime = DateTime.ParseExact(formattedStartDateTime, "yyyyMMdd HHmm", null);
            DateTime dateEndTime = DateTime.ParseExact(formattedEndDateTime, "yyyyMMdd HHmm", null);

            // 새로운 형식의 문자열로 변환
            string formattedStartTime = dateStartTime.ToString("yyyy.MM.dd. HH:mm:ss");
            string formattedEndTime = dateEndTime.ToString("yyyy.MM.dd. HH:mm");
            timeControllerStartTime = dateStartTime.ToString("HH : mm : ss");
            timeControllerEndTime = dateEndTime.ToString("HH : mm ") + ": 59";

            startTimeData.Text = formattedStartTime;
            endTimeData.Text = formattedEndTime + ":59";

            lastMinutes = Convert.ToInt32(dateEndTime.ToString("mm"));
            firstMinutes = Convert.ToInt32(dateStartTime.ToString("mm"));
            lastHours = Convert.ToInt32(dateEndTime.ToString("HH"));
            firstHours = Convert.ToInt32(dateStartTime.ToString("HH"));
            immutablefirstMinutes = Convert.ToInt32(dateStartTime.ToString("mm"));
            immutablelastMinutes = Convert.ToInt32(dateEndTime.ToString("mm"));
            immutablefirstHours = Convert.ToInt32(dateStartTime.ToString("HH"));
            immutablelastHours = Convert.ToInt32(dateEndTime.ToString("HH"));

            timeController.StartFileTime = timeControllerStartTime;
            timeController.EndFileTime = timeControllerEndTime;

            int hours = takenTime / 60;
            int minutes = takenTime % 60;
            if (hours <= 0)
            {
                fullTimeData.Text = $"{minutes}분";
            }
            else
            {
                fullTimeData.Text = $"{hours}시간 {minutes}분";
            }

            firstSeconds = 0;
            lastSeconds = 59;

            // 버튼 설정
            UpdateButtonState("stop");

            // 비동기 로드 후
            filePackets = FileDatatest.Instance.FileDataDictionary;
            packetCount = filePackets.Count;
            Console.WriteLine("패킷메인" + packetCount);

            int fileNum = FileDatatest.Instance.SelectedFileCount;
            currTime = "00 : 00 : 00";
            string totalTimeNum = $"{fileNum * 60 - 1}";

            timeController.TotalTime = totalTimeNum;

            timeController.CurrTime = ChangeTimeToStrSec(currTime);

            timeController.StartRepeatTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerStartTime));
            timeController.EndRepeatTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerEndTime));


            Console.WriteLine(ChangeTimeToStrSec(currTime));
            Console.WriteLine(ChangeTimeToStrSec(timeControllerStartTime));
            Console.WriteLine(ChangeTimeToStrSec(timeControllerEndTime));

            timeControllerCurrText = timeControllerStartTime;
            currTimeText.Text = timeControllerCurrText;
        }

        private void addFolderBtn_Click(object sender, EventArgs e)
        {
            // 쓰레드 실행중이면 정지
            if (udpSenderThread != null && udpSenderThread.IsAlive)
            {
                udpSenderThread.Abort();
            }
            timeController.First = true;
            FolderData.Instance.SelectFolder();
            Thread dataLoadThread = new Thread(() => FolderData.Instance.LoadFile());
            dataLoadThread.Start();
            startTime = FolderData.Instance.FirstFileName;
            endTime = FolderData.Instance.LastFileName;
            firstFileName.Text = $"{startTime}";
            lastFileName.Text = $"{endTime}";
            fileLocation.Text = FolderData.Instance.FolderPath;
            // fileCount = dto.FileCount;
            takenTime = FolderData.Instance.TakenTime;

            // "yyyyMMdd HHmm" 형식으로 변환
            string formattedStartDateTime = startTime.Substring(0, 4) + startTime.Substring(5, 2) + startTime.Substring(8, 2) + " " + startTime.Substring(11, 4);
            string formattedEndDateTime = endTime.Substring(0, 4) + endTime.Substring(5, 2) + endTime.Substring(8, 2) + " " + endTime.Substring(11, 4);

            // 주어진 형식의 문자열을 DateTime으로 파싱
            DateTime dateStartTime = DateTime.ParseExact(formattedStartDateTime, "yyyyMMdd HHmm", null);
            DateTime dateEndTime = DateTime.ParseExact(formattedEndDateTime, "yyyyMMdd HHmm", null);

            // 새로운 형식의 문자열로 변환
            string formattedStartTime = dateStartTime.ToString("yyyy.MM.dd. HH:mm:ss");
            string formattedEndTime = dateEndTime.ToString("yyyy.MM.dd. HH:mm");
            timeControllerStartTime = dateStartTime.ToString("HH : mm : ss");
            timeControllerEndTime = dateEndTime.ToString("HH : mm ") + ": 59";

            startTimeData.Text = formattedStartTime;
            endTimeData.Text = formattedEndTime + ":59";

            lastMinutes = Convert.ToInt32(dateEndTime.ToString("mm"));
            firstMinutes = Convert.ToInt32(dateStartTime.ToString("mm"));
            immutablefirstMinutes = Convert.ToInt32(dateStartTime.ToString("mm"));
            immutablelastMinutes = Convert.ToInt32(dateEndTime.ToString("mm"));

            timeController.StartFileTime = timeControllerStartTime;
            timeController.EndFileTime = timeControllerEndTime;

            int hours = takenTime / 60;
            int minutes = takenTime % 60;
            if (hours <= 0)
            {
                fullTimeData.Text = $"{minutes}분";
            }
            else
            {
                fullTimeData.Text = $"{hours}시간 {minutes}분";
            }

            firstSeconds = 0;
            lastSeconds = 59;

            // 버튼 설정
            UpdateButtonState("stop");

            // 비동기 로드 후
            filePackets = FolderData.Instance.FolderDataDictionary;
            packetCount = filePackets.Count;
            Console.WriteLine("패킷메인" + packetCount);

            int fileNum = FolderData.Instance.SelectedFileCount;
            currTime = "00 : 00 : 00";
            string totalTimeNum = $"{fileNum * 60 - 1}";

            timeController.TotalTime = totalTimeNum;

            timeController.CurrTime = ChangeTimeToStrSec(currTime);

            timeController.StartRepeatTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerStartTime));
            timeController.EndRepeatTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerEndTime));


            Console.WriteLine(ChangeTimeToStrSec(currTime));
            Console.WriteLine(ChangeTimeToStrSec(timeControllerStartTime));
            Console.WriteLine(ChangeTimeToStrSec(timeControllerEndTime));

            timeControllerCurrText = timeControllerStartTime;
            currTimeText.Text = timeControllerCurrText;

        }


        // 시간(문자) -> 시간초(숫자)
        private string ChangeTimeToStrSec(string dateTime)
        {
            int sec_HH = Convert.ToInt32(dateTime.Substring(0, 2)) * 3600;
            int sec_mm = Convert.ToInt32(dateTime.Substring(5, 2)) * 60;
            int sec_ss = Convert.ToInt32(dateTime.Substring(10, 2));

            string strSec = (sec_HH + sec_mm + sec_ss).ToString();

            return strSec;
        }

        // 시간초(숫자) -> 시간(문자)
        private string ChangeSecToTime(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            currentHours = hours;
            currentMinutes = minutes;
            currentSeconds = seconds;
            string timeString = $"{hours:D2} : {minutes:D2} : {seconds:D2}";

            return timeString;
        }

        public void updateCurrTime(string currTime)
        {
            int updatecurrTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerStartTime)) + Convert.ToInt32(currTime);

            currTimeText.Text = ChangeSecToTime(updatecurrTime);
        }

        


        // 타이머_프로그래스바 GUI 업데이트
        //private void timer_update_Tick(object sender, EventArgs e)
        //{
        //    // 변환 시도
        //    // currTimeText.Text = ChangeSecToTime(Convert.ToInt32(timeController.CurrTime));
        //}

        // 타이머_1초마다 프로그래스바 업데이트

        // 속도 설정
        private void speedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 각 인덱스에 대응하는 데이터 속도 배열
            int[] speedValues = { 1, 1, 1, 1, 2, 4, 10, 20, 800 };
            int[] fileValues = { 10, 4, 2, 1, 1, 1, 1, 1, 1 };
            int[] dustTimeValues = { 10, 10, 10, 10, 10, 10, 10, 10, 0 };

            // 선택된 인덱스를 이용하여 데이터 속도 설정
            if (speedComboBox.SelectedIndex >= 0 && speedComboBox.SelectedIndex < speedValues.Length)
            {
                dataSpeed = speedValues[speedComboBox.SelectedIndex];
                dustTime = dustTimeValues[speedComboBox.SelectedIndex];
                filecontrol = fileValues[speedComboBox.SelectedIndex];
            }

            if (speedComboBox.SelectedIndex >= 4)
            {
                dataViewCheckBox.Visible = false;
                dataViewTextBox.Visible = false;
                this.Size = new Size(570, 400);
            }
            else
            {
                dataViewCheckBox.Visible = true;
            }
        }

        int seconds = 0;
        private void timer_progress_Tick(object sender, EventArgs e)
        {

            seconds++;

            // TimeSpan을 사용하여 초를 시:분:초 형태의 문자열로 변환
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            string formattedTime = timeSpan.ToString(@"hh\:mm\:ss");

            // 변환된 문자열을 UI에 표시
            // realTime.Text = formattedTime;

        }

        public void threadRestart()
        {
            if (runState == "run")
            {
                udpSenderThread.Abort();

                if (udpSenderThread == null || !udpSenderThread.IsAlive)
                {
                    udpSenderThread = new Thread(UdpSenderThread);
                    udpSenderThread.Start();
                }
            }
        }

        private void dataViewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dataViewCheckBox.Checked)
            {
                // 체크되었을 때의 폼 크기 설정
                dataViewTextBox.Visible = true;
                this.Size = new Size(570, 800);
            }
            else
            {
                // 체크가 해제되었을 때의 폼 크기 설정
                dataViewTextBox.Visible = false;
                this.Size = new Size(570, 400);
            }
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
                    timeController.UseController = false;
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
                    timeController.UseController = true;
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
            if (takenTime > 0)
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
                    timeController.CurrTime = "0";
                    currTimeText.Text = timeControllerStartTime;
                    stopRequested = true;
                    UpdateButtonState("stop");
                }
            }
        }

        #endregion 동작 버튼 이벤트 (시작, 일시정지, 정지)

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
    }
}
