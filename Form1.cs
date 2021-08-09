using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using SetProxy;

namespace Watcherr
{
    public partial class Form1 : Form
    {
        double counterfix=35;
        double counter = 0;
        string name = "";
        int m = 0;
        public List<string> file_text = new List<string>();
        string html = "";
        bool green = false;
        int timeout = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public void proxyCheck()
        { 
           
        }
        public void nav()
        {
            webBrowser2.DocumentText = "";
            webBrowser1.DocumentText = "";
             green = false;
            string proxy = file_text.First();
            file_text.RemoveAt(0);
            WinInetInterop.SetConnectionProxy(proxy);
              webBrowser1.DocumentText = html;

        //webBrowser1.Refresh();
        //webBrowser1.Navigate("https://www.youtube.com/watch?v="+VideoTag);
        //  webBrowser2.Navigate("https://you-itech.fr/spencer/ModMenu/green.html");
       webBrowser2.Navigate(" https://www.youtube.com/watch?v="+VideoTag);
            m++;
            toolStripStatusLabel2.Text = m.ToString();
            label2.Text = proxy;
            counter = counterfix;
            timerzone.Text = counter.ToString();
            timer1.Start();
            timeout = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate("https://youtu.be/" + VideoTag + "?autoplay=1");
            nav();
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            //MessageBox.Show(webBrowser2.DocumentText);
         if(webBrowser2.DocumentText.Contains("unusual traffic") || webBrowser2.DocumentText.Contains("Refresh the page") || webBrowser2.DocumentText.Contains("checkTLSError") || webBrowser2.DocumentText.Contains("Secure"))
            {
               nav();
            }
            //if (webBrowser2.DocumentText.Contains("green")) green = true;
            else timeout++;
            if (timeout > int.Parse(VideoLengths.Text)) nav();
            if (webBrowser1.Document != null && green)
            {
                counter--;
                timerzone.Text = counter.ToString();
            }
            if (counter == 0)
            {
                if (file_text.Count() > 0) nav();
                else
                {
                    timerzone.Text = counter.ToString();
                    timer1.Stop();
                    MessageBox.Show("Job Done :) ! ");

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            webBrowser2.DocumentText = "";
            openFileDialog1.Title = "Choose Proxy List (.txt) / ip:port";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) ;
            {
                name = openFileDialog1.FileName;
                file_text = File.ReadAllLines(name).ToList();
            }
            toolStripStatusLabel5.Text = file_text.Count().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            proxyCheck();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            html = "<html><head>" + "<meta content='IE=Edge,chrome=1' http-equiv='X-UA-Compatible'/>" + "<iframe id='video' src= 'https://www.youtube.com/embed/" + VideoTag.Text + "' width='600' height='300' frameborder='0' allowfullscreen></iframe>" + "</body></html>";
            webBrowser1.DocumentText = html;
           html = "<html><head>" + "<meta content='IE=Edge,chrome=1' http-equiv='X-UA-Compatible'/>" + "<iframe id='video' src= 'https://www.youtube.com/embed/" + VideoTag.Text + "?autoplay=1' width='600' height='300' frameborder='0' allowfullscreen></iframe>" + "</body></html>";
            counterfix = double.Parse(VideoLengths.Text);
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
            if (webBrowser2.DocumentText.Contains("unusual traffic") || webBrowser2.DocumentText.Contains("Refresh the page") || webBrowser2.DocumentText.Contains("checkTLSError") || webBrowser2.DocumentText.Contains("Secure"))
            {
               
                nav();
            }
            else
            {
                green = true;
            }
        }
    }
}
