<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebAuthen.aspx.cs" Inherits="Line_Bot_Application2.WebAuthen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style1">
        <br />
        <asp:Image ID="Image1" runat="server" Height="217px" ImageUrl="~/17495751_753035091529353_1517969527_n.jpg" Width="420px" />
        <br />

    </div>
        <p class="auto-style1">

        <asp:Label ID="Label2" runat="server" Text="StudentID : "></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="138px"></asp:TextBox>
        </p>
        <p class="auto-style1">
        <asp:Label ID="Label3" runat="server" Text="Password : "></asp:Label>
            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="141px"></asp:TextBox>
        </p>
        <p class="auto-style1">
            &nbsp;</p>
        
        <p class="auto-style1">
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        <p class="auto-style1">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Login" Width="162px" OnClick="Button1_Click" Height="29px" style="text-align: center" />
    </form>
</body>
</html>
