using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Website.dashboard.sections.warehouses
{
    public partial class Warehouses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            string selectQuery = "SELECT TOP (1000) wh.[id]," +
                "wh.[name]," +
                "wh.[city]," +
                "wh.[address]," +
                "wh.[phone]," +
                "peri.first_name + ' ' + peri.last_name as ManagerName " +
                "FROM [STORE_DB].[dbo].[warehouses] as wh JOIN personal_information as peri " +
                "ON wh.manager_id = peri.user_id";

            string searchCondition = "WHERE wh.name LIKE '%' + @SearchTerm + '%' " +
                    "OR wh.city LIKE '%' + @SearchTerm + '%' " +
                    "OR wh.address LIKE '%' + @SearchTerm + '%' " +
                    "OR wh.phone LIKE '%' + @SearchTerm + '%' " +
                    "OR peri.first_name + ' ' + peri.last_name LIKE '%' + @SearchTerm + '%'";

            if (string.IsNullOrEmpty(searchTerm))
            {
                Warehouses_DS_SQL.SelectCommand = selectQuery;
                Warehouses_DS_SQL.SelectParameters.Clear();
            }
            else
            {
                Warehouses_DS_SQL.SelectCommand = selectQuery + " " + searchCondition;
                Warehouses_DS_SQL.SelectParameters.Clear();
                Warehouses_DS_SQL.SelectParameters.Add("SearchTerm", searchTerm);
            }

            WarehousesGridView.DataBind();
        }

        protected void WarehousesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteWarehouse")
            {
                if (Session["Department"].ToString().ToLower() == "warehouses")
                {
                    if (Session["Role"].ToString().ToLower() != "manager" && Session["Role"].ToString().ToLower() != "employee")
                    {
                        Master.StoreSessionAlert("You'r not permitted to access this feature.", "danger");
                        Response.Redirect("~/dashboard/sections/warehouses/Warehouses.aspx");
                    }
                }
                else if (Session["Role"].ToString().ToLower() != "owner")
                {
                    Master.StoreSessionAlert("You'r not permitted to access this feature.", "danger");
                    Response.Redirect("~/dashboard/sections/warehouses/Warehouses.aspx");
                }

                int warehouseId = Convert.ToInt32(e.CommandArgument);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                string query = "DELETE FROM [warehouses] WHERE id = @WarehouseId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WarehouseId", warehouseId);
                    connection.Open();
                    int affected_rows = command.ExecuteNonQuery();
                    if (affected_rows > 0)
                        Master.ShowAlert("Warehouse deleted successfully.", "success");
                }

                WarehousesGridView.DataBind();
            }
        }
    }
}