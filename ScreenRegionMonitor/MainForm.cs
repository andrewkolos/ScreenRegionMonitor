using Microsoft.Test.VisualVerification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRegionMonitor
{
    public partial class MainForm : Form
    {

        private static string CACHE_FILE_NAME = "cache.txt";
        private static int PER_PIXEL_DIFF_TOLERANCE = 10;
        private static int MAX_CONSEC_FAILURES = 10;
        public Form captureForm { get; set; }
        private Rectangle captureRegion;
        private Bitmap captureImage;
        private int consecutiveFailures = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        public void SetCapture(Rectangle region)
        {
            captureImage = CaptureRegion(region);
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
            var current = CaptureRegion(captureRegion);

            Log("Checking");

            if (!CompareBitmapsFast(current, captureImage))
            {
                consecutiveFailures++;
                Log("Image match failed " + consecutiveFailures + " times consecutively.");

                if (consecutiveFailures > MAX_CONSEC_FAILURES)
                {
                    Log("Running commands and stopping.");
                    RunCommands();
                    Stop();
                    SaveDiffToDisk();
                }
            }
            else
            {
                consecutiveFailures = 0;
            }

            void SaveDiffToDisk()
            {
                var difference = Snapshot.FromBitmap(captureImage).CompareTo(Snapshot.FromBitmap(current));
                difference.ToFile("joj.png", ImageFormat.Png);
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

        private bool CompareBitmapsFast(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (Math.Abs(b1bytes[n] - b2bytes[n]) > PER_PIXEL_DIFF_TOLERANCE)
                {
                    result = false;
                    break;
                }
            }

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
        }
    }
}
