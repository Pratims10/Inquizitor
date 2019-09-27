<%@ Page Title="Verify your email" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"  CodeBehind="emailverification.aspx.cs" Inherits="WebApplication3.emailverification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <style>
        .sidebar{
            margin-top:0%;
        }
    </style>
    <br />
    <div style="margin-top:10%">
<br />
<br />
<br />
    <br />
    <br />
    <br />
    <table style="margin-left:40%">
        <tr>
            <td>
                Enter the 6-digit One Time Password(OTP) sent to your mail:
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Panel ID="Panel1" runat="server" DefaultButton="confirmotp">
                <asp:TextBox ID="txtotp" style="border-bottom:2px solid black; width:125px; height:27px; margin-left:25%" runat="server" BorderColor="#000066" BorderStyle="None" Font-Bold="True" Font-Names="Arial Rounded MT Bold" Font-Size="XX-Large" MaxLength="6" OnTextChanged="txtotp_TextChanged" ToolTip="Enter the OTP sent to your e-mail"></asp:TextBox>
                    </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="confirmotp" CssClass="btnregister2" style="margin-left:10%;margin-top:10px; " runat="server" Text="Confirm OTP" OnClick="btnotp_click" Height="36px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" CssClass="btnregister2"  Text="Resend OTP" Height="36px" OnClick="resendotp_click" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="Label1" runat="server" style="margin-left:30%"></asp:Label>
            </td>
        </tr>
    </table>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
    <br />
<br />
<br />
<br />
<br />
<br />
<br />
    <br/>
    <br /><br />
</asp:Content>
