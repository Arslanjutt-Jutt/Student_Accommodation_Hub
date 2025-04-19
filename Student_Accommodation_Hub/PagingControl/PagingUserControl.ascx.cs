using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.PagingControl
{
    public partial class PagingUserControl : System.Web.UI.UserControl
    {
        public delegate void PageChangedHandler(int pageNumber);
        public event PageChangedHandler OnPageChanged;
        public int PageSize
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
        public int TotalRecords
        {
            get
            {
                if (ViewState["TotalRecords"] != null)
                    return (int)ViewState["TotalRecords"];
                return 0;
            }
            set
            {
                ViewState["TotalRecords"] = value;
            }
        }

        private int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public int CurrentPage {
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
            if (!IsPostBack)
            {
                
                    BindPagination();
                 
                
               
            }
        }
        public void BindPagination()
        {
            List<object> pages = new List<object>();
            int MaxPageButtons = 10;
            // Calculate the range of page numbers to display
            int startPage = ((CurrentPage - 1) / MaxPageButtons) * MaxPageButtons + 1;
            int endPage = Math.Min(startPage + MaxPageButtons - 1, TotalPages);

            for (int i = startPage; i <= endPage; i++)
            {
                pages.Add(new { PageNumber = i });
            }

            rptPagination.DataSource = pages;
            rptPagination.DataBind();

            // Enable/Disable navigation buttons
            btnFirst.Enabled = btnPrev.Enabled = (CurrentPage > 1);
            btnNext.Enabled = btnLast.Enabled = (CurrentPage < TotalPages);
            lbtnNext.Enabled= (CurrentPage < TotalPages);
            lbtnPre.Enabled = (CurrentPage > 1);


        }

        protected void rptPagination_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument);
                OnPageChanged?.Invoke(CurrentPage);
                BindPagination();
            }

        }

        protected void btnFirst_Click(object sender, EventArgs e) => ChangePage(1);
        protected void btnPrev_Click(object sender, EventArgs e) => ChangePage(CurrentPage==1? CurrentPage: CurrentPage - 1);
        protected void btnNext_Click(object sender, EventArgs e) 
        {
            CurrentPage += 1;
            ChangePage(CurrentPage);
        }
        protected void btnLast_Click(object sender, EventArgs e) => ChangePage(TotalPages);

        private void ChangePage(int page)
        {
            if (page < 1 || page > TotalPages) return;
            CurrentPage = page;
            OnPageChanged?.Invoke(CurrentPage);
            BindPagination();
        }

        protected void rptPagination_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var btnPage =(LinkButton) e.Item.FindControl("btnPage");

                if (CurrentPage == Convert.ToInt32(btnPage.Text))
                {
                    btnPage.Enabled = false;
                   
                }
            }
        }
    }
}