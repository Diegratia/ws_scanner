namespace ws_scanner.WinForms.Forms
{
    partial class HomeForm
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
            WsHomeButton = new Button();
            QrConfigHomeButton = new Button();
            TcpHomeButton = new Button();
            statusHome = new StatusStrip();
            toolStripStatusLabelWs = new ToolStripStatusLabel();
            panelStatus = new Panel();
            flowLayoutPanelStatus = new FlowLayoutPanel();
            btnCheckStatusHome = new Button();
            statusHome.SuspendLayout();
            panelStatus.SuspendLayout();
            SuspendLayout();
            // 
            // WsHomeButton
            // 
            WsHomeButton.Location = new Point(12, 12);
            WsHomeButton.Name = "WsHomeButton";
            WsHomeButton.Size = new Size(128, 36);
            WsHomeButton.TabIndex = 0;
            WsHomeButton.Text = "WebSocket";
            WsHomeButton.UseVisualStyleBackColor = true;
            WsHomeButton.Click += WsHomeButton_Click;
            // 
            // QrConfigHomeButton
            // 
            QrConfigHomeButton.Location = new Point(253, 12);
            QrConfigHomeButton.Name = "QrConfigHomeButton";
            QrConfigHomeButton.Size = new Size(128, 36);
            QrConfigHomeButton.TabIndex = 1;
            QrConfigHomeButton.Text = "QrConfig";
            QrConfigHomeButton.UseVisualStyleBackColor = true;
            QrConfigHomeButton.Click += QrConfigHomeButton_Click;
            // 
            // TcpHomeButton
            // 
            TcpHomeButton.Location = new Point(12, 54);
            TcpHomeButton.Name = "TcpHomeButton";
            TcpHomeButton.Size = new Size(128, 36);
            TcpHomeButton.TabIndex = 2;
            TcpHomeButton.Text = "TCP";
            TcpHomeButton.UseVisualStyleBackColor = true;
            TcpHomeButton.Click += TcpHomeButton_Click;
            // 
            // statusHome
            // 
            statusHome.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelWs });
            statusHome.Location = new Point(0, 213);
            statusHome.Name = "statusHome";
            statusHome.Size = new Size(393, 22);
            statusHome.TabIndex = 3;
            statusHome.Text = "statusHome";
            statusHome.ItemClicked += statusHome_ItemClicked;
            // 
            // toolStripStatusLabelWs
            // 
            toolStripStatusLabelWs.Name = "toolStripStatusLabelWs";
            toolStripStatusLabelWs.Size = new Size(128, 17);
            toolStripStatusLabelWs.Text = "toolStripStatusLabelWs";
            toolStripStatusLabelWs.Click += toolStripStatusLabelWs_Click;
            // 
            // panelStatus
            // 
            panelStatus.Controls.Add(flowLayoutPanelStatus);
            panelStatus.Location = new Point(12, 93);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(381, 120);
            panelStatus.TabIndex = 4;
            // 
            // flowLayoutPanelStatus
            // 
            flowLayoutPanelStatus.AutoScroll = true;
            flowLayoutPanelStatus.Dock = DockStyle.Fill;
            flowLayoutPanelStatus.Location = new Point(0, 0);
            flowLayoutPanelStatus.Name = "flowLayoutPanelStatus";
            flowLayoutPanelStatus.Size = new Size(381, 120);
            flowLayoutPanelStatus.TabIndex = 0;
            flowLayoutPanelStatus.WrapContents = false;
            flowLayoutPanelStatus.Paint += flowLayoutPanelStatus_Paint;
            // 
            // btnCheckStatusHome
            // 
            btnCheckStatusHome.Location = new Point(253, 54);
            btnCheckStatusHome.Name = "btnCheckStatusHome";
            btnCheckStatusHome.Size = new Size(128, 33);
            btnCheckStatusHome.TabIndex = 5;
            btnCheckStatusHome.Text = "Check Status";
            btnCheckStatusHome.UseVisualStyleBackColor = true;
            btnCheckStatusHome.Click += btnCheckStatusHome_Click;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(393, 235);
            Controls.Add(btnCheckStatusHome);
            Controls.Add(panelStatus);
            Controls.Add(statusHome);
            Controls.Add(TcpHomeButton);
            Controls.Add(QrConfigHomeButton);
            Controls.Add(WsHomeButton);
            MaximizeBox = false;
            Name = "HomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HomeForm";
            Load += HomeForm_Load;
            statusHome.ResumeLayout(false);
            statusHome.PerformLayout();
            panelStatus.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button WsHomeButton;
        private Button QrConfigHomeButton;
        private Button TcpHomeButton;
        private StatusStrip statusHome;
        private ToolStripStatusLabel toolStripStatusLabelWs;
        private Panel panelStatus;
        private FlowLayoutPanel flowLayoutPanelStatus;
        private Button btnCheckStatusHome;
    }
}