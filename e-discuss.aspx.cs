using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;

namespace WebApplication3
{
    public partial class e_discuss : System.Web.UI.Page
    {
        string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        string s;
        string sess;
        int ctr = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
           // sess = Session["userid"].ToString();
           // Session["userid"] = "";
            cn = new SqlConnection(sr);
            cn.Open();
            cm = new SqlCommand();
            cm.Connection = cn;
            if (!IsPostBack)
            {
                cm.CommandText="select languagetype,catid  from category";
                dr = cm.ExecuteReader();
                DropDownList1.DataSource = dr;
                DropDownList1.DataTextField = "languagetype";
                DropDownList1.DataValueField = "catid";
                DropDownList1.DataBind();
                dr.Close();
            }
            //newques.InnerText = "";
            //DropDownList1.SelectedValue = "1";
            //TextBox2.Text = "";
            ctr=func("time","desc");
          //  if(Request.QueryString[0]!=null&& Request.QueryString[0].ToString()=="true")
            if (Session["speak"] != null && Session["speak"].ToString() == "true")
            {
                string str = Session["userid"].ToString();
                Session["speak"] = "false";
                Response.Write("<script>alert('Welcome "+str+"')</script>");
                //  ScriptManager.RegisterClientScriptBlock(this,typeof())   "utterusername(" + Session["userid"].ToString() + ")"
              //  ScriptManager.RegisterStartupScript(this,typeof(System.Web.UI.Page),"clientScript","utterusername(" + Session["userid"].ToString() + ")",true);
            }
            //List<String> CatName = new List<string>;
            //while (dr.Read)
            //{
            //    CatName.Add = dr.GetString(0);
            //}
            //Button[] l1 = new Button[10];
            //while(int i < 10)
            //{
            //    l1[i] = new Button();
            //    l1[i].Text = CatName[i];

            // l1[i].Click += new EventHandler( Button_Click( l1[i].text));
            
