using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userid"] = null;
            Session["fn"] = null;
            Session["ln"] = null;
            Session["ins"] = null;
            Session["un"] = null;
            Session["pwd"] = null;
            Session["nextid"] = null;
            Session["e-mail"] = null;
            Session["check"] = null;
            Response.Write("<script>alert'See you back soon :)'</script>");
            Response.Redirect("e-discuss.aspx");
        }
    }
}