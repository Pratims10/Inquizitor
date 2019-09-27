using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication3
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection scn;
        SqlCommand scm;
        SqlDataReader sdr;
        protected void Page_Load(object sender, EventArgs e)
        {
            scn = new SqlConnection(sr);
            scn.Open();
            if(Session["userid"]==null)
            {
                StringBuilder sb=new StringBuilder();
                sb.AppendFormat(@"<a href=""Login.aspx?prev=e-discuss.aspx"" style=""font-family:Tahoma; color:#000000; text-decoration:none"">Login</a>");
                Label1.Text=sb.ToString();
                StringBuilder st = new StringBuilder();
                st.AppendFormat(@"<a href=""Register.aspx"" style='font-family:Tahoma; color:#000000; text-decoration:none'>Register</a>");
                Label2.Text=st.ToString();
            }
        else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@"<a href=""Profile.aspx?username={0}""style=""font-family:Tahoma; color:#000000; text-underline-position:initial"">{0}</a>", Session["userid"].ToString());
                Label1.Text = sb.ToString();
                StringBuilder st = new StringBuilder();
                st.AppendFormat(@"<a href=""Logout.aspx?"" onclick=""logout"" style=""font-family:Tahoma; color:#000000; text-underline-position:initial"">Logout</a>");
                Label2.Text = st.ToString();
            }
            if (Session["userid"] == null)
                Label3.Text = "<table style='width:250px; box-shadow:rgba(0,0,0,0.6) 0 2px 15px;'><tr><td>Please Login with your account to see your notifications</td></tr></table>";
          else
            {
                string str = Session["userid"].ToString();
                SqlConnection cn = new SqlConnection(@"Data Source=.;Initial Catalog=e-discuss;Integrated Security=True");
                SqlCommand cm=new SqlCommand();
                SqlDataReader dr;
                StringBuilder sb=new StringBuilder();
                sb.AppendFormat("select notnumber,not0,not1,not2,not3,not4,not5,not6,not7,not8,not9 from userdetails where username='{0}'",str);
                int ctr=0;
                cn.Open();
                cm.CommandText=sb.ToString();
                cm.Connection=cn;
                dr=cm.ExecuteReader();
                dr.Read();
         /*    dr.Close();
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataSet ds = new DataSet();
                da.Fill(ds, "userdetails");
                ListBox1.DataSource = ds.Tables[0].DefaultView;
                ListBox1.dis
                ListBox1.DataBind();  */
                int notno=dr.GetInt32(0),p=0;
                StringBuilder st=new StringBuilder();
                st.AppendFormat("<table style='width:250px; box-shadow:rgba(0,0,0,0.6) 0 2px 15px;'>");
                while(notno>0)
                {
                    ctr++;
                    notno--;
                    if (dr.GetString(notno + 1) != "")
                        st.AppendFormat(@"<tr><td style='border-bottom:1px solid blue;'>{0}</tr></td>", dr.GetString(notno + 1));
                    else
                        p++;
                }
                notno=10;
                while(ctr!=10)
                {
                    ctr++;
                    if (dr.GetString(notno) != "")
                        st.AppendFormat(@"<tr><td style='border-bottom:1px solid blue'>{0}</tr></td>", dr.GetString(notno));
                    else
                        p++;
                    notno--;
                }
                st.AppendFormat("</table>");
                if (p !=10)
                    Label3.Text = st.ToString();
                else
                    Label3.Text = "No Notifications";
                //Response.Write(p);
                dr.Close();
                cn.Close();
            }
            scm = new SqlCommand();
            scm.Connection = scn;
            scm.CommandText = "select LanguageType, COUNT(LanguageType) as ctr from questions group by LanguageType";
            sdr = scm.ExecuteReader();
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("<table border='0'>");
            while (sdr.Read())
            {
                sb1.AppendFormat(@"<tr><td style='background-color:#e4eaef;border-radius:10px;'>&nbsp;&nbsp;<a href='category.aspx?cat={0}'>{1}</a>&nbsp;</td>",Server.UrlEncode( sdr.GetString(0)),sdr.GetString(0));
                sb1.AppendFormat(@"<td>({0})<br/></td></tr>", sdr.GetInt32(1));
            }
            sb1.Append("</table>");
            sdr.Close();
            Label4.Text = sb1.ToString();
        }
        void logout(object sender,EventArgs e)
        {
            Session["userid"] = null;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string s = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            Timerlabel.Text = s;
        }
        [System.Web.Services.WebMethodAttribute(),System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] getlist(string prefixtext,int count)
        {
            string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
            SqlConnection cn=new SqlConnection(sr);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"select question from questions where question like '%{0}%'",prefixtext);
            
            SqlDataAdapter da = new SqlDataAdapter(sb.ToString(),cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "questions");
            int rcount, size;
            rcount = ds.Tables[0].Rows.Count;
            if (rcount >= count)
                size = count;
            else
                size = rcount;
            string[] pnames = new string[size];
            for(int i=0;i<size;i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                pnames[i] = row["question"].ToString();

            }
            return pnames;
        }

        protected void btnsearch_click(object sender, EventArgs e)
        {
            string s=TextBox1.Text.ToString();
            if (TextBox1.Text.ToString().Trim() != "")
            {
                s = s.Replace(' ', '%');
                Session["srch"] = s;
                    Response.Redirect("search_results.aspx");
            }
        }
    }
}