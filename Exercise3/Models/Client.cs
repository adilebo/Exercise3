using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3.Models
{
    public class Client 
    {
        private TcpListener listener;
        private TcpClient client;
        private BinaryReader reader;
        private NetworkStream stream;
        private bool isConnected;
        public bool IsConnected { get; set; }

        #region Singelton

        private Client()
        {
            Connect(InfoModel.Instance.Port, InfoModel.Instance.IP);
            isConnected = false;
        }
        private static Client instance = null;
        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }
                return instance;
            }
        }

        #endregion

        public void Connect(int port, string ip)
        {
            isConnected = true;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);
            listener.Start();            client = listener.AcceptTcpClient();
            reader = new BinaryReader(client.GetStream());
            stream = client.GetStream();
        }

        public double GetCommandResuelt(string command)
        {
            double output = 0;
            byte[] send = Encoding.ASCII.GetBytes(command.ToString());
            stream.Write(send, 0, send.Length);
            
            string input = ""; 
            char s;
            bool seeTag = false;
            while ((s = reader.ReadChar()) != '\n' || input == "")
            {
                if (seeTag && s != '\'')
                {
                    input += s;
                }
                if (seeTag && s == '\'')
                {
                    break;
                }
                if (s == '\'')
                {
                    seeTag = !seeTag;

                }
            }
            try
            {
                output = Convert.ToDouble(input);
            }
            catch (Exception e) { }

            return output;
        }
        public void Close()
        {
            client.Close();
            isConnected = false;
        }

    }
}
