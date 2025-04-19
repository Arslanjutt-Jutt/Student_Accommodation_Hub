using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Student_Accommodation_Hub.Admin
{
    public partial class _default : System.Web.UI.Page
    {
        public int roomId
        {
            get
            {
                if (ViewState["roomId"] != null)
                    return (int)ViewState["roomId"];
                return 0;
            }
            set
            {
                ViewState["roomId"] = value;
            }
        }
        public int pageSize
        {
            get
            {
                if(ViewState["pageSize"]!=null)
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
                roomId = Convert.ToInt32(Request.QueryString[AppConstants.QueryStringVariables.roomId]);
                preparePage();
                LoadData(1);
               
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            // Unsubscribe to avoid multiple subscriptions
            PagingUserControl1.OnPageChanged -= PaginationControl_PageChanged;
        }

        private void  preparePage()
        {
            
            LoadCountries();
            List<RoomModel> rooms = Room.GetRooms();
            ddlRoomNumber.DataSource = rooms;
            ddlRoomNumber.DataTextField = "RoomNumber";
            ddlRoomNumber.DataValueField = "RoomId";
            ddlRoomNumber.DataBind();
            ddlRoomNumber.Items.Insert(0, new ListItem("All", "0"));
            ddlRoomNumber.SelectedValue = "0";
            chkSecurityDeposit.Checked = true;
            hlReset.NavigateUrl = AppConstants.CommonPath.defaultPage;
            if (roomId > 0)
            {
                ddlRoomNumber.SelectedValue = roomId.ToString();
                
            }
        }
        private void LoadCountries()
        {

            List<CountryModel> countries = WebUtilty.GetAllCountries();

            ddlCountry.DataSource = countries;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("All", "0"));
            ddlCountry.SelectedValue = "0";

        }

        private void LoadData(int pageNumber)
        {
            try
            {
                var student = FillStudentModel();
                
                int totalRecords = 0;
                bool isLastPage = (pageNumber >= Math.Ceiling((double)totalRecords / pageSize));
                List<StudentDataModel> students= Student.GetStudents(student, pageSize, pageNumber, out totalRecords);
                if (students != null && students.Count>0)
                {
                    rptStudents.DataSource = students;
                    rptStudents.DataBind();
                    pnlStudentDetail.Visible = true;
                    pnlNoRec.Visible = false;
                   
                    if (totalRecords != 0)
                    {
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
                             lblToRec.Text= Math.Min(pageNumber * pageSize, totalRecords).ToString();
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
                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlStudentDetail.Visible = false;
                    PagingUserControl1.Visible = false;
                }
                if (roomId > 0)
                {
                    lblManageStudents.Text = "Manage " + ddlRoomNumber.SelectedItem + " Students";
                }
               
                
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, "Error", true);
                PagingUserControl1.Visible = false;
                pnlStudentDetail.Visible = false;
            }
        }
        public StudentDataModel FillStudentModel()
        {
            // Create a new StudentDataModel instance
            var student = new StudentDataModel();

            // Assign values to the object properties
            student.StudentName = string.IsNullOrWhiteSpace(txtStudentName.Text) ? null : txtStudentName.Text.Trim();
            student.CNIC = string.IsNullOrWhiteSpace(txtStudentID.Text) ? null : txtStudentID.Text.Trim();
            student.Gender = ddlGender.SelectedValue != "0" ? ddlGender.SelectedValue : null;
            student.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            student.PhoneNumber = string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ? null : txtPhoneNumber.Text.Trim();

            student.CountryID = (ddlCountry.SelectedValue != "0" ? Convert.ToInt32(ddlCountry.SelectedValue) : (int?)null);
            if(ddlState.SelectedValue =="0" || ddlState.SelectedValue == "")
            {
                student.StateID = (int?)null;
               
            }
            else
            {
                student.StateID = Convert.ToInt32(ddlState.SelectedValue);
            }

            student.BlockNo = ddlBlockNo.SelectedValue != "0" ? ddlBlockNo.SelectedValue : null;
            student.RoomId = (ddlRoomNumber.SelectedValue != "0" ? Convert.ToInt32(ddlRoomNumber.SelectedValue) : (int?)null);

            student.HasSecurityDeposit = chkSecurityDeposit.Checked;

            // Return the filled object
            return student;
        }


        private void ShowMessage(string message, string title, bool isStayOnPage)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblDialogTitle.Text = title;
            
            mpePopup.Show();

        }
        protected void btnCloseDialog_Click(object sender, EventArgs e)
        {
            mpePopup.Hide();
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(1);
        }

        protected void ddlPageSize_TextChanged(object sender, EventArgs e)
        {
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            LoadData(1);
        }
        protected void PaginationControl_PageChanged(int pageNumber)
        {
            LoadData(pageNumber);
        }

        protected void rptStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var student =(StudentDataModel)  e.Item.DataItem  ;
                var lblDob = e.Item.FindControl("lblDob") as Label;
                var hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
                lblDob.Text = student.Dob.ToString("MM/dd/yyyy");
                hlEdit.NavigateUrl = AppConstants.CommonPath.AddEditStudentPage + "?" + AppConstants.QueryStringVariables.studentId + "=" + student.StudentID;
                var hlViewHostelRent = e.Item.FindControl("hlViewHostelRent") as HyperLink;
                hlViewHostelRent.NavigateUrl = AppConstants.CommonPath.ManageRent + "?" + AppConstants.QueryStringVariables.studentId + "=" + student.StudentID;
                var hlViewMessBill = e.Item.FindControl("hlViewMessBill") as HyperLink;
                hlViewMessBill.NavigateUrl = AppConstants.CommonPath.ManageMessBill + "?" + AppConstants.QueryStringVariables.studentId + "=" + student.StudentID;
            }
        }

        

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var btnDelete = (Button)sender;
                int studentID = Convert.ToInt32(btnDelete.CommandArgument);
               int result= Student.DeleteStudentRecord(studentID);
                if (result == 1)
                {
                    ShowMessage("Student has been deleted successfully.", "Message", true);
                }
                else
                {
                    ShowMessage("Student has not been deleted.", "Message", true);
                }
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, "Error", true);
            }
        }
    }
}