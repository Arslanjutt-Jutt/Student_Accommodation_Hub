using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Admin
{
    public partial class ManageMessManu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                LoadData();
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