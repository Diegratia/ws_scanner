using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ws_scanner.Application.Dtos
{
    public class OcrRequestDto
    {
        public IFormFile? image { get; set; }
    }
}
