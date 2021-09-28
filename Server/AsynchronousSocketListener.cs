using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;

// State object for reading client data asynchronously  
public class StateObject
{
    // Kích thước của buffer 
    public const int BufferSize = 1024;

    // Dữ liệu nhận được từ client sẽ được lưu vào mảng byte buffer  
    public byte[] buffer = new byte[BufferSize];

    // Chuỗi nhận được
    public StringBuilder sb = new StringBuilder();

    // Client socket
    public Socket workSocket = null;
}

public class AsynchronousSocketListener
{
    // Tín hiệu Thread;
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public AsynchronousSocketListener()
    {
    }

    public static void StartListening()
    {
        // Thiết lập địa chỉ cuối cho server  
        // Tên DNS của Máy tính  
        // running the listener is "host.contoso.com".

        //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        int port = 7777; //Cổng tiếp nhận
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); //Địa chỉ IP
        //Thiết lập địa chỉ IP và cổng cho Server
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        // Tạo TCP/IP socket
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
        try
        {
            // Liên kết socket với IPEndPoint
            listener.Bind(localEndPoint);

            //Server bắt đầu đợi các kết nối đến (tối đa 100 người)
            listener.Listen(100);

            while (true)
            {
               // Đặt sự kiện sang trạng thái không báo trước
                allDone.Reset();

                // Chạy Socket ở trên dưới dạng bất đồng bộ và đợi các kết nối đến
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Đợi cho đến khi có một kết nối được thiết lập trước khi tiếp tục
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {

        // ra hiệu cho Thread chính tiếp tục  
        allDone.Set();

        // Get the socket that handles the client request.  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Tạo một StateObject mới  
        StateObject state = new StateObject();

        // Gán client socket vừa nhận được và client socket StateObject
        state.workSocket = handler;

        //Bắt đầu nhận dữ liệu từ client
        //Hàm BeginRececive truyên vào tham số:
        // + mảng buffer chứ dữ liệu nhận được
        // + offset vị trí bắt đầu của buffer
        // + Kích thước của mảng buffer
        // + Socket Flags
        // + Truyền vào hàm xử lý dữ liệu nhân được (bất đồng bộ)
        // + truyền vào StateObject vừa tạo ở trên
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        //Chuỗi content khởi tạo trống, chứa dữ liệu nhận được từ Client
        String content = String.Empty;

        // Retrieve the state object and the handler socket  
        // from the asynchronous state object. 
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        //Đọc kích thước mảng dữ liệu byte nhận được từ Server;
        int bytesRead = handler.EndReceive(ar);

        //Nếu kích thước > 0 (tức là có gửi lên Server)
        if (bytesRead > 0)
        {
            //Chuyển mảng dữ liệu byte nhận được sang dạng stringbuilder và gán nó vào
            //thuộc tính sb của object state ở trên;
            state.sb.Append(Encoding.UTF8.GetString(
                state.buffer, 0, bytesRead));


           // Gán dữ liệu vừa truyển cho content
            content = state.sb.ToString();

            Console.WriteLine(content);

            // Lấy được đường dẫn, gửi đường dẫn nhận được để lấy tất cả đường dẫn con
            String result = getAllDirectories(content);
            //Console.WriteLine("Dir: " + result);

            //Gọi hàm gửi kết quả lại cho Client
            Send(handler, result);

        }
    }

    private static String getAllDirectories(String path)
    {
        String result = "";
        if (!Directory.Exists(path))
        {
            result = "Path is not exist on Server";
        }
        else
        {
            var files = Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            var directories = Directory.GetDirectories(path, "*.*", System.IO.SearchOption.AllDirectories);
            
            for (int i = 0; i < directories.Length; i++)
            {
                result += directories[i].ToString() + "\n";
           
            }
            for (int i = 0; i < files.Length; i++)
            {
                result += files[i].ToString() + "\n";
            }
        }
        return result;
    }

    private static void Send(Socket handler, String data)
    {
        //Tạo mảng byte từ dữ liệu data truyền vào
        byte[] byteData = Encoding.UTF8.GetBytes(data);

        //Bắt đầu gửi dữ liệu cho Client, gọi hàm async
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Trường hợp các lệnh 
            Socket handler = (Socket)ar.AsyncState;

            
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    //public static void Main(String[] args)
    //{
    //    AsynchronousSocketListener.StartListening();
    //}
}