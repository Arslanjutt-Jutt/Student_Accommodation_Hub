using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using Student_Accommodation_Hub.PagingControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Admin
{
    public partial class ManageRoom : System.Web.UI.Page
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
                hlReset.NavigateUrl = AppConstants.CommonPath.ManageRoom;
                ddlPageSize.SelectedValue = pageSize.ToString();
                LoadData(1);
            }
        }
        public void LoadData(int pageNumber)
        {
            try
            {
                var room = new RoomModel();
                room.RoomNumber = txtRoomNo.Text;
                room.RoomType = ddlRoomType.SelectedValue;
                room.BlockNo = ddlBlockNo.SelectedValue; 
                int totalRecords = 0;
              
                List<RoomModel> rooms = Room.GetRooms(room, pageSize, pageNumber, out totalRecords);
                if (room != null && rooms.Count>0)
                {
                    rptRooms.DataSource = rooms;
                    rptRooms.DataBind();
                    pnlRoomDetail.Visible = true;
                    pnlNoRec.Visible = false;

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

                }
                else
                {
                    pnlNoRec.Visible = true;
                    pnlRoomDetail.Visible = false;
                    PagingUserControl1.Visible = false;
                }
            }
            catch (Exception ex) 
            {
                ShowMessage(ex.Message, "Error", true);
                PagingUserControl1.Visible = false;
                pnlRoomDetail.Visible = false;
                pnlNoRec.Visible = true;
            }

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

        protected void rptRooms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var room = e.Item.DataItem as RoomModel;

                var lnkStudentCount = e.Item.FindControl("lnkStudentCount") as HyperLink;
                lnkStudentCount.Text = room.StudentCount.ToString();
                if (room.StudentCount > 0)
                {
                    lnkStudentCount.NavigateUrl = AppConstants.CommonPath.defaultPage + "?" + AppConstants.QueryStringVariables.roomId + "=" + room.RoomId;
                    lnkStudentCount.Style.Add("cursor", "pointer");
                }
                var hlEditRoom = e.Item.FindControl("hlEditRoom") as HyperLink;
                hlEditRoom.NavigateUrl = AppConstants.CommonPath.AddEditRoom + "?" + AppConstants.QueryStringVariables.roomId + "=" + room.RoomId;
            } 
        }
    }
}