<%@ Page Title="Forgot Password?" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="forgot_password2.aspx.cs" Inherits="WebApplication3.forgot_password2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }

        .auto-style2 {
            height: 26px;
            width: 182px;
        }
        .auto-style3 {
            width: 182px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <br /><br /><br /><br /><br /><br /><br />
    <asp:Label ID="Label3" style="margin-left:25%" runat="server"></asp:Label>
    <br /><br /><br />
    <table style="margin-left:35%">
        <tr>
            <td class="auto-style2">
                Enter OTP
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="TextBox1" Height="20px" Width="175px" CssClass="btnregister" runat="server" MaxLength="6" ToolTip="Enter the 6-digit OTP sent to your e-mail address"></asp:TextBox>
            </td>
        </tr>
        <tr><td><br /></td></tr>
        <tr>
            <td class="auto-style3">
                Enter new password
            </td>
            <td>
                <asp:TextBox ID="TextBox2" Height="20px" Width="175px"  CssClass="btnregister" runat="server" ToolTip="Enter new Password"></asp:TextBox>
            </td>
        </tr>
        <tr><td><br /></td></tr>
        <tr>
            <td class="auto-style3">
                Confirm Password
            </td>
            <td>
                <asp:TextBox ID="TextBox3" Height="20px" Width="175px"  CssClass="btnregister" runat="server" ToolTip="Re-type the password"></asp:TextBox>
            </td>
        </tr>
        <tr><td>
            <asp:Label ID="Label1" style="margin-left:7%" runat="server" Text=""></asp:Label>
            </td></tr>
        <tr><td><br /></td></tr>
        <tr>
            <td>
                <asp:Button ID="Button1" style="margin-left:7%" Height="35px" Width="150px" CssClass="btnregister2" runat="server" Text="Confirm new Password" OnClick="btn_click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br />
</asp:Content>
