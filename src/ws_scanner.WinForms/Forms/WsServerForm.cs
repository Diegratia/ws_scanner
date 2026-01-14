using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ws_scanner.Application.Services;
using ws_scanner.Infrastructure.Messaging.WebScoket;
using ws_scanner.WinForms.Forms;

namespace ws_scanner.WinForms.Forms
{
    public partial class WsServerForm : Form
    {
        private readonly WebSocketServer _wsServer;
        private readonly ImagePipelineService _pipeline;

        public WsServerForm(
            WebSocketServer wsServer,
            ImagePipelineService pipeline
            )
        {
            InitializeComponent();
            _wsServer = wsServer;
            _pipeline = pipeline;
        }

        private async void btnStartWs_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPortWs.Text, out var port))
            {
                MessageBox.Show("Port tidak valid");
                return;
            }

            try
            {
                await _wsServer.StartAsync(port);
                _pipeline.Start();
                Debug.WriteLine("📡 PIPELINE STARTED");
                MessageBox.Show($"WebSocket Server START di port {port}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnStopWs_Click(object sender, EventArgs e)
        {
            _wsServer.Stop();
            MessageBox.Show("WebSocket Server STOP");
        }


        private void txtPortWs_TextChanged(object sender, EventArgs e)
        {
            txtPortWs.Text = txtPortWs.Text.ToUpper();
            txtPortWs.SelectionStart = txtPortWs.Text.Length;
        }

        private void WsServerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
