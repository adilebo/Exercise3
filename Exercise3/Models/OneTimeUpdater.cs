using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Exercise3.Models
{
    public class OneTimeUpdater : IUpdater
    {
        private Plane plane;
        Client client;
        public OneTimeUpdater()
        {
            plane = InfoModel.Instance.Plane;
            client = Client.Instance;
            Update();
        }

        public void Update()
        {

            plane.Lon = client.GetCommandResuelt("get /position/longitude-deg\r\n");
            plane.Lat = client.GetCommandResuelt("get /position/latitude-deg\r\n");
            //if (Client.Instance.IsConnected)
            //{
            //    Client.Instance.Close();
            //}
            //Client.Instance.Connect(InfoModel.Instance.Port, InfoModel.Instance.IP);
        }

        public void Close()
        {

        }
    }
}