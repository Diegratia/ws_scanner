namespace ws_scanner.WinForms.Controls
{
    partial class StatusBarControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWs;
        private System.Windows.Forms.Panel panelRoot;

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
            panelRoot = new Panel();
            lblWs = new Label();
            panelRoot.SuspendLayout();
            SuspendLayout();
            // 
            // panelRoot
            // 
            panelRoot.BackColor = SystemColors.ControlLight;
            panelRoot.Controls.Add(lblWs);
            panelRoot.Dock = DockStyle.Fill;
            panelRoot.Location = new Point(0, 0);
            panelRoot.Name = "panelRoot";
            panelRoot.Padding = new Padding(10);
            panelRoot.Size = new Size(400, 40);
            panelRoot.TabIndex = 0;
            panelRoot.Paint += panelRoot_Paint;
            // 
            // lblWs
            // 
            lblWs.AutoSize = true;
            lblWs.Dock = DockStyle.Left;
            lblWs.ForeColor = Color.DarkRed;
            lblWs.Location = new Point(10, 10);
            lblWs.Name = "lblWs";
            lblWs.Size = new Size(136, 15);
            lblWs.TabIndex = 0;
            lblWs.Text = "🔴 WebSocket: STOPPED";
            // 
            // StatusBarControl
            // 
            Controls.Add(panelRoot);
            Name = "StatusBarControl";
            Size = new Size(400, 40);
            panelRoot.ResumeLayout(false);
            panelRoot.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
