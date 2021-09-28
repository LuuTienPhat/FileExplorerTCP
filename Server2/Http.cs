using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;
using System.Threading.Tasks;

namespace Server2
{
    class Http
    {
        public async static Task Main1314()
        {
            string[] prefixes = new string[] { "http://127.0.0.1:7777/" };

            HttpListener listener = new HttpListener();

            if (!HttpListener.IsSupported) throw new Exception("Hệ thống hỗ trợ HttpListener.");

            if (prefixes == null || prefixes.Length == 0) throw new ArgumentException("prefixes");

            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }

            Console.WriteLine("Server start ...");

            // Http bắt đầu lắng nghe truy vấn gửi đến
            listener.Start();

            // Vòng lặp chấp nhận và xử lý các client kết nối
            do
            {
                // Chấp nhận khi có cliet kết nối đế
                HttpListenerContext context = await listener.GetContextAsync();

                // ....
                // Xử lý context - đọc  thông tin request,  ghi thông tin response
                // ... ví dụ như sau:

                var response = context.Response;
                var outputstream = response.OutputStream;                               // lấy Stream lưu nội dung gửi cho client

                context.Response.Headers.Add("content-type", "text/html");              // thiết lập respone header
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Hello world!");     // dữ liệu content
                response.ContentLength64 = buffer.Length;
                await outputstream.WriteAsync(buffer, 0, buffer.Length);                  // viết content ra stream
                outputstream.Close();                                                   // Đóng stream (gửi về cho cliet)

            }
            while (listener.IsListening);
        }
    }


}
