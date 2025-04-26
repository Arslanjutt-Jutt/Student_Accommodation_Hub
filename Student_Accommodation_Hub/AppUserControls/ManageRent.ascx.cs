using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using Student_Accommodation_Hub.PagingControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class ManageRent : System.Web.UI.UserControl
    {
        public int studentId
        {
            get
            {
                if (ViewState["studentId"] != null)
                    return (int)ViewState["studentId"];
                return 0;
            }
            set
            {
                ViewState["studentId"] = value;
            }
        }
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
                if (UserBaseControl.UserRole == AppConstants.UserRole.Student)
                {
                    studentId = UserBaseControl.UserId;
                }
                else
                {
                    studentId = Convert.ToInt32(Request.QueryString[AppConstants.QueryStringVariables.studentId]);
                }
                PreparePage();
                LoadData(1);
            }
        }
        public void PreparePage()
        {
            if (UserBaseControl.UserRole == AppConstants.UserRole.Student)
            {
                lblPageHeading.Text = "Hostel Rent History";
            }
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
        public void LoadData(int pageNumber)
        {
           
            int totalRecords = 0;
            try
            {
                if (studentId > 0)
              {
                var model = fillModel();
                List<HostelRentModel> list = HostelRent.GetHostelRentData(model, pageSize, pageNumber, out totalRecords);
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
                    rprHostelRent.DataSource = list;
                    rprHostelRent.DataBind();
                    pnlRentDetail.Visible = true;
                    pnlNoRec.Visible = false;


                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlRentDetail.Visible = false;
                    PagingUserControl1.Visible = false;
                }
                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlRentDetail.Visible = false;
                    PagingUserControl1.Visible = false;
                }

            }
            catch (Exception) 
            {
                ShowMessage("An error occurred.please try again.", "Error");
                PagingUserControl1.Visible = false;
                pnlRentDetail.Visible = false;
            }
        }
        private HostelRentModel fillModel()
        {
            var model = new HostelRentModel();
            model.StudentId = studentId;
            model.MonthName = ddlMonths.SelectedValue;
            model.Year = txtYear.Text;
            model.PaymentStatus = Convert.ToInt32(ddlStatus.SelectedValue);


            return model;
        }
        protected void PaginationControl_PageChanged(int pageNumber)
        {
            CurrentPage = pageNumber;
            LoadData(pageNumber);
        }

        protected void rprHostelRent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                var pnlActionHead = e.Item.FindControl("pnlActionHead") as Panel;
                if (UserBaseControl.UserRole == AppConstants.UserRole.Student)
                {

                    pnlActionHead.Visible = false;
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var model = e.Item.DataItem as HostelRentModel;
                var btnChangeStatus = e.Item.FindControl("btnChangeStatus") as Button;
                var pnlAction = e.Item.FindControl("pnlAction") as Panel;

                if (UserBaseControl.UserRole == AppConstants.UserRole.Student)
                {
                    pnlAction.Visible = false;

                }
                if (model.PaymentStatus==(int)AppConstants.RoomRentStatus.paid)
                {
                    btnChangeStatus.CssClass= "btn btn-outline-success btn-sm";
                    btnChangeStatus.CommandName = "paid";
                }
                else
                {
                    btnChangeStatus.CommandName = "pending";
                }
            }

            }

        protected void btnChangeStatus_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int rentId = Convert.ToInt32(btn.CommandArgument);
            try
            {
                if (btn.CommandName == "paid")
                {
                    int result = HostelRent.UpdatePaymentStatusById(rentId, 0);
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage();", true);
                        LoadData(CurrentPage);
                    }

                }
                if (btn.CommandName == "pending")
                {
                    int result = HostelRent.UpdatePaymentStatusById(rentId, 1);
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage();", true);
                        LoadData(CurrentPage);
                    }
                }
            }
            catch(Exception ex)
            {
                ShowMessage("An error occurred.please try again.", "Error");
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new HostelRentModel();
                model.DueDate = Convert.ToDateTime(txtDueDate.Text);
                model.TotalRent = Convert.ToDecimal(txtTotalRent.Text);
                model.Remarks = txtRemarks.Text;
                model.RentId = Convert.ToInt32(hfRentID.Value);
                int result = HostelRent.UpdateHostelRentById(model);
                if (result == 1)
                {
                    mpeEditHostelRentPopup.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage('Rent detail updated successfully!');", true);
                    LoadData(CurrentPage);
                }
                if (result == -1)
                {
                    mpeEditHostelRentPopup.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage('An error Occurred.');", true);
                }


            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, "Error");
            }
        }

        protected void hlReset_Click(object sender, EventArgs e)
        {
            txtYear.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlMonths.SelectedIndex = 0;
            LoadData(1);
        }

        protected void ddlPageSize_TextChanged(object sender, EventArgs e)
        {
            pageSize= Convert.ToInt32(ddlPageSize.SelectedValue);
            LoadData(1);
        }
    }
}