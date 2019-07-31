using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class FileWriterUpdater : IUpdater
    {
        private ClientUpdater serverUpdater;
        private DateTime start;
        private int time;
        private string fileName;
        private Plane plane;
        private bool timeOutClose;
        private Client client;
        public FileWriterUpdater(int time, string fileName)
        {
            serverUpdater = new ClientUpdater();
            start = DateTime.UtcNow;
            this.time = time * 1000;
            this.fileName = AppDomain.CurrentDomain.BaseDirectory + @"\" + fileName + ".txt";
            plane = InfoModel.Instance.Plane;
            timeOutClose = false;
            client = Client.Instance;
        }

        public void Close()
        {
            if (!timeOutClose)
            {
                serverUpdater.Close();
            }

        }

        public void Update()
        {
            TimeSpan timeDiff = DateTime.UtcNow - start;
            int diff = Convert.ToInt32(timeDiff.TotalMilliseconds);
            if (diff < time)
            {
                serverUpdater.Update();

                double rudder = client.GetCommandResuelt("get /controls/flight/rudder\r\n");
                double throttle = client.GetCommandResuelt("get /controls/engines/current-engine/throttle\r\n");
                double heading = client.GetCommandResuelt("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");

                using (StreamWriter streamWriter = File.AppendText(fileName))
                {
                    streamWriter.WriteLine(plane.Lon.ToString() + "," + plane.Lat.ToString()
                        + ",", rudder.ToString()+",", throttle.ToString()+",", heading.ToString());
                }
            }
            else
            {
                timeOutClose = true;
                InfoModel.Instance.MyUpdater = serverUpdater;
            }
        }
    }
}