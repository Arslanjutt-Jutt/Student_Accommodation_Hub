<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageMessManu.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.ManageMessManu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="pageHeading">
        <table style="width: 100%; margin-right: 5px">
            <tr>
                <td>
                    <asp:Label ID="lblPageHeading" runat="server" Text="Manage Mess Manu"></asp:Label>
                </td>
               
            </tr>
        </table>

    </div>
    <asp:Panel ID="pnlMessManu" runat="server" Style="width: 100%; margin-top: 30px">
        <div>
            <asp:Repeater ID="rprMessManu" runat="server" OnItemDataBound="rprMessManu_ItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%" class="tblShowData">

                        <tr>
                            <th>DayOfWeek</th>
                            <th>Break Fast</th>
                            <th>Lunch</th>
                            <th>Dinner</th>
                            <th>Action</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="rowControl">
                        <td>
                            <label id="lblDayofWeek">  <%# Eval("DayOfWeek") %></label>
                            <asp:HiddenField ID="hfMessId" runat="server" Value='<%# Eval("ID") %>' />
                           </td>

                        <td>
                            <label id="lblBreakFast"> <%# Eval("Breakfast") %></label>
                           </td>

                        <td>
                            <label id="lblLunch"> <%# Eval("Lunch") %></label>
                           </td>

                        <td>
                            <label id="lblDinner"> <%# Eval("Dinner") %></label>
                           </td>
                        <td style="width:70px">
                            <asp:LinkButton ID="hlMessManu" Style="cursor: pointer" runat="server" OnClientClick="return openPopup(this)">
                                <asp:Image ID="imgEdit" runat="server" Height="22px" ImageUrl="~/ImageIcons/EditIcon.svg" ToolTip="Edit this weak mess manu." />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
       <asp:Panel ID="pnlNoRec" CssClass="pnlNoRecFound" runat="server" style="width:100%">
       <div>
           No records found.
       </div>
   </asp:Panel>
    <!-- Success Message Box (Initially Hidden) -->
<div id="successMessageBox" class="success-box" style="display: none;">
    Mess Menu Updated successfully!
</div>

<ajaxToolkit:ModalPopupExtender 
    ID="mpeUpdateMessPopup" 
    runat="server" 
    TargetControlID="lblDummy" 
    PopupControlID="pnlMessageDialog"  
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" style="display:none;width:700px;height:300px">
    <div class="dialog-header">
      <asp:Label ID="lblDialogTitle" runat="server" Text="Update Mess Manu" style="font-weight:bold;font-size:14px"></asp:Label> 
         <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClientClick="return closePopup();" />
    </div>
    <div class="dialog-body" style="height:70%" >
         <asp:HiddenField ID="hfMessID" runat="server" />
        <table class="tblPopupFields">
            <tr>
                <td style="width:30px"></td>
                <td class="lbltd"> <label>Day of Week:</label></td>
                <td>
                    <asp:DropDownList ID="ddlDayOfWeek" Enabled="false" runat="server" Width="200px" CssClass="ftrFields">
                         
                        <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                        <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                        <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                        <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                        <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                        <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                        <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>Breakfast:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtBreakfast" runat="server" CssClass="ftrFields"></asp:TextBox>
                 </td>
            </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>Lunch:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtLunch" runat="server" CssClass="ftrFields"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>Dinner:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtDinner" runat="server" CssClass="ftrFields"></asp:TextBox>
                 </td>
             </tr>
        </table>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="Button1" runat="server" Width="80px" Height="30px" BackColor="White" OnClientClick="return closePopup();" CssClass="btnCancel" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnUpdate" runat="server" Width="80px" Height="30px" CssClass="btn btn-primary btn-sm" OnClick="btnUpdate_Click" Text="Update" />
           
   </div>
</asp:Panel>

    <ajaxToolkit:ModalPopupExtender
        ID="mpePopup"
        runat="server"
        TargetControlID="dummyLabel"
        PopupControlID="pnlMessageDialog"
        BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="dummyLabel" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="Panel1" runat="server" CssClass="message-dialog" style="display:none">
    <div class="dialog-header">
        <asp:Label ID="lblTitle" runat="server" Text="Success/Error" style="font-weight:bold"></asp:Label>
        <asp:Button ID="butnclose" BackColor="White" runat="server" CssClass="btn-close" />
       
      
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>
    <script>
        function openPopup(sender) {
            // Close the ModalPopupExtender using JavaScript
            var popup = $find('<%= mpeUpdateMessPopup.ClientID %>');
            var item = $(sender).closest(".rowControl");
            var dayOfWeek = $(item).find("#lblDayofWeek").text().trim();
            var breakfast = $(item).find("#lblBreakFast").text().trim();
            var lunch = $(item).find("#lblLunch").text().trim();
            var dinner = $(item).find("#lblDinner").text().trim();
            var id = $(item).find("input[type='hidden'][id*='hfMessId']").val();
           

            // Fill the modal fields
            $("#<%= hfMessID.ClientID %>").val(id);
            $("#<%= ddlDayOfWeek.ClientID %>").val(dayOfWeek);
            $("#<%= txtBreakfast.ClientID %>").val(breakfast);
            $("#<%= txtLunch.ClientID %>").val(lunch);
            $("#<%= txtDinner.ClientID %>").val(dinner);
             popup.show();
             return false;
        }
        function closePopup() {
            // Close the ModalPopupExtender using JavaScript
            var popup = $find('<%= mpeUpdateMessPopup.ClientID %>');
              popup.hide();
              return false;
        }
        function closePopupAndShowMessage(message) {
            // Close the modal popup
            var popup = $find('<%= mpeUpdateMessPopup.ClientID %>');
                if (popup) {
                    popup.hide();
                }
            var msgBox = document.getElementById('successMessageBox');

            if (message) {
                msgBox.textContent = message;
            } // Show the success message box
           
            
                msgBox.style.display = 'block';

                // Hide the message after 1 second
            $("#successMessageBox").stop(true, true).fadeIn(800).delay(1500).fadeOut(1000);
        }
        function hidePopup() {
            var popup = $find('<%= mpePopup.ClientID %>'); // Get the modal popup extender
                if (popup) {
                    popup.hide(); // Hide the popup
                }
            }
    </script>
</asp:Content>
