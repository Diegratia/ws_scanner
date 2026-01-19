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
using Microsoft.Extensions.DependencyInjection;
using ws_scanner.Application.Interfaces;
using ws_scanner.Application.Services;
using ws_scanner.Infrastructure.Device;
using ws_scanner.Infrastructure.Messaging.WebScoket;
using ws_scanner.WinForms.Controls;
using ws_scanner.WinForms.Forms;

namespace ws_scanner.WinForms.Forms
{
    public partial class WsServerForm : Form
    {
        private readonly WebSocketServer _wsServer;
        private readonly IImagePipelineService _pipeline;
        private readonly IServiceProvider _serviceProvider;


        public WsServerForm(
            WebSocketServer wsServer,
            IImagePipelineService pipeline,
            IServiceProvider serviceProvider
            )
        {
            InitializeComponent();
            _wsServer = wsServer;
            _pipeline = pipeline;
            _serviceProvider = serviceProvider;
        }

        //private async void btnStartWs_Click(object sender, EventArgs e)
        //{
        //    if (!int.TryParse(txtPortWs.Text, out var port))
        //    {
        //        MessageBox.Show("Port tidak valid");
        //        return;
        //    }

        //    try
        //    {
        //        btnStartWs.Enabled = false; // lock sementara
        //        await _wsServer.StartAsync(port);
        //        _pipeline.Start();
        //        Debug.WriteLine("📡 PIPELINE STARTED");
        //        SetWsButtonState(isRunning: true);
        //        MessageBox.Show($"WebSocket Server START di port {port}");
        //    }
        //    catch (Exception ex)
        //    {
        //        btnStartWs.Enabled = true; // rollback kalau gagal
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private async void btnStartWs_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPortWs.Text, out var port))
            {
                MessageBox.Show("Port tidak valid");
                return;
            }

            try
            {
                btnStartWs.Enabled = false;

                await _wsServer.StartAsync(port);

                _pipeline.Start();

                SetWsButtonState(_wsServer.IsRunning);

                MessageBox.Show($"WebSocket Server START di port {port}");
            }
            catch (Exception ex)
            {
                btnStartWs.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }



        //private void btnStopWs_Click(object sender, EventArgs e)
        //{
        //    _wsServer.Stop();
        //    SetWsButtonState(isRunning: false);
        //    MessageBox.Show("WebSocket Server STOP");
        //}

        private async void btnStopWs_Click(object sender, EventArgs e)
        {
            try
            {
                await _wsServer.StopAsync();

                SetWsButtonState(_wsServer.IsRunning);

                MessageBox.Show("WebSocket Server STOP");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void txtPortWs_TextChanged(object sender, EventArgs e)
        {
            txtPortWs.Text = txtPortWs.Text.ToUpper();
            txtPortWs.SelectionStart = txtPortWs.Text.Length;
        }

        private void WsServerForm_Load(object sender, EventArgs e)
        {
     
        }

        private void btnBackWsServer_Click(object sender, EventArgs e)
        {
            var hmForm = _serviceProvider.GetRequiredService<HomeForm>();
            hmForm.Show();
            this.Hide();
        }

        private void SetWsButtonState(bool isRunning)
        {
            btnStartWs.Enabled = !isRunning;
            btnStopWs.Enabled = isRunning;
            txtPortWs.Enabled = !isRunning;

            //lblStatus.Text = isRunning ? "RUNNING" : "STOPPED";
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void statusBarControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
