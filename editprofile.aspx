<%@ Page Title="Edit Profile" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="editprofile.aspx.cs" Inherits="WebApplication3.editprofile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <div style="margin-top:-5%;margin-left:39%;height:70%">
        <table style="width:60%;margin-top:25%">
            <tr style="height:16%"><td><br /><br /></td></tr>
            <tr>
                <td>
                    First Name
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" Height="23px" Width="239px" runat="server" CssClass="Button"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr><td></td><td></td></tr>
            <tr>
                <td>
                    Last Name
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" CssClass="Button" Height="23px" Width="239px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td><td></td>
            </tr>
            <tr><td></td><td></td></tr>
            <tr><td>Institution</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Height="23px" Width="239px" CssClass="Button"></asp:TextBox>
                </td>
            </tr>
            <tr><td></td><td></td></tr><tr><td></td><td></td></tr>
            <tr>
                <td>
                    About Yourself
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="Button" Height="80px" style="margin-left:30px" Width="269px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td></td></tr>
        </table>
        <br /><br />
        <asp:Button ID="Button1" CssClass="btnregister2" style="margin-left:12%" Height="29px" Width="100px" runat="server" Text="Save changes" OnClick="Button1_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
</asp:Content>
