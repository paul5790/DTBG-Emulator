﻿using DTBGEmulator.Classes.DTO;
using DTBGEmulator.Properties;
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
        private static FileDatatest instance = null;
        // private Dictionary<string, List<string>> fileDataDictionary; // 파일 이름을 키로 갖는 Dictionary
        private string[] filePaths = null;
        private int selectedFileCount = 0;
        private string firstFileName;
        private string lastFileName;
        private int takenTime;
        private SortedDictionary<string, List<string>> fileDataDictionary;

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

                    takenTime = Math.Abs(Convert.ToInt32(firstfileDate) - Convert.ToInt32(lastfileDate)) + 1;
                }
            }
        }

        /// <summary>
        /// 데이터 메모리 저장
        /// </summary>
        /// <returns></returns>
        public bool LoadFile()
        {
            fileDataDictionary = new SortedDictionary<string, List<string>>(); // 파일 데이터를 담을 Dictionary 초기화
            // 각 파일에 대한 처리를 위해 반복
            try
            {
                foreach (string filePath in filePaths)
                {
                    ProcessFileAsync(filePath);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ProcessFileAsync(string filePath)
        {
            // 비동기적으로 파일 읽고 처리
            string fileContent = ReadFileAsync(filePath);
            List<string> filePackets = SplitIntoPackets(fileContent);

            // 파일 이름에서 숫자 부분만 추출 (예: "2023-11-09 1003_FleetNormalLog" -> "1003")
            string[] fileNameParts = Path.GetFileNameWithoutExtension(filePath).Split(' ');
            string fileName = new string(fileNameParts[1].Where(char.IsDigit).ToArray());

            // 현재 파일의 패킷을 전체 리스트에 추가
            fileDataDictionary.Add(fileName, filePackets);
        }

        private string ReadFileAsync(string filePath)
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
    }
}
