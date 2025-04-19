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
    public partial class AddMessBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PreparePage();
            }
        }
        public void PreparePage()
        {
            Dictionary<string, string> months = new Dictionary<string, string>
        {
            { "January", "January" }, { "February", "February" }, { "March", "March" },
            { "April", "April" }, { "May", "May" }, { "June", "June" },
            { "July", "July" }, { "August", "August" }, { "September", "September" },
            { "October", "October" }, { "November", "November" }, { "December", "December" }
        };

            ddlMonths.DataSource = months;
            ddlMonths.DataTextField = "Value";  // Display month name
            ddlMonths.DataValueField = "Key";   // Store month number
            ddlMonths.DataBind();
            ddlMonths.Items.Insert(0, new ListItem("Select Month --", "-1"));

        }

        protected void btnUploadMessBill_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new MessBillModel();
                model.Month = ddlMonths.SelectedValue;
                model.Year = Convert.ToInt32(txtYear.Text);
                model.DueDate = Convert.ToDateTime(txtDueDate.Text);
                model.Remarks = txtRemarks.Text;
                model.TotalBill = Convert.ToDecimal(txtBillAmount.Text);
                int result = MessBill.InsertMessBill(model);
                if (result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessageBox", "ShowMessageBox();", true);
                    txtDueDate.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    ddlMonths.SelectedValue = "-1";
                    txtYear.Text = string.Empty;
                    txtBillAmount.Text = string.Empty;
                }
                else if(result==-2)
                {
                    ShowMessage("Mess bill of this month and year has already exist.", "Message");
                }
                else
                {
                    ShowMessage("An error occurred", "Error");
                }
            }
            catch(Exception) 
            {
                ShowMessage("An error occurred", "Error");
            }
        }
        private void ShowMessage(string message, string title)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblDialogTitle.Text = title;

            mpePopup.Show();

        }

      
    }
}