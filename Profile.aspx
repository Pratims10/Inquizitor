<%@ Page Language="C#" Title="User Profile" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="Profile.aspx.cs" Inherits="WebApplication3.WebForm3" %>
<asp:Content ID="jdw" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet2.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style type="text/css">
       
       
        .auto-style5 {
            width: 8%;
        }
       .btn:hover
       {
           color:#bdbdbd;
       }
       
    </style>
</asp:Content>
<asp:Content ID="wdscf" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <div style="margin-left:12%; margin-top:10%">
    
    <asp:Label ID="Label8" runat="server" style="margin-left:20%" Font-Italic="True" ForeColor="Red"></asp:Label>
    <table id="editprofile"  style="margin-left:42%;border-radius:3px;border:.3px solid blue;background-color:white;"><tr><td class="btn">
  
         <asp:Label ID="Label13" runat="server"></asp:Label>
       
      </td></tr></table>
    <table style="width: 40%; margin-left:10%; margin-top:.5%; background-color:#f1ffd7;box-shadow:rgba(0,0,0,0.6) 0 2px 5px" >
        <tr><td><br /></td>
            <td></td>
            <td>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align: left; font-size: large;border-right:1px solid blue; ">Name</td>
            <td>
                <asp:Label ID="Label1" runat="server" style="font-size:large;  "></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="border-right:1px solid blue;"><br /></td></tr>
        <tr>
            <td></td>
            <td style="text-align: left; font-size: large;border-right:1px solid blue; ">Institution Name</td>
            <td>
                <asp:Label ID="Label3" runat="server" style="font-Size:large; "></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="border-right:1px solid blue;"> <br /> </td></tr>
        <tr>
            <td></td>
            <td style="text-align: left; font-size: large;border-right:1px solid blue;">Username</td>
            <td>
                <asp:Label ID="Label6" runat="server" style="font-Size:large;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="border-right:1px solid blue;"><br /></td></tr>
        <tr>
            <td></td>
            <td style="text-align: left; font-size: large;border-right:1px solid blue;">E-mail Address</td>
            <td>
                <asp:Label ID="Label7" runat="server" style="font-Size:large;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td  style="border-right:1px solid blue;"><br /></td></tr>
        <tr><td></td><td style="text-align: left; font-size: large;border-right:1px solid blue;">Answer Likes</td>
            <td>
                <asp:Label ID="Label9" runat="server"  style="font-Size:large;"></asp:Label>
            </td>
        </tr>
        
    <tr><td></td>
        <td  style="border-right:1px solid blue;"><br /></td></tr>    
        <tr><td></td><td style="text-align: left; font-size: large;border-right:1px solid blue;">Views</td>
            <td>
                <asp:Label ID="Label10" runat="server"  style="font-Size:large;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="border-right:1px solid blue;"><br /></td></tr>    
        <tr><td></td><td style="text-align: left; font-size: large;border-right:1px solid blue;">Questions answered</td>
            <td>
                <asp:Label ID="Label11" runat="server"  style="font-Size:large;"></asp:Label>
            </td>
        </tr>
    <tr>
        <td></td>
        <td style="border-right:1px solid blue;"><br /></td></tr>    
    <tr><td></td><td style="text-align: left; font-size: large;border-right:1px solid blue;">Questions asked</td>
            <td>
                <asp:Label ID="Label12" runat="server"  style="font-Size:large;"></asp:Label>
            </td>
        </tr>
    </table>
    <link rel="stylesheet" type="text/css" href="StyleSheet3.css" />
     <div class="flip-card" style="margin-top:-37%" >
  <div class="flip-card-inner" >
    <div class="flip-card-front" >
      <asp:Image ID="profile_image" runat="server" style="width:400px;height:500px;z-index:-2" OnDataBinding="profile_image_DataBinding"/>
    </div>
    <div class="flip-card-back">
      <h1>&nbsp;</h1>
        <h1>&nbsp;</h1>
        <h1 class="brown"><asp:Label runat="server" id="lblname"></asp:Label></h1>
      <p>&nbsp;</p>
        <h3 class="brown"><asp:Label runat="server" id="lblquesasked"></asp:Label></h3>
      <p>&nbsp;</p>
        <h4 ><asp:Label runat="server" id="lblansgiven"></asp:Label></h4>
    </div>
  </div>
</div>

    <asp:Image ID="Image1" runat="server" />
         &nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:FileUpload ID="FileUpload1" CssClass="btnregister" runat="server" style="margin-left: 61%; height:36px;"  />
        <asp:Button ID="btnupload" runat="server" CssClass="btnregister2" OnClick="btnupload_Click" Text="Upload" Height="36px" Width="80px" />
        <br />
<asp:Label style="margin-left:66%" ID="Label5" runat="server"></asp:Label>
        <br />

 <center>    <table border="0" style="width:95%;margin-left:3%"> 
        <tr> <td> &nbsp;</td> 
             <td rowspan="2" ><h2 style="color:#2e0ca1; width: 45%;"> <asp:Label ID="Label2" runat="server"></asp:Label></h2> </td> 
            <td class="auto-style5" rowspan="2" >&nbsp;</td> 
            <td rowspan="2"><h2 style="color:#2e0ca1; width: 45%"> <asp:Label ID="Label4" runat="server"></asp:Label> </h2> </td> 
            <td>&nbsp;</td> 
        </tr>   
        <tr> <td> &nbsp;</td> 
            <td>&nbsp;</td> 
        </tr>   
    <tr> 
        <td>&nbsp;</td> 
        <td style="vertical-align:top; width:45%" > <asp:Label runat="server" id="lbluserques"></asp:Label></td> 
        <td class="auto-style5"> &nbsp;&nbsp;&nbsp;</td> 
        <td style="vertical-align:top; width:45%">  <asp:Label ID="lbluserans" runat="server"></asp:Label></td>
        <td>&nbsp;</td> 
     </tr>
     </table> </center>
        </div>
    </asp:Content>