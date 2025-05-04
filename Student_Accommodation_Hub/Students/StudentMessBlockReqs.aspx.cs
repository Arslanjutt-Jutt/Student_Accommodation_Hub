using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Students
{
    public partial class StudentMessBlockReqs : System.Web.UI.Page
    {
        public int pageSize
        {
            get
            {
                if (ViewState["pageSize"] != null)
                    return (int)ViewState["pageSize"];
                return 10;
            }
            set
            {
                ViewState["pageSize"] = value;
            }
        }
        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] != null)
                    return (int)ViewState["CurrentPage"];
                return 1;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PagingUserControl1.OnPageChanged += PaginationControl_PageChanged;
            if (!IsPostBack)
            {
                PreparePage();
                LoadData(1);
            }
        }
        public void PreparePage()
        {
            Dictionary<string, string> month = new Dictionary<string, string>
 {
     { "January", "January" }, { "February", "February" }, { "March", "March" },
     { "April", "April" }, { "May", "May" }, { "June", "June" },
     { "July", "July" }, { "August", "August" }, { "September", "September" },
     { "October", "October" }, { "November", "November" }, { "December", "December" }
 };

            ddlMonths.DataSource = month;
            ddlMonths.DataTextField = "Value";  // Display month name
            ddlMonths.DataValueField = "Key";   // Store month number
            ddlMonths.DataBind();
            ddlMonths.Items.Insert(0, new ListItem("Any", "-1"));

            ddlMonthPopup.DataSource = month;
            ddlMonthPopup.DataTextField = "Value";
            ddlMonthPopup.DataValueField = "Key";
            ddlMonthPopup.DataBind();
            ddlMonthPopup.Items.Insert(0, new ListItem("Any", "-1"));

        }
        private MessBlockRequestModel fillModel()
        {
            var model = new MessBlockRequestModel();
            model.Month = ddlMonths.SelectedValue;
            model.Year = txtYear.Text == "" ? 0 : Convert.ToInt32(txtYear.Text);
            model.Status = ddlStatus.SelectedValue;
            model.StudentId = UserBaseControl.UserId;
            return model;
        }

        public void LoadData(int pageNumber)
        {

            int totalRecords = 0;
            try
            {

                var model = fillModel();
                List<MessBlockRequestModel> list = Student_Accommodation_Hub.DAL.MessBill.GetMessBlockRequestsByStudent(model, pageSize, pageNumber, out totalRecords);
                if (list != null && list.Count > 0)
                {

                    if (totalRecords != 0)
                    {
                        bool isLastPage = (pageNumber >= Math.Ceiling((double)totalRecords / pageSize));
                        lblTotalRec.Text = totalRecords.ToString();
                        if (totalRecords < pageSize)
                        {
                            lblFromRec.Text = (((pageNumber - 1) * pageSize) + 1).ToString();
                            lblToRec.Text = totalRecords.ToString();
                        }
                        else
                        {
                            lblFromRec.Text = (((pageNumber - 1) * pageSize) + 1).ToString();
                            lblToRec.Text = (pageNumber * pageSize).ToString();
                            if (isLastPage)
                            {
                                lblToRec.Text = Math.Min(pageNumber * pageSize, totalRecords).ToString();
                            }
                        }
                        if (totalRecords > pageSize)
                        {
                            PagingUserControl1.TotalRecords = totalRecords;
                            PagingUserControl1.PageSize = pageSize;
                            PagingUserControl1.CurrentPage = pageNumber;
                            PagingUserControl1.BindPagination();
                            PagingUserControl1.Visible = true;
                        }
                        else
                        {
                            PagingUserControl1.Visible = false;
                        }



                    }
                    rprStudentMessBlockReqs.DataSource = list;
                    rprStudentMessBlockReqs.DataBind();
                    pnlPageDetail.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlPageDetail.Visible = false;
                    PagingUserControl1.Visible = false;
                }


            }
            catch (Exception)
            {
                ShowMessage("An error occurred.please try again.", "Error");
                PagingUserControl1.Visible = false;
                pnlPageDetail.Visible = false;
            }
        }
        private void ShowMessage(string message, string title)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblDialogTitle.Text = title;
            mpePopup.Show();

        }

        protected void rprStudentMessBlockReqs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var model = e.Item.DataItem as MessBlockRequestModel;
                var lblStatus = e.Item.FindControl("lblStatus") as Label;
                var lblApprovedDate = e.Item.FindControl("lblApprovedDate") as Label;
                if (model.Status == AppConstants.MessBlockRequestStatus.Approved)
                {
                    lblStatus.ForeColor = Color.Green;
                }
                else if (model.Status == AppConstants.MessBlockRequestStatus.Rejected)
                {
                    lblStatus.ForeColor = Color.Red;
                }
                else if (model.Status == AppConstants.MessBlockRequestStatus.Pending)
                {
                    lblStatus.ForeColor = Color.Yellow;
                    lblApprovedDate.Text = "";

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var model = new MessBlockRequestModel();
            model.StudentId = UserBaseControl.UserId;
            model.Month = ddlMonthPopup.SelectedValue;
            model.Year = Convert.ToInt32(txtYearPopup.Text);
            model.StartDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            model.EndDate = model.StartDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ;
            model.Reason = txtReason.Text;
            model.RequestedDate = DateTime.Now;
            model.Status = AppConstants.MessBlockRequestStatus.Pending;
            int result = Student_Accommodation_Hub.DAL.MessBill.SaveStudentMessBlockRequest(model);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccess", "showSuccessMessage();", true);
                LoadData(1);
            }
        }
        protected void PaginationControl_PageChanged(int pageNumber)
        {
            LoadData(pageNumber);
        }

        protected void ddlPageSize_TextChanged(object sender, EventArgs e)
        {
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            LoadData(1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }
    }
}