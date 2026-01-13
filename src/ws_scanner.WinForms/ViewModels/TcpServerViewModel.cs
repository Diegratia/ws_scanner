namespace ws_scanner.WinForms.ViewModels
{
    public class TcpServerViewModel
    {
        public string Port { get; set; } = string.Empty;
        public int CameraCount { get; set; }
        public List<string> CameraNames { get; set; } = new();
        public bool IsRunning { get; set; }
    }
}
