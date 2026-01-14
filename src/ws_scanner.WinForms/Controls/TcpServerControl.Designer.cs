namespace ws_scanner.WinForms.Controls
{
    partial class TcpServerControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            txtPort = new TextBox();
            btnStart = new Button();
            btnStop = new Button();
            lblPort = new Label();
            btnCheckWebcam = new Button();
            lblCheckWebcam = new Label();
            SuspendLayout();
            // 
            // txtPort
            // 
            txtPort.Location = new Point(60, 46);
            txtPort.Name = "txtPort";
            txtPort.PlaceholderText = "PORT";
            txtPort.Size = new Size(300, 23);
            txtPort.TabIndex = 0;
            txtPort.TextAlign = HorizontalAlignment.Center;
            txtPort.TextChanged += txtPort_TextChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(59, 141);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(120, 35);
            btnStart.TabIndex = 1;
            btnStart.Text = "START";
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(240, 141);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(120, 35);
            btnStop.TabIndex = 2;
            btnStop.Text = "STOP";
            btnStop.Click += btnStop_Click;
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(59, 25);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(68, 15);
            lblPort.TabIndex = 3;
            lblPort.Text = "Engine Port";
            // 
            // btnCheckWebcam
            // 
            btnCheckWebcam.Location = new Point(240, 79);
            btnCheckWebcam.Name = "btnCheckWebcam";
            btnCheckWebcam.Size = new Size(120, 32);
            btnCheckWebcam.TabIndex = 4;
            btnCheckWebcam.Text = "CHECK";
            btnCheckWebcam.Click += btnCheckWebcam_Click;
            // 
            // lblCheckWebcam
            // 
            lblCheckWebcam.AutoSize = true;
            lblCheckWebcam.Location = new Point(60, 88);
            lblCheckWebcam.Name = "lblCheckWebcam";
            lblCheckWebcam.Size = new Size(90, 15);
            lblCheckWebcam.TabIndex = 5;
            lblCheckWebcam.Text = "Check Webcam";
            // 
            // TcpServerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblCheckWebcam);
            Controls.Add(btnCheckWebcam);
            Controls.Add(lblPort);
            Controls.Add(txtPort);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Name = "TcpServerControl";
            Size = new Size(393, 235);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPort;
        private Button btnStart;
        private Button btnStop;
        private Label lblPort;
        private Button btnCheckWebcam;
        private Label lblCheckWebcam;
    }
}
