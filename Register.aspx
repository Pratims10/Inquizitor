<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="Register.aspx.cs" Inherits="WebApplication3.WebForm1" Title="Register" %>
<asp:Content ID="bh" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="gdwewejgr" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css"/>
    <link href="StyleSheet2.css" rel="stylesheet" />
    <style type="text/css">
        /*body{
            background:url(images/background-images-for-registration-page-12.jpg) no-repeat center fixed;
        }*/
        .new{
            font-family:Algerian;
            font-size:xx-large;
            text-align: center;
            color:#490909;
        }
        .sidebar
        {
            margin-top:2%;
        }
        .auto-stle44 {
            font-size: medium;
            font-weight: bold;
            color: black;
            text-align: left;
            height: 42px;
            width: 169px;
        }
        .auto-stle99 {
            height: 46px;
            text-align: center;
            font-weight: 700;
            font-style: italic;
            font-size:large;
            color: #490909;
        }
       /* .auto-style2 {
            height: 67px;
            text-align: left;
            font-weight: 700;
            font-style: italic;
            font-size: large;
            color: #FFFF00;
        }
        
        .auto-style40 {
            width: 394px;
        }
        
        .auto-style71 {
            width: 44px;
            height: 42px;
        }
        .auto-style72 {
            width: 104px;
            height: 42px;
        }
        .auto-style91 {
            height: 42px;
            width: 300px;
            text-align: left;
        }
        
        .auto-style105 {
            width: 394px;
            height: 42px;
        }
        .auto-style106 {
            width: 394px;
            height: 41px;
        }
        .auto-style107 {
            width: 44px;
            height: 41px;
        }
        .auto-style108 {
            width: 104px;
            height: 41px;
        }
        .auto-style109 {
            font-size: medium;
            font-weight: bold;
            color: #FFFFFF;
            text-align: left;
            height: 41px;
            width: 747px;
        }
        .auto-style110 {
            width: 394px;
            height: 39px;
        }
        .auto-style111 {
            width: 394px;
            height: 18px;
        }
        .auto-style112 {
            width: 369px;
        }*/
        .auto-stle101 {
        height: 42px;
    }
        .auto-stl1 {
            font-family: Algerian;
            font-size: x-large;
            text-align: center;
            color: #490909;
        }
    </style>
<!--<body background="images/background-images-for-registration-page-12.jpg">-->
    <div style="margin-top:2%">
     <center> 
       <table style="width: 58%; height: 532px; margin-left: 0px;" border="0">
        <tr>
            <td  colspan="2">
                <p class="auto-stl1">Welcome to Inquizitor </p>
            </td>
        </tr>
        <tr>
            <td class="auto-stle99" colspan="2" align="center" >
                Education is the passport to the future, for tomorrow belongs to the those who prepare for it today.<br />
                
<p align="right"> -Malcolm</p> </td>
        </tr>
        <tr>
            <td style="width:20%">
               
            </td>
            <td draggable="true">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnregister">
                <table style="width: 115%; height: 271px; margin-top: 2px; text-decoration-color:black;">
                    <tr >
                        <td class="auto-stle44" style="width:35%"> First Name *</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtfirstname" runat="server" CssClass="Button" ToolTip="Please enter your first name" Height="20px" Width="277px" ></asp:TextBox>
                        </td>
                        <td style="width:25%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirstname" ErrorMessage="This filed cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-stle44"> Last Name </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtlastname" runat="server" CssClass="Button" ToolTip="Enter your last name" Height="20px" Width="277px"></asp:TextBox>
                        </td>
                        <td class="auto-stle106">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-stle44"> Institution </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtinstitution" CssClass="Button" runat="server" ToolTip="Enter the name of the institution where you study" Height="20px" Width="277px"></asp:TextBox>
                        </td>
                        <td class="auto-stle105">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-stle44"> Username *</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtusername" CssClass="Button" runat="server" ToolTip="Enter an username using characters a-z,A-Z,0-9" Height="20px" Width="277px"></asp:TextBox>
                        </td>
                        <td class="auto-stle105">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtusername" ErrorMessage="Username cannot be empty" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-stle44"> E-mail *</td>
                        <td >
                            <asp:TextBox ID="txtemail" CssClass="Button" runat="server" ToolTip="Enter your e-mail correct address. We wil send an OTP to verify it." Height="20px" Width="277px"></asp:TextBox>
                        </td>
                        <td class="auto-stle101">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtemail" ErrorMessage="E-mail cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter a valid e-mail address. We will verify this address by sending an OTP" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-stle44">About Yourself (max 250 characters)</td>
                        <td class="auto-style2" style="text-align:center">
                            <asp:TextBox ID="txtaboutyourself" CssClass="Button" runat="server" TextMode="MultiLine" style="margin-left:8.5%" ToolTip="Tell others something about yourself" Height="80px" Width="277px" MaxLength="2"></asp:TextBox>
                        </td>
                        <td class="auto-stle40">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-stle44"> Password *</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtpassword" CssClass="Button" runat="server" TextMode="Password" ToolTip="Enter an username using characters a-z,A-Z,0-9,&amp;,$,#,@" Height="20px" Width="277px"></asp:TextBox>
                        </td>
                        <td class="auto-stle110">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtpassword" ErrorMessage="Password field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-stle44">Confirm Password *</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtconfirmpassword" CssClass="Button" runat="server" TextMode="Password" ToolTip="Re-enter the password" Height="20px" Width="277px"></asp:TextBox>
                        </td>
                        <td class="auto-stle40">
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtpassword" ControlToValidate="txtconfirmpassword" ErrorMessage="Enter the same value as in the Password field" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                    
                </table>
                    </asp:Panel>
            </td>
        </tr>
    </table>
      </center>  
    
        <asp:Button ID="btnregister" runat="server" Text="Register"  CssClass="btnregister2" style="margin-left:45%" Width="125px" OnClick="btnregister_Click" Height="36px"/>
        <br />
   </div> 
</asp:Content>
<asp:Content ID="dffa" ContentPlaceHolderID="Spaces" runat="server">
    <br />
</asp:Content>