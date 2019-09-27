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
    public partial class editprofile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 0)
            {
                Button1.Visible = false;
                return;
            }
            if(Session["userid"]==null)
            {
                Button1.Visible = false;
                return;
            }
            if(Session["userid"].ToString()!=Request.QueryString[0].ToString())
            {
                Button1.Visible = false;
                return;
            }
            if (!IsPostBack)
            {
                string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
                SqlConnection cn = new SqlConnection(sr);
                SqlCommand cm = new SqlCommand();
                SqlDataReader dr;
                cn.Open();
                cm.Connection = cn;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@"select  firstname,lastname,institution,about from userdetails where username='{0}'", Request.QueryString[0].ToString());
                cm.CommandText = sb.ToString();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    TextBox1.Text = dr.GetString(0);
                    TextBox2.Text = dr.GetString(1);
                    TextBox3.Text = dr.GetString(2);
                    TextBox4.Text = dr.GetString(3);
                }
                dr.Close();
                cn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
            SqlConnection cn = new SqlConnection(sr);
            SqlCommand cm = new SqlCommand();
            cn.Open();
            cm.Connection = cn;
            StringBuilder sb = new StringBuilder();
            if (Session["userid"].ToString() == Request.QueryString[0].ToString())
            {
                sb.AppendFormat(@"update userdetails set firstname='{0}',lastname='{1}',institution='{2}',about='{3}' where username='{4}'", TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, Session["userid"].ToString());
                Response.Write(sb.ToString());
            } 
            cm.CommandText = sb.ToString();
            cm.ExecuteNonQuery();
            cn.Close();
        }
    }
}