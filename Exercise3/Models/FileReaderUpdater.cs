using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Exercise3.Models
{
    public class FileReaderUpdater : IUpdater
    {
        private string[] lines;
        private int i;
        private Plane plane;

        public FileReaderUpdater(string fileName)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + fileName + ".txt";
            lines = File.ReadAllLines(filePath);
            i = 0;
            plane = InfoModel.Instance.Plane;
            Update();
        }

        public void Close()
        {

        }

        public void Update()
        {
            if (i < lines.Length)
            {
                string[] data = lines[i].Split(',');
                plane.Lon = Double.Parse(data[0]);
                plane.Lat = Double.Parse(data[1]);
                i++;
            }
            else
            {
                plane.Done = true;
            }
        }
    }
}