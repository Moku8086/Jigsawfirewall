using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.Text.RegularExpressions;

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
            blackList.Add("5159a9002a2f758b7b7e44c3f21c464a");

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

        private void Button2_Click(object sender, EventArgs e)
        {
            TaskService tsksrvs = new TaskService();


            foreach (Task tsk in tsksrvs.RootFolder.GetTasks())
            {
                

                textBox1.Text += tsk.Name + " (" + tsk.Folder + ")" + "     " + tsk.Definition.Actions.ToString() + Environment.NewLine;
                //foreach (Trigger trigger in tsk.Definition.Triggers)
                //{

                //}
            }

            //foreach (Task tsk in tsksrvs.AllTasks)
            //{
            //    //TaskDefinition td = tsk. ;
            //    //textBox1.Text += tsk.Name + " (" + tsk.Folder + ")"+ "123" + tsk.GetInstances. + "123" + Environment.NewLine;
                
            //}

        }
    }

       
    }

