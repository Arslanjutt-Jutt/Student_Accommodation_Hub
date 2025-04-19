<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddEditRoom.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.AddEditRoom" %>

<%@ Register Src="~/Admin/UserControls/AddEditRoom.ascx" TagPrefix="uc1" TagName="AddEditRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            width: 80%;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
        }

        .form-table {
            width: 100%;
            border-collapse: collapse;
        }

            .form-table td {
                padding: 10px;
                vertical-align: top;
            }

        label {
            display: block;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="pageHeading">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblPageHeading" runat="server" Text="Add New Room"></asp:Label>
            </td>
        </tr>
    </table>

</div>
<div class="form-container">
    <table class="form-table">
        <tr>
            <td class="lbltd">
                <label for="RoomNumber">Room Number:</label></td>
            <td>
                <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>

        <tr>
            <td class="lbltd">
                <label for="RoomType">Room Type:</label></td>
            <td>
                <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Room Type--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="4-Seater Room" Value="4"></asp:ListItem>
                    <asp:ListItem Text="6-Seater Room" Value="6"></asp:ListItem>
                    <asp:ListItem Text="8-Seater Room" Value="8"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td class="lbltd">
                <label for="BlockNo">Block No:</label></td>
            <td>
                <asp:DropDownList ID="ddlBlockNo" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select One--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>

       

        <tr>
            <td class="lbltd">
                <label for="RoomRent">Room Rent (Per Month):</label></td>
            <td>
                <asp:TextBox ID="txtRoomRent" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>

        <tr>
            <td class="lbltd">
                <label for="SecurityDeposit">Security Deposit:</label></td>
            <td>
                <asp:TextBox ID="txtSecurityDeposit" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>


        <tr>
            <td colspan="2">
                <table style="width: 500px">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkHasAttachedBathroom" TextAlign="Left" Text="Has Attached Bathroom" CssClass="chkBox" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkHasAC" TextAlign="Left" Text="Has AC?" CssClass="chkBox" runat="server" /></td>
                        <td>
                            <asp:CheckBox ID="chkHasWifi" TextAlign="Left" Text="Has WiFi?" CssClass="chkBox" runat="server" /></td>
                    </tr>
                </table>




        </tr>

        <tr>
            <td colspan="2" style="padding-top: 20px">
                <asp:Button ID="btnCencel" runat="server" Text="Cancel" OnClick="btnCencel_Click" CssClass="btnCancel" />
                &nbsp; &nbsp;
                       
                <asp:Button ID="btnAddRoom" runat="server" style="min-width:90px" Text="Add Room" OnClick="btnAddRoom_Click" OnClientClick="return validateForm() && !document.getElementById('pnlMessageDialog').style.display;" CssClass="btn btn-primary" />

            </td>
        </tr>
    </table>
</div>
<!-- Success/Error Message Panel -->
<ajaxToolkit:ModalPopupExtender 
    ID="mpePopup" 
    runat="server" 
    TargetControlID="lblDummy" 
    PopupControlID="pnlMessageDialog" 
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" style="display:none">
    <div class="dialog-header">
        <asp:Label ID="lblDialogTitle" runat="server" Text="Success/Error" style="font-weight:bold"></asp:Label>

        <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClick="btnCloseDialog_Click" />
       
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" OnClick="btnCloseDialog_Click" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>

<script>
    function validateForm() {
    var isValid = true;
    
    // Function to check if a field is empty
    function checkField(id) {
        var field = document.getElementById(id);
        if (field.value.trim() === "" || field.value === "-1") {
            field.style.backgroundColor = 'yellow';
            isValid = false;
        } else {
            field.style.backgroundColor = '';
        }
    }

    // Validate required fields
    checkField('<%= txtRoomNumber.ClientID %>');
    checkField('<%= ddlRoomType.ClientID %>');
    checkField('<%= ddlBlockNo.ClientID %>');
    
    checkField('<%= txtRoomRent.ClientID %>');
    
    if (!isValid) {
        alert('Please fill all required fields highlighted in yellow.');
        return false;
    }
    return true;
    }
  function hidePopup() {
        var popup = $find('<%= mpePopup.ClientID %>'); // Get the modal popup extender
     if (popup) {
         popup.hide(); // Hide the popup
     }
 }
</script>
</asp:Content>
