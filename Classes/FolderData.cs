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
        private string folderPath;
        private Dictionary<string, string> fileContents; // 파일 이름과 내용을 저장하는 Dictionary

        public void SelectFolder()
        {
            // FolderBrowserDialog를 사용하여 폴더 선택
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder containing text files.";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderBrowserDialog.SelectedPath;
                    ReadTextFiles();
                }
            }
        }

        private void ReadTextFiles()
        {
            // 선택한 폴더 내의 모든 텍스트 파일을 읽어와서 fileContents에 저장
            fileContents = new Dictionary<string, string>();

            try
            {
                foreach (var filePath in Directory.GetFiles(folderPath, "*.txt"))
                {
                    string fileName = Path.GetFileName(filePath);
                    string fileContent = File.ReadAllText(filePath);
                    fileContents.Add(fileName, fileContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading text files: {ex.Message}");
            }
        }

        public Dictionary<string, string> GetFileContents()
        {
            return fileContents;
        }
    }
}
