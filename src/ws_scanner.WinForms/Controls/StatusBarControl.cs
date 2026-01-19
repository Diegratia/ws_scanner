using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ws_scanner.WinForms.Controls
{
    public partial class StatusBarControl : UserControl
    {
        public StatusBarControl()
        {
            InitializeComponent();
        }

        public void SetWebSocketStatus(bool isRunning)
        {
            lblWs.Text = isRunning
                ? "🟢 WebSocket: RUNNING"
                : "🔴 WebSocket: STOPPED";

            lblWs.ForeColor = isRunning
                ? Color.ForestGreen
                : Color.DarkRed;
        }

        private void panelRoot_Paint(object sender, PaintEventArgs e)
        {

        }

        //public void SetCameraStatus(int count)
        //{
        //    lblCamera.Text = $"📷 Camera: {count}";
        //    lblCamera.ForeColor = count > 0
        //        ? Color.Green
        //        : Color.Red;
        //}
    }

}
