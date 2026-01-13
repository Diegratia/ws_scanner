using ws_scanner.Application.Interfaces;
using ws_scanner.Infrastructure.Device;
using ws_scanner.WinForms.ViewModels;

namespace ws_scanner.WinForms.Controls
{
    public partial class TcpServerControl : UserControl
    {
        private readonly IDeviceService _deviceService;
        private readonly TcpServerViewModel _vm = new();

        public TcpServerControl()
        {
            InitializeComponent();
            MessageBox.Show("TcpServerControl created");
            _deviceService = new DeviceService();
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            txtPort.Text = txtPort.Text.ToUpper();
            txtPort.SelectionStart = txtPort.Text.Length;

            _vm.Port = txtPort.Text;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _vm.IsRunning = true;
            MessageBox.Show($"START ENGINE\nPORT: {_vm.Port}");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _vm.IsRunning = false;
            MessageBox.Show("STOP ENGINE");
        }

        private void btnCheckWebcam_Click(object sender, EventArgs e)
        {
            _vm.CameraCount = _deviceService.GetCameraCount();
            _vm.CameraNames = _deviceService.GetCameraNames();

            lblCheckWebcam.Text = $"Camera detected: {_vm.CameraCount}";
            lblCheckWebcam.ForeColor = _vm.CameraCount > 0
                ? Color.Green
                : Color.Red;

            if (_vm.CameraCount > 0)
            {
                MessageBox.Show(
                    string.Join("\n", _vm.CameraNames),
                    "Camera List",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }


    }
}
