using System.Diagnostics;
using ws_scanner.Application.Interfaces;

namespace ws_scanner.Infrastructure
{
    public class ImageWatcher : IImageWatcher
    {
        private readonly FileSystemWatcher _watcher;
        public event Action<string>? OnImageReady;

        public ImageWatcher()
        {
            //_watcher = new FileSystemWatcher(@"C:\Data\scaner\scanner\Image");
            _watcher = new FileSystemWatcher(@"D:\Gawe\image");

            _watcher.NotifyFilter =
                NotifyFilters.FileName |
                NotifyFilters.LastWrite |
                NotifyFilters.Size;

            // HANYA RENAMED YANG PENTING UNTUK SCANNER
            _watcher.Renamed += OnRenamed;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!IsImage(e.FullPath))
                return;

            Debug.WriteLine($"🔁 RENAMED TO IMAGE: {e.FullPath}");
            _ = WaitUntilFileReady(e.FullPath);
        }

        private async Task WaitUntilFileReady(string path)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (!File.Exists(path))
                        return;

                    using var fs = File.Open(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.None);

                    Debug.WriteLine($"📁 FILE READY: {path}");
                    OnImageReady?.Invoke(path);
                    return;
                }
                catch
                {
                    await Task.Delay(300);
                }
            }

            Debug.WriteLine($"❌ FILE NOT READY: {path}");
        }

        private static bool IsImage(string path)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png";
        }

        public void Start() => _watcher.EnableRaisingEvents = true;
        public void Stop() => _watcher.EnableRaisingEvents = false;
    }
}