            //}
        }
        protected void btnpost_Click(object sender, EventArgs e)
        {
            //s = Request.QueryString[0];
            if (Session["userid"]==null)
                Response.Redirect("Login.aspx?prev=e-discuss.aspx#abcd");
            else
            {
                string p = newques.InnerText.ToString();
                p = p.Trim();
                if (p == "")
                {
                    //Response.Write("<script>alert('The question is not a joke.Please enter something valid to post.')</script>");
                //    Response.Redirect("e-discuss.aspx#abcd");
                    Label3.Text = "Enter your question.";
                    Label3.ForeColor = System.Drawing.Color.Red;
                }
                s = Session["userid"].ToString();
                StringBuilder sb = new StringBuilder();
                string s3="";
                bool flag=true;
                if (DropDownList1.SelectedValue != "1")
                    s3 = DropDownList1.SelectedItem.Text;
                else
                {
                    if (TextBox2.Text.ToString().Trim() != "")
                        s3 = TextBox2.Text;
                    else
                    {
                        flag = false;
                        Label3.Text = "You must select a category for your question or enter a new category";
                        Label3.ForeColor = System.Drawing.Color.Red;
                 //       Response.Redirect("e-discuss.aspx#abcd");
                    }
                }
                if(flag)
                {
                    sb.AppendFormat("insert into questions(username,quesno,question,views,answers,languagetype)  values('{0}',{1},'{2}',0,0,'{3}')", s, ctr, newques.InnerText,s3);
                    SqlCommand cm = new SqlCommand();
                    cm.Connection = cn;
                    cm.CommandText = sb.ToString();
                    //cm.Parameters.AddWithValue("@time", DateTime.Now);
                    cm.ExecuteNonQuery();
                    StringBuilder stm = new StringBuilder();
                    stm.AppendFormat(@"update questions set time = getdate() where quesno={0}", ctr);
                    cm.CommandText = stm.ToString();
                    cm.ExecuteNonQuery();
                    ctr = func("time","desc");
                    StringBuilder s1 = new StringBuilder();
                    s1.AppendFormat(@"select quesasked from userdetails where username='{0}'", s);
                    cm.CommandText = s1.ToString();
                    dr = cm. ExecuteReader();
                    int ques;
                    if (dr.Read())
                        ques = dr.GetInt32(0) + 1;
                    else
                        ques = 1;
                    dr.Close();
                    StringBuilder s2 = new StringBuilder();
                    s2.AppendFormat(@"update userdetails set quesasked={0} where username='{1}'", ques, s);
                    cm.CommandText = s2.ToString();
                    cm.ExecuteNonQuery();
                    //   newquestion.Text = "s";
                }
                cn.Close();
            }
            newques.InnerText = "";
            DropDownList1.SelectedValue = "1";
            TextBox2.Text = "";
        }
        protected void btnview_click(object sender,EventArgs e)
        {
            if (Session["userid"]==null)
                Response.Redirect("Login.aspx?prev=e-discuss.aspx");
            //Response.Write("D");
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Answer.aspx?quesno={0}", sender);
                Response.Redirect(sb.ToString());
            }
        }
        [System.Web.Services.WebMethod]
        public static string  like(int quesno,string username)
        {
            string sr = System.Configuration.ConfigurationManager.ConnectionStrings["cok"].ToString();
            if (username == "null")
                return "Please Login to like this question";
            SqlConnection scn = new SqlConnection(sr);
            scn.Open();
            SqlCommand scm = new SqlCommand();
            scm.Connection = scn;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(@"select * from likedislike where username='{0}' and quesno={1}", username, quesno);
            scm.CommandText = sb1.ToString();
            SqlDataReader dr;
            dr = scm.ExecuteReader();
         //   scm.ExecuteNonQuery();
                StringBuilder sb = new StringBuilder();
             if(dr.Read())
             {
                 if(dr.GetInt32(1)==0)
                 {
                     sb.AppendFormat(@"update likedislike set lkes=1 where username='{0}' and quesno={1}", username, quesno);
                     dr.Close();
                     scm.CommandText = sb.ToString();
                     scm.ExecuteNonQuery();
                     scn.Close();
                     return "dislike removed and liked";
                 }
                 else
                 {
                     sb.AppendFormat(@"delete from likedislike where username='{0}' and quesno={1}", username, quesno);
                     dr.Close();
                     scm.CommandText = sb.ToString();
                     scm.ExecuteNonQuery();
                     
                     scn.Close();
                     return "like removed";
                 }
             }
             else
             {
                 sb.AppendFormat(@"insert into likedislike values ('{0}',1,{1})", username, quesno);
                 dr.Close(); scm.CommandText = sb.ToString();
                 scm.ExecuteNonQuery();
                 scn.Close();
                 return "liked";
             }
        }
        [System.Web.Services.WebMethod]
        public static string dislike(int quesno, string username)
        {
            if (username == "null")
                return "Please Login to dislike this question";
            SqlConnection scn = new SqlConnection(@"Data Source=.;Initial Catalog=e-discuss;Integrated Security=True");
            scn.Open();
            SqlCommand scm = new SqlCommand();
            scm.Connection = scn;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(@"select * from likedislike where username='{0}' and quesno={1}", username, quesno);
            scm.CommandText = sb1.ToString();
            SqlDataReader dr;
            dr = scm.ExecuteReader();
           // scm.ExecuteNonQuery();
            StringBuilder sb = new StringBuilder();
            if (dr.Read())
            {
                if (dr.GetInt32(1) == 0)
                {
                    sb.AppendFormat(@"delete from likedislike where username='{0}' and quesno={1}", username, quesno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();
                    
                    scn.Close();
                    return "dislike removed";
                }
                else
                {
                    sb.AppendFormat(@"update likedislike set lkes=0 where username='{0}' and quesno={1}", username, quesno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();
                  
                    scn.Close();
                    return "like removed and disliked";
                }
            }
            else
            {
                try
                {
                    sb.AppendFormat(@"insert into likedislike values('{0}',0,{1})", username, quesno);
                    dr.Close();
                    scm.CommandText = sb.ToString();
                    scm.ExecuteNonQuery();
                    scn.Close();
                    return "disliked";
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return e.ToString();
                }
            }
        }
        int func(string st,string sort)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat(@"select top(100) questions.quesno, questions.question,questions.time,questions.views,questions.answers, userdetails.username, userdetails.firstname + ' ' + userdetails.lastname as FullName, userdetails.image as img from questions  , userdetails where questions.username= userdetails.username order by questions.{0} {1}",st,sort);
            int c=1;
            cm.CommandText = str.ToString();
            dr = cm.ExecuteReader() ;
            StringBuilder sb=new StringBuilder();
            while(dr.Read())
            {
                sb.Append("<table border='0' style='margin-left:5%; width:100%'>");
                sb.AppendFormat(@"<tr><td style='text-align:center;height:50px;width:10%;border-radius:50%'><a href='profile.aspx?username={1}'><img alt='No image' src='{0}' style='height:40px; width:40px; border-radius:50%'/></a> </td>", dr.GetString(7),dr.GetString(5));
                sb.AppendFormat(@"<td style='text-align:center;height:50px;width:20%'><a href='Profile.aspx?username={1}' style='text-decoration:none'>{0}</a></td><td></td>", dr.GetString(6), dr.GetString(5));
                sb.Append("<td style='width:60%'>&nbsp;</td>");
                if (Session["userid"] != null)
                {
                    if(Session["userid"].ToString()==dr.GetString(5))
                    sb.AppendFormat(@"<td style='align-content:right'><div class='dropdown'><button class='dropbtn'><i style='font-size:20px' class='fa'>&#xf013;</i></button><div class='dropdown-content'><a href='edit.aspx?qno={0}'>Edit</a><a href='delete.aspx?qno={0}'>Delete</a></div></div></td></tr>", dr.GetInt32(0));
                }
                sb.Append(@"</table><table style='margin-left:5%'><tr>");
                sb.AppendFormat("<td class='shadow' style='background-color:white;border:.1px solid #e6e6e6;border-radius:2px'><article style='margin:14px 12px 14px 22px'>{0}</article></td></tr>", dr.GetString(1));
                sb.Append("</table><table border='0' style='border-bottom:1px solid blue; margin-left:5%; width:100%;'>");
                if(Session["userid"]!=null)
                {
                    SqlConnection scn = new SqlConnection(sr);
                    scn.Open();
                    SqlCommand scm = new SqlCommand();
                    scm.Connection = scn;
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendFormat(@"select * from likedislike where username='{0}' and quesno={1}", Session["userid"].ToString(), dr.GetInt32(0));
                    SqlDataReader dr2;
                    scm.CommandText = sb2.ToString();
                    dr2 = scm.ExecuteReader();
                    if (dr2.Read())
                    {
                        if(dr2.GetInt32(1)==0)
                        {
                            sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0), Session["userid"].ToString());
                            sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down blue'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                        else
                        {
                            sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up blue'></i></td>", dr.GetInt32(0), Session["userid"].ToString());
                            sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                        }
                    }
                    else
                    {
                        sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0), Session["userid"].ToString());
                        sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0) * 10, Session["userid"].ToString());
                    }
                    scn.Close();
                    dr2.Close();
                //    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>",dr.GetInt32(0),Session["userid"].ToString());
                //    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0),Session["userid"].ToString());
                }
                else
                {
                    sb.AppendFormat(@"<tr><td><i onclick=""likefunc(this,'{1}')"" id='{0}' class='fa fa-thumbs-up'></i></td>", dr.GetInt32(0), "null");
                    sb.AppendFormat(@"<td>&nbsp;</td><td><i onclick=""dislikefunc(this,'{1}')"" id='{0}0' class='fa fa-thumbs-down'></i></td>", dr.GetInt32(0)*10, "null");
                }
                    sb.AppendFormat(@"<td style='text-align:center; height:50px;width:20%'><a href='Answers.aspx?quesno={0}' target=""_blank"">View</a></td><td>{1} Views</td><td>{2} answers</td>", dr.GetInt32(0), dr.GetInt32(3),dr.GetInt32(4));
                    sb.AppendFormat(@"<td style='text-align:left'>Posted on {0}</td>", dr.GetDateTime(2).ToString("MM/dd/yyyy hh:mm tt"));
                //else
                  //  Response.Redirect("Login.apsx");
                    //sb.AppendFormat(@"<td><a href='Answers.aspx?quesno={0}' target=""_blank"">View</a></td>", dr.GetInt32(1));
                //sb.AppendFormat(@"<td><asp:Button ID=""Button1"" runat=""server"" Text=""Button"" /></td>");
                //sb.AppendFormat(@"<td>fs<asp:Button ID=""{0}"" runat=""server"" Text=""View"" OnClick=""btnview_click"" /></td>", dr.GetInt32(1));
                sb.Append("</tr>");
                sb.Append("</table>");
                c++;
                
            }
            dr.Close();
            Label1.Text=sb.ToString();
            cm.CommandText = "select max(quesno) from questions";
            dr = cm.ExecuteReader();
            dr.Read();
            c = dr.GetInt32(0);
            dr.Close();
            return c+1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("fhs");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            func("time","desc");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            func("answers","desc");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            func("views","desc");
        }

        protected void Button5_Click1(object sender, EventArgs e)
        {
            func("answers", "asc");
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            func("time", "asc");
        }

        protected void Button6_Click1(object sender, EventArgs e)
        {
            func("views", "asc");
        }
    }
}