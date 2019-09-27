<%@ Page Title="View All Answers" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewmoreans.aspx.cs" Inherits="WebApplication3.viewmoreans" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="StyleSheet2.css" rel="stylesheet" />
    <style>
       .fa
        {
            -webkit-transition: 0.6s ease-out;
    -moz-transition:  0.6s ease-out;
    transition:  0.6s ease-out;
            font-size:20px;
            color:gray;
            cursor:pointer;
        }
        .fa:hover{
           color:blue;
        }
        .blue
        {
            color:blue;
        }
        </style>
    <script>
        function likefunc(x,usrname)
        {
            var qno = x.id;
            qno = qno / 10;
            //  alert(usrname);
            $.ajax({
                url: 'answers.aspx/like',
                method: 'post',
                contentType: 'application/json',
                data: '{answerno:' + qno + ',username:'+'"'+usrname+'"'+'}',
                success:function(data)
                {
                    if (data.d == "liked") {
                        x.classList.toggle("blue");
                        //  alert(data.d);
                    }
                    else if (data.d == "like removed") {
                        x.classList.toggle("blue");
                        //  alert(data.d);
                    } else if (data.d == "dislike removed and liked") {
                        x.classList.toggle("blue");
                        qno = qno * 100;
                        //alert(qno);
                        var y = document.getElementById(qno.toString());
                        y.classList.toggle("blue");
                        // alert(y);
                    }
                    else
                        alert(data.d);
                },
                error:function(error)
                {
                    alert(error);
                }
            });
        }
        function dislikefunc(x, usrname) {
            var qno = x.id;
            qno = qno / 100;
            //  alert(qno);
            //  alert(usrname);
            $.ajax({
                url: 'answers.aspx/dislike',
                method: 'post',
                contentType: 'application/json',
                data: '{answerno:' + qno + ',username:' + '"' + usrname + '"' + '}',
                success: function (data) {
                    if (data.d == "disliked") {
                        x.classList.toggle("blue");
                        //     alert(data.d);
                    } else if (data.d == "dislike removed") {
                        x.classList.toggle("blue");
                        //     alert(data.d);
                    }else if (data.d == "like removed and disliked") {
                        x.classList.toggle("blue");
                        qno = qno * 10;
                        var y = document.getElementById(qno.toString());
                        y.classList.toggle("blue");
                        //     alert(data.d);
                    }
                    else
                        alert(data.d);
                },
                error: function (error) {
                    alert(error);
                }
            });
        }
        </script>
    <div style="margin-top:10%;margin-left:23%">
   
    <asp:Label ID="Label1" Font-Size="34px" style="margin-left:-29%" runat="server" ForeColor="#333399" Font-Bold="True" ></asp:Label>
    <br />
    <br />
    <br />
       
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
</asp:Content>
