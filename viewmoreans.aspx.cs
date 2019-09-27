using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication3
{
    public partial class viewmoreans : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("All Answers given by {0}", Request.QueryString[0]);
            Label1.Text = s.ToString();
            StringBuilder sb = new StringBuilder();
            cn = new SqlConnection(sr);
            cn.Open();
            cm = new SqlCommand();
            cm.Connection = cn;
            sb.AppendFormat("select * from allanswers where username='{0}' order by anstime desc", Request.QueryString[0]);
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            int c = 0;
            StringBuilder st = new StringBuilder();
            while (dr.Read())
            {
                c++;
                st.Append("<table border='0' style='width:70%'>");
                st.AppendFormat("<tr><td class='shadow' style='background-color:white;width:65%;text-align:left;border:.1px solid #e6e6e6;border-radius:2px'><article style='margin:14px 12px 14px 22px'>{0}</article></td></tr></table><table style='width:50%'>", dr.GetString(1));
            //    st.Append("</table><table border='0' style='margin-left:0%; width:55%'>");
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
                            st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0)*10, Session["userid"].ToString());
                            st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down blue'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                        else
                        {
                            st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up blue'></i></td>", dr.GetInt32(0)*10, Session["userid"].ToString());
                            st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                    }
                    else
                    {
                        st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0)*10, Session["userid"].ToString());
                        st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                    }
                    scn.Close();
                    dr2.Close();
                    //    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>",dr.GetInt32(0),Session["userid"].ToString());
                    //    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0),Session["userid"].ToString());
                }
                else
                {
                    st.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0) * 10, "null");
                    st.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 100, "null");
                }
                st.AppendFormat("<td style='width:6%; text-align:right'><a href='answers.aspx?quesno={0}'>View</a></td>", dr.GetInt32(2));
                st.AppendFormat("<td>&nbsp;</td><td>Answer given on {0}</td>", dr.GetDateTime(4).ToString("MM/dd/yyyy hh:mm tt"));
                st.Append("</tr><tr><td><br/></td></tr>");
            }
            st.Append("</table>");
            Label2.Text = st.ToString();
            if (c == 0)
                sb.AppendFormat("No questions posted by user-{0}", Request.QueryString[0]);
            cn.Close();
            dr.Close();
        }
    }
}