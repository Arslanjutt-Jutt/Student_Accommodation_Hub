using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class MessManuControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreparePage();
                LoadData();
            }
        }
        private void PreparePage()
        {
            if (UserBaseControl.UserRole == AppConstants.UserRole.Student)
            {
                lblPageHeading.Text = "Weekly Mess Manu";
            }
        }
        public void LoadData()
        {
            try
            {
                List<MessMenuModel> messManu = MessManu.GetWeeklyMessMenu();
                if (messManu != null && messManu.Count > 0)
                {
                    rprMessManu.DataSource = messManu;
                    rprMessManu.DataBind();
                    pnlNoRec.Visible = false;
                    pnlMessManu.Visible = true;
                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlMessManu.Visible = false;
                }
            }
            catch (Exception ex)
            {
                pnlNoRec.Visible = true;
                pnlMessManu.Visible = false;
            }
        }

        protected void rprMessManu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Header)
            {
                var pnlActionHead = e.Item.FindControl("pnlActionHead") as Panel;
                if (UserBaseControl.UserRole == AppConstants.UserRole.Student)
                {
                    
                    pnlActionHead.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var pnlAction = e.Item.FindControl("pnlAction") as Panel;
                
                if (UserBaseControl.UserRole== AppConstants.UserRole.Student)
                {
                    pnlAction.Visible = false;
                   
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MessMenuModel manu = new MessMenuModel();
                manu.DayOfWeek = ddlDayOfWeek.SelectedValue;
                manu.Breakfast = txtBreakfast.Text;
                manu.Lunch = txtLunch.Text;
                manu.Dinner = txtDinner.Text;
                manu.ID = Convert.ToInt32(hfMessID.Value);
                int result = MessManu.UpdateMessMenu(manu);
                if (result == 1)
                {
                    mpeUpdateMessPopup.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "closePopupAndShowMessage", "closePopupAndShowMessage();", true);
                    LoadData();
                }
                else
                {
                    mpeUpdateMessPopup.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "closePopupAndShowMessage", "closePopupAndShowMessage('Error Occured.Manu not Updated');", true);
                    //ShowMessage("An error occured", "Message");
                }

            }
            catch (Exception ex)
            {
                mpeUpdateMessPopup.Hide();
                ShowMessage(ex.Message, "Error");
            }
        }
        private void ShowMessage(string message, string title)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblTitle.Text = title;

            btnOk.OnClientClick = "hidePopup(); return false;";
            butnclose.OnClientClick = "hidePopup(); return false;";

            mpePopup.Show();

        }
    }

}