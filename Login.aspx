<%@ Page Language="C#" Title="Login"  MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication3.WebForm2" %>
<asp:content ID="content1" ContentPlaceHolderID="head" Runat="server"></asp:content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css"/>
    <style>
            body
            {
                background:url(images/istockphoto-912872748-1024x1024.jpg) no-repeat center fixed;
                background-size:cover;
                /*margin-left:0px;
                margin-top:0px;
                margin-bottom:0px;*/
            }
        </style>
    <asp:Panel ID="UpdatePanel1" runat="server" DefaultButton="loginbutton">
    <div style="margin-top:10%;margin-left:12%">
    
    <center>
        <br /><br /><br /><br /> <br />
        <table style="margin-left:27%" border="0">
                <tr>
                <td style="width:90px">
                    Username</td>
                <td style="width:250px">
                    <asp:TextBox ID="TextBox1" CssClass="Button" runat="server" Width="220px" Height="20px" style="margin-left:25%" ForeColor="#6600FF" ToolTip="Enter your username"></asp:TextBox> </td>
                <td style="width:400px"> <asp:Label ID="Label1" runat="server" style="margin-left:10%" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td style="width:90px">
                    Password</td>
                <td style="width:250px">
                   <asp:TextBox ID="TextBox2" CssClass="Button" runat="server" TextMode="Password" Width="220px" style="margin-left:25%" Height="20px" ForeColor="#6600FF" ToolTip="Enter your password"></asp:TextBox> 
                    </td> <td style="width:400px">
                    
                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" style="margin-left:10%" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td  colspan="2">
                   <asp:Button ID="loginbutton" CssClass="btnregister2" style="margin-left:30%;cursor:pointer" runat="server" Text="Login" Height="34px" Width="97px" OnClick="loginbutton_Click" />
                </td> <td></td>
                    </tr>
                <tr> <td colspan="2" style="text-align:right">
                    
                        <a href="forgot_password.aspx"  style="text-decoration:none;margin-left:10%">Forgot Password?</a>
                    </td> <td></td> </tr>
            </table>    
             
                 </center>
        </div>
        </asp:Panel>
        </asp:Content>
<asp:Content ID="spaces" ContentPlaceHolderID="Spaces" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br /><br /><br /><br />
</asp:Content>