using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Exercise3.Models
{
    public class ClientUpdater : IUpdater
    {
        private Plane plane;
        Client client;
        public ClientUpdater()
        {
            plane = InfoModel.Instance.Plane;
            client = Client.Instance;
            Update();
        }

        public void Close()
        {

        }

        public void Update()
        {
            
            plane.Lon = client.GetCommandResuelt("get /position/longitude-deg\r\n");
            plane.Lat = client.GetCommandResuelt("get /position/latitude-deg\r\n");
        }
    }
}