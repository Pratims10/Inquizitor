<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="WebApplication3.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <div style="margin-top:10%">
    <br /><br /><br /><br /><br />
    <table style="margin-left:20%;width:70%"><tr><td>
    <textarea class="ckeditor" runat="server" style="margin-left:15%" id="txt"></textarea>
    <br /><br /></td></tr>
        <tr>
    <td><asp:Button ID="btnedit" style="margin-left:43%;height:32px;width:90px" runat="server" CssClass="btnregister2" Text="Save Changes" OnClick="btnedit_Click" />
    </td></tr></table>
        <br />
    <br />
    <br />
    <br />
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
</asp:Content>
