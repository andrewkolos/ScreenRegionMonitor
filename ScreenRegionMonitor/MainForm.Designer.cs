
namespace ScreenRegionMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.commandTextBox = new System.Windows.Forms.RichTextBox();
            this.captureButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.screenCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.testCommandsButton = new System.Windows.Forms.Button();
            this.toleranceTextBox = new System.Windows.Forms.TextBox();
            this.toleranceLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxConsecutiveFailuresTextBox = new System.Windows.Forms.TextBox();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(12, 27);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(315, 59);
            this.commandTextBox.TabIndex = 1;
            this.commandTextBox.Text = "";
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(12, 372);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(75, 27);
            this.captureButton.TabIndex = 9;
            this.captureButton.Text = "Capture";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(172, 372);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 27);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(252, 372);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 27);
            this.stopButton.TabIndex = 12;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // screenCheckTimer
            // 
            this.screenCheckTimer.Interval = 1000;
            this.screenCheckTimer.Tick += new System.EventHandler(this.screenCheckTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Commands";
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(12, 215);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(315, 151);
            this.logTextBox.TabIndex = 8;
            this.logTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Log";
            // 
            // testCommandsButton
            // 
            this.testCommandsButton.Location = new System.Drawing.Point(92, 372);
            this.testCommandsButton.Name = "testCommandsButton";
            this.testCommandsButton.Size = new System.Drawing.Size(75, 27);
            this.testCommandsButton.TabIndex = 10;
            this.testCommandsButton.Text = "Test Cmds";
            this.testCommandsButton.UseVisualStyleBackColor = true;
            this.testCommandsButton.Click += new System.EventHandler(this.testCommandsButton_Click);
            // 
            // toleranceTextBox
            // 
            this.toleranceTextBox.Location = new System.Drawing.Point(154, 92);
            this.toleranceTextBox.Name = "toleranceTextBox";
            this.toleranceTextBox.Size = new System.Drawing.Size(173, 20);
            this.toleranceTextBox.TabIndex = 3;
            // 
            // toleranceLabel
            // 
            this.toleranceLabel.AutoSize = true;
            this.toleranceLabel.Location = new System.Drawing.Point(13, 95);
            this.toleranceLabel.Name = "toleranceLabel";
            this.toleranceLabel.Size = new System.Drawing.Size(135, 13);
            this.toleranceLabel.TabIndex = 2;
            this.toleranceLabel.Text = "Per Pixel Tolerance (0-255)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Max consecutive failures";
            // 
            // maxConsecutiveFailuresTextBox
            // 
            this.maxConsecutiveFailuresTextBox.Location = new System.Drawing.Point(154, 116);
            this.maxConsecutiveFailuresTextBox.Name = "maxConsecutiveFailuresTextBox";
            this.maxConsecutiveFailuresTextBox.Size = new System.Drawing.Size(173, 20);
            this.maxConsecutiveFailuresTextBox.TabIndex = 5;
            // 
            // modeComboBox
            // 
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Items.AddRange(new object[] {
            "Detect Change",
            "Detect Specific Color"});
            this.modeComboBox.Location = new System.Drawing.Point(16, 145);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(311, 21);
            this.modeComboBox.TabIndex = 6;
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(16, 173);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(311, 23);
            this.colorButton.TabIndex = 7;
            this.colorButton.Text = "Pick Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 408);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.maxConsecutiveFailuresTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toleranceLabel);
            this.Controls.Add(this.toleranceTextBox);
            this.Controls.Add(this.testCommandsButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.captureButton);
            this.Controls.Add(this.commandTextBox);
            this.Name = "MainForm";
            this.Text = "ScreenMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox commandTextBox;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer screenCheckTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button testCommandsButton;
        private System.Windows.Forms.TextBox toleranceTextBox;
        private System.Windows.Forms.Label toleranceLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxConsecutiveFailuresTextBox;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button colorButton;
    }
}

