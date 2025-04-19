using Student_Accommodation_Hub.Constants;
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
    public partial class AddHostelRent : System.Web.UI.Page
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

        protected void btnUploadRent_Click(object sender, EventArgs e)
        {
            try
            {
                HostelRentModel model = fillModel();
                if (model != null)
                {
                    int result;
                    HostelRent.UploadHostelRent(model, out result);
                    if (result == 1)
                    {
                      ScriptManager.RegisterStartupScript(this, GetType(), "closePopupAndShowMessage", "closePopupAndShowMessage();", true);
                        txtDueDate.Text = string.Empty;
                        txtRemarks.Text = string.Empty;
                        ddlMonths.SelectedValue = "-1";
                        txtYear.Text = string.Empty;

                    }
                    if (result == -2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "closePopupAndShowMessage", "closePopupAndShowMessage('Rent of this month and year is already exist!');", true);
                                            }
                    if (result == -1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "closePopupAndShowMessage", "An error occurred!');", true);

                        Console.WriteLine("An error occured!");
                    }
                }
            }
            catch (Exception ex) 
            {
               ScriptManager.RegisterStartupScript(this, GetType(), "closePopupAndShowMessage", "An error occured!');", true);

            }

        }
        public HostelRentModel fillModel()
        {
            var model= new HostelRentModel();
            model.MonthName= ddlMonths.SelectedValue;
            model.Year = txtYear.Text;
            model.DueDate= Convert.ToDateTime(txtDueDate.Text);
            model.PaymentStatus = (int)AppConstants.RoomRentStatus.pending;
            model.Remarks = txtRemarks.Text;

            return model;
        }
    }
}