using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Admin
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserBaseControl.UserId<=0 || UserBaseControl.UserRole != AppConstants.UserRole.Admin)
            {
                Response.Redirect(AppConstants.CommonPath.AdminLogin);
            }
        }
    }
}