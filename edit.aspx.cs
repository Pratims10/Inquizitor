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
    public partial class edit : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 0)
                return;
            if (!IsPostBack)
            {
                SqlConnection cn = new SqlConnection(sr);
                cn.Open();
                SqlCommand cm = new SqlCommand();
                cm.Connection = cn;
                string qno = Request.QueryString[0].ToString();
                SqlDataReader dr;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@"select question,username from questions where quesno={0}", qno);
                cm.CommandText = sb.ToString();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    if (Session["userid"] != null)
                    {
                        if (Session["userid"].ToString() == dr.GetString(1))
                        {
                            txt.InnerText = dr.GetString(0);
                            dr.Close();
                            cn.Close();
                        }
                        else
                        {
                            dr.Close();
                            cn.Close();
                            Response.Write("<script>alert('You are not allowed to edit this question');</script>");
                        }
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();
                        Response.Write("<script>alert('Login to edit the question');</script>");
                    }
                }
            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(sr);
            cn.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            string qno = Request.QueryString[0].ToString();
            SqlDataReader dr;
            StringBuilder sb=new StringBuilder();
            sb.AppendFormat(@"select question,username from questions where quesno={0}", qno);
            cm.CommandText=sb.ToString();
            dr = cm.ExecuteReader();
            if(dr.Read())
            {
                if(Session["userid"]!=null)
                {
                    if(Session["userid"].ToString()==dr.GetString(1))
                    {
                        dr.Close();
                        StringBuilder s1=new StringBuilder();
                        s1.AppendFormat(@"update questions set question='{0}' where quesno={1}",txt.InnerText,qno);
                        cm.CommandText=s1.ToString();
                     //   Response.Write(s1.ToString());
                        cm.ExecuteNonQuery();
                        cn.Close();
                        Response.Redirect("e-discuss.aspx");
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();
                        Response.Write("<script>alert('You are not allowed to edit this question');</script>");
                    }
                }
                else
                {
                    dr.Close();
                    cn.Close();
                    Response.Write("<script>alert('Login to edit the question');</script>");
                }
            }
        }
    }
}