using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace WebApplication3
{
    public partial class emailverification : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtotp_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnotp_click(object sender, EventArgs e)
        {
         //   Response.Write(txtotp.Text.ToString());
         //   Response.Write(Session["otp"].ToString());
            if (txtotp.Text.ToString() == Session["otp"].ToString())
            {
                Session["check"] = "true";
                Response.Redirect("Register.aspx");
            }
            else
            {
                Session["check"] = "false";
                Label1.Text = "Enter the correct OTP";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void resendotp_click(object sender, EventArgs e)
        {
            sendmail();
        }
        public void sendmail()
        {
            MailMessage mm = new MailMessage();
            mm.To.Add(Session["e-mail"].ToString());
            mm.From = new MailAddress("inquizitors10@gmail.com");
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
            mm.Body = "Hello " + Session["un"].ToString() + "," + "\n" + "Your One Time Password(OTP) is " + num + " .Please use this OTP for verifying your e-mail account and create your new e-Discuss account";
            //Response.Write("Hello " + txtfirstname.Text.ToString() + ",<br/>Your One Time Password is <b>" + num + "</b>.Please use this OTP for verifying your e-mail account and create your new e-Discuss account");
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Timeout = 10000;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("inquizitors10@gmail.com", "inquizitor123");
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mm);
            Response.Redirect("emailverification.aspx");
        }
    }
}