using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using ws_scanner.Application.Interfaces;
using ws_scanner.Infrastructure.Device;
using ws_scanner.Infrastructure.Messaging.WebScoket;
using ws_scanner.WinForms.Controls;
using static System.Windows.Forms.DataFormats;

namespace ws_scanner.WinForms.Forms
{
    public partial class HomeForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly WebSocketServer _wsServer;
        private readonly IDeviceService _deviceService;
        public HomeForm(
            IServiceProvider serviceProvider,
            WebSocketServer wsServer,
            IDeviceService deviceService
            )

        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _wsServer = wsServer;
            _deviceService = deviceService;
        }

        private void QrConfigHomeButton_Click(object sender, EventArgs e)
        {

        }

        private void WsHomeButton_Click(object sender, EventArgs e)
        {
            var wsForm = _serviceProvider.GetRequiredService<WsServerForm>();
            wsForm.Show();
            this.Hide();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            UpdateStatus();

        }

        private void statusHome_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        private void UpdateStatus()
        {
            if (_wsServer.IsRunning)
            {
                toolStripStatusLabelWs.Text = "WebSocket: RUNNING";
                toolStripStatusLabelWs.ForeColor = Color.ForestGreen;
            }
            else
            {
                toolStripStatusLabelWs.Text = "WebSocket: STOPPED";
                toolStripStatusLabelWs.ForeColor = Color.DarkRed;
            }
        }


        private void toolStripStatusLabelWs_Click(object sender, EventArgs e)
        {
            
        }

        private void TcpHomeButton_Click(object sender, EventArgs e)
        {
            var tcpForm = _serviceProvider.GetRequiredService<TcpServerForm>();
            tcpForm.Show();
            this.Hide();
        }

        private void panelStatus_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanelStatus_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateCameraStatus()
        {
            flowLayoutPanelStatus.Controls.Clear();

            var count = _deviceService.GetCameraCount();
            var names = _deviceService.GetCameraNames();

            var lblHeader = new Label
            {
                Text = $"Camera detected:{count}",
                ForeColor = count > 0 ? Color.Green : Color.Red,
                Font = new Font(Font, FontStyle.Bold),
                AutoSize = true
            };

            flowLayoutPanelStatus.Controls.Add(lblHeader);

            foreach (var name in names)
            {
                flowLayoutPanelStatus.Controls.Add(new Label
                {
                    Text = $"📷{name}",
                    AutoSize = true
                });
            }
        }

        private void btnCheckStatusHome_Click(object sender, EventArgs e)
        {
            UpdateCameraStatus();
        }

        private void statusBarControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
