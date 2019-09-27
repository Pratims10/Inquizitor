<%@ Page Title="Forgot Password?" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="forgot_password.aspx.cs" Inherits="WebApplication3.forgot_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <asp:Label ID="Label1" style="margin-left:33%" runat="server" Text="Enter Your Username"></asp:Label>
    &nbsp;<asp:TextBox CssClass="btnregister" Width="220px" Height="20px" style="margin-left:5%" ID="txtemail" runat="server" ></asp:TextBox>
    <br />
    <asp:Label ID="Label2" style="margin-left:33%" runat="server" Text=""></asp:Label>
    <br />
    <asp:Button style="margin-left:43%" Width="78px" Height="32px" ID="Button1" CssClass="btnregister2" runat="server" Text="Send OTP" OnClick="btnotp_click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</asp:Content>
