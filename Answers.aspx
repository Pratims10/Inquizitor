<%@ Page Language="C#" Title="Answers" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="Answers.aspx.cs" Inherits="WebApplication3.WebForm4" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="hfuj" ContentPlaceHolderID="head" runat="server">
    <link rel="shortcut icon" href="http://bit.ly/ghfavicon" width=32px>
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css" />
     <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" /><!-- integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">   --> 
    <link rel="stylesheet" href="http://webbot.000webhostapp.com/css/glyphicons.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesomw.min.css" />
	<link rel="stylesheet" href="style.css">
    <link rel="prefetch" href="pause.svg">
	<link rel="prefetch" href="play.svg">
	<link rel="prefetch" href="stop.svg">
    <link rel="prefetch" href="pause1.svg">
	<link rel="prefetch" href="play1.svg">
	<link rel="prefetch" href="stop1.svg">
    <meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0">
   <!-- <script src="script.js" defer></script>-->
    <style type="text/css">
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
        .sidebar{
            margin-top:-10%;
        }
        .item,li
        {
            border-left:1px solid black;
        }
        .box ul li
        {
            list-style:none;
        }
        .box ul li .item
        {
            position:relative;
            padding:30px 20px;
        }
        /*.box ul li .item:hover{
            padding:4px 12px;
            background:#00ff90;
            border-radius:10px;
            border-color:black;
            transition:0.5s;
        }*/
        .box ul li .item:before
        {
            content:'';
            position:absolute;
            top:32px;
            left:-7.8px;
            width:12px;
            height:12px;
            border:2px solid black;
            background-color:#fafafa;
            border-radius:50%;
        }
        .box ul li .item:before:hover{
            animation:animate 0.5s linear infinite;
        }
        @keyframes animate
        {
            0%
            {
                opacity:0;
            }
            50%
            {
                opacity:1;
            }
            100%
            {
                opacity:0;
            }
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
        //    $(document).ready({
        //        $('#91').click(function(){
        //            fa.color=blue;
        //       })
        //    });
   
        function func(x) {
         //   alert(x);
            if ('speechSynthesis' in window) {
                var synth = speechSynthesis;
                var flag = true;
                var str = x.toString();
                var playEle = document.getElementById("play" + str);
                var pauseEle = document.getElementById("pause" + str);
                var stopEle = document.getElementById("stop" + str);
             //   alert(stopEle.id);
                playEle.addEventListener("click", function () {
                    onClickPlay(x);
                },false);
                pauseEle.addEventListener("click", onClickPause);
                stopEle.addEventListener("click", onClickStop);
                function onClickPlay(x) {
          //          alert("qqq");
                    if (flag) {
                        var str = x.toString();
                  //      flag = true;
                        utterance = new SpeechSynthesisUtterance(
                              document.getElementById(str).textContent);
                        utterance.voice = synth.getVoices()[0];
                        utterance.onend = function () {
                            flag = false;
                        };
                        synth.speak(utterance);
                    }
                    if (synth.paused) { /* unpause/resume narration */
                        synth.resume();
                    }
                }

                function onClickPause() {
           //         alert("dh");
                    if (synth.speaking && !synth.paused) { /* pause narration */
                        synth.pause();
                    }
                }
                function onClickStop() {
           //         alert("stop");
                    if (synth.speaking) {
                        flag = false;
                        synth.cancel();
                    }
                }
            }
            else {
                alert("Sorry,this text cannot be read.");
            }
      //      alert("dsh");
        }
        $(document).ready(function () {
            // Add smooth scrolling to all links
            $("a").on('click', function (event) {

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
                    }, 800, function () {

                        // Add hash (#) to URL when done scrolling (default click behavior)
                        window.location.hash = hash;
                    });
                } // End if
            });
        });
    </script>
</asp:Content>
<asp:Content ID="ef" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <link href="StyleSheet1.css" rel="stylesheet" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <div style="margin-left:12%;margin-top:10%">
    <table style="width:99%; margin-top:5%; margin-left: 20px;" border="0">
            <tr>
                <td style="width:75%">
                    <asp:Label ID="lblques" style="margin-left:5%" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblanswer" runat="server" Text="" ></asp:Label>
                    </td>
                <td style="vertical-align:top;">
                    <asp:Label ID="similarques" runat="server" Text=""></asp:Label>
                    </td>
            </tr>
            
        </table>
        <table style="margin-left:10%"><tr><td >
         <script src="Scripts/ckeditor/ckeditor.js"></script>
            <p id="abcd"></p>
        <textarea class="ckeditor" runat="server" id="txtcomment"></textarea>
                   </td></tr></table>
        <table border="0" style="width:120%"><tr><td style="width:10%">&nbsp;</td><td style="width:20%">
            <asp:FileUpload ID="FileUpload1" runat="server" style="margin-left:2%"/>
          
            </td><td style="width:25%">
                <asp:Label ID="Label5" style="margin-left:0%" runat="server" Text="You may upload a video or audio file with your answer"></asp:Label>
                          </td><td>
            <asp:Button ID="Button1" runat="server" Height="36px" style="margin-left:0%" Text="Remove uploaded file" Width="132px" CssClass="btnregister2" OnClick="Button1_Click"/>
                          </td></tr><tr><td>
                &nbsp;</td><td></td><td>
            <asp:Button ID="btncomment" CssClass="btnregister2"  runat="server" Height="36px" Width="120px" style="margin-left: 0%;" Text="Comment" OnClick="btncomment_Click" />
            
                </td><td></td>

                                    </tr></table>
        </div>
        </asp:Content>
<asp:Content ID="sdc" ContentPlaceHolderID="Spaces" runat="server"></asp:Content>