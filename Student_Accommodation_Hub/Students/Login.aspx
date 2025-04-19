<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Student_Accommodation_Hub.Students.Login" %>
<%@ Register Src="~/AppUserControls/UserLogin.ascx" TagPrefix="uc1" TagName="UserLogin" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="../Admin/css/main.css" rel="stylesheet" />
<script src="../Scripts/jquery-3.7.1.js"></script>
<script src="../Scripts/jquery-3.7.1.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
<script src="js/app.js"></script>
<script src="../Scripts/bootstrap.bundle.min.js"></script>
<link href="../Content/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <uc1:UserLogin runat="server" id="UserLogin" />
        </div>
    </form>
</body>
</html>
