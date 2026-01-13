using ws_scanner.WinForms.Controls;

namespace ws_scanner.WinForms.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var tcpServerControl = new TcpServerControl
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(tcpServerControl);
            tcpServerControl.BringToFront(); // 🔥 penting untuk safety
            MessageBox.Show("MainForm created");
        }
    }
}
