using DTBGEmulator.Classes;
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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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

        private Setting setting = new Setting();
        SettingClass settingClass = SettingClass.GetInstance();

        // 쓰레드 관련
        private Thread udpSenderThread;

        // UI 관련
        private bool mDragForm = false;                     // 폼 이동 플래그
        private Point mMousePosition = Point.Empty;         // 마우스 위치


        // 데이터 속도
        private int dataSpeed;
        private int dustTime;
        private int filecontrol;
        int invokeControl = 1; // invoke 횟수제한

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
        public bool currEvent = false;


        // 파일, 폴더 데이터
        private bool trueFile;
        private SortedDictionary<string, List<string>> fullFilePackets; // 보낼 dictionary 데이터
        private int takenTime;
        private string startTime;
        private string endTime;
        private int fileNum;

        // skip 상태
        bool skipState = false;
        int skipTime = 0;

        // 데이터 재생
        private string runState = "stop";
        private ManualResetEvent pauseEvent = new ManualResetEvent(true); // 초기 상태는 신호가 올라가 있음

        // 데이터 로딩
        private int progressValue;

        #endregion 변수 정의
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            currTime = "00 : 00 : 00";
            TotalTime = "01 : 00 : 00";

            timeController.TotalTime = ChangeTimeToStrSec(TotalTime);
            timeController.CurrTime = ChangeTimeToStrSec(currTime);

            speedComboBox.SelectedIndex = 3;

            // 버튼 설정
            UpdateButtonState("default");
        }

        private void UpdateDataViewTextBox(string data)
        {
            Console.WriteLine("데이터 길이 : " + data.Length);
            if (data.Length > 10000)
            {
                data = data.Substring(0, 5000);
            }
            if (dataViewTextBox.InvokeRequired)
            {
                dataViewTextBox.BeginInvoke((MethodInvoker)(() => { UpdateDataViewTextBox(data); }));
            }
            else
            {
                dataViewTextBox.Text = data;
            }
        }

        private void UpdateLoadStateText(string text)
        {
            if (loadStateText.InvokeRequired)
            {
                loadStateText.Invoke((MethodInvoker)delegate {
                    loadStateText.Text = text;
                });
            }
            else
            {
                loadStateText.Text = text;
            }
        }

        private void udpSender(string data, string ip, int port)
        {
            try
            {
                using (UdpClient udpClient = new UdpClient())
                {
                    // 서버의 IP 주소와 포트 번호
                    IPAddress serverIP = IPAddress.Parse(ip);
                    int serverPort = port;

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
            // sdto = setting.GenerateSettingDTO();
            string setip = settingClass.UdpTargetIPAddress;
            int setport = settingClass.UdpTargetPortNum;
            if (trueFile) fullFilePackets = FileDatatest.Instance.FileDataDictionaryVirtual;
            else fullFilePackets = FolderData.Instance.FolderDataDictionaryVirtual;

            int sleepTime = 1000;
            bool firstTime = true;
            // Stopwatch 객체 생성
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            
            while (true)
            {
                // 코드 실행 시작 시간 기록
                int keyCount = fullFilePackets.Count; // dict의 key갯수 
                int keyIndex = 1; // 첫 번째 키부터 시작 (몇번째 파일이 진행중인지)
                int keyFirst = 1;
                int defingI;
                int loopI;
                
                // 사용자가 조작했는지
                if (currEvent)
                {
                    UpdateCurrTimeText();
                    // if (getCurr % 60 != currentSeconds) continue;
                    // 현재시간 - 파일의 처음시간 = 몇번째 파일부터 loop 시작해야하는지
                    defingI = (currentMinutes + (currentHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60));
                    
                }
                else
                {
                    // 루프 시작시간 - 파일의 처음시간 = 몇번째 파일부터 loop 시작해야하는지
                    defingI = (firstMinutes + (firstHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60));
                }


                loopI = (lastMinutes + (lastHours * 60)) - (firstMinutes + (firstHours * 60)) + 1 + ((firstMinutes + (firstHours * 60)) - (immutablefirstMinutes + (immutablefirstHours * 60)));


                try
                {
                    // 파일단위 (defingI번째 파일부터 loopI번째 파일까지 loop)
                    for (int i = defingI; i < loopI; i++)
                    {
                        // var kvp = fullFilePackets.ElementAt(i);
                        var kvp = fullFilePackets.ElementAt(i);
                        Console.WriteLine($"{keyIndex} 번째 키의 리스트 갯수: {kvp.Value.Count}");
                        // 만약 skip 상태면
                        if (skipState == true)
                        {
                            // 만약 스킵된 곳에 진행바를 놓으면
                            if (kvp.Value.Count == 0)
                            {
                                if (keyFirst == 1 && !currEvent)
                                {
                                    timeController.CurrTime = (Convert.ToInt32(timeController.CurrTime) + 60 - currentSeconds).ToString();
                                    currentSeconds = firstSeconds;
                                    keyFirst++;
                                }
                                else if (currEvent)
                                {
                                    timeController.CurrTime = (Convert.ToInt32(timeController.CurrTime) + 60 - currentSeconds).ToString();
                                    currentSeconds = 0;
                                }
                                else
                                {
                                    timeController.CurrTime = (Convert.ToInt32(timeController.CurrTime) + 60).ToString();
                                    if(timeController.CurrTime == timeController.EndTime.ToString())
                                    {
                                        currEvent = true;
                                        firstTime = true;
                                        break;
                                    }
                                }

                                // 파일값이 존재하는 index까지 계속 스킵
                                keyIndex++;
                                if (i == loopI - 1)
                                {
                                    timeController.CurrTime = "0";
                                    currentMinutes = 0;
                                }
                                continue;
                            }
                            else
                            {
                                keyFirst++;
                            }
                        }

                        int count = 0; // dictionary List의 index값
                        int count1 = 0; // 저배속을 위한 타임 조절
                        int loopJ = 60;
                        int defingK = 0;
                        int loopK;
                        int slowSpeed;
                        int oneSecond = 1000;
                        if (kvp.Value.Count < 60) { 
                            slowSpeed = 1;
                            oneSecond = 1000 * filecontrol;
                        }
                        else { slowSpeed = filecontrol; }

                        if (kvp.Value.Count % 60 == 0)
                        {
                            loopK = (kvp.Value.Count / 60) / slowSpeed;
                        }
                        else
                        {
                            loopK = (kvp.Value.Count / 60 + 1) / slowSpeed;
                        }
                        if (currEvent)
                        {
                            loopJ = 60 - currentSeconds;
                            count = currentSeconds * loopK;
                            currEvent = false;
                        }
                        else
                        {
                            if (keyIndex == 1) // 첫번째 루프인지
                            {
                                count = firstSeconds * loopK; // 초 * 1초당 보낼 갯수 = 시작 count 계산
                                if (keyCount == keyIndex) // 총 파일 갯수 == 현재 진행중인 파일 순번 (마지막 파일인지)
                                {
                                    loopJ = lastSeconds - firstSeconds + 1; // 왜있는지 의문 까먹음.
                                }
                                else
                                {                              // firstSeconds는 구간반복의 처음 Second(초)임
                                    loopJ = 60 - firstSeconds; // 구간반복시간이 34초일때, 반복문을 60 - 34 번만 하기 위해.
                                }
                            }
                            else if (i == keyCount - 1)
                            {
                                // 마지막 루프일때
                                loopJ = lastSeconds + 1;
                            }
                            else
                            {
                                // 중간 루프일때
                            }
                        }
                        loopJ = loopJ * slowSpeed;
                        for (int j = 0; j < loopJ; j++)
                        {
                            stopwatch.Reset();
                            stopwatch.Start();

                            stopwatch1.Reset();
                            stopwatch1.Start();

                            if (kvp.Value.Count > 0)
                            {
                                int sendFileNumber;

                                if (kvp.Value.Count % 60 == 0)
                                {
                                    sendFileNumber = (kvp.Value.Count / 60) / slowSpeed;
                                }
                                else
                                {
                                    sendFileNumber = (kvp.Value.Count / 60 + 1) / slowSpeed;
                                }
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
                                UpdateDataViewTextBox(kvp.Value[count - sendFileNumber].ToString());
                            }
                            else
                            {
                                UpdateDataViewTextBox("데이터 없음");
                            }



                            count1++;
                            if (slowSpeed == count1)
                            {
                                int getStart = timeController.StartTime;
                                int getEnd = timeController.EndTime;
                                int getCurr = Convert.ToInt32(timeController.CurrTime);
                            if (!firstTime)
                            {
                                getCurr++;
                            }
                            else firstTime = false;
                            if (getCurr > getEnd)
                                {
                                    timeController.CurrTime = getStart.ToString();
                                    if (lastSeconds != 59 || lastMinutes != immutablelastMinutes)
                                    {
                                        firstTime = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    timeController.CurrTime = getCurr.ToString();
                                }
                                count1 = count1 - slowSpeed;
                            }


                            UpdateCurrTimeText();


                            stopwatch.Stop();
                            sleepTime = (oneSecond / dataSpeed - dustTime) - Convert.ToInt32(stopwatch.ElapsedMilliseconds);
                            if (sleepTime <= 0) sleepTime = 0;
                            Thread.Sleep(sleepTime);

                            // 일시정지 여부 확인
                            pauseEvent.WaitOne();
                            stopwatch1.Stop();
                            Console.WriteLine($"Code execution time: {stopwatch1.ElapsedMilliseconds} ms");
                        }
                        if (firstTime) break;
                        keyIndex++;
                        if (keyIndex > keyCount) keyIndex = 1;
                    }
                }
                catch(Exception ex) 
                {
                    if (ex is ThreadAbortException)
                    {
                        Console.WriteLine("Abort");
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(ex.Message);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 현재시간 update 및 invoke
        /// </summary>
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

        /// <summary>
        /// 현재시간 invoke 업데이트 속도 조절
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        /// 
        private bool ShouldInvokeUpdate(int speed)
        {
            // dataSpeed에 따라서 조건에 따른 Invoke 호출 빈도 결정
            if (speed > 20) return invokeControl % 15 == 0;
            else if (speed == 20) return invokeControl % 5 == 0;
            else if (speed >= 10 && speed < 20) return invokeControl % 2 == 0;
            else return true;
        }

        /// <summary>
        /// 현재시간 업데이트 변환 함수
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string IncrementTimeByOneSecond(string time)
        {
            // 공백을 제거하여 시간 문자열 파싱
            string trimmedTime = time.Replace(" ", "");
            TimeSpan currentTime = TimeSpan.ParseExact(trimmedTime, "hh':'mm':'ss", CultureInfo.InvariantCulture);

            // 1초 추가
            currentTime = currentTime.Add(TimeSpan.FromSeconds(Convert.ToInt32(timeController.CurrTime) + 60 * skipTime));

            // TimeSpan을 다시 문자열로 변환
            string incrementedTime = currentTime.ToString(@"hh' : 'mm' : 'ss");

            currentHours = Convert.ToInt32(currentTime.ToString(@"hh"));
            currentMinutes = Convert.ToInt32(currentTime.ToString(@"mm"));
            currentSeconds = Convert.ToInt32(currentTime.ToString(@"ss"));

            return incrementedTime;
        }

        /// <summary>
        /// 프로그램 종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 세팅 모달창
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// 시간 대입
        /// </summary>
        /// <param name="dateStartTime"></param>
        /// <param name="dateEndTime"></param>
        private void SetTimeControllerData(DateTime dateStartTime, DateTime dateEndTime)
        {
            lastMinutes = dateEndTime.Minute;
            firstMinutes = dateStartTime.Minute;
            immutablefirstMinutes = dateStartTime.Minute;
            immutablelastMinutes = dateEndTime.Minute;
            lastHours = dateEndTime.Hour;
            firstHours = dateStartTime.Hour;
            immutablefirstHours = dateStartTime.Hour;
            immutablelastHours = dateEndTime.Hour;
        }

        private void SetTimeFormatSetting()
        {
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

            SetTimeControllerData(dateStartTime, dateEndTime);
        }

        /// <summary>
        /// 진행 시간(총시간) 업데이트 함수
        /// </summary>
        /// <param name="time"></param>
        private void SetFullTimeData(int time)
        {
            int hours = time / 60;
            int minutes = time % 60;
            if (hours <= 0)
            {
                fullTimeData.Text = $"{minutes}분";
            }
            else
            {
                fullTimeData.Text = $"{hours}시간 {minutes}분";
            }

        }

        /// <summary>
        /// 타임컨트롤러 관련 함수
        /// </summary>
        private void SetTimeControllerValue()
        {
            currTime = "00 : 00 : 00";
            string totalTimeNum = $"{takenTime * 60 - 1}";
            timeController.FileCount = takenTime;
            timeController.First = true;

            timeController.TotalTime = totalTimeNum;
            timeController.CurrTime = ChangeTimeToStrSec(currTime);

            timeController.StartRepeatTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerStartTime));
            timeController.EndRepeatTime = Convert.ToInt32(ChangeTimeToStrSec(timeControllerEndTime));

            timeControllerCurrText = timeControllerStartTime;
            currTimeText.Text = timeControllerCurrText;

            whiteSpaceCheckBox.Enabled = true;
        }

        private async void LoadFileData()
        {
            timeController.UseController = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            await FileDatatest.Instance.LoadFile();

            stopwatch.Stop();
            Console.WriteLine($"Code execution time: {stopwatch.ElapsedMilliseconds} ms");
            timeController.UseController = true;
            fullFilePackets = FileDatatest.Instance.FileDataDictionaryVirtual;
            timeController.InvalidatePanel();
            UpdateLoadStateText("로딩완료");
            Console.WriteLine("파일 갯수 카운트" + fullFilePackets.Count);
        }


        private async void LoadFolderData()
        {
            timeController.UseController = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            await FolderData.Instance.LoadFile();

            
            stopwatch.Stop();
            Console.WriteLine($"Code execution time: {stopwatch.ElapsedMilliseconds} ms");
            timeController.UseController = true;
            fullFilePackets = FolderData.Instance.FolderDataDictionaryVirtual;
            timeController.InvalidatePanel();
            UpdateLoadStateText("로딩완료");
            Console.WriteLine("파일 갯수 카운트" + fullFilePackets.Count);
        }

        /// <summary>
        /// 파일 데이터 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFileBtn_Click(object sender, EventArgs e)
        {
            fileLocation.Text = "";
            bool fileSelected = FileDatatest.Instance.SelectFile();
            // 파일이 선택되었을 때만 쓰레드를 생성하고 시작
            if (fileSelected)
            {
                trueFile = true;
                timeController.TrueFile = true;
                loadStateText.Text = "로딩 중";

                // 쓰레드 실행중이면 정지
                if (udpSenderThread != null && udpSenderThread.IsAlive)
                {
                    udpSenderThread.Abort();
                }

                Thread dataLoadThread = new Thread(LoadFileData);
                dataLoadThread.Start();


                startTime = FileDatatest.Instance.FirstFileName;
                endTime = FileDatatest.Instance.LastFileName;
                takenTime = FileDatatest.Instance.TakenTime;

                SetTimeFormatSetting();

                firstFileName.Text = $"{startTime}";
                lastFileName.Text = $"{endTime}";
                timeController.StartFileTime = timeControllerStartTime;
                timeController.EndFileTime = timeControllerEndTime;

                SetFullTimeData(takenTime);

                firstSeconds = 0;
                lastSeconds = 59;

                // 버튼 설정
                UpdateButtonState("stop");

                fullFilePackets = FileDatatest.Instance.FileDataDictionaryVirtual;
                fileNum = FileDatatest.Instance.SelectedFileCount;

                SetTimeControllerValue();

                // 데이터 로딩 확인

                loadingProgressBar.Maximum = fileNum;
                timer.Start();
            }
            else
            {
                MessageBox.Show("올바르지 않은 형식의 파일 선택입니다.");
                UpdateButtonState("default");
            }
        }

        private void addFolderBtn_Click(object sender, EventArgs e)
        {
            bool folderSelected = FolderData.Instance.SelectFolder();
            if (folderSelected)
            {
                timeController.TrueFile = false;
                trueFile = false;
                loadStateText.Text = "로딩 중";

                // 쓰레드 실행중이면 정지
                if (udpSenderThread != null && udpSenderThread.IsAlive)
                {
                    udpSenderThread.Abort();
                }
                timeController.First = true;

                Thread dataLoadThread = new Thread(LoadFolderData);
                dataLoadThread.Start();

                startTime = FolderData.Instance.FirstFileName;
                endTime = FolderData.Instance.LastFileName;
                fileLocation.Text = FolderData.Instance.FolderPath;
                takenTime = FolderData.Instance.TakenTime;

                SetTimeFormatSetting();

                firstFileName.Text = $"{startTime}";
                lastFileName.Text = $"{endTime}";
                timeController.StartFileTime = timeControllerStartTime;
                timeController.EndFileTime = timeControllerEndTime;

                SetFullTimeData(takenTime);

                firstSeconds = 0;
                lastSeconds = 59;

                UpdateButtonState("stop");

                fullFilePackets = FolderData.Instance.FolderDataDictionaryVirtual;
                fileNum = FolderData.Instance.SelectedFileCount;

                SetTimeControllerValue();

                // 데이터 로딩 확인

                loadingProgressBar.Maximum = fileNum;
                timer.Start();
            }
            else
            {
                MessageBox.Show("올바르지 않은 형식의 파일 선택입니다.");
                UpdateButtonState("default");
            }
        }


        /// <summary>
        /// 시간(문자) -> 시간초(숫자)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string ChangeTimeToStrSec(string dateTime)
        {
            int sec_HH = Convert.ToInt32(dateTime.Substring(0, 2)) * 3600;
            int sec_mm = Convert.ToInt32(dateTime.Substring(5, 2)) * 60;
            int sec_ss = Convert.ToInt32(dateTime.Substring(10, 2));

            string strSec = (sec_HH + sec_mm + sec_ss).ToString();

            return strSec;
        }

        /// <summary>
        /// 시간초(숫자) -> 시간(문자)
        /// </summary>
        /// <param name="totalSeconds"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 속도 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 각 인덱스에 대응하는 데이터 속도 배열
            int[] speedValues = { 1, 1, 1, 1, 2, 4, 10, 20, 1000 };
            int[] fileValues = { 10, 4, 2, 1, 1, 1, 1, 1, 1 };
            int[] dustTimeValues = { 10, 10, 10, 10, 10, 10, 10, 10, 0 };

            // 선택된 인덱스를 이용하여 데이터 속도 설정
            if (speedComboBox.SelectedIndex >= 0 && speedComboBox.SelectedIndex < speedValues.Length)
            {
                dataSpeed = speedValues[speedComboBox.SelectedIndex];
                dustTime = dustTimeValues[speedComboBox.SelectedIndex];
                filecontrol = fileValues[speedComboBox.SelectedIndex];
                if (runState == "run")
                {
                    currEvent = true;
                    updateCurrTime(timeController.CurrTime);
                    threadRestart();
                }
            }

            if (speedComboBox.SelectedIndex >= 4)
            {
                dataViewCheckBox.Visible = false;
                dataViewTextBox.Visible = false;
                dataViewCheckBox.Checked = false;
                this.Size = new Size(570, 400);
            }
            else
            {
                dataViewCheckBox.Visible = true;
            }
        }

        public void threadRestart()
        {
            if (udpSenderThread != null || udpSenderThread.IsAlive)
            {
                udpSenderThread.Abort();
                udpSenderThread.Join(); // 이전 스레드가 완전히 종료될 때까지 기다림

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
                dataViewText.Visible = true;
                this.Size = new Size(570, 800);
            }
            else
            {
                // 체크가 해제되었을 때의 폼 크기 설정
                dataViewTextBox.Visible = false;
                dataViewText.Visible = false;
                this.Size = new Size(570, 400);
            }
        }


        private void whiteSpaceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (whiteSpaceCheckBox.Checked)
            {
                skipState = true;
                timeController.skipState = true;
                SetFullTimeData(fileNum);
            }
            else
            {
                skipState = false;
                timeController.skipState = false;
                SetFullTimeData(takenTime);
            }
            threadRestart();
        }



        #region 동작 버튼 이벤트 (시작, 일시정지, 정지)
        private void UpdateButtonState(string newState)
        {
            string settingImageKey, runImageKey, pauseImageKey, stopImageKey;
            bool settingEnabled, runEnabled, pauseEnabled, stopEnabled, selectEnabled;

            switch (newState)
            {
                case "default":
                    settingImageKey = "settings_nn";
                    runImageKey = "run_c";
                    pauseImageKey = "pause_c";
                    stopImageKey = "square_c";
                    settingEnabled = true;
                    runEnabled = false;
                    pauseEnabled = false;
                    stopEnabled = false;
                    selectEnabled = true;
                    break;
                case "run":
                    settingImageKey = "settings_ccc";
                    runImageKey = "run_c";
                    pauseImageKey = "pause_n";
                    stopImageKey = "square_n";
                    settingEnabled = false;
                    runEnabled = false;
                    pauseEnabled = true;
                    stopEnabled = true;
                    selectEnabled = false;
                    break;
                case "pause":
                    settingImageKey = "settings_ccc";
                    runImageKey = "run_n";
                    pauseImageKey = "pause_c";
                    stopImageKey = "square_n";
                    settingEnabled = false;
                    runEnabled = true;
                    pauseEnabled = false;
                    stopEnabled = true;
                    selectEnabled = false;
                    break;
                case "stop":
                    settingImageKey = "settings_nn";
                    runImageKey = "run_n";
                    pauseImageKey = "pause_c";
                    stopImageKey = "square_c";
                    settingEnabled = true;
                    runEnabled = true;
                    pauseEnabled = false;
                    stopEnabled = false;
                    selectEnabled = true;
                    break;
                default:
                    return;
            }

            // 버튼 이미지 변경
            settingBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(settingImageKey);
            runBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(runImageKey);
            pauseBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(pauseImageKey);
            stopBtn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(stopImageKey);

            // 버튼 상태
            runState = newState;
            settingBtn.Enabled = settingEnabled;
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
            whiteSpaceCheckBox.Enabled = false;
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {

            if (runState == "run")
            {
                pauseMethod();
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
                    udpSenderThread.Join();
                    timeController.CurrTime = timeController.StartTime.ToString();
                    string newCurrTime = ChangeSecToTime(timeController.StartAction);
                    currTimeText.Text = newCurrTime;
                    pauseEvent.Set();
                    UpdateButtonState("stop");
                    speedComboBox.SelectedIndex = 3;
                }
                whiteSpaceCheckBox.Enabled = true;
            }
        }

        public void pauseMethod()
        {
            if (runState == "run")
            {
                pauseEvent.Reset();
                UpdateButtonState("pause");
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (trueFile) { loadingProgressBar.Value = FileDatatest.Instance.LoadingValue; }
            else { loadingProgressBar.Value = FolderData.Instance.LoadingValue; }
            
            if (progressValue >= loadingProgressBar.Maximum)
            {
                timer.Stop();
            }
        }
    }
}
