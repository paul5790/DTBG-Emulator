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
        private string filePath;
        private string fileContent;
        private DateTime earliestStartTime;

        public async Task SelectFile()
        {
            // OpenFileDialog를 사용하여 텍스트 파일 선택
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    await ReadFileContentAsync();
                    FindEarliestStartTime();
                }
            }
        }

        private async Task ReadFileContentAsync()
        {
            // 선택한 파일의 내용을 읽어와서 fileContent에 저장
            try
            {
                fileContent = await Task.Run(() => File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        private void FindEarliestStartTime()
        {
            // 정규 표현식으로 "Start" 값을 추출
            var regex = new Regex("\"Start\":\\s*\"(\\d{4}-\\d{2}-\\d{2}T\\d{2}:\\d{2}:\\d{2}\\.\\d{6}Z)\"");
            var matches = regex.Matches(fileContent);

            DateTime earliestStartTime = DateTime.MaxValue.AddDays(-1);

            foreach (Match match in matches)
            {
                string startTimeStr = match.Groups[1].Value;
                Console.WriteLine($"Raw StartTime: {startTimeStr}");

                DateTime startTime;
                if (DateTime.TryParseExact(startTimeStr, "yyyy-MM-ddTHH:mm:ss.ffffffZ", null, System.Globalization.DateTimeStyles.None, out startTime))
                {
                    if (startTime < earliestStartTime)
                    {
                        earliestStartTime = startTime;
                    }
                }
                else
                {
                    Console.WriteLine("날짜와 시간을 파싱할 수 없습니다.");
                }
            }

            if (earliestStartTime != DateTime.MaxValue.AddDays(-1))
            {
                Console.WriteLine($"가장 빠른 시작 시간: {earliestStartTime}");
            }
            else
            {
                Console.WriteLine("시작 시간을 찾을 수 없습니다.");
            }
        }

        public string GetFileContent()
        {
            return fileContent;
        }

        public DateTime GetstartTime()
        {
            return earliestStartTime;
        }

        public int GetPackageCount()
        {
            if (fileContent != null)
            {
                int count = 0;
                int index = fileContent.IndexOf("Package");

                while (index != -1)
                {
                    count++;
                    index = fileContent.IndexOf("Package", index + 1);
                }

                return count;
            }

            return 0;
        }
    }
}
