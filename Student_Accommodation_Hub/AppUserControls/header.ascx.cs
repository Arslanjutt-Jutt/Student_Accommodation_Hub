using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
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
            else if(pageUrl.Contains("MessManu.aspx"))
            {
                hlMessManu.Style["background-color"] = "white";
                hlMessManu.Style["color"] = "black";
            }
            else if (pageUrl.Contains("HostelRent.aspx"))
            {
                hlHostelRent.Style["background-color"] = "white";
                hlHostelRent.Style["color"] = "black";
            }
            else if (pageUrl.Contains("MessBill.aspx"))
            {
                hlMessBill.Style["background-color"] = "white";
                hlMessBill.Style["color"] = "black";
            }
            else if (pageUrl.Contains("StudentMessBlockReqs.aspx"))
            {
                hlMessBlockRequests.Style["background-color"] = "white";
                hlMessBlockRequests.Style["color"] = "black";
            }
            if (!IsPostBack)
            {
                hlMessBill.NavigateUrl= AppConstants.CommonPath.MessBill;
                hlHostelRent.NavigateUrl = AppConstants.CommonPath.HostelRent ;
            }
        }

        protected void lbtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(AppConstants.CommonPath.StudentLogin,false);
        }

      
    }
}