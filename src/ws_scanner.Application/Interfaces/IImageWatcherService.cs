using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws_scanner.Application.Interfaces
{
    public interface IImageWatcher
    {
        event Action<string> OnImageReady;
        void Start();
        void Stop();
    }



}
