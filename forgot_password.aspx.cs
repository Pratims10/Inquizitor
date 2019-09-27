using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication3
{
    public partial class forgot_password : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnotp_click(object sender,EventArgs e)
        {
            SqlConnection cn = new SqlConnection(sr);
            SqlCommand cm = new SqlCommand();
            cn.Open();
            SqlDataReader dr;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select email from userdetails where username='{0}'", txtemail.Text);
            cm.Connection = cn;
            cm.CommandText = sb.ToString();
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                Session["e-mail"] = dr.GetString(0);
                sendmail();
            }
            else
            {
                Label2.Text = "Invalid Username. Enter correct username";
                Label2.ForeColor = System.Drawing.Color.Red;
            }
            
        }
        public void sendmail()
        {
            MailMessage mm = new MailMessage();
            mm.To.Add(Session["e-mail"].ToString());
            mm.From = new MailAddress("inquizitors10@gmail.com");
            mm.From = new MailAddress("");
            mm.Subject = "OTP for e-Discuss registration";
            string s = "0123456789";
            string num = string.Empty;
            int n = new Random().Next(100000, 999999);
            for (int i = 1; i <= 6; i++)
            {
                int r = n % 10;
                num = num + s[r];
                n = n / 10;
            }
            Session["otp"] = num;
            mm.Body = "Hello," + "\n" + "Your One Time Password(OTP) is " + num + " .Please use this OTP for verifying your e-mail account and create your new password.";
            //Response.Write("Hello " + txtfirstname.Text.ToString() + ",<br/>Your One Time Password is <b>" + num + "</b>.Please use this OTP for verifying your e-mail account and create your new e-Discuss account");
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Timeout = 10000;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("inquizitors10@gmail.com", "not provided here");
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mm);
            Response.Redirect("forgot_password2.aspx");
        }
    }
}