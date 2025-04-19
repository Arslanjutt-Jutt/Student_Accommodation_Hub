using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Admin.UserContriols
{
    public partial class ManuBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreparePage();
            }
        }
        public void PreparePage()
        {
            lblUserName.Text = UserBaseControl.UserName;
        }

        protected void lbtnLogOut_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            Response.Redirect(AppConstants.CommonPath.AdminLogin);
        }
    }
}