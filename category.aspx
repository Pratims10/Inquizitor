<%@ Page Title="Search by Tags" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="WebApplication3.category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" /><!-- integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">   --> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesomw.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" /><!-- integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">   --> 
<link rel="stylesheet" href="http://webbot.000webhostapp.com/css/glyphicons.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesomw.min.css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css"/>
    <style>
    .fa
        {
        -webkit-transition: 0.6s ease-out;
        -moz-transition:  0.6s ease-out;
        transition:  0.6s ease-out;
        cursor:pointer;
        }
        .fa
        {
            font-size:20px;
            color:gray;
        }
        .fa:hover{
           color:blue;
        }
        .blue
        {
            color:blue;
        }
        .auto-styl5 {
            width: 75%;
        }
    </style>
    <script>
        function likefunc(x, usrname) {
            var qno = x.id;

            //  alert(usrname);
            $.ajax({
                url: 'e-discuss.aspx/like',
                method: 'post',
                contentType: 'application/json',
                data: '{quesno:' + qno + ',username:' + '"' + usrname + '"' + '}',
                success: function (data) {
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
                error: function (error) {
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
                url: 'e-discuss.aspx/dislike',
                method: 'post',
                contentType: 'application/json',
                data: '{quesno:' + qno + ',username:' + '"' + usrname + '"' + '}',
                success: function (data) {
                    if (data.d == "disliked") {
                        x.classList.toggle("blue");
                        //     alert(data.d);
                    } else if (data.d == "dislike removed") {
                        x.classList.toggle("blue");
                        //     alert(data.d);
                    } else if (data.d == "like removed and disliked") {
                        x.classList.toggle("blue");
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
    <div style="margin-left:19%;margin-top:10%">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Spaces" runat="server">
</asp:Content>
