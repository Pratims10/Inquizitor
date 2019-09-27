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
    public partial class forgot_password2 : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Text = "Check your e-mail address for the One Time Password(OTP)";
        }

        protected void btn_click(object sender, EventArgs e)
        {
            if (Session["otp"].ToString() == TextBox1.Text.ToString())
            {
                SqlConnection cn = new SqlConnection(sr);
                SqlCommand cm=new SqlCommand();
                cn.Open();
                SqlDataReader dr;
                StringBuilder sb=new StringBuilder();
                sb.AppendFormat("update userdetails set password='{0}' where email='{1}'",TextBox2.Text,Session["e-mail"]);
                cm.Connection=cn;
                cm.CommandText=sb.ToString();
                cm.ExecuteNonQuery();
                cn.Close();
                Response.Redirect("Login.aspx?prev=e-discuss.aspx");
            }
            else
            {
                Label1.Text = "Enter the corrct OTP";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}