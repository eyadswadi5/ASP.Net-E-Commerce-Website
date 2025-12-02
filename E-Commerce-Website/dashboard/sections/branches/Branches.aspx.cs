using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Website.dashboard.sections.branches
{
    public partial class Branches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                Branches_SQL_DS.SelectCommand = @"SELECT TOP (1000)" +
                    "stores.[name]" +
                    ",stores.[address] + ', ' + stores.[city] as Location" +
                    ",stores.[phone]" +
                    ",personal_information.first_name + ' ' + personal_information.last_name as ManagerName " +
                    "FROM [STORE_DB].[dbo].[stores] " +
                    "JOIN personal_information ON personal_information.user_id = stores.manager_id;";

                Branches_SQL_DS.SelectParameters.Clear();

            } else
            {
                Branches_SQL_DS.SelectCommand = @"SELECT TOP (1000)" +
                    "stores.[name]" +
                    ",stores.[address] + ', ' + stores.[city] as Location" +
                    ",stores.[phone]" +
                    ",personal_information.first_name + ' ' + personal_information.last_name as ManagerName " +
                    "FROM [STORE_DB].[dbo].[stores] JOIN personal_information ON personal_information.user_id = stores.manager_id " +
                    "WHERE stores.[name] LIKE '%' + @SearchTerm + '%' " +
                    "OR stores.[address] LIKE '%' + @SearchTerm + '%' " +
                    "OR stores.[city] LIKE '%' + @SearchTerm + '%' " +
                    "OR stores.[phone] LIKE '%' + @SearchTerm + '%' " +
                    "OR personal_information.first_name LIKE '%' + @SearchTerm + '%' " +
                    "OR personal_information.last_name LIKE '%' + @SearchTerm + '%';";
                Branches_SQL_DS.SelectParameters.Clear();
                Branches_SQL_DS.SelectParameters.Add("SearchTerm", searchTerm);
            }

            BranchesGridView.DataBind();

        }

        protected void BranchesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void BranchesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteBranch")
            {
                int branchId = Convert.ToInt32(e.CommandArgument);

                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                string query = "DELETE FROM stores WHERE id = @id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id", branchId);

                    conn.Open();
                    int affected_rows = cmd.ExecuteNonQuery();
                    if (affected_rows > 0)
                        Master.ShowAlert("Branch Deleted Successfully!", "success");
                    else
                        Master.ShowAlert("Unenable to delete branch", "danger");
                }

                BranchesGridView.DataBind();
            }
        }
    }
}