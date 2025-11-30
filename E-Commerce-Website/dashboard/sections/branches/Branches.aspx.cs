using System;
using System.Collections.Generic;
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
    }
}