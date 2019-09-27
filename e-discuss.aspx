<%@ Page Language="C#" Title="e-Discuss" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="e-discuss.aspx.cs" Inherits="WebApplication3.e_discuss" %>
<asp:Content ID="jf" ContentPlaceHolderID="head" runat="server">
    <title>e-Discuss</title>
</asp:Content>
<asp:Content ID="Ffdwe" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" /><!-- integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">   --> 
    <link rel="stylesheet" href="http://webbot.000webhostapp.com/css/glyphicons.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesomw.min.css" />
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css"/>
    <style type="text/css">
        html{
            scroll-behaviour:smooth;
        }
        body {
            
            background: url(images/istockphoto-912872748-1024x1024.jpg) no-repeat center fixed;
            background-size: cover;
            margin-left: 0px;
            margin-top: 0px;
            margin-bottom: 0px;
        }

        code
        {
            overflow:auto;
            display: block;
            color: #393318;
            background-color:#eff0f1;
        }
.dropbtn {
  background-color: white;
  color: black;
  padding: 1px;
  font-size: 1px;
  border: none;
  border-radius:50%;
}
.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f1f1f1;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

.dropdown-content a {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}
.fa {

-webkit-transition: 0.6s ease-out;
    -moz-transition:  0.6s ease-out;
    transition:  0.6s ease-out;
    cursor:pointer;
}

.dropdown-content a:hover {background-color: #ddd;}

.dropdown:hover .dropdown-content {display: block;}

.dropdown:hover .dropbtn .fa {color: blue;
 -webkit-transform: rotateZ(180deg);
      -moz-transform: rotateZ(180deg);
      transform: rotateZ(180deg);
}
 
       .style10{
            width:100px;
            background-color:white;
            text-align:center;
            
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>
        
    $(document).ready(function(){
        // Add smooth scrolling to all links
        $("a").on('click', function(event) {

            // Make sure this.hash has a value before overriding default behavior
            if (this.hash !== "") {
                // Prevent default anchor click behavior
                event.preventDefault();

                // Store hash
                var hash = this.hash;

                // Using jQuery's animate() method to add smooth page scroll
                // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function(){

                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
            } // End if
        });
    });


        function likefunc(x,usrname)
        {
            var qno = x.id;
            
          //  alert(usrname);
            $.ajax({
                url: 'e-discuss.aspx/like',
                method: 'post',
                contentType: 'application/json',
                data: '{quesno:' + qno + ',username:'+'"'+usrname+'"'+'}',
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
                    }else if (data.d == "like removed and disliked") {
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
    //    $(document).ready({
    //        $('#91').click(function(){
    //            fa.color=blue;
    //       })
    //    });
        //function utterusername(x) {
        //    alert(x);
        //    if ('speechSynthesis' in window) {
        //        var synth = speechSynthesis;
        //        var flag = true;
        //        if (flag) {
        //            utterance = new SpeechSynthesisUtterance(x);
        //            utterance.voice = synth.getVoices()[0];
        //            utterance.onend = function () {
        //                flag = false;
        //            };
        //            synth.speak(utterance);
        //        }
        //    }
        //    else {
        //        alert("Sorry,username cannot be read.");
        //    }
        //}
    </script>
    <script src="Scripts/ckeditor/ckeditor.js">

    </script>
    
    <!-- -->
    <div style="overflow:auto; margin-left:170px;margin-top:10%">
    <table style="text-align:left;background-image:url(images/back.png)" border="0"><tr>
        <td rowspan="2" class="auto-styl5">    
    <p id="top" style="margin-left:5%; color:#1c1137; font-family:Tahoma; font-size:15px">
            <asp:Button ID="Button1" Width="200px" runat="server" style="cursor:pointer" Text="Most Recent questions" OnClick="Button1_Click1" />
            <asp:Button ID="Button2" Width="200px" runat="server" style="cursor:pointer" Text="Most answered Questions" OnClick="Button2_Click" />
            <asp:Button ID="Button3" Width="200px" runat="server" style="cursor:pointer" Text="Most viewed questions" OnClick="Button3_Click" />
            <asp:Button ID="Button4" Width="200px" runat="server" style="cursor:pointer" Text="Least Recent questions" OnClick="Button4_Click1" />
            <asp:Button ID="Button5" Width="200px" runat="server" style="cursor:pointer" Text="Least answered questions" OnClick="Button5_Click1" />
            <asp:Button ID="Button6" Width="200px" runat="server" style="cursor:pointer" Text="Least viewed questions" OnClick="Button6_Click1" />
            </p>
        <p>
            <asp:Label ID="Label1" runat="server" ></asp:Label>
        </p>
        <!--<div style="float:right; margin-top:-150%; margin-left:-20%">dwhfg</div>-->
    
    <p id="abcd">&nbsp;</p>
              <table style="margin-left:8%"><tr><td> <textarea class="ckeditor" runat="server" id="newques"></textarea></td></tr></table>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Select a category for your question: 
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        
        <asp:Label ID="Label3" style="margin-left:5%" runat="server"></asp:Label>
        
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; If your question doesnot fall into any category,then enter a new category here:
            <asp:TextBox ID="TextBox2" CssClass="" runat="server" Height="27px" Width="171px"></asp:TextBox>
&nbsp;
        <br /><br />
        <asp:Button ID="btnpost" runat="server" Height="40px" CssClass="btnregister2" style="margin-left:25%;border-radius:8px;" Text="Post Your Question" OnClick="btnpost_Click" />
    
           <a style="margin-left:30%" href="#top">Go back to top</a></td>
        <td style="vertical-align:top">
            
            <a style="position:fixed;margin-left:9%" href="#abcd">Post a new question</a>
            <br />
            <p style="font-size:36px; font-weight: bold;">&nbsp;</p>
        </td>
        <tr>
        <td>
            &nbsp;</td>
        </tr>
        </table>
        </div>
        </asp:Content>
<asp:Content ID="hffdah" ContentPlaceHolderID="Spaces" runat="server"></asp:Content>