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
        private string[] filePaths;
        private string filePath;
        private string folderPath;
        private List<string> filePackets;
        private string startDateStr;
        private string endDateStr;
        private string startTimeStr;
        private string endTimeStr;
        private string storage;
        private List<string> allFilePackets;
        private int selectedFileCount;
        private int totalPacketCount;
        Dictionary<string, List<string>> dictPackets = new Dictionary<string, List<string>>();

        private Dictionary<string, string> folderContents = new Dictionary<string, string>(); // 폴더 이름과 내용을 저장하는 Dictionary

        public void SelectFolder()
        {
            // FolderBrowserDialog를 사용하여 폴더 선택
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder containing text files.";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {

                    // 모든 파일의 패킷을 담을 리스트 초기화
                    allFilePackets = new List<string>();

                    folderPath = folderBrowserDialog.SelectedPath;
                    // 폴더 내의 모든 텍스트 파일 경로를 filePaths 배열에 저장
                    filePaths = Directory.GetFiles(folderPath, "*.txt");

                    // 폴더 안의 파일 갯수를 selectedFileCount 변수에 할당
                    selectedFileCount = filePaths.Length;

                    foreach (var filePath in Directory.GetFiles(folderPath, "*.txt"))
                    {
                        // filePath를 현재 선택한 파일로 변경
                        this.filePath = filePath;

                        // 기존 코드 유지
                        SplitIntoPackets();

                        // 현재 파일의 패킷을 전체 리스트에 추가
                        allFilePackets.AddRange(filePackets);

                        CalculateFileSize();
                    }
                    // 추가: List에 담긴 변수 갯수 (전체) 설정
                    totalPacketCount = allFilePackets.Count;

                    Console.WriteLine(selectedFileCount);
                    Console.WriteLine("패킷 파일" + totalPacketCount);

                    // Get the first packet's Start value
                    string startDate = ExtractStartEndDate(allFilePackets[0], "Start");

                    // Get the last packet's End value
                    string endDate = ExtractStartEndDate(allFilePackets[allFilePackets.Count - 1], "End");

                    startDateStr = ConvertToLocalTime(startDate).ToString("yyyy.MM.dd. HH:mm:ss");
                    endDateStr = ConvertToLocalTime(endDate).ToString("yyyy.MM.dd. HH:mm:ss");
                    startTimeStr = ConvertToLocalTime(startDate).ToString("HH:mm:ss");
                    endTimeStr = ConvertToLocalTime(endDate).ToString("HH:mm:ss");

                    Console.WriteLine(startDateStr);
                    Console.WriteLine(endDateStr);
                }
            }
        }
        // 패키지 별로 리스트에 저장
        private void SplitIntoPackets()
        {
            try
            {
                // 선택한 파일의 내용을 읽어와서 fileContent에 저장
                string fileContent = File.ReadAllText(filePath);

                // Split the content into packets based on "}\r\n{" and remove leading/trailing whitespace
                filePackets = new List<string>(fileContent.Split(new string[] { "}\r\n{", "}\n{", "}{" }, StringSplitOptions.None));

                // Iterate through each packet and process it
                for (int i = 0; i < filePackets.Count; i++)
                {
                    // Trim leading and trailing whitespaces
                    filePackets[i] = filePackets[i].Trim();

                    // Add opening brace to the first packet
                    if (i == 0)
                    {
                        filePackets[i] = filePackets[i] + "\r\n}";
                    }
                    else
                    {
                        filePackets[i] = "{\r\n  " + filePackets[i] + "\r\n}";
                    }

                    // Add closing brace to the last packet
                    if (i == filePackets.Count - 1)
                    {
                        filePackets[i] = filePackets[i].TrimEnd('}');
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

        // 파일 크기 저장
        private void CalculateFileSize()
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    long totalFileSizeInBytes = 0;

                    foreach (string selectedFile in filePaths)
                    {
                        FileInfo fileInfo = new FileInfo(selectedFile);
                        totalFileSizeInBytes += fileInfo.Length;
                    }

                    storage = FormatFileSize(totalFileSizeInBytes);
                }
                catch (Exception ex)
                {
                    // 파일 크기를 가져오는 도중에 예외가 발생한 경우 처리
                    Console.WriteLine("Error calculating file size: " + ex.Message);
                }
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            const int byteConversion = 1024;

            if (bytes == 0)
                return "0 " + sizeSuffixes[0];

            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, byteConversion)));
            double fileSize = bytes / Math.Pow(byteConversion, place);

            return $"{fileSize:F2} {sizeSuffixes[place]}";
        }

        // 포맷 변환
        private DateTime ConvertToLocalTime(string utcDateTime)
        {
            try
            {
                // UTC 형식의 문자열을 DateTime으로 파싱하고 로컬 시간으로 변환
                DateTime utcTime = DateTime.Parse(utcDateTime);
                DateTime localTime = utcTime.ToLocalTime();
                return localTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UTC 시간을 로컬 시간으로 변환하는 중 오류 발생: {ex.Message}");
                return DateTime.MinValue;
            }
        }

        // Json 변환, Time 찾기
        private string ExtractStartEndDate(string packet, string key)
        {
            try
            {
                JObject jsonPacket = JObject.Parse(packet);
                return jsonPacket["Package"]["Header"]["TimeSpan"][key].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting {key} from packet: {ex.Message}");
                return string.Empty;
            }
        }
        public DataDTO GenerateDataDTO()
        {
            // 파일을 선택하지 않았거나 null인 경우 null을 반환
            if (string.IsNullOrEmpty(filePath) || filePaths == null || filePaths.Length == 0)
            {
                // 여기에 처리를 추가할 수 있습니다.
                Console.WriteLine("파일을 선택해주세요.");
                return null;
            }

            DataDTO dto = new DataDTO
            {
                //FilePackets = allFilePackets,
                FileCount = selectedFileCount,
                PacketCount = totalPacketCount
            };

            return dto;
        }

    }
}
