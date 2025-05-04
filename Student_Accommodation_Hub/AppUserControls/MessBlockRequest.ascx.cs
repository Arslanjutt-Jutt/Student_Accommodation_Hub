using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using Student_Accommodation_Hub.PagingControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class MessBlockRequest : System.Web.UI.UserControl
    {
        public int pageSize
        {
            get
            {
                if (ViewState["pageSize"] != null)
                    return (int)ViewState["pageSize"];
                return 5;
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

        }
        private MessBlockRequestModel fillModel()
        {
            var model = new MessBlockRequestModel();
            model.StudentName=txtStudentName.Text;
            model.Month = ddlMonths.SelectedValue;
            model.Year = txtYear.Text==""?0: Convert.ToInt32(txtYear.Text);
            model.Status = ddlStatus.SelectedValue;

            return model;
        }
        public void LoadData(int pageNumber)
        {

            int totalRecords = 0;
            try
            {
               
                    var model = fillModel();
                    List<MessBlockRequestModel> list = MessBill.GetMessBlockRequests(model, pageSize, pageNumber, out totalRecords);
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
                        rprMessBlockRequests.DataSource = list;
                    rprMessBlockRequests.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtYear.Text = string.Empty;
            txtStudentName.Text = string.Empty;
            ddlMonths.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            LoadData(1);

        }

        protected void rprMessBlockRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var model = e.Item.DataItem as MessBlockRequestModel;
                    var lbtnAccept = e.Item.FindControl("lbtnAccept") as LinkButton;
                    var lbtnReject = e.Item.FindControl("lbtnReject") as LinkButton;
                    var lblStatus = e.Item.FindControl("lblStatus") as Label;
                    if (model.Status == AppConstants.MessBlockRequestStatus.Approved || model.Status == AppConstants.MessBlockRequestStatus.Rejected)
                    {
                        lbtnAccept.Enabled = false;
                        lbtnReject.Enabled = false;
                    }
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
                    }
                }
            }
            catch (Exception)
            {

                ShowMessage("An error occurred.please try again.", "Error");
            }
        }

        protected void lbtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                int requestId = Convert.ToInt32(btn.CommandArgument);
                if (requestId > 0)
                {
                    int result = MessBill.RejectMessBlockRequest(requestId);
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccess", "showSuccessMessage();", true);
                        LoadData(1);
                    }
                    else
                    {
                        ShowMessage("An error occurred.please try again.", "Error");
                    }
                }
            }
            catch (Exception)
            {

                ShowMessage("An error occurred.please try again.", "Error");
            }
        }

        protected void lbtnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (LinkButton)sender;
                int requestId = Convert.ToInt32(btn.CommandArgument);
                if (requestId > 0)
                {
                    int result = MessBill.ApproveMessBlockRequest(requestId);
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccess", "showSuccessMessage('Request Approved successfully!');", true);
                        LoadData(1);
                    }
                    else
                    {
                        ShowMessage("An error occurred.please try again.", "Error");
                    }
                }
            }
            catch (Exception)
            {

                ShowMessage("An error occurred.please try again.", "Error");
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
    }
}