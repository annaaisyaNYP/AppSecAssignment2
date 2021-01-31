using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment2
{
    public partial class LoggedIn : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }                
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }

            Response.Redirect("Login.aspx");
        }
    }
}
