using DTBGEmulator.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DTBGEmulator.Classes
{
    internal class SettingClass
    {
        #region 변수 선언
        private static SettingClass mInstance = null;                       // Instance

        public string UdpTargetIPAddress { get; set; }   // Target IP Address for UDP
        public int UdpTargetPortNum { get; set; } = 12345;               // Recive Port Number for UDP

        #endregion 변수 선언


        private SettingClass()
        {
            // Read settings
            ReadSettings();
        }

        public static SettingClass GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new SettingClass();
            }

            return mInstance;
        }

        private void ReadSettings()
        {
            try
            {
                // Open XML file
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Get main node
                XmlNode node = doc.SelectSingleNode("/Settings/Values");

                // Get values

                UdpTargetIPAddress = node.SelectSingleNode("UdpTargetIP").InnerText;
                UdpTargetPortNum = int.Parse(node.SelectSingleNode("UdpTargetPortNum").InnerText);
                // ==================================================================================================================================
            }
            catch (Exception) { }
        }

        public void SaveSettings()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

            // 디렉토리 존재 식별 및 생성 
            DirectoryInfo dirInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));
            if (!dirInfo.Exists)
            {
                Directory.CreateDirectory(dirInfo.FullName);
            }

            try
            {
                // Read xml file 
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.Load(filePath);

                // Set values
                XmlNode node = doc.SelectSingleNode("Settings/Values");

                node.SelectSingleNode("UdpTargetIP").InnerText = UdpTargetIPAddress;
                node.SelectSingleNode("UdpTargetPortNum").InnerText = UdpTargetPortNum.ToString();

                doc.Save(filePath);
            }
            catch (Exception) { }
        }

    }
}
