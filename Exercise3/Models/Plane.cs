using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Exercise3.Models
{
    public class Plane
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public bool Done { get; set; }

        public Plane()
        {
            Done = false;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Plane");
            writer.WriteElementString("Lon",Lon.ToString());
            writer.WriteElementString("Lat", Lat.ToString());
            writer.WriteElementString("Done", Done.ToString());
            writer.WriteEndElement();
        }
    }
}