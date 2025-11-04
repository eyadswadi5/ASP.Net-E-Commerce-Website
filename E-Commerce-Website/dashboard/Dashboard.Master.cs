using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Website.dashboard
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated &&
                !Request.Url.AbsolutePath.ToLower().Contains("login.aspx"))
            {
                Response.Redirect("~/auth/Login.aspx");
            }

            lblUserName.Text = Session["FirstName"] + " " + Session["LastName"];
            lblUserRole.Text = Session["Role"].ToString();

        }
    }
}