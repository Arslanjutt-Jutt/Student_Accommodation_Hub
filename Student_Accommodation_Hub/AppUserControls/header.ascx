<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.header" %>
<%@ Register Src="~/AppUserControls/ChangePassword.ascx" TagPrefix="uc4" TagName="ChangePassword" %> 
<div class="header-container">
    <div style="float:left">
        <img style="height:40px;width:200px" src="../ImageIcons/BrandLogo.svg" />
    </div>

    <div style="position: absolute; left: 50%; transform: translateX(-50%); top: 50%; transform: translate(-50%, -50%);font-weight:bold;font-size:18px;color:#199ad6">
        Student's Portal
    </div>
    <div class="dropdown-container">
        <div class="dropdown-box">
            <div class="user-avatar">
                <asp:Image ID="imgProfile" Width="18px" runat="server" ImageUrl="~/ImageIcons/admin.svg" />
            </div>&nbsp;&nbsp;
            <div class="user-info">Welcome: <%= Student_Accommodation_Hub.AppUtilties.UserBaseControl.UserName %></div>
            <div class="dropdown-icon">
                <img src="../ImageIcons/arrow.svg" width="10px" />

            </div>
        </div>
        <div class="dropdown-itemsBox">
            <asp:HyperLink ID="lbtnProfile" runat="server" NavigateUrl="~/Students/Profile.aspx" class="dropdown-option">Profile</asp:HyperLink>
            <asp:LinkButton ID="lbtnChangePassword" runat="server" OnClientClick="return openPopup();" class="dropdown-option">Change Password</asp:LinkButton>
            <asp:LinkButton ID="lbtnLogOut" runat="server" OnClick="lbtnLogOut_Click" class="dropdown-option fa fa-sign-out">
                <img src="../ImageIcons/logout.png" width="15px" /> Logout</asp:LinkButton>
        </div> 
    </div>
</div>

<div class="manu-container">
    <div style="width: 1000px; display: flex; margin: auto;">
        <div class="lnk-page"> 
            <asp:HyperLink runat="server" NavigateUrl="~/Students/default.aspx" ID="hlDefaultPage">Room Detail</asp:HyperLink>
        </div>
        <div class="lnk-page">
            <asp:HyperLink ID="hlHostelRent" runat="server">Hostel Rent</asp:HyperLink>
        </div>
        <div class="lnk-page">
           <asp:HyperLink ID="hlMessBill" runat="server">Mess Bill</asp:HyperLink>
        </div>
        <div class="lnk-page">
            <asp:HyperLink runat="server" NavigateUrl="~/Students/MessManu.aspx" ID="hlMessManu">Mess Manu</asp:HyperLink>
        </div>
        <div class="lnk-page">
            <asp:HyperLink runat="server" NavigateUrl="~/Students/StudentMessBlockReqs.aspx" ID="hlMessBlockRequests">Mess Block Requests</asp:HyperLink>
        </div>
    </div>
</div>


 <div id="successMessageChangePassword" class="alert alert-success" style="display:none; position: fixed; top: 20px; left: 50%; transform: translateX(-50%); z-index: 9999; min-width: 250px;">
     Password changed successfully!
 </div>

<uc4:ChangePassword id="ucChangePassword" runat="server" />
<script>
        $(document).ready(function() {
            let fadeSpeed = 200;
            let timeoutId;
            // Show dropdown on hover with fade effect
            $("#userDropdown").hover(
                function() {
                    clearTimeout(timeoutId);
                    $("#dropdownMenu").stop().fadeIn(fadeSpeed);
                },
                function() {
                    timeoutId = setTimeout(function() {
                        $("#dropdownMenu").stop().fadeOut(fadeSpeed);
                    }, 200); // Small delay before hiding to improve UX
                }
            ); 
           
        });
    function showSuccessChangePasswordMessage() {
        $('#successMessageChangePassword').fadeIn().delay(3000).fadeOut();
    }
   
</script>