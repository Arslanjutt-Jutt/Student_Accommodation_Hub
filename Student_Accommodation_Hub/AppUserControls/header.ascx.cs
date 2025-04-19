using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pageUrl= Request.Url.ToString();
            if (pageUrl.Contains("default.aspx"))
            {
                hlDefaultPage.Style["background-color"] = "white";
                hlDefaultPage.Style["color"] = "black";

            }
        }
    }
}