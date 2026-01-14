using System.Runtime.InteropServices;
using ws_scanner.Application.Interfaces;
using ws_scanner.Infrastructure.Device;
namespace ws_scanner.WinForms.Forms
{
    public partial class TcpServerForm : Form
    {
        private readonly IDeviceService _deviceService;

        public TcpServerForm()
        {
            InitializeComponent();
            _deviceService = new DeviceService();
        }


        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            txtPort.Text = txtPort.Text.ToUpper();
            txtPort.SelectionStart = txtPort.Text.Length;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"START ENGINE\nPORT: {txtPort.Text}");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            MessageBox.Show("STOP ENGINE");
        }

        private void btnCheckWebcam_Click(object sender, EventArgs e)
        {
            var count = _deviceService.GetCameraCount();
            var names = _deviceService.GetCameraNames();

            lblCheckWebcam.Text = $"Camera detected: {count}";
            lblCheckWebcam.ForeColor = count > 0 ? Color.Green : Color.Red;

            if (count > 0)
            {
                MessageBox.Show(
                    string.Join("\n", names),
                    "Camera List",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void TcpServerForm_Load(object sender, EventArgs e)
        {

        }

        //private void btnCheckWebcam_Click(object sender, EventArgs e)
        //{

        //}
    }
}
