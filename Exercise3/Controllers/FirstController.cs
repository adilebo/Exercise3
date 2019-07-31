using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Exercise3.Models;
namespace Exercise3.Controllers
{
    public class FirstController : Controller
    {
        public static int X { get; set; }
        public static int Y { get; set; }

        // GET: First
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            bool isOK = Regex.IsMatch(ip, @"[0-9]*\.[0-9]*\.[0-9]*\.[0-9]*");
            if (!isOK)
            {
                return displayFlight(ip, port);
            }
            var info = InfoModel.Instance;
            info.IP = ip;
            info.Port = port;
            info.MyUpdater = new OneTimeUpdater();
            info.MyUpdater.Update();
            ViewBag.Info = info;
            
            return View();
        }

        //[Route("display/{ip}/{port}/{time}")]
        //public ActionResult DisplayAnimation(string ip, int port, int frenquency)
        //{
        //    var info = InfoModel.Instance;
        //    info.IP = ip;
        //    info.Port = port;
        //    info.Time = frenquency;
        //    Session["time"] = frenquency;
        //    info.MyUpdater = new ServerUpdater();
        //    ViewBag.Info = info;

        //    return View();
        //}


        private string ToXml(Plane plane)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Planes");

            plane.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }


        /**in this function we will found the place where the airplane is
         we will connect to the server by ip and port*/
        public void GetAirport()
        {
            /**Model- connect to server(ip, port)*/
            /**set x and y by the given data*/
        }


        [HttpPost]
        public string GetPlane()
        {
            var plane = InfoModel.Instance.Plane;

            InfoModel.Instance.MyUpdater?.Update();

            return ToXml(plane);
        }

        [HttpGet]
        public ActionResult displayByTime(string ip, int port, int time)
        {
            var info = InfoModel.Instance;
            info.IP = ip;
            info.Port = port;
            info.Time = time;
            Session["time"] = time;
            info.MyUpdater = new ClientUpdater();
            ViewBag.Info = info;


            return View();
        }

        [HttpGet]
        public ActionResult displaySave(string ip, int port, int frequancy, int time, string fileName)
        {
            var info = InfoModel.Instance;
            info.IP = ip;
            info.Port = port;
            info.Time = frequancy;
            Session["time"] = frequancy;
            info.MyUpdater = new FileWriterUpdater(time, fileName);
            ViewBag.Info = info;

            return View();
        }


        [HttpGet]
        public ActionResult displayFlight(string fileName, int time)
        {
            {
                var info = InfoModel.Instance;
                Session["time"] = time;
                info.Time = time;

                info.MyUpdater = new FileReaderUpdater(fileName);
                ViewBag.Info = info;

                return View("displayFlight");
            }
        }
    }
}