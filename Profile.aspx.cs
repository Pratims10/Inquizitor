using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication3
{
    
    public partial class WebForm3 : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        StringBuilder st;
        protected void Page_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(sr);
            cm = new SqlCommand();
            cn.Open();
            cm.Connection = cn;
            if (Request.QueryString.Count == 0)
            {
                btnupload.Visible = false;
                FileUpload1.Visible = false;
                return;
            }
            if (Session["userid"] == null)
            {
                btnupload.Visible = false;
                FileUpload1.Visible = false;
            }
            else
            {
                string s1 = Session["userid"].ToString();
                string s2 = Request.QueryString[0];
                if (s1 != s2)
                {
                    btnupload.Visible = false;
                    FileUpload1.Visible = false;
                    Label13.Visible = false;
                    
                }
                else
                {
                    StringBuilder stt = new StringBuilder();
                    stt.AppendFormat(@"<a href='editprofile.aspx?username={0}'><i class='fa fa-pencil' style='font-size:17px'></i>Edit Profile</a>",Session["userid"].ToString());
                    Label13.Text = stt.ToString();
                }
            }
                st = new StringBuilder();
            st.AppendFormat(@"select * from userdetails where username='{0}'",Request.QueryString[0]);
            cm.CommandText = st.ToString();
            dr = cm.ExecuteReader();
            //Response.Write(@" <script> profile_image.src = 'images/blog-eLearning-templates.png' </script>" + "Hello");
            if(!dr.Read())
            {
                Label8.Text = "No such user exists";
                //lblclg.Text = "No such profile exists";
                Label8.ForeColor = System.Drawing.Color.Red;
                return;
            }
            lblname.Text = dr.GetString(0)+" "+dr.GetString(1);
            lblquesasked.Text = dr.GetString(21);
           // lblansgiven.Text = dr.GetInt32(9).ToString()+" answers given";
            Label11.Text = dr.GetInt32(9).ToString();
            Label12.Text = dr.GetInt32(8).ToString();
            Label1.Text = dr.GetString(0) + " " + dr.GetString(1);
            Label3.Text = dr.GetString(2);
            Label6.Text = dr.GetString(3);
            if (Session["userid"]!=null && Session["userid"].ToString() == Request.QueryString[0].ToString())
                Label7.Text = dr.GetString(7)+"<br/>(not visible to others)";
            else
                Label7.Text = "Email not visible to others";
            dr.Close();
            cm.CommandText = "select sum(anslikedislike.lkes)  from allanswers,anslikedislike where anslikedislike.answerno=AllAnswers.answerno and AllAnswers.username='" + Request.QueryString[0] + "'";
            dr = cm.ExecuteReader();
            if (dr.Read())
                Label9.Text = dr.GetInt32(0).ToString();
            else Label9.Text = "0";
            dr.Close();
            cm.CommandText="select sum(views) from questions where username='"+Request.QueryString[0]+"'";
            dr = cm.ExecuteReader();
            if (dr.Read())
                Label10.Text = dr.GetInt32(0).ToString();
            else
                Label10.Text = "0";
            dr.Close();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"select top(6) * from questions where username='{0}' order by time desc",Request.QueryString[0]);
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            StringBuilder s = new StringBuilder();
            int c = 0;
            if (dr.Read())
            {
                StringBuilder qw = new StringBuilder();
                qw.AppendFormat(@"<table dtyle='width:100%'><tr><td><h3 style=""color:blue"">Recent Questions asked</td></tr></table></h3>");
                //qw.AppendFormat(@"<h3 style=""color:green"">{0}</h3>", Request.QueryString[0]);
                bool flag=true;
                Label2.Text = qw.ToString();
                
                while (c < 5 && flag)
                {
                    c++;
                    s.Append(@"<table border='0' style='width:100%'>");
                    s.AppendFormat(@"<tr><td style='background-color:white;border:.1px solid #e6e6e6;border-radius:6px' class='shadow'><article style='margin:5px 12px 5px 12px'>{0}</article></td></tr>", dr.GetString(2));
                    s.AppendFormat(@"<table><table style='width:100%; text-align:center'><tr><td><a href='answers.aspx?quesno={0}'>View</a></td><td>{1} replies</td><td>{2} views</td>", dr.GetInt32(1),dr.GetInt32(6),dr.GetInt32(5));
                    s.AppendFormat(@"<td style='text-align:right'>Question posted on {0}</td></tr><tr><td><br/></td></tr>", dr.GetDateTime(3).ToString("MM/dd/yyyy hh:mm tt"));
                    if(dr.Read())
                        flag=true;
                    else
                        flag=false;
                        //Response.Write(c);
                }
                if (flag)
                    s.AppendFormat(@"<tr><td><a href=""viewmoreques.aspx?username={0}"">View more...</a></td></tr>", Request.QueryString[0]);
                s.Append("</table>");
                }
            else
                s.Append("<h1>No questions posted yet</h1>");
            lbluserques.Text = s.ToString();
            dr.Close();
            StringBuilder b = new StringBuilder();
            b.AppendFormat(@" select top(6) * from allanswers where username='{0}' order by anstime desc",Request.QueryString[0]);
            cm.CommandText = b.ToString();
            dr = cm.ExecuteReader();
            StringBuilder ans=new StringBuilder();
            if(dr.Read())
            {
                StringBuilder m = new StringBuilder();
                m.AppendFormat(@"<h3 style=""color:blue"">Recent answers given</h3>");
                Label4.Text = m.ToString();
                c = 0;
                bool flag=true;
                
                while(c<5 && flag)
                {
                    c++;
                    ans.Append(@"<table border='0' style='width:100%'>");
                    ans.AppendFormat(@"<tr><td style='background-color:white;border:.1px solid #e6e6e6;border-radius:6px' class='shadow'><article style='margin:5px 12px 5px 12px'>{0}</article></td></tr>", dr.GetString(1));
                    ans.AppendFormat(@"</table><table border='0' style='width:100%'><tr><td style=' text-align:center'><a href='answers.aspx?quesno={0}'>View</a></td>", dr.GetInt32(2));
                    ans.AppendFormat(@"<td style='text-align:right'>Answer given on {0}</td></tr><tr><td><br/></td></tr>", dr.GetDateTime(4).ToString("MM/dd/yyyy hh:mm tt"));
                    if (dr.Read())
                        flag = true;
                    else
                        flag = false;
                }
                if(flag)
                    ans.AppendFormat(@"<tr><td><a href=""viewmoreans.aspx?username={0}"">View more...</a></td></tr>", Request.QueryString[0]);
                ans.Append("</table>");
            }

            else
            {
                ans.AppendFormat("<h1>No answers given yet</h1>");
            }
            lbluserans.Text = ans.ToString();
            dr.Close();
            profile_image_DataBinding(sender, e);
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileextension = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (fileextension.ToLower() != ".jpg" && fileextension.ToLower() != ".png" && fileextension.ToLower() != ".jpeg")
                {
                    Label5.Text = "Please upload a file of extension .jpg or .jpeg or .png only"+fileextension.ToLower();
                    Label5.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    int filesize = FileUpload1.PostedFile.ContentLength;
                    if (filesize > 2097152)
                    {
                        Label5.Text = "Please upload an image within 2 MB size only";
                        Label5.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        string filename = Session["userid"].ToString() + fileextension;
                        FileUpload1.SaveAs(Server.MapPath("~/uploads/" + filename));
                        Label5.Text = "File uploaded successfully :)" + "<br/>" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "&nbsp" + "Reload page to view changes if profile image hasn't changed";
                        Label5.ForeColor = System.Drawing.Color.Green;
                        cn.Open();
                        cm.Connection = cn;
                        StringBuilder sb = new StringBuilder();
                        filename = "uploads/" + filename;
                        sb.AppendFormat("update userdetails set image='{0}' where username='{1}'", filename, Session["userid"]);
                        cm.CommandText = sb.ToString();
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                }
            else
            {
                Label5.Text = "Please select a file to upload";
                Label5.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void profile_image_DataBinding(object sender, EventArgs e)
        {
            //cn.Open();
            cm.Connection = cn;
            StringBuilder sb=new StringBuilder();
            sb.AppendFormat("select image from userdetails where username='{0}'", Request.QueryString[0]);
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            dr.Read();
            profile_image.ImageUrl = dr.GetString(0);
            //Response.Write(dr.GetString(0));
            profile_image.AlternateText ="No profile image";
            cn.Close();
            dr.Close();
        }
    }
}