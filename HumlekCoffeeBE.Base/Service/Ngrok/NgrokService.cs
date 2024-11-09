using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Service.Ngrok
{
    public class NgrokService
    {
        public void StartNgrok()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "D:\\HaoCN\\.NetCore\\Project\\ngrok\\ngrok.exe",
                    Arguments = "http 8080",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            process.Start();
            // Có thể xử lý Output nếu cần
        }
    }
}
