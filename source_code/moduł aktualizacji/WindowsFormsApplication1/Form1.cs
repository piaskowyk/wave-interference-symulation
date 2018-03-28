using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            label2.Text = "";
            label3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (WebClient myWebClient = new WebClient())
            {
                Ping pg = new Ping();
                bool status;
                try
                {
                    PingReply reply = pg.Send("216.58.209.67");
                    status = true;
                }
                catch
                {
                    status = false;
                }
                if (status)
                {
                    try
                    {
                        myWebClient.DownloadFile("http://piaskowy.ayz.pl/fale/version.txt", "tmp/version.txt");
                    }
                    catch { }
                }
                else
                {
                    MessageBox.Show("Brak połączenia z internetem.");
                    Close();
                }
            }
            if (File.Exists("tmp/version.txt"))
            {
                string[] tab1 = File.ReadAllLines("tmp/version.txt");
                int server = int.Parse(tab1[0]);
                if (File.Exists("src/version.txt"))
                {
                    string[] tab2 = File.ReadAllLines("src/version.txt");
                    int user = int.Parse(tab2[0]);
                    if (server > user)
                    {
                        label1.Text = "Dostępna jest nowa wersja programu.";
                        button1.Enabled = false;
                        button2.Enabled = true;
                    }
                    else
                    {
                        label1.Text = "Posiadasz aktualną wersję programu";
                        button1.Enabled = false;
                        button4.Enabled = true;
                    }
                }
            }
            else
            {
                label1.Text = "Nieudna próba połączenia z serwerem.";
                button4.Enabled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            label1.Text = "";
            startDownload();
            button2.Enabled = false;
        }

        Thread thread;
        WebClient client;
        private void startDownload()
        {
            
            thread = new Thread(() => {
                client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri("http://piaskowy.ayz.pl/fale/fale.zip"), "tmp/fale.zip");
            });
            thread.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                label2.Text = "Pobrano: " + Math.Round(percentage).ToString() + "%";
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                label2.Text = "Aktualizacja gotowa do zainstalowania.";
                button3.Enabled = true;
                client.CancelAsync();
                client.Dispose();
                client = null;
                thread.Abort();
                thread = null;
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("backup"))
            {
                Directory.Delete("backup", true);
            }
            DirectoryInfo d = new DirectoryInfo("./");
            Directory.CreateDirectory("backup");
            foreach (FileInfo file in d.GetFiles())
            {
                if (file.Name != "update.exe") file.MoveTo("backup/"+file.Name);
            }
            foreach (DirectoryInfo directory in d.GetDirectories())
            {
                if(directory.Name!="tmp" && directory.Name != "backup") directory.MoveTo("backup/" + directory.Name);
            }
            FileInfo file2 = new FileInfo("tmp/fale.zip");
            file2.MoveTo(file2.Name);
            ZipStorer zip = ZipStorer.Open("fale.zip", FileAccess.Read);
            List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
            foreach (ZipStorer.ZipFileEntry entry in dir)
            {
                zip.ExtractFile(entry, "./" + entry.FilenameInZip);
            }
            zip.Close();
            Directory.Delete("tmp", true);
            Directory.CreateDirectory("tmp");
            file2.MoveTo("tmp/"+file2.Name);
            button3.Enabled = false;
            button4.Enabled = true;
            label2.Text = "";
            label3.Text = "Zainstalowano aktualizacje.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists("Fale.exe"))
            {
                Process fale = new Process();
                fale.StartInfo.FileName = "Fale.exe";
                fale.Start();
                Application.Exit();
            } 
        }
    }
}
