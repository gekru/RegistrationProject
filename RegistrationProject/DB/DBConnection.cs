using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RegistrationProject.DB
{
    public class DBConnection
    {
        public string XmlPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"DB\data.xml";
        }
        public XElement DataBase()
        {
            return XElement.Load(XmlPath());
        }

    }
}