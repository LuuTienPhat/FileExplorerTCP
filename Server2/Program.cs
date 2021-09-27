using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.IO;
using System.Threading;

namespace Server
{
    class Program
    {
        //public static TcpListener serverSocket;
        //public static TcpClient clientSocket;
        public static void Main12(string[] args)
        {
            //serverSocket = new TcpListener(IPAddress.Any, 7777);
            //clientSocket = default(TcpClient);

            
            var localIp = IPAddress.Any;
            var localPort = 7777;
            var localEndPoint = new IPEndPoint(localIp, localPort);

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            listener.Bind(localEndPoint);

            listener.Listen(10);
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waiting for request ...");


            int counter = 0;
            var size = 1024;
            var receiveBuffer = new byte[size];

            counter = 0;
            while (true)
            {
                counter += 1;
                var clientSocket = listener.Accept();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                Console.WriteLine($"Accepted connection from {clientSocket.RemoteEndPoint}");

                
                var length = clientSocket.Receive(receiveBuffer);
                var path = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Received: {path}");

                String result = "";
                if (!Directory.Exists(path))
                {
                    result = "Path is not exist on Server";
                }
                else
                {
                    var directories = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                    for (int i = 0; i < directories.Length; i++)
                    {
                        result += directories[i].ToString() + "\n";
                    }
                }

                var sendBuffer = Encoding.UTF8.GetBytes(result);
                clientSocket.Send(sendBuffer);

                Console.WriteLine($"Sent: {result}");
                Array.Clear(receiveBuffer, 0, size);
            }

        }

        private static void Main2(string[] args)
        {
            // giá trị Any của IPAddress tương ứng với Ip của tất cả các giao diện mạng trên máy
            var localIp = IPAddress.Any;
            // tiến trình server sẽ sử dụng cổng tcp 1308
            var localPort = 7777;
            // biến này sẽ chứa "địa chỉ" của tiến trình server trên mạng
            var localEndPoint = new IPEndPoint(localIp, localPort);

            // tcp sử dụng đồng thời hai socket: 
            // một socket để chờ nghe kết nối, một socket để gửi/nhận dữ liệu
            // socket listener này chỉ làm nhiệm vụ chờ kết nối từ Client
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // yêu cầu hệ điều hành cho phép chiếm dụng cổng tcp 1308
            // server sẽ nghe trên tất cả các mạng mà máy tính này kết nối tới
            // chỉ cần gói tin tcp đến cổng 1308, tiến trình server sẽ nhận được
            listener.Bind(localEndPoint);
            // bắt đầu lắng nghe chờ các gói tin tcp đến cổng 1308
            listener.Listen(10);
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waiting for request ...");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {

                // tcp đòi hỏi một socket thứ hai làm nhiệm vụ gửi/nhận dữ liệu
                // socket này được tạo ra bởi lệnh Accept
                var socket = listener.Accept();
                Console.WriteLine($"Accepted connection from {socket.RemoteEndPoint}");

                // nhận dữ liệu vào buffer
                var length = socket.Receive(receiveBuffer);
                // không tiếp tục nhận dữ liệu nữa
                socket.Shutdown(SocketShutdown.Receive);
                var path = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Received: {path}");

                // chuyển chuỗi thành dạng in hoa
                String result = "";
                if (!Directory.Exists(path))
                {
                    result = "Path is not exist on Server";
                }
                else
                {
                    var directories = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                    for (int i = 0; i < directories.Length; i++)
                    {
                        result += directories[i].ToString() + "\n";
                    }
                }

                var sendBuffer = Encoding.UTF8.GetBytes(result);
                // gửi kết quả lại cho client
                socket.Send(sendBuffer);

                Console.WriteLine($"Sent: {result}");

                // không tiếp tục gửi dữ liệu nữa
                socket.Shutdown(SocketShutdown.Send);

                // đóng kết nối và giải phóng tài nguyên
                Console.WriteLine($"Closing connection from {socket.RemoteEndPoint}rn");
                socket.Close();

                Array.Clear(receiveBuffer, 0, size);
            }
        }
    }
}