using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace firewall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String startMenu = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            List<String> blackList = new List<string>();
            blackList.Add("7f1698bab066b764a314a589d338daae");

            foreach (String tempPath in System.IO.Directory.GetFiles(startMenu))
            {
                String tempMd5;
                tempMd5 = CalculateMD5(tempPath);
                if (blackList.Contains(tempMd5))
                {
                    MessageBox.Show("偵測到惡意檔案");
                    Trace.WriteLine("偵測到惡意檔案" + "    " + tempPath, "MD5-掃描");
                }

                Trace.WriteLine(tempMd5 + "    " + tempPath, "MD5-紀錄");
            }
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
