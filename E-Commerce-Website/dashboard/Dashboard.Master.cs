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

            if (Session["AlertMessage"] != null && Session["AlertType"] != null)
            {
                string message = Session["AlertMessage"].ToString();
                string alertType = Session["AlertType"].ToString();
                ShowAlert(message, alertType);
                Session.Remove("AlertMessage");
                Session.Remove("AlertType");
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/auth/Login.aspx");
        }

        public void StoreSessionAlert(string message, string alertType)
        {
            Session["AlertMessage"] = message;
            Session["AlertType"] = alertType;
        }

        public void ShowAlert(string message, string alertType)
        {
            if (alertType == "warning")
            {
                AlertTitle.InnerText = "Warning!";
                lblAlert.Text = message;
                AlertPanel.CssClass = "alert alert-warning alert-dismissible fade show";
            }

            if (alertType == "success")
            {
                AlertTitle.InnerText = "Success!";
                lblAlert.Text = message;
                AlertPanel.CssClass = "alert alert-success alert-dismissible fade show";
            }

            if (alertType == "danger")
            {
                AlertTitle.InnerText = "Something Went Wrong!";
                lblAlert.Text = message;
                AlertPanel.CssClass = "alert alert-danger alert-dismissible fade show";
            }
        }
    }
}