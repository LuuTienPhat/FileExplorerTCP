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
    // Size of receive buffer.  
    public const int BufferSize = 1024;

    // Receive buffer.  
    public byte[] buffer = new byte[BufferSize];

    // Received data string.
    public StringBuilder sb = new StringBuilder();

    // Client socket.
    public Socket workSocket = null;
}

public class AsynchronousSocketListener
{ 
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public AsynchronousSocketListener()
    {
    }

    public static void StartListening()
    {
        int port = 7777;
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = IPAddress.Parse("127.0.0.2");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);


        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
               
                allDone.Reset();

                
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                 
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
          
        allDone.Set();

       
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            
            state.sb.Append(Encoding.UTF8.GetString(
                state.buffer, 0, bytesRead));


           
            content = state.sb.ToString();

            Console.WriteLine(content);

            
            String result = getAllDirectories(content);
            //Console.WriteLine("Dir: " + result);

            
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
            var files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            var directories = System.IO.Directory.GetDirectories(path, "*.*", System.IO.SearchOption.AllDirectories);
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
        
        byte[] byteData = Encoding.UTF8.GetBytes(data);

        
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
             
            Socket handler = (Socket)ar.AsyncState;

            
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void Main(String[] args)
    {
        AsynchronousSocketListener.StartListening();
        
    }
}