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
    public partial class WebForm2 : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        String decryptedpwd;
        protected void Page_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(sr);
            cn.Open();
            cm = new SqlCommand();
            cm.Connection = cn;
            //SqlDataReader dr;
            //cm.CommandText = "Select * from UserDetails";
            //dr = cm.ExecuteReader();
            //dr.Read();
            //    TextBox1.Text = dr.GetString(0);

        }
        public string decryptpwd(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }  

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            string s1=TextBox1.Text;
            string s2=TextBox2.Text;
            s2=encryption1(s2);
            if(s1.Trim()=="")
            {
                Label2.Text = "Enter a non-empty username";
                return;
            }
            if (s2.Trim() == "")
            {
                Label2.Text = "Enter a non-empty password";
                return;
            }
            cm.CommandText ="select * from userdetails where username='"+s1+"'";// and password='"+s2+"'";
            SqlDataReader dr;
            dr = cm.ExecuteReader();
            string password="";
            if (dr.Read())
            {
                password = dr.GetString(4);
                //decryptpwd(s2);
            }


            if (s2==password)
            {
                Session["userid"] = s1.ToLower();
                Session["speak"] = "true";
                //Session["UserName"] = dr.GetString(0);
                dr.Close();
                cn.Close();
                Response.Redirect(Request.QueryString[0]+"?val=true");
                //Response.Write(Session["userid"].ToString());
            }
            else
            {
                //Session["userid"] = s1;
                //string p = Session["userid"].ToString();
                //Response.Write(p);
                dr.Close();
                cn.Close();
                Label2.Text = "*Username and Password didn't match";
            }
        }
        public string encryption1(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}