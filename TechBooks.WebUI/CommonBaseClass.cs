using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TechBooks.WebUI
{
    public class CommonBaseClass : System.Web.UI.Page
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TechBooksCString"].ConnectionString;
            }
        }
    }
}