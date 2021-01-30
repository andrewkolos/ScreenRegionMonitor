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
        private static int DEFAULT_PER_PIXEL_DIFF_TOLERANCE = 50;
        private static int DEFAULT_MAX_CONSEC_FAILURES = 10;

        public Form captureForm { get; set; }
        private Rectangle captureRegion;
        private Bitmap captureImage;
        private int consecutiveFailures = 0;
        private ICaptureVerifier captureVerifier;

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

            if (!captureVerifier.verify(current))
            {
                consecutiveFailures++;
                Log("Image match failed " + consecutiveFailures + " times consecutively.");

                if (consecutiveFailures > int.Parse(maxConsecutiveFailuresTextBox.Text))
                {
                    Log("Running commands and stopping.");
                    SaveCaptures();
                    SaveDifference();
                    PrintScreen();
                    RunCommands();
                    Stop();
                }
            }
            else
            {
                consecutiveFailures = 0;
            }

            void SaveCaptures()
            {
                captureImage.Save("expected.png", ImageFormat.Png);
                current.Save("observed.png", ImageFormat.Png);
            }

            void SaveDifference()
            {
                var difference = Snapshot.FromBitmap(captureImage).CompareTo(Snapshot.FromBitmap(current));
                difference.ToFile("difference.png", ImageFormat.Png);
            }

            void PrintScreen()
            {
                var bounds = Screen.PrimaryScreen.Bounds;
                var screen = new Bitmap(bounds.Width, bounds.Height);
                var graphics = Graphics.FromImage(screen);
                graphics.CopyFromScreen(0, 0, 0, 0, screen.Size);
                screen.Save("screen.png", ImageFormat.Png);
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
            File.WriteAllLines(CACHE_FILE_NAME, new string[] {
                toleranceTextBox.Text,
                maxConsecutiveFailuresTextBox.Text,
            }.Concat(commandTextBox.Lines));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(CACHE_FILE_NAME))
            {
                var lines = File.ReadAllLines(CACHE_FILE_NAME);
                toleranceTextBox.Text = lines[0];
                maxConsecutiveFailuresTextBox.Text = lines[1];
                commandTextBox.Text = string.Join("\r\n", lines.Skip(2));
            }
            else
            {
                toleranceTextBox.Text = DEFAULT_PER_PIXEL_DIFF_TOLERANCE.ToString();
                maxConsecutiveFailuresTextBox.Text = DEFAULT_MAX_CONSEC_FAILURES.ToString();
            }

            modeComboBox.DataSource = new string[] {
                "Detect Difference",
                "Detect Color",
            };
            modeComboBox.SelectedItem = (modeComboBox.DataSource as string[])[0];
        }

        private void testCommandsButton_Click(object sender, EventArgs e)
        {
            RunCommands();
        }


        private void colorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorButton.BackColor = colorDialog1.Color;
            }
        }

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var text = modeComboBox.SelectedItem as string;

            Action<string> logFunc = logText => Log(logText);

            if (text.Equals("Detect Difference"))
            {
                toleranceTextBox.Enabled = true;
                colorButton.Enabled = false;
                captureVerifier = new SamenessCaptureVerifier(int.Parse(toleranceTextBox.Text), logFunc);
            }

            if (text.Equals("Detect Color"))
            {
                toleranceTextBox.Enabled = false;
                colorButton.Enabled = true;
                captureVerifier = new NoColorVerifier(colorButton.BackColor, logFunc);
            }
        }
    }
}
