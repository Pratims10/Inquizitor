using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
namespace WebApplication3
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string sr= ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        string str;
        int ctr=1;
        bool flag = false;
        protected void Page_Load(object sender, EventArgs e) 
        {
            if(Request.QueryString.Count==0)
            {
                return;
            }
            //txtcomment.Text = "";
            if(Session["userid"]!=null)
            str = Session["userid"].ToString();
            //Response.Write(str);
            cn = new SqlConnection(sr);
            cn.Open();
            StringBuilder sb = new StringBuilder();
            cm = new SqlCommand();
            cm.Connection = cn;
            sb.AppendFormat("select * from questions where quesno={0}",Request.QueryString[0]);
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            flag = false;
            StringBuilder st = new StringBuilder();
            if (dr.Read())
            {
                flag = true;
                st.AppendFormat(@"<table><tr><td style='font-size:12pt;margin-left:16%'>&emsp;&emsp;Question given by <a href='Profile.aspx?username={0}'>{0}</a> at {1}<a style='margin-left:14%' href='#abcd'>Reply to this question</a></tr></td></table>", dr.GetString(0), dr.GetDateTime(3).ToString("MM/dd/yyyy hh:mm tt"));
                st.Append("<table style='font-size:12pt;margin-left:5%;width:70%'");
                st.AppendFormat(@"<tr><td style='background-color:white;border-radius:5px;border:.15px solid #e6e6e6';class='shadow'><article style='margin:8px 15px 8px 15px'>{0}</tr></td></table>", dr.GetString(2));
                lblques.Text = st.ToString();
               // dr.Close();
            }
            string tag = dr.GetString(4);
            dr.Close();
            StringBuilder s10=new StringBuilder();
            s10.AppendFormat(@"select top(30) * from questions where languagetype='{0}'",tag);
            cm.CommandText = s10.ToString();
            dr = cm.ExecuteReader();
            StringBuilder s11 = new StringBuilder();
            s11.AppendFormat("<p style='font-size:14pt;'>Similar questions<br/></p><table>");
            while(dr.Read())
            {
                s11.AppendFormat(@"<tr style='width:18%'><td><a href='answers.aspx?quesno={0}'>{1}</a></td></tr><tr><td>&nbsp;</tr></td>",dr.GetInt32(1),dr.GetString(2));
            }
            s11.Append("</table>");
            dr.Close();
            similarques.Text = s11.ToString();
            StringBuilder s1 = new StringBuilder();
            s1.AppendFormat("select views from questions where quesno={0}", Request.QueryString[0]);
            cm.CommandText = s1.ToString();
            dr = cm.ExecuteReader();
            //dr.Close();
            int view1;
            if (!IsPostBack)
            {
                if (dr.Read())
                {
                    view1 = dr.GetInt32(0);
                    view1 += 1;
                }
                else
                    view1 = 0;
                //Response.Write(dr.GetInt32(0) + " " + view1);
                dr.Close();
                StringBuilder s2 = new StringBuilder();
                s2.AppendFormat(@"update questions set views={0} where quesno={1}", view1, Request.QueryString[0]);
                cm.CommandText = s2.ToString();
                cm.ExecuteNonQuery();
            }
            dr.Close();
            ctr = func();
            //cn.Close();
        }
        int func()
        {
            StringBuilder st=new StringBuilder();
            cm = new SqlCommand();
            cm.Connection=cn;
           // st.AppendFormat("select * from allanswers where quesno={0}",Request.QueryString[0]);
            st.AppendFormat("select an.answerno, an.answer, an.quesno, an.username, an.AnsTime, ud.image,an.type,an.filename from AllAnswers an, userdetails ud where ud.username = an.username and an.quesno='{0}'",Request.QueryString[0]);
            cm.CommandText=st.ToString();
            dr=cm.ExecuteReader();
            int c = 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='box'><ul class='timeline'>");
            while(dr.Read())
            {
                sb.Append("<li><div class='item' style='width:90%;'><table border='0'>");
                sb.AppendFormat(@"<tr><td style='background: #010060;border-radius:2px;color:white' style='blink'>{2}</td></tr></table><table border='0' style='margin-left:5% ;width:55%'><tr><td style='height:50px; width:60px'><div><a href='profile.aspx?username={1}'><img alt='No image' src='{0}' style='height:40px; width:40px; border-radius:50%'/></a></div> </td>", dr.GetString(5), dr.GetString(3), dr.GetDateTime(4).ToString("MM/dd/yyyy hh:mm tt"));
                sb.AppendFormat(@"<td style='width:10%'><a href='Profile.aspx?username={0}'>{0}</td><td>&nbsp;&nbsp;&nbsp;</td><td></td></tr>", dr.GetString(3));
                sb.AppendFormat(@"</table><table border='0' style='margin-left:5%;width:75%'><tr><td class='shadow' style='background-color:white;border:.1px solid #e6e6e6;border-radius:3px'><article style='margin:4% 4% 4% 4%' id='{1}'>{0}</article></td></tr></table><table style='margin-left:5%'>", dr.GetString(1),dr.GetInt32(0));
                if (Session["userid"] != null)
                {
                    SqlConnection scn = new SqlConnection(sr);
                    scn.Open();
                    SqlCommand scm = new SqlCommand();
                    scm.Connection = scn;
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendFormat(@"select * from anslikedislike where username='{0}' and answerno={1}", Session["userid"].ToString(), dr.GetInt32(0));
                    SqlDataReader dr2;
                    scm.CommandText = sb2.ToString();
                    dr2 = scm.ExecuteReader();
                    if (dr2.Read())
                    {
                        if (dr2.GetInt32(1) == 0)
                        {
                            sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0)*10, Session["userid"].ToString());
                            sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down blue'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                        else
                        {
                            sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up blue'></i></td>", dr.GetInt32(0)*10, Session["userid"].ToString());
                            sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                    }
                    else
                    {
                        sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0)*10, Session["userid"].ToString());
                        sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                    }
                    scn.Close();
                    dr2.Close();
                    //    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>",dr.GetInt32(0),Session["userid"].ToString());
                    //    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0),Session["userid"].ToString());
                }
                else
                {
                    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0)*10, "null");
                    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 100, "null");
                }


                sb.AppendFormat(@"<td><input type='button' style='padding:9px 16.5px; border-radius:50% 50% 50% 50%' class='play' onclick='func({0});return false' id='play{0}'><input type='button' class='pause' id='pause{0}' style='padding:9px 16.5px; border-radius:50% 50% 50% 50%'><input type='button' class='stop' id='stop{0}' style='padding:9px 16.5px; border-radius:50% 50% 50% 50%'>", dr.GetInt32(0));
                sb.AppendFormat(@"</td></tr></table>");
                if (dr.GetString(6) != "null")
                {
                    string s = dr.GetString(7);
                    int len = s.Length;
                    s = s.Substring(len - 3, 3);
                    if (dr.GetString(6) == "audio")
                        sb.AppendFormat(@"<audio controls><source src='videos/{0}' type='audio/{1}'>Your browser doesn't suppot this file type.</audio>", dr.GetString(7), s);
                    else if (dr.GetString(6) == "video")
                        sb.AppendFormat(@"<video controls width='560' height='400'><source src='videos/{0}' type='video/{1}'>Your browser doesn't suppot this file type.</video>", dr.GetString(7), s);
                }
                    sb.Append("</div></li>");
                c++;
            }
            sb.Append("</ul></div>");
            dr.Close();
            if (flag)
                lblanswer.Text = sb.ToString();
            else
            {
                lblanswer.Text = "Such question doesnot exist";
                lblanswer.ForeColor = System.Drawing.Color.Red;
                txtcomment.Visible = false;
                btncomment.Visible = false;
            }
            return c;
        }

        protected void btncomment_Click(object sender, EventArgs e)
        {
            //str = Session["userid"].ToString();
            string p = txtcomment.InnerText.ToString();
            p = p.Trim();
            if (p == "")
            {
                Response.Write("<script>alert('Please enter something to comment')</script>");
                return;
            }
            if (Session["userid"]==null)
            {
                Response.Write("<script>alert('Please create an account to post an answer')</script>");
                StringBuilder ps = new StringBuilder();
                ps.AppendFormat(@"Login.aspx?prev=Answers.aspx?ques={0}",Request.QueryString[0]);
                Response.Redirect(ps.ToString());
            }
            else
            {
                //cn = new SqlConnection(@"Data Source=.;Initial Catalog=e-discuss;Integrated Security=True");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select max(answerno) from allanswers";
                dr = cmd.ExecuteReader();
                dr.Read();
                int ansno = dr.GetInt32(0) + 1;
                //Response.Write(ansno);
                StringBuilder sb = new StringBuilder();
                dr.Close();
                bool flag=true;
                string filename="null", type="null";
                if(FileUpload1.HasFile)
                {
                    string fileextension = System.IO.Path.GetExtension(FileUpload1.FileName);
                    fileextension=fileextension.ToLower();
                    if(fileextension==".mp4"||fileextension==".webm"||fileextension==".ogg")
                    {
                        filename = FileUpload1.FileName;
                        type="video";
                        FileUpload1.SaveAs(Server.MapPath("~/videos/" + filename));
                    }
                    else if(fileextension==".ogg"||fileextension==".mp3"||fileextension==".wav")
                    {
                        type="audio";
                        filename = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/videos/" + filename));
                    }
                    else
                    {
                        Label5.Text = "<p style='margin-left:0%'>File extension not supported.<br/>Video: .mp4, .webm, .ogg<br/>Audio: .mp3, .wav, .ogg</p>";
                        flag = false;
                    }
                }
                if (flag)
                {
                    sb.AppendFormat(@"insert into allanswers (answerno,answer,quesno,username,filename,type)  values({0},'{1}',{2},'{3}','{4}','{5}')", ansno, txtcomment.InnerText, Request.QueryString[0], Session["userid"].ToString(),filename, type);
                    StringBuilder stm = new StringBuilder();
                    stm.AppendFormat(@"update AllAnswers set Anstime = getdate() where answerno={0}", ansno);
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = stm.ToString();
                    cmd.ExecuteNonQuery();
                    StringBuilder s1 = new StringBuilder();
                    StringBuilder s2 = new StringBuilder();
                    s1.AppendFormat(@"select ansgiven from userdetails where username='{0}'", Session["userid"].ToString());
                    cm.CommandText = s1.ToString();
                    dr = cm.ExecuteReader();
                    int ans;
                    if (dr.Read())
                        ans = dr.GetInt32(0) + 1;
                    else
                        ans = 0;
                    dr.Close();
                    s2.AppendFormat(@"update userdetails set ansgiven={0} where username='{1}'", ans, Session["userid"].ToString());
                    cm.CommandText = s2.ToString();
                    cm.ExecuteNonQuery();
                    StringBuilder s3 = new StringBuilder();
                    s3.AppendFormat(@"select aNswers from questions where quesno={0}", Request.QueryString[0]);
                    cm.CommandText = s3.ToString();
                    dr = cm.ExecuteReader();
                    int answwer;
                    if (dr.Read())
                    {
                        answwer = dr.GetInt32(0) + 1;
                    }
                    else
                        answwer = 0;
                    dr.Close();
                    StringBuilder s4 = new StringBuilder();
                    s4.AppendFormat(@"update questions set anSwers={0} where quesno={1}", answwer, Request.QueryString[0]);
                    cm.CommandText = s4.ToString();
                    cm.ExecuteNonQuery();
                    StringBuilder s5 = new StringBuilder();
                    s5.AppendFormat(@"select username from questions where quesno={0}", Request.QueryString[0]);
                    cm.CommandText = s5.ToString();
                    dr = cm.ExecuteReader();
                    dr.Read();
                    StringBuilder username = new StringBuilder();
                    username.AppendFormat(@"{0} replied to your question. <a href=""answers.aspx?quesno={1}"">Click here</a> to view.", Session["userid"].ToString(), Request.QueryString[0]);
                    StringBuilder s6 = new StringBuilder();
                    s6.AppendFormat(@"select notnumber from userdetails where username='{0}'", dr.GetString(0));
                    string abcd = dr.GetString(0);
                    dr.Close();
                    cm.CommandText = s6.ToString();
                    dr = cm.ExecuteReader();
                    dr.Read();
                    int notno = dr.GetInt32(0);
                    int newnotno = notno + 1;
                    if (newnotno == 10)
                        newnotno = 0;
                    StringBuilder s7 = new StringBuilder();
                    s7.AppendFormat(@"update userdetails set not{0}='{1}',notnumber={2} where username='{3}'", notno, username.ToString(), newnotno, abcd);
                    dr.Close();
                    //Response.Write(s7.ToString());
                    cm.CommandText = s7.ToString();
                    cm.ExecuteNonQuery();
                    ctr = func();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileUpload1.Dispose();
        }
        [System.Web.Services.WebMethod]
        public static string like(int answerno, string username)
        {
            string sr = ConfigurationManager.ConnectionStrings["cok"].ToString();
            if (username == "null")
                return "Please Login to like this question";
            SqlConnection scn = new SqlConnection(sr);
            scn.Open();
            SqlCommand scm = new SqlCommand();
            scm.Connection = scn;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(@"select * from anslikedislike where username='{0}' and answerno={1}", username, answerno);
            scm.CommandText = sb1.ToString();
            SqlDataReader dr;
            dr = scm.ExecuteReader();
            //   scm.ExecuteNonQuery();
            StringBuilder sb = new StringBuilder();
            if (dr.Read())
            {
                if (dr.GetInt32(1) == 0)
                {
                    sb.AppendFormat(@"update anslikedislike set lkes=1 where username='{0}' and answerno={1}", username, answerno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();
                    scn.Close();
                    return "dislike removed and liked";
                }
                else
                {
                    sb.AppendFormat(@"delete from anslikedislike where username='{0}' and answerno={1}", username, answerno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();

                    scn.Close();
                    return "like removed";
                }
            }
            else
            {
                sb.AppendFormat(@"insert into anslikedislike values ('{0}',1,{1})", username, answerno);
                dr.Close(); scm.CommandText = sb.ToString();
                scm.ExecuteNonQuery();
                scn.Close();
                return "liked";
            }
        }
        [System.Web.Services.WebMethod]
        public static string dislike(int answerno, string username)
        {
            string sr = ConfigurationManager.ConnectionStrings["cok"].ToString();
            if (username == "null")
                return "Please Login to dislike this question";
            SqlConnection scn = new SqlConnection(sr);
            scn.Open();
            SqlCommand scm = new SqlCommand();
            scm.Connection = scn;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(@"select * from anslikedislike where username='{0}' and answerno={1}", username, answerno);
            scm.CommandText = sb1.ToString();
            SqlDataReader dr;
            dr = scm.ExecuteReader();
            // scm.ExecuteNonQuery();
            StringBuilder sb = new StringBuilder();
            if (dr.Read())
            {
                if (dr.GetInt32(1) == 0)
                {
                    sb.AppendFormat(@"delete from anslikedislike where username='{0}' and answerno={1}", username, answerno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();

                    scn.Close();
                    return "dislike removed";
                }
                else
                {
                    sb.AppendFormat(@"update anslikedislike set lkes=0 where username='{0}' and answerno={1}", username, answerno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();

                    scn.Close();
                    return "like removed and disliked";
                }
            }
            else
            {
                try
                {
                    sb.AppendFormat(@"insert into anslikedislike values('{0}',0,{1})", username, answerno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();
                    scn.Close();
                    return "disliked";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return e.ToString();
                }
            }
        }
    }
}