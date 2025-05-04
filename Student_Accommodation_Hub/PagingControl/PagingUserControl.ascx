<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagingUserControl.ascx.cs" Inherits="Student_Accommodation_Hub.PagingControl.PagingUserControl" %>
<style>
    .pagination-container {
       
        margin-top: 10px;
        border:1px solid #CCCCCC;
        display:flex;
        margin-bottom:5px
       
    }
    .pagination-btn
    { 
        padding: 5px 15px;
        border-right: 1px solid #CCCCCC;
        color: #007bff;
        text-decoration: none;
        display: inline-block;
        border-top:none;
        border-bottom:none;
        height:100%;
        box-sizing:border-box;
        font-size:14px;

    }

    .pagination-link {
        padding: 5px 20px;
        border-right: 1px solid #CCCCCC;
        color: #007bff;
        text-decoration: none;
        display: inline-block;
        border-top: none;
        border-bottom: none;
        font-size:14px;
        height: 100%
    }
    .pagination-link:hover 
    { 
        
        color: black; 

    }
    .tblPaging a.active
    {
        color:#CCCCCC;
        
    }
    .tblPaging .aspNetDisabled 
    {
         color:#CCCCCC;
    }
    
</style>
<div class="pagination-container">
    <table class="tblPaging" style="width:100%">
        <tr>
            <td>
               
             <asp:LinkButton ID="lbtnPre" runat="server" CssClass="pagination-btn" OnClick="btnPrev_Click"><</asp:LinkButton>
                <asp:Repeater ID="rptPagination" runat="server" OnItemDataBound="rptPagination_ItemDataBound" OnItemCommand="rptPagination_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPage" runat="server"
                            Text='<%# Eval("PageNumber") %>'
                            CommandArgument='<%# Eval("PageNumber") %>'
                            CssClass='<%# (Convert.ToInt32(Eval("PageNumber")) == CurrentPage) ? "pagination-link active" : "pagination-link" %>'
                            CommandName="ChangePage"></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                 <asp:LinkButton ID="lbtnNext" runat="server" CssClass="pagination-btn" OnClick="btnNext_Click">></asp:LinkButton>
            </td>
            <td style="text-align:right">
             <asp:LinkButton ID="btnFirst" runat="server" style="border-left:1px solid #CCCCCC" CssClass="pagination-btn" OnClick="btnFirst_Click"><< First</asp:LinkButton>
             <asp:LinkButton ID="btnPrev" runat="server" CssClass="pagination-btn" OnClick="btnPrev_Click">< Prev</asp:LinkButton>
             <asp:LinkButton ID="btnNext" runat="server" CssClass="pagination-btn" OnClick="btnNext_Click">Next ></asp:LinkButton>
             <asp:LinkButton ID="btnLast" runat="server" style="border-right:none" CssClass="pagination-btn" OnClick="btnLast_Click">Last >></asp:LinkButton>
            
            </td>
            
        </tr>
    </table>
   

   

   
</div>

