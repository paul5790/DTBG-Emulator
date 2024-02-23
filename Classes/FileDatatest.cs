using DTBGEmulator.Classes.DTO;
using DTBGEmulator.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTBGEmulator.Classes
{
    public class FileDatatest
    {
        #region 변수 정의
        private static FileDatatest instance = null;
        // private Dictionary<string, List<string>> fileDataDictionary; // 파일 이름을 키로 갖는 Dictionary
        private string[] filePaths;
        private int selectedFileCount = 0;
        private string firstFileName;
        private string lastFileName;
        private int takenTime;
        private List<int> skipFile = new List<int>();
        private SortedDictionary<string, List<string>> fileDataDictionary;
        private SortedDictionary<string, List<string>> fileDataDictionaryVirtual;


        string formattedStartTime;
        string formattedEndTime;
        string timeControllerStartTime;
        string timeControllerEndTime;

        // virtual
        private string firstFileTime;
        private string lastFileTime;
        private List<string> fileTimes;

        #endregion 변수 정의
        #region 프로퍼티 정의

        public List<int> SkipFile
        {
            get { return skipFile; }
            set { skipFile = value; }
        }
        public string FirstFileName
        {
            get { return firstFileName; }
            set { firstFileName = value; }
        }

        public string LastFileName
        {
            get { return lastFileName; }
            set { lastFileName = value; }
        }

        public int TakenTime
        {
            get { return takenTime; }
            set { takenTime = value; }
        }

        public int SelectedFileCount
        {
            get { return selectedFileCount; }
            set { selectedFileCount = value; }
        }

        public SortedDictionary<string, List<string>> FileDataDictionary
        {
            get { return fileDataDictionary; }
            set { fileDataDictionary = value; }
        }

        public SortedDictionary<string, List<string>> FileDataDictionaryVirtual
        {
            get { return fileDataDictionaryVirtual; }
            set { fileDataDictionaryVirtual = value; }
        }

        #endregion 프로퍼티 정의
        private FileDatatest() { }

        public static FileDatatest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileDatatest();
                }
                return instance;
            }
        }




        /// <summary>
        /// 파일 열고 메타데이터 추출
        /// </summary>
        public bool SelectFile()
        {
            if (fileDataDictionary != null && fileDataDictionary.Count > 0)
            {
                filePaths = null;
                firstFileName = "";
                lastFileName = "";
                fileDataDictionary.Clear();
                fileDataDictionaryVirtual.Clear();
            }
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFileCount = openFileDialog.FileNames.Length;
                    filePaths = openFileDialog.FileNames;

                    // 파일 이름 초기화
                    firstFileName = filePaths.Length > 0 ? Path.GetFileName(filePaths[0]) : string.Empty;
                    lastFileName = filePaths.Length > 0 ? Path.GetFileName(filePaths[filePaths.Length - 1]) : string.Empty;

                    // 파일 이름에서 시간을 분 단위로 계산하여 시간 차이 계산
                    takenTime = CalculateTimeDifference(firstFileName, lastFileName);

                    // 사이 시간 계산
                    CalculateFileTimes();

                    return true; // 파일이 선택되었음을 알림
                }
                else
                {
                    return false; // 파일이 선택되지 않았음을 알림
                }
            }
        }

        private int CalculateTimeDifference(string firstFileName, string lastFileName)
        {
            // 파일 이름에서 시간 부분을 추출하여 분 단위로 변환
            int firstTimeInMinutes = ExtractTimeInMinutes(firstFileName);
            int lastTimeInMinutes = ExtractTimeInMinutes(lastFileName);
            firstFileTime = ExtractHourMinute(firstFileName);
            lastFileTime = ExtractHourMinute(lastFileName);

            // 시간 차이를 절대값으로 반환
            return Math.Abs(lastTimeInMinutes - firstTimeInMinutes) + 1;
        }

        private int ExtractTimeInMinutes(string fileName)
        {
            // 파일 이름에서 숫자 부분을 추출하여 시간과 분으로 나누고 분 단위로 계산
            string[] fileNameParts = Path.GetFileNameWithoutExtension(fileName).Split(' ');
            int hour = int.Parse(fileNameParts[1].Substring(0, 2));
            int minute = int.Parse(fileNameParts[1].Substring(2, 2));

            return hour * 60 + minute;
        }

        private string ExtractHourMinute(string fileName)
        {
            // 파일 이름에서 숫자 부분을 추출하여 시간과 분으로 나누고 분 단위로 계산
            string[] fileNameParts = Path.GetFileNameWithoutExtension(fileName).Split(' ');
            string keyStr = new string(fileNameParts[1].Where(char.IsDigit).ToArray());
            int key = int.Parse(keyStr);

            return keyStr;
        }

        private void CalculateFileTimes()
        {
            fileTimes = new List<string>();

            // 처음 선택된 파일의 시간부터 마지막 선택된 파일의 시간까지 순회하며 시간을 추가
            int firstTime = int.Parse(firstFileTime);
            int lastTime = int.Parse(lastFileTime);
            for (int time = firstTime; time <= lastTime; time++)
            {
                // 시간 포맷에 맞게 변환하여 리스트에 추가
                string timeString = time.ToString().PadLeft(4, '0');
                if (timeString.EndsWith("60")) // 60분이면 다음 시간으로 넘어가야 함
                {
                    time += 40; // 60분이면 00으로 가기 위해 40을 더함 (다음 시간인 00분까지 40분)
                    timeString = time.ToString().PadLeft(4, '0');
                }
                fileTimes.Add(timeString);
            }
        }

        /// <summary>
        /// 데이터 메모리 저장
        /// </summary>
        /// <returns></returns>
        /// 

        //public void PopulateVirtualDictionary()
        //{
        //    // 새로운 SortedDictionary 생성
        //    fileDataDictionaryVirtual = new SortedDictionary<string, List<string>>(fileDataDictionary);

        //    // fileTimes 리스트값 중에 fileDataDictionary의 key값이 아닌 값을 찾아서 처리
        //    foreach (string time in fileTimes)
        //    {
        //        if (!fileDataDictionary.ContainsKey(time))
        //        {
        //            // fileDataDictionary에 포함되지 않은 키를 가진 경우 빈 리스트를 값으로 설정
        //            //fileDataDictionaryVirtual[time] = new List<string>(Enumerable.Repeat("", 60));
        //            fileDataDictionaryVirtual[time] = new List<string>();
        //        }
        //    }
        //    Console.WriteLine(fileDataDictionaryVirtual);
        //}

        public bool LoadFile()
        {
            fileDataDictionaryVirtual = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            fileDataDictionary = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            skipFile.Clear();
            Stopwatch stopwatch = new Stopwatch();
            // 각 파일에 대한 처리를 위해 반복
            try
            {
                foreach (string filePath in filePaths)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    ProcessFile(filePath);
                    stopwatch.Stop();
                    // Console.WriteLine($"Code execution time: {stopwatch.ElapsedMilliseconds} ms");
                }
                // fileTimes에는 있지만 filePaths의 fileName에 없는 경우에 대해 처리
                int check = 0;
                foreach (string fileTime in fileTimes)
                {
                    if (!fileDataDictionaryVirtual.ContainsKey(fileTime))
                    {
                        fileDataDictionaryVirtual.Add(fileTime, new List<string>()); // 빈 리스트 추가
                        skipFile.Add(check);
                    }
                    check++;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ProcessFile(string filePath)
        {
            
            string fileContent = ReadFile(filePath);
            List<string> filePackets = SplitIntoPackets(fileContent);

            // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
            string[] fileNameParts = Path.GetFileNameWithoutExtension(filePath).Split(' ');
            string fileName = new string(fileNameParts[1].Where(char.IsDigit).ToArray());

            // 현재 파일의 패킷을 전체 리스트에 추가
            fileDataDictionaryVirtual.Add(fileName, filePackets);
            fileDataDictionary.Add(fileName, filePackets);
        }

        //public bool LoadFile()
        //{
        //    fileDataDictionary = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
        //    fileDataDictionaryVirtual = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
        //    // 각 파일에 대한 처리를 위해 반복
        //    try
        //    {
        //        foreach (string filePath in filePaths)
        //        {
        //            ProcessFile(filePath);
        //        }
        //        PopulateVirtualDictionary();

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //private void ProcessFile(string filePath)
        //{

        //    string fileContent = ReadFile(filePath);
        //    List<string> filePackets = SplitIntoPackets(fileContent);

        //    // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
        //    string[] fileNameParts = Path.GetFileNameWithoutExtension(filePath).Split(' ');
        //    string fileName = new string(fileNameParts[1].Where(char.IsDigit).ToArray());

        //    // 현재 파일의 패킷을 전체 리스트에 추가
        //    fileDataDictionary.Add(fileName, filePackets);
        //}

        private string ReadFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file asynchronously: {ex.Message}");
                return string.Empty;
            }
        }

        private List<string> SplitIntoPackets(string fileContent)
        {
            // Split the content into packets based on "}\r\n{" and remove leading/trailing whitespace
            List<string> packets = new List<string>(fileContent.Split(new string[] { "}\r\n{", "}\n{", "}{" }, StringSplitOptions.None));

            // Iterate through each packet and process it
            for (int i = 0; i < packets.Count; i++)
            {
                // Trim leading and trailing whitespaces
                packets[i] = packets[i].Trim();

                // Add opening brace to the first packet
                if (i == 0)
                {
                    packets[i] = packets[i] + "\r\n}";
                }
                else
                {
                    packets[i] = "{\r\n  " + packets[i] + "\r\n}";
                }

                // Add closing brace to the last packet
                if (i == packets.Count - 1)
                {
                    packets[i] = packets[i].TrimEnd('}');
                }
            }

            return packets;
        }

    }
}
