using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static Socket soc = null;
        public Form1()
        {
            InitializeComponent();
            
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 1024);
       
            soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("224.5.5.5"), IPAddress.Any));
            soc.Bind(ipep);

            Thread sendMessageThread = new Thread(new ThreadStart(Recieve));
            sendMessageThread.IsBackground = true;
            sendMessageThread.Start();
        }
        void Recieve()
        {
            while(true)
            {
                byte[] buf = new byte[1024];
                soc.Receive(buf);
                updLab(Encoding.Default.GetString(buf));
            }
        }
        void updLab(string str)
        {
            if(label1.InvokeRequired)
            {
                label1.Invoke(new Action<string>(updLab), str);
            }
            else
            {
                label1.Text = str;
            }
        }
    }
}
