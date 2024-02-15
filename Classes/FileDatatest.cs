using DTBGEmulator.Classes.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        // private Dictionary<string, List<string>> fileDataDictionary; // 파일 이름을 키로 갖는 Dictionary
        private string[] filePaths = null;
        private int selectedFileCount = 0;
        private int totalPacketCount = 0;
        private string firstFileName;
        private string lastFileName;
        private int takenTime;
        private SortedDictionary<string, List<string>> fileDataDictionary;

        public void SelectFile()
        {
            // OpenFileDialog를 사용하여 텍스트 파일 선택
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

                    // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
                    string[] firstfileNameParts = Path.GetFileNameWithoutExtension(firstFileName).Split(' ');
                    string firstfileDate = new string(firstfileNameParts[1].Where(char.IsDigit).ToArray());

                    // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
                    string[] lastfileNameParts = Path.GetFileNameWithoutExtension(lastFileName).Split(' ');
                    string lastfileDate = new string(lastfileNameParts[1].Where(char.IsDigit).ToArray());

                    Console.WriteLine("첫번째 파일" + firstfileDate);
                    Console.WriteLine("마지막 파일" + lastfileDate);
                    takenTime = Convert.ToInt32(lastfileDate) - Convert.ToInt32(firstfileDate) + 1;
                    Console.WriteLine("소요 시간: " + takenTime + "분");
                }
            }
        }

        public async Task LoadFile()
        {
            fileDataDictionary = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            // 각 파일에 대한 처리를 위해 반복
            foreach (string filePath in filePaths)
            {
                await ProcessFileAsync(filePath);
            }

            // 추가: List에 담긴 변수 갯수 (전체) 설정
            totalPacketCount = fileDataDictionary.Values.Sum(list => list.Count);

            foreach (var key in fileDataDictionary.Keys)
            {
                Console.WriteLine($"Key: {key}");
            }

            // LoadFile 메서드 안에서 fileDataDictionary의 특정 key 값에 대한 value를 출력하는 부분입니다.
            foreach (var kvp in fileDataDictionary)
            {
                if (kvp.Key == "1000") // key가 '1000'인 것을 찾습니다.
                {
                    Console.WriteLine($"Key: {kvp.Key}");
                    foreach (var value in kvp.Value)
                    {
                        //Console.WriteLine(value);
                    }
                }
                else { Console.WriteLine("존재 x"); }
            }

            Console.WriteLine(selectedFileCount);
            Console.WriteLine("패킷 파일" + totalPacketCount);
        }

        private async Task ProcessFileAsync(string filePath)
        {
            // 비동기적으로 파일 읽고 처리
            string fileContent = await ReadFileAsync(filePath);
            List<string> filePackets = SplitIntoPackets(fileContent);

            // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
            string[] fileNameParts = Path.GetFileNameWithoutExtension(filePath).Split(' ');
            string fileName = new string(fileNameParts[1].Where(char.IsDigit).ToArray());

            // 현재 파일의 패킷을 전체 리스트에 추가
            fileDataDictionary.Add(fileName, filePackets);
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

        private async Task<string> ReadFileAsync(string filePath)
        {
            try
            {
                // StreamReader를 사용하여 비동기적으로 파일 읽기
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file asynchronously: {ex.Message}");
                return string.Empty;
            }
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

        //public DataDTO GenerateDataDTO()
        //{
        //    // 파일을 선택하지 않았거나 null인 경우 null을 반환
        //    if (filePaths == null || filePaths.Length == 0)
        //    {
        //        // 여기에 처리를 추가할 수 있습니다.
        //        Console.WriteLine("파일을 선택해주세요.");
        //        return null;
        //    }

        //    DataDTO dto = new DataDTO
        //    {
        //        FilePackets = fileDataDictionary,
        //        FileCount = selectedFileCount,
        //        PacketCount = totalPacketCount,
        //        FirstFileName = firstFileName,
        //        LastFileName = lastFileName,
        //        takenTime = takenTime
        //    };

        //    return dto;
        //}
    }
}
