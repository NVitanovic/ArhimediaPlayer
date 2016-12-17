using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;


namespace CIITLAB_UltraSonic2016
{
    public partial class frmMain : Form
    {
        protected WebClient client;
        protected Config configuration = null;
        protected bool currentState; //ON - true, //OFF - false
        protected Process videoApplication;
        protected int currentDistance;
        protected int numOfReads;
        protected SerialPort serialClient;
        protected const int RGBMAX = 255;
        protected int lastDistance = 0;
        public frmMain()
        {
            InitializeComponent();
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            StreamReader reader = new StreamReader("config.xml");
            configuration = (Config)serializer.Deserialize(reader);
            reader.Close();
            client = new WebClient();
            currentState = false;
            videoApplication = new Process();
            
            videoApplication.StartInfo.FileName = configuration.applicationPath;
            videoApplication.StartInfo.UseShellExecute = true;
            currentDistance = 0;

            if (!String.IsNullOrEmpty(configuration.logoPath))
            {
                if(File.Exists(configuration.logoPath))
                    pbLogo.Load(configuration.logoPath);
            }
            if (!String.IsNullOrEmpty(configuration.backgroundColor))
            {
                ColorConverter c = new ColorConverter();
                Color color = (Color)c.ConvertFromString(configuration.backgroundColor);
                this.BackColor = Color.FromArgb(color.R, color.B, color.G);
            }
            if (configuration.useWifi)
                client.OpenRead("http://" + configuration.serverAddress);
            else
            {
                try
                {
                    serialClient = new SerialPort();
                    serialClient.PortName = configuration.comPortSettings.port;
                    serialClient.BaudRate = configuration.comPortSettings.baudRate;
                    serialClient.Parity = Parity.None;
                    serialClient.Handshake = Handshake.None;
                    serialClient.DataBits = 8;
                    serialClient.StopBits = StopBits.One;
                    if(!serialClient.IsOpen)
                        serialClient.Open();
                    else
                    {
                        serialClient.Close();
                        serialClient.Open();
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.InnerException + "\n\n\n" + e.Message + "\n\n\n" + e.StackTrace);
                }
            }
            if (configuration.samplingTime <= 10)
                configuration.samplingTime = 100;

            tCheck.Interval = configuration.samplingTime;
            tCheck.Enabled = true;

            lblConsole2.ForeColor = lblConsole.ForeColor = InvertColor(this.BackColor);
            this.WindowState = FormWindowState.Maximized;
        }
        Color InvertColor(Color ColourToInvert)
        {
            return Color.FromArgb(RGBMAX - ColourToInvert.R,
              RGBMAX - ColourToInvert.G, RGBMAX - ColourToInvert.B);
        }
        void Clear()
        {
            numOfReads = currentDistance = 0;
        }
        private string ReadNumFromSerial()
        {
            byte[] buff = new byte[10];
            char b = '0';
            string val = "";

            while (b != '\n')
            {
                if (b == -1)
                    return null;
                val += b;
                b = (char)serialClient.BaseStream.ReadByte();
            }
            return val;
        }
        private void tCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                int distance = 0;
                //preskaci manje vrednosti od 10
                //while(distance <= 10)
                //{
                    var response = "0";
                    if (configuration.useWifi)
                        response = client.DownloadString("http://" + configuration.serverAddress);
                    else
                    {
                        serialClient.DiscardInBuffer();
                        response = serialClient.ReadLine();
                        //response = ReadNumFromSerial();
                    }
                        
                    //var response = "40";
                    if (String.IsNullOrEmpty(response))
                        return;

                    distance = Convert.ToInt32(response);
                if (distance <= 10)
                    distance = lastDistance;
                lastDistance = distance;
                if (configuration.debug)
                {
                    lblConsole2.Text = "RAW-B: " + distance.ToString() + "\nRAW-N: " + ReadNumFromSerial() + "\nREADS: " + numOfReads + " / " + configuration.numberOfSamples;
                    if (distance < 10)
                        lblConsole2.Text = "!!!TOO SMALL!!! =>" + distance.ToString();
                }
                else
                    lblConsole2.Text = "";

                if (numOfReads < configuration.numberOfSamples) //citamo vrednost vise puta
                {
                    currentDistance += distance;    //dodajemo vrednost
                    numOfReads++;                   //povecavamo broj procitanih vrenosti
                    return;
                }

                //racunamo srednju vrednost iz svih procitanih
                distance = currentDistance / (configuration.numberOfSamples + 1);
                if (configuration.debug)
                    lblConsole.Text = "MIN: " + configuration.minDistance + " MAX: " + configuration.maxDistance + " CUR: " + distance.ToString() + " STATE: " + currentState.ToString();
                else
                    lblConsole.Text = "";
                if(distance >= configuration.minDistance && distance <= configuration.maxDistance) //u okviru smo dozvoljene daljine
                {
                    if(!currentState) //ako nije upaljen upali ga
                    {
                        //MessageBox.Show("ON");
                        currentState = true;
                        //this.WindowState = FormWindowState.Minimized;
                        numOfReads = currentDistance = 0; //reset distance
                        videoApplication.Start();
                    }
                }
                else //ako nema nikog u okviru zone
                { 
                    if(currentState) //ako je upaljen
                    {
                        //this.WindowState = FormWindowState.Maximized;
                        //MessageBox.Show("OFF");
                        currentState = false;
                        numOfReads = currentDistance = 0; //reset distance
                        videoApplication.Kill();
                    }
                }
                Clear();

            }
           catch {}      
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serialClient.IsOpen)
                    serialClient.Close();
            }
            catch
            {
                serialClient.Close();
                videoApplication.Kill();
            }
            
        }
    }
}
