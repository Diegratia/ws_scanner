namespace ws_scanner.WinForms.Forms
{
    partial class WsServerForm
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
            btnStartWs = new Button();
            btnStopWs = new Button();
            txtPortWs = new TextBox();
            btnBackWsServer = new Button();
            SuspendLayout();
            // 
            // btnStartWs
            // 
            btnStartWs.Location = new Point(59, 96);
            btnStartWs.Name = "btnStartWs";
            btnStartWs.Size = new Size(120, 35);
            btnStartWs.TabIndex = 1;
            btnStartWs.Text = "START";
            btnStartWs.Click += btnStartWs_Click;
            // 
            // btnStopWs
            // 
            btnStopWs.Location = new Point(239, 96);
            btnStopWs.Name = "btnStopWs";
            btnStopWs.Size = new Size(120, 35);
            btnStopWs.TabIndex = 2;
            btnStopWs.Text = "STOP";
            btnStopWs.Click += btnStopWs_Click;
            // 
            // txtPortWs
            // 
            txtPortWs.Location = new Point(59, 50);
            txtPortWs.Name = "txtPortWs";
            txtPortWs.PlaceholderText = "PORT";
            txtPortWs.Size = new Size(300, 23);
            txtPortWs.TabIndex = 3;
            txtPortWs.TextAlign = HorizontalAlignment.Center;
            txtPortWs.TextChanged += txtPortWs_TextChanged;
            // 
            // btnBackWsServer
            // 
            btnBackWsServer.Location = new Point(2, 1);
            btnBackWsServer.Name = "btnBackWsServer";
            btnBackWsServer.Size = new Size(36, 29);
            btnBackWsServer.TabIndex = 4;
            btnBackWsServer.Text = "<-";
            btnBackWsServer.UseVisualStyleBackColor = true;
            btnBackWsServer.Click += btnBackWsServer_Click;
            // 
            // WsServerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(393, 235);
            Controls.Add(btnBackWsServer);
            Controls.Add(txtPortWs);
            Controls.Add(btnStartWs);
            Controls.Add(btnStopWs);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "WsServerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Websocket Server";
            Load += WsServerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Button btnStartWs;
        private Button btnStopWs;
        private TextBox txtPortWs;
        private Button btnBackWsServer;
        private Label lblStatus;
    }
}