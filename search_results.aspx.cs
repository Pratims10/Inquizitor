using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;

namespace WebApplication3
{
    public partial class search_results : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["srch"] == null)
                return;
            string s = Session["srch"].ToString();
            Label1.Text = "Search results for <b>" + s + "</b>";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"select questions.quesno, questions.question,questions.time,questions.views,questions.answers, userdetails.username, userdetails.firstname + ' ' + userdetails.lastname as FullName, userdetails.image as img from questions  , userdetails where questions.username= userdetails.username and question like '%{0}%' order by questions.time desc ", s);
            cn = new SqlConnection(sr);
            cn.Open();
            cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            StringBuilder st = new StringBuilder();
            bool flag = false;
            //Response.Write(sb.ToString());
            while(dr.Read())
            {
                flag = true;
                st.Append("<table border='0' style='margin-left:9%; width:10%'>");
                st.AppendFormat(@"<tr><td style='text-align:center;height:50px;width:5%;border-radius:50%'><a href='profile.aspx?username={1}'><img alt='No image' src='{0}' style='height:40px; width:40px; border-radius:50%'/></a> </td>", dr.GetString(7), dr.GetString(5));
                st.AppendFormat(@"<td style='text-align:center;height:50px'><a href='Profile.aspx?username={1}' style='text-decoration:none'>{0}</a></td><td></td></tr></table>", dr.GetString(6), dr.GetString(5));
                st.AppendFormat("<table style='margin-left:9%;width:60%'><tr><td style='text-align:left;background-color:white;border:.1px solid #e6e6e6;border-radius:2px' class='shadow'><article style='margin:14px 12px 14px 22px'>{0}</article></td></tr>", dr.GetString(1));
                st.Append("</table><table border='0' style='margin-left:9%;  width:55%'>");
                
                if (Session["userid"] != null)
                {
                    SqlConnection scn = new SqlConnection(sr);
                    scn.Open();
                    SqlCommand scm = new SqlCommand();
                    scm.Connection = scn;
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendFormat(@"select * from likedislike where username='{0}' and quesno={1}", Session["userid"].ToString(), dr.GetInt32(0));
                    SqlDataReader dr2;
                    scm.CommandText = sb2.ToString();
                    dr2 = scm.ExecuteReader();
                    if (dr2.Read())
                    {
                        if (dr2.GetInt32(1) == 0)
                        {
                            st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0), Session["userid"].ToString());
                            st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down blue'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                        else
                        {
                            st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up blue'></i></td>", dr.GetInt32(0), Session["userid"].ToString());
                            st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                    }
                    else
                    {
                        st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0), Session["userid"].ToString());
                        st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                    }
                    scn.Close();
                    dr2.Close();
                    //    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>",dr.GetInt32(0),Session["userid"].ToString());
                    //    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0),Session["userid"].ToString());
                }
                else
                {
                    st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0), "null");
                    st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, "null");
                }
                st.AppendFormat("<td> {1} views</td><td>{2} answers</td><td style='width:10%; text-align:right'><a href='answers.aspx?quesno={0}'>View</a></td>", dr.GetInt32(0),dr.GetInt32(3),dr.GetInt32(4));
                st.AppendFormat("<td style= 'text-align:right'>Posted on {0}</td>", dr.GetDateTime(2).ToString("MM/dd/yyyy hh:mm tt"));
                st.Append("</tr></table><table style='border-bottom:1px solid red'><tr><td>&nbsp;</td></tr></table>");
            }
            if(flag==false)
            {
                Label3.Text = "<p style=''>No search results:( Try entering some more common keywords and shorter search expressions.</p>";
                Label3.ForeColor=System.Drawing.Color.Red;
            }
            else
            Label3.Text = st.ToString();
            dr.Close();
            cn.Close();
            Session["srch"] = null;
        }
    }
}