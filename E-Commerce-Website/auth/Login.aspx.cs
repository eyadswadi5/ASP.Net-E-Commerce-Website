using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Website
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnLogin.Text = "Signing in ...";

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM users WHERE username=@Username AND password=@Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int userId = Convert.ToInt32(reader["id"]);
                    Session["UserID"] = userId;
                    Session["Username"] = reader["username"].ToString();

                    cmd.Parameters.Clear();
                    reader.Close();

                    query = "select " +
                        "personal_information.*, " +
                        "roles.type as role_type " +
                        "from personal_information " +
                        "join roles " +
                        "on personal_information.role_id = roles.id " +
                        "where personal_information.user_id = @UserID";

                    cmd.CommandText = query;
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        Session["auth_error"] = "";
                        Session["Role"] = reader["role_type"].ToString();
                        Session["Email"] = reader["email"].ToString();
                        Session["FirstName"] = reader["first_name"].ToString();
                        Session["LastName"] = reader["last_name"].ToString();
                        Session["Phone"] = reader["phone"].ToString();
                        Session["Gender"] = reader["gender"].ToString();
                        Session["Photo_url"] = reader["photo_url"].ToString();

                        FormsAuthentication.RedirectFromLoginPage(username, false);

                        Response.Redirect("~/dashboard/sections/LandingPage.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid username or password.";
                }
            }

            btnLogin.Enabled = true;
            btnLogin.Text = "Sign in";
        }
    }
}