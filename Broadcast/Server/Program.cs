using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        static Socket soc = null;

        static void Main(string[] args)
        {
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("224.5.5.5"), 1024);
            soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
            soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("224.5.5.5")));
            soc.Connect(ipep);

            TimerCallback tm = new TimerCallback(UpdateTime);
            Timer timer = new Timer(tm, null, 0, 1000);
            Console.ReadKey();
        }

        private static void UpdateTime(object sock)
        {
            Console.Clear();
            string str = DateTime.Now.ToLongTimeString();
            soc.Send(Encoding.Default.GetBytes(str));
            Console.WriteLine(str);
        }
    }
}
