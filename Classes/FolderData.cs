using DTBGEmulator.Classes.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTBGEmulator.Classes
{
    public class FolderData
    {
        #region 변수 정의
        private static FolderData instance = null;

        private string[] filePathsAIS = null;
        private string[] filePathsOnboard = null;
        private string[] filePathsWeather = null;
        private string[] filePathsTarget = null;
        List<string[]> allFilePaths = new List<string[]>();

        private string firstFileName;
        private string lastFileName;
        private int takenTime;
        private List<int> skipFile = new List<int>();
        private List<int> isFile = new List<int>();
        private SortedDictionary<string, List<string>> folderDataDictionary;

   
        private SortedDictionary<string, List<string>> folderDataDictionaryAIS;
        private SortedDictionary<string, List<string>> folderDataDictionaryOnboard;
        private SortedDictionary<string, List<byte[]>> folderDataDictionaryWeather;
        private SortedDictionary<string, List<byte[]>> folderDataDictionaryTarget;
        private string folderPath;
        private int loadingValue;

        private string filePath;

        // virtual
        private List<string> firstFilesTime;    
        private List<string> lastFilesTime;    
        private string firstFileTime;
        private string lastFileTime;
        private List<string> fileTimes;
        private List<string> allFilePackets;


        // 파일 갯수
        private List<int> selectedTotalFileCount;
        private int selectedFileCount;
        private int selectedOnboardFileCount;
        private int selectedAISFileCount;
        private int selectedWeatherFileCount;
        private int selectedTargetFileCount;
        private int totalPacketCount;
        #endregion 변수 정의
        #region 프로퍼티 정의

        public SortedDictionary<string, List<string>> FolderDataDictionaryAIS
        {
            get { return folderDataDictionaryAIS; }
            set { folderDataDictionaryAIS = value; }
        }

        public SortedDictionary<string, List<string>> FolderDataDictionaryOnboard
        {
            get { return folderDataDictionaryOnboard; }
            set { folderDataDictionaryOnboard = value; }
        }

        public SortedDictionary<string, List<byte[]>> FolderDataDictionaryWeather
        {
            get { return folderDataDictionaryWeather; }
            set { folderDataDictionaryWeather = value; }
        }

        public SortedDictionary<string, List<byte[]>> FolderDataDictionaryTarget
        {
            get { return folderDataDictionaryTarget; }
            set { folderDataDictionaryTarget = value; }
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

        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
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

        public List<int> SkipFile
        {
            get { return skipFile; }
            set { skipFile = value; }
        }

        public List<int> IsFile
        {
            get { return isFile; }
            set { isFile = value; }
        }

        public List<int> SelectedTotalFileCount
        {
            get { return selectedTotalFileCount; }
            set { selectedTotalFileCount = value; }
        }

        public SortedDictionary<string, List<string>> FolderDataDictionary
        {
            get { return folderDataDictionary; }
            set { folderDataDictionary = value; }
        }

        public SortedDictionary<string, List<string>> FolderDataDictionaryVirtual
        {
            get { return folderDataDictionaryOnboard; }
            set { folderDataDictionaryOnboard = value; }
        }

        public int LoadingValue
        {
            get { return loadingValue; }
            set { loadingValue = value; }
        }

        #endregion 프로퍼티 정의
        private FolderData() { }

        public static FolderData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FolderData();
                }
                return instance;
            }
        }

        /// <summary>
        /// 폴더안의 파일 열고 메타데이터 추출
        /// </summary>
        public bool SelectFolder()
        {
            if (folderDataDictionaryOnboard != null && folderDataDictionaryOnboard.Count > 0)
            {
                // filePaths = null;
                filePathsAIS = null;
                filePathsOnboard = null;
                filePathsWeather = null;
                filePathsTarget = null;
                selectedFileCount = 0;
                firstFileName = "";
                lastFileName = "";
                allFilePaths.Clear();
                folderDataDictionaryOnboard.Clear();
                folderDataDictionaryAIS.Clear();
                folderDataDictionaryWeather.Clear();
                folderDataDictionaryTarget.Clear();
            }
            // FolderBrowserDialog를 사용하여 폴더 선택
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder containing text files.";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedTotalFileCount = new List<int> { selectedOnboardFileCount, selectedAISFileCount, selectedTargetFileCount, selectedWeatherFileCount };
                        firstFilesTime = new List<string> { "", "", "", "" };
                        // 기존 배열들을 리스트에 추가
                        allFilePaths.Add(filePathsAIS);
                        allFilePaths.Add(filePathsOnboard);
                        allFilePaths.Add(filePathsWeather);
                        allFilePaths.Add(filePathsTarget);

                        folderPath = folderBrowserDialog.SelectedPath;
                        Console.WriteLine(folderPath);

                        // if (folderPath.Length >= 6 && folderPath.Substring(0, 6).All(char.IsDigit))
                        if (true)
                        {
                            string[] requiredFolders = { "AIS", "Onboard", "TargetInfo", "WeatherInfo" };

                            // 선택한 폴더 안에 필요한 폴더들을 확인하고, 없는 폴더는 생성
                            foreach (string folderName in requiredFolders)
                            {
                                string lfolderPath = Path.Combine(folderPath, folderName);
                                if (!Directory.Exists(lfolderPath))
                                {
                                    Directory.CreateDirectory(lfolderPath);
                                    Console.WriteLine($"폴더 '{folderName}'가 생성되었습니다.");
                                }
                                else
                                {
                                    Console.WriteLine($"폴더 '{folderName}'이(가) 이미 존재합니다.");
                                }
                            }


                            int minTime = 100000;
                            int maxTime = 0;
                            // 폴더 내의 모든 폴더 경로를 subDirectories 배열에 저장

                            string[] subDirectories = Directory.GetDirectories(folderPath);

                            for (int i = 0; i < subDirectories.Length; i++)
                            {
                                allFilePaths[i] = Directory.GetFiles(subDirectories[i]);
                                selectedTotalFileCount[i] = allFilePaths[i].Length;
                                selectedFileCount += allFilePaths[i].Length;

                                if (allFilePaths[i].Length > 0)
                                {
                                    string firstfile = allFilePaths[i].Length > 0 ? Path.GetFileName(allFilePaths[i][0]) : string.Empty;
                                    int firsttime = Convert.ToInt32(ExtractHourMinute(firstfile));
                                    if (firsttime < minTime)
                                    {
                                        minTime = firsttime;
                                        firstFileName = allFilePaths[i].Length > 0 ? Path.GetFileName(allFilePaths[i][0]) : string.Empty;
                                    }

                                    string lastfile = allFilePaths[i].Length > 0 ? Path.GetFileName(allFilePaths[i][allFilePaths[i].Length - 1]) : string.Empty;
                                    int lasttime = Convert.ToInt32(ExtractHourMinute(lastfile));
                                    if (lasttime > maxTime)
                                    {
                                        maxTime = lasttime;
                                        lastFileName = allFilePaths[i].Length > 0 ? Path.GetFileName(allFilePaths[i][allFilePaths[i].Length - 1]) : string.Empty;
                                    }
                                }
                            }


                            // AIS 폴더 파일 갯수
                            // selectedFileCount = allFilePaths[0].Length;

                            // 파일 이름에서 시간을 분 단위로 계산하여 시간 차이 계산
                            takenTime = CalculateTimeDifference(firstFileName, lastFileName);

                            folderPath = folderBrowserDialog.SelectedPath;

                            // 사이 시간 계산
                            CalculateFileTimes();

                            loadingValue = 0;

                            return true; // 파일이 선택되었음을 알림 
                        }
                        else
                        {
                            MessageBox.Show("올바르지 않은 형식의 파일 선택입니다.");
                            return false; // 예외 발생 시 false 반환
                        }
                    }
                    catch (Exception ex)
                    {
                        // 예외 처리
                        MessageBox.Show("올바르지 않은 형식의 파일 선택입니다.");
                        Console.WriteLine("Error occurred while selecting folder: " + ex.Message);
                        return false; // 예외 발생 시 false 반환
                    }
                }
                else
                {
                    MessageBox.Show("파일이 선택되지 않았습니다.");
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
            int hour = int.Parse(fileNameParts[2].Substring(0, 2));
            int minute = int.Parse(fileNameParts[2].Substring(2, 2));

            return hour * 60 + minute;
        }

        private string ExtractHourMinute(string fileName)
        {
            // 파일 이름에서 숫자 부분을 추출하여 시간과 분으로 나누고 분 단위로 계산
            string[] fileNameParts = Path.GetFileNameWithoutExtension(fileName).Split(' ');
            string keyStr = new string(fileNameParts[2].Where(char.IsDigit).ToArray());
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
        public async Task LoadFile()
        {
            folderDataDictionaryAIS = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            folderDataDictionaryOnboard = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            folderDataDictionaryWeather = new SortedDictionary<string, List<byte[]>>(); // 파일 데이터를 담을 Dictionary 초기화
            folderDataDictionaryTarget = new SortedDictionary<string, List<byte[]>>(); // 파일 데이터를 담을 Dictionary 초기화


            skipFile.Clear();
            isFile.Clear();
            // 각 파일에 대한 처리를 위해 반복
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    foreach (string filePath in allFilePaths[i])
                    {
                        ProcessFile(filePath);
                        loadingValue++;
                    }
                }
                // fileTimes에는 있지만 filePaths의 fileName에 없는 경우에 대해 처리
                int check = 0;
                foreach (string fileTime in fileTimes)
                {
                    bool oSkip = false;
                    bool aSkip = false;
                    bool wSkip = false;
                    bool tSkip = false;
                    if (!folderDataDictionaryOnboard.ContainsKey(fileTime))
                    {
                        folderDataDictionaryOnboard.Add(fileTime, new List<string>()); // 빈 리스트 추가
                        oSkip = true;
                    }
                    if (!folderDataDictionaryAIS.ContainsKey(fileTime))
                    {
                        folderDataDictionaryAIS.Add(fileTime, new List<string>()); // 빈 리스트 추가
                        aSkip = true;
                    }
                    if (!folderDataDictionaryWeather.ContainsKey(fileTime))
                    {
                        folderDataDictionaryWeather.Add(fileTime, new List<byte[]>()); // 빈 리스트 추가
                        wSkip = true;
                    }
                    if (!folderDataDictionaryTarget.ContainsKey(fileTime))
                    {
                        folderDataDictionaryTarget.Add(fileTime, new List<byte[]>()); // 빈 리스트 추가
                        tSkip = true;
                    }

                    if (oSkip && aSkip && wSkip && tSkip)
                    {
                        skipFile.Add(check);
                    }
                    else
                    {
                        isFile.Add(check);
                    }
                    check++;
                }
            }
            catch (Exception e)
            {

            }
        }

        private void ProcessFile(string filePath)
        {
            string[] fileNameParts = Path.GetFileNameWithoutExtension(filePath).Split(' ');

            string dataType = fileNameParts[0];
            // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
            string fileName = new string(fileNameParts[2].Where(char.IsDigit).ToArray());

            int chunkSize = 1024;
           
            if (dataType == "AIS")
            {
                string fileContent = ReadTextFile(filePath);
                List<string> filePackets = SplitAISPackets(fileContent);
                folderDataDictionaryAIS.Add(fileName, filePackets);
            }
            else if (dataType == "Onboard")
            {
                string fileContent = ReadTextFile(filePath);
                List<string> filePackets = SplitOnboardPackets(fileContent);
                folderDataDictionaryOnboard.Add(fileName, filePackets);
            }
            else if (dataType == "WeatherInfo")
            {
                byte[] binContent = ReadBinaryFile(filePath);
                List<byte[]> chunks = ChunkByteArray(binContent, chunkSize);
                folderDataDictionaryWeather.Add(fileName, chunks);
            }
            else if (dataType == "TargetInfo")
            {
                byte[] binContent = ReadBinaryFile(filePath);
                List<byte[]> chunks = ChunkByteArray(binContent, chunkSize);
                folderDataDictionaryTarget.Add(fileName, chunks);
            }
            else
            {
                //
            }
        }

        private string ReadTextFile(string filePath)
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

        private byte[] ReadBinaryFile(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading binary file: {ex.Message}");
                return null;
            }
        }

        private List<string> SplitAISPackets1(string data)
        {
            List<string> packets = new List<string>();

            string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder currentPacket = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // 첫 번째 라인이거나 이전 라인이 빈 라인인 경우 새로운 패킷 시작
                if (i == 0 || string.IsNullOrWhiteSpace(lines[i - 1]))
                {
                    currentPacket.Clear();
                }

                currentPacket.AppendLine(line);

                // 현재 라인이 빈 라인이고 이후 라인도 빈 라인이면 패킷 추가
                if (string.IsNullOrWhiteSpace(line) && (i == lines.Length - 1 || string.IsNullOrWhiteSpace(lines[i + 1])))
                {
                    packets.Add(currentPacket.ToString().Trim());
                }
            }

            return packets;
        }

        private List<string> SplitAISPackets(string data)
        {
            List<string> packets = new List<string>(data.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None));

            return packets;
        }


        private List<string> SplitOnboardPackets(string fileContent)
        {
            // Split the content into packets based on "}\r\n{" and remove leading/trailing whitespace
            List<string> packets = new List<string>(fileContent.Split(new string[] { "}\r\n-----\r\n{" }, StringSplitOptions.None));

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

        private List<byte[]> ChunkByteArray(byte[] array, int chunkSize)
        {
            List<byte[]> chunks = new List<byte[]>();

            for (int i = 0; i < array.Length; i += chunkSize)
            {
                int size = Math.Min(chunkSize, array.Length - i);
                byte[] chunk = new byte[size];
                Array.Copy(array, i, chunk, 0, size);
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
