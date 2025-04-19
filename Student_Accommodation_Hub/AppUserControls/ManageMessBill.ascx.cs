using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using Student_Accommodation_Hub.PagingControl;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class ManageMessBill : System.Web.UI.UserControl
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
                studentId = Convert.ToInt32(Request.QueryString[AppConstants.QueryStringVariables.studentId]);
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
        private MessBillModel fillModel()
        {
            var model = new MessBillModel();
            model.Month = ddlMonths.SelectedValue;
            model.Year = txtYear.Text == "" ? 0 : Convert.ToInt32(txtYear.Text);
            model.PaymentStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            model.StudentId = studentId;
            return model;
        }

        public void LoadData(int pageNumber)
        {
            int totalRecords = 0;
            try
            {

                var model = fillModel();
                List<MessBillModel> list = MessBill.GetMessBillData(model, pageSize, pageNumber, out totalRecords);
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
                    rprMessBill.DataSource = list;
                    rprMessBill.DataBind();
                    pnlMessDetail.Visible = true;
                    pnlNoRec.Visible = false;


                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlMessDetail.Visible = false;
                    PagingUserControl1.Visible = false;
                }

            }
            catch (Exception)
            {
                ShowMessage("An error occurred.please try again.", "Error");
                PagingUserControl1.Visible = false;
                pnlMessDetail.Visible = false;
            }
        }
        private void ShowMessage(string message, string title)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblDialogTitle.Text = title;

            mpePopup.Show();

        }
        protected void PaginationControl_PageChanged(int pageNumber)
        {
            CurrentPage = pageNumber;
            LoadData(pageNumber);
        }

        protected void rprMessBill_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var model = e.Item.DataItem as MessBillModel;
                string year = "20" + model.Year.ToString();
                int monthNumber = DateTime.ParseExact(model.Month, "MMMM", CultureInfo.InvariantCulture).Month;
                int totalDays = DateTime.DaysInMonth(Convert.ToInt32(year), monthNumber);
                int deductionAmount = model.BlockedDays * (Convert.ToInt32(model.TotalBill) / totalDays);

                var lblDeductionAmount = e.Item.FindControl("lblDeductionAmount") as Label;
                lblDeductionAmount.Text = deductionAmount.ToString();
                var lblFinalBill = e.Item.FindControl("lblFinalBill") as Label;
                lblFinalBill.Text = (Convert.ToInt32(model.TotalBill) - deductionAmount).ToString();
                var btnChangeStatus = e.Item.FindControl("btnChangeStatus") as Button;

                if (model.PaymentStatus == (int)AppConstants.RoomRentStatus.paid)
                {
                    btnChangeStatus.CssClass = "btn btn-outline-success btn-sm";
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
                    int result = MessBill.UpdatePaymentStatusById(rentId, 0);
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage();", true);
                        LoadData(CurrentPage);
                    }

                }
                if (btn.CommandName == "pending")
                {
                    int result = MessBill.UpdatePaymentStatusById(rentId, 1);
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage();", true);
                        LoadData(CurrentPage);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred.please try again.", "Error");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new MessBillModel();
                model.DueDate = Convert.ToDateTime(txtDueDate.Text);
                model.TotalBill = Convert.ToDecimal(txtTotalBill.Text);
                model.Remarks = txtRemarks.Text;
                model.BillId = Convert.ToInt32(hfMessBillID.Value);
                int result = MessBill.UpdateMessBillById(model);
                if (result == 1)
                {
                    mpeEditMessBillPopup.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage('Mess Bill updated successfully!');", true);
                    LoadData(CurrentPage);
                }
                if (result == -1)
                {
                    mpeEditMessBillPopup.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "ShowMessage('An error Occurred.');", true);
                }


            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Error");
            }
        }
    }
}