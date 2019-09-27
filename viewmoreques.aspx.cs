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
    public partial class viewmoreques : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder s=new StringBuilder();
            s.AppendFormat("All questions posted by {0}",Request.QueryString[0]);
            Label1.Text = s.ToString();
            StringBuilder sb = new StringBuilder();
            cn = new SqlConnection(sr);
            cn.Open();
            cm=new SqlCommand();
            cm.Connection=cn;
            sb.AppendFormat("select * from questions where username='{0}' order by time desc", Request.QueryString[0]);
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            int c = 0;
            StringBuilder st = new StringBuilder();
            while(dr.Read())
            {
                c++;
                st.Append("<table border='0' style='margin-left:20%; width:55%'>");
                st.AppendFormat("<tr><td style='text-align:left;background-color:white;border:.1px solid #e6e6e6' class='shadow'><article style='margin:15px 15px 15px 15px'>{0}</article></td></tr>", dr.GetString(2));
                st.Append("</table><table border='0' style='margin-left:20%; max-width:55%;width:inherit'>");
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
                            st.AppendFormat(@"<tr><td><i onclick='likefunc(this,'{1}')' id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(1), Session["userid"].ToString());
                            st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down blue'></i></td>", dr.GetInt32(1) * 10, Session["userid"].ToString());
                        }
                        else
                        {
                            st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up blue'></i></td>", dr.GetInt32(1), Session["userid"].ToString());
                            st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1) * 10, Session["userid"].ToString());
                        }
                    }
                    else
                    {
                        st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(1), Session["userid"].ToString());
                        st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1) * 10, Session["userid"].ToString());
                    }
                    scn.Close();
                    dr2.Close();
                    //    st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>",dr.GetInt32(1),Session["userid"].ToString());
                    //    st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1),Session["userid"].ToString());
                }
                else
                {
                 //   sb.AppendFormat("<tr><td>fj</td>");
                    st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(1), "null");
                    st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(1) * 10, "null");
                }
                st.AppendFormat(@"<td>{0} views</td><td>{1} answers</td>", dr.GetInt32(5), dr.GetInt32(6));
                st.AppendFormat("<td style='width:10%; text-align:right'><a href='answers.aspx?quesno={0}'>View</a></td>", dr.GetInt32(1));
                st.AppendFormat("<td style='width:35%; text-align:right'>Posted on {0}</td>", dr.GetDateTime(3).ToString("MM/dd/yyyy hh:mm tt"));
                st.Append("</tr></table><table><tr><td><br/></td></tr></table>");
            }
            Label2.Text = st.ToString();
            if (c == 0)
                sb.AppendFormat("No questions posted by user-{0}", Request.QueryString[0]);
            cn.Close();
            dr.Close();
        }
    }
}