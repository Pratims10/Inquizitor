using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Data;
using System.Text;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        Int32 NextID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            SqlConnection cn = new SqlConnection(sr);
            cn.Open();
            SqlCommand cm = new SqlCommand("Select UserID From UserDetails order by UserId Desc", cn);
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                NextID = dr.GetInt32(0) + 1;//new userid is old highest userid+1
            }
            else NextID = 1;
            dr.Close();
            if (Session["check"] != null)
            {
                if (Session["check"].ToString() == "true")
                {
                    SqlCommand cmd = new SqlCommand("Insert into userdetails(firstname,lastname,institution,username,password,userid,image,email,quesasked,ansgiven,notnumber,not0,not1,not2,not3,not4,not5,not6,not7,not8,not9,about) values('" + Session["fn"].ToString() + "','" + Session["ln"].ToString() + "','" + Session["ins"].ToString() + "','" + Session["un"].ToString() + "','" + Session["pwd"].ToString() + "'," + Session["nextid"].ToString() + ",'uploads/img-avatar.png','" + Session["e-mail"].ToString() + "',0,0,0,'','','','','','','','','','','"+Session["about"].ToString()+"')", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    Session["speak"] = "true";
                    Session["userid"] = Session["un"];
                    Session["userid"] = Session["userid"];
                    Response.Redirect("e-discuss.aspx");
                }
            }
        }
        protected void btnregister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            //CHECKING TO BE DONE HERE
            Session["fn"] = txtfirstname.Text;
            Session["ln"] = txtlastname.Text;
            Session["ins"] = txtinstitution.Text;
            Session["un"] = txtusername.Text;
            Session["e-mail"] = txtemail.Text;
            Session["pwd"]=encryption1(txtpassword.Text);
            Session["about"] = txtaboutyourself.Text;
            Session["nextid"] = NextID.ToString();
            sendmail();//sending verification mail
        }

        public string encryption1(string str)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }  
        public void sendmail()
        {
            MailMessage mm = new MailMessage();
            mm.To.Add(txtemail.Text);
            mm.From = new MailAddress("inquizitors10@gmail.com");
            mm.Subject = "OTP for e-Discuss registration";
            string s="0123456789";
            string num = string.Empty;
            int n = new Random().Next(100000, 999999);
            for(int i=1;i<=6;i++)
            {
                int r = n % 10;
                num = num + s[r];
                n = n / 10;
            }
            Session["otp"] = num;
            mm.Body = "Hello " + txtfirstname.Text.ToString()+"," +"\n"+ "Your One Time Password(OTP) is " + num + " .Please use this OTP for verifying your e-mail account and create your new e-Discuss account";
            SmtpClient client = new SmtpClient("smtp.gmail.com",587);
            client.Timeout = 10000;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("inquizitors10@gmail.com","password not provided here" );
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mm);
            Response.Redirect ("emailverification.aspx");
        }
    }
}
