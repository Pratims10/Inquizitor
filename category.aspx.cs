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
    public partial class category : System.Web.UI.Page
    {
        string sr = ConfigurationManager.ConnectionStrings["cok"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count!=0)
            {
                SqlConnection cn = new SqlConnection(sr);
                cn.Open();
                string st = Server.UrlDecode(Request.QueryString[0].ToString());
                SqlCommand cm = new SqlCommand();
                cm.Connection = cn;
                SqlDataReader dr;
                StringBuilder sb = new StringBuilder();
                cm.CommandText = "select  questions.username,questions.quesno,questions.question,questions.time,questions.LanguageType,questions.views,questions.answers, userdetails.image from questions,userdetails where questions.username=userdetails.username and questions.LanguageType='" + st + "'";
             //   Response.Write("select * from questions where languagetype='" + Request.QueryString[0] + "'");
                dr = cm.ExecuteReader();
                bool flag = false;
                while (dr.Read())
                {
                    flag = true;
                    sb.AppendFormat(@"<table style='width:70%;text-align:left'><tr><td style='width:10%'> <a href='profile.aspx?username={0}'><img alt='No image' src='{1}' style='height:40px; width:40px; border-radius:50%'/></a>  </td><td style='text-align:left;width:25%'><a href='profile.aspx?username={0}'>{0}</a></td><td></td></tr></table><table style='width:70%'>", dr.GetString(0),dr.GetString(7));
                    sb.AppendFormat(@"<tr><td class='shadow' style='text-align:left;margin:.1px solid #bfbfbf;background-color:white;border-radius:3px'><article style='margin:10px 10px 10px 18px'>{0}</article></td></tr></table><table style='width:70%;border-bottom:1px solid brown;text-align:left'>", dr.GetString(2));
                    if (Session["userid"] != null)
                    {
                        SqlConnection scn = new SqlConnection(@"Data Source=.;Initial Catalog=e-discuss;Integrated Security=True");
                        scn.Open();
                        SqlCommand scm = new SqlCommand();
                        scm.Connection = scn;
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendFormat(@"select * from likedislike where username='{0}' and quesno={1}", Session["userid"].ToString(), dr.GetInt32(1));
                        SqlDataReader dr2;
                        scm.CommandText = sb2.ToString();
                        dr2 = scm.ExecuteReader();
                        if (dr2.Read())
                        {
                            if (dr2.GetInt32(1) == 0)
                            {
                                sb.AppendFormat(@"<tr><td><i onclick='likefunc(this,'{1}')' id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(1), Session["userid"].ToString());
                                sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down blue'></i></td>", dr.GetInt32(1) * 10, Session["userid"].ToString());
                            }
                            else
                            {
                                sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up blue'></i></td>", dr.GetInt32(1), Session["userid"].ToString());
                                sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1) * 10, Session["userid"].ToString());
                            }
                        }
                        else
                        {
                            sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(1), Session["userid"].ToString());
                            sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1) * 10, Session["userid"].ToString());
                        }
                        scn.Close();
                        dr2.Close();
                        //    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>",dr.GetInt32(1),Session["userid"].ToString());
                        //    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1),Session["userid"].ToString());
                    }
                    else
                    {
                        sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(1), "null");
                        sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1) * 10, "null");
                    }
                    sb.AppendFormat(@"<td><a href='answers.aspx?quesno={3}'>View</a></td><td>{1} views</td><td>{2} answers</td><td>Posted on {0}</td></tr></table><table><tr><td>&nbsp;</td></tr></table>", dr.GetDateTime(3).ToString("MM/dd/yyyy hh:mm tt"), dr.GetInt32(5), dr.GetInt32(6), dr.GetInt32(1));
                }
                if (flag == false)
                    sb.AppendFormat("No questions in this category yet.");
                Label1.Text = sb.ToString();
                dr.Close();
                cn.Close();
            }
        }
    }
}