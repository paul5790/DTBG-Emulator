using DTBGEmulator.Classes.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DTBGEmulator.Classes
{
    public class FileData
    {
        private string filePath;
        private List<string> filePackets;
        private DateTime startTime;
        private DateTime endTime;
        private string startTimeStr;
        private string endTimeStr;
        private string storage;

        public void SelectFile()
        {
            // OpenFileDialog를 사용하여 텍스트 파일 선택
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    SplitIntoPackets();
                    CalculateFileSize();
                }
            }
        }

        private void SplitIntoPackets()
        {
            try
            {
                // 선택한 파일의 내용을 읽어와서 fileContent에 저장
                string fileContent = File.ReadAllText(filePath);

                // Split the content into packets based on "}\r\n{" and remove leading/trailing whitespace
                filePackets = new List<string>(fileContent.Split(new string[] { "}\r\n{", "}\n{", "}{" }, StringSplitOptions.None));

                Console.WriteLine("Split Into Packets:");

                // Iterate through each packet and process it
                for (int i = 0; i < filePackets.Count; i++)
                {
                    // Trim leading and trailing whitespaces
                    filePackets[i] = filePackets[i].Trim();

                    // Add opening brace to the first packet
                    if (i == 0)
                    {
                        filePackets[i] = filePackets[i] + "\r\n}";
                        ExtractStartTime(filePackets[i]);
                    }
                    else
                    {
                        filePackets[i] = "{\r\n  " + filePackets[i] +"\r\n}";
                    }

                    // Add closing brace to the last packet
                    if (i == filePackets.Count - 1)
                    {
                        filePackets[i] = filePackets[i].TrimEnd('}');
                        ExtractEndTime(filePackets[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

        private void ExtractStartTime(string packet)
        {
            try
            {
                // Deserialize the JSON to dynamic object
                dynamic jsonObj = JsonConvert.DeserializeObject(packet);

                // Extract the TimeSpan values from the first packet
                startTimeStr = jsonObj.Package.Header.TimeSpan.Start;

                // Output the startTimeStr for debugging
                Console.WriteLine($"StartTimeStr: {startTimeStr}");

                // Use the appropriate format for parsing
                startTime = DateTime.ParseExact(startTimeStr, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeUniversal);

                // Convert to UTC if it's not already
                startTime = startTime.Kind == DateTimeKind.Utc ? startTime : startTime.ToUniversalTime();

                startTimeStr = startTime.ToString("yyyy.MM.dd. HH:mm:ss");

                // Output in the desired format
                Console.WriteLine($"StartTime (Formatted): {startTime.ToString("yyyy.MM.dd. HH:mm:ss")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting start time: {ex.Message}");
            }
        }

        private void ExtractEndTime(string packet)
        {
            try
            {
                // Deserialize the JSON to dynamic object
                dynamic jsonObj = JsonConvert.DeserializeObject(packet);

                // Extract the TimeSpan values from the last packet
                endTimeStr = jsonObj.Package.Header.TimeSpan.End;

                // Output the endTimeStr for debugging
                Console.WriteLine($"EndTimeStr: {endTimeStr}");

                // Use the appropriate format for parsing
                endTime = DateTime.ParseExact(endTimeStr, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeUniversal);

                // Convert to UTC if it's not already
                endTime = endTime.Kind == DateTimeKind.Utc ? endTime : endTime.ToUniversalTime();

                endTimeStr = endTime.ToString("yyyy.MM.dd. HH:mm:ss");

                // Output in the desired format
                Console.WriteLine($"EndTime (Formatted): {endTime.ToString("yyyy.MM.dd. HH:mm:ss")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting end time: {ex.Message}");
            }
        }

        private void CalculateFileSize()
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    // 파일의 크기를 바이트로 가져오기
                    FileInfo fileInfo = new FileInfo(filePath);
                    long fileSizeInBytes = fileInfo.Length;

                    // 파일 크기를 적절한 단위로 변환하여 storage 변수에 할당
                    storage = FormatFileSize(fileSizeInBytes);
                    Console.WriteLine(storage);
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
            double fileSize = Math.Round(bytes / Math.Pow(byteConversion, place), 2);

            return $"{fileSize} {sizeSuffixes[place]}";
        }

        public DataDTO GenerateDataDTO()
        {
            DataDTO dto = new DataDTO
            {
                Storage = storage,
                StartTimeStr = startTimeStr,
                EndTimeStr = endTimeStr,
                FilePackets = filePackets
            };

            return dto;
        }

        //// DataDTO에 값을 설정하는 메서드 추가
        //public void SetDataDTOValues(DataDTO dto)
        //{
        //    dto.Storage = storage;
        //    dto.StartTimeStr = startTimeStr;
        //    dto.EndTimeStr = endTimeStr;
        //    dto.FilePackets = filePackets;
        //}
    }
}
