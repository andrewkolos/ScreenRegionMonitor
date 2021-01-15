using Microsoft.Test.VisualVerification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRegionMonitor
{
    public partial class MainForm : Form
    {

        private static string CACHE_FILE_NAME = "cache.txt";
        public Form captureForm { get; set; }
        private Rectangle captureRegion;
        private Snapshot captureImage;
        private int consecutiveFailures = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        public void SetCapture(Rectangle region)
        {
            captureImage = Snapshot.FromBitmap(CaptureRegion(region));
            captureRegion = region;
            Log("Received screen capture.");
            Show();
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None; 
            Hide();
            captureForm = new CaptureForm(this);
            captureForm.Show();
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            screenCheckTimer.Start();
            stopButton.Enabled = true;
            startButton.Enabled = false;
            commandTextBox.Enabled = false;
            captureButton.Enabled = false;
            testCommandsButton.Enabled = false;
            consecutiveFailures = 0;
        }

        private void screenCheckTimer_Tick(object sender, EventArgs e)
        {
            var current = Snapshot.FromBitmap(CaptureRegion(captureRegion));

            var difference = captureImage.CompareTo(current);
            var verifier = new SnapshotColorVerifier(Color.Black, new ColorDifference());
            difference.ToFile("joj.png", ImageFormat.Png);

            Log("Checking");

            if (verifier.Verify(difference) == VerificationResult.Fail)
            {
                consecutiveFailures++;
                Log("Image match failed " + consecutiveFailures + " times consecutively.");

                if (consecutiveFailures > 10)
                {
                    Log("Running commands and stopping.");
                    RunCommands();
                    Stop();
                }
            }
            else
            {
                consecutiveFailures = 0;
            }
        }

        private void RunCommands()
        {
            var lines = commandTextBox.Text.Split(new[] { "\n" }
                                          , StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                System.Diagnostics.Process.Start("CMD.exe", "/c " + line);
            }
        }
        private Bitmap CaptureRegion(Rectangle region)
        {
            Bitmap _img = new Bitmap(region.Width, region.Height, PixelFormat.Format32bppArgb);
            //create graphic variable
            Graphics g = Graphics.FromImage(_img);
            g.CopyFromScreen(region.Left, region.Top, 0, 0, region.Size);

            Clipboard.SetImage(_img);

            startButton.Enabled = true;

            return _img;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            screenCheckTimer.Stop();
            startButton.Enabled = true;
            stopButton.Enabled = false;
            commandTextBox.Enabled = true;
            captureButton.Enabled = true;
            testCommandsButton.Enabled = true;
        }

        private void Log(string text)
        {
            if (!string.IsNullOrWhiteSpace(logTextBox.Text))
            {
                logTextBox.AppendText("\r\n" + text);
            }
            else
            {
                logTextBox.AppendText(text);
            }
            logTextBox.ScrollToCaret();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(CACHE_FILE_NAME, commandTextBox.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(CACHE_FILE_NAME))
            {
                commandTextBox.Text = File.ReadAllText(CACHE_FILE_NAME);
            }
        }

        private void testCommandsButton_Click(object sender, EventArgs e)
        {
            RunCommands();
        }
    }
}
