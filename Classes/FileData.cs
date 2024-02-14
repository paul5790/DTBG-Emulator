using DTBGEmulator.Classes.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTBGEmulator.Classes
{
    public class FileData
    {
        private Dictionary<string, List<string>> fileDataDictionary; // 파일 이름을 키로 갖는 Dictionary

        Dictionary<string, List<List<string>>> dictionaryOfListsOfLists;

        private string[] filePaths = null;
        private int selectedFileCount = 0;
        private int totalPacketCount = 0;
        private string firstFileName;
        private string lastFileName;
        private int takenTime;

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
                    Console.WriteLine("소요 시간: " + takenTime+"분");
                }
            }
        }

        public async Task LoadFile()
        {
            fileDataDictionary = new Dictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            dictionaryOfListsOfLists = new Dictionary<string, List<List<string>>>();

            List<int> allFileKeys = new List<int>();
            // 각 파일에 대한 처리를 위해 반복
            foreach (string filePath in filePaths)
            {
                await ProcessFileAsync(filePath);
            }

            // 추가: List에 담긴 변수 갯수 (전체) 설정
            totalPacketCount = fileDataDictionary.Values.Sum(list => list.Count);

            foreach (var key in dictionaryOfListsOfLists.Keys)
            {
                Console.WriteLine($"Key: {key}");
            }

            //// 2번째 키의 리스트 안의 첫 번째 리스트 출력
            //if (dictionaryOfListsOfLists.Count > 1)
            //{
            //    var secondKey = dictionaryOfListsOfLists.Keys.ElementAt(1);
            //    var firstListInSecondKey = dictionaryOfListsOfLists[secondKey][59];
            //    if (firstListInSecondKey != null)
            //    {
            //        Console.WriteLine("2번째 키의 첫 번째 리스트:");
            //        foreach (var item in firstListInSecondKey)
            //        {
            //            Console.WriteLine(item);
            //        }
            //    }
            //}

            // key 값이 1003인 것 중에서 30번째 값을 출력
            //var key1003 = dictionaryOfListsOfLists["1003"];
            //if (key1003 != null && key1003.Count > 30)
            //{
            //    var thirtiethValue = key1003[29]; // 리스트는 0부터 시작하므로 30번째 값은 인덱스 29에 위치합니다.
            //    Console.WriteLine("1003 키의 30번째 값:");
            //    foreach (var item in thirtiethValue)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("1003 키의 30번째 값이 존재하지 않습니다.");
            //}

            Console.WriteLine(selectedFileCount);
            Console.WriteLine("패킷 파일" + totalPacketCount);
        }

        private async Task ProcessFileAsync(string filePath)
        {
            // 비동기적으로 파일 읽고 처리
            string fileContent = await ReadFileAsync(filePath);
            List<List<string>> filePackets = SplitIntoPackets(fileContent);

            // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
            string[] fileNameParts = Path.GetFileNameWithoutExtension(filePath).Split(' ');
            string fileName = new string(fileNameParts[1].Where(char.IsDigit).ToArray());

            // 현재 파일의 패킷을 전체 리스트에 추가
            dictionaryOfListsOfLists.Add(fileName, filePackets);
        }

        private List<List<string>> SplitIntoPackets(string fileContent)
        {
            // Split the content into packets based on "}\r\n{" and remove leading/trailing whitespace
            List<string> packets = new List<string>(fileContent.Split(new string[] { "}\r\n{", "}\n{", "}{" }, StringSplitOptions.None));
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
            List<List<string>> listOfLists1 = new List<List<string>>();
            int currentValue = 1; // 초기 값 설정
            int secData;
            if(packets.Count % 60 == 0)
            {
                secData = packets.Count / 60;
            }
            else
            {
                secData = packets.Count / 60 + 1;
            }
            Console.WriteLine(secData);
            for (int i = 0; i < 60; i++)
            {
                List<string> newList = new List<string>();
                for (int j = 1; j <= secData; j++)
                {
                    if (currentValue - 1 >= packets.Count)
                    {
                        if (newList.Count > 0)
                        {
                            listOfLists1.Add(newList);
                        }
                        // 패킷 인덱스가 리스트 범위를 벗어나면 중단하고 현재까지 생성된 리스트 반환
                        return listOfLists1;
                    }
                    newList.Add(packets[currentValue - 1]);
                    currentValue++; // 1부터 88까지 순차적으로 반복되도록 계산
                }
                listOfLists1.Add(newList);
            }
            return listOfLists1;
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

        public DataDTO GenerateDataDTO()
        {
            // 파일을 선택하지 않았거나 null인 경우 null을 반환
            if (filePaths == null || filePaths.Length == 0)
            {
                // 여기에 처리를 추가할 수 있습니다.
                Console.WriteLine("파일을 선택해주세요.");
                return null;
            }

            DataDTO dto = new DataDTO
            { 
                FilePacketsData = dictionaryOfListsOfLists,
                FilePackets = fileDataDictionary,
                FileCount = selectedFileCount,
                PacketCount = totalPacketCount,
                FirstFileName = firstFileName,
                LastFileName = lastFileName,
                takenTime = takenTime
            };

            return dto;
        }

    }
}
