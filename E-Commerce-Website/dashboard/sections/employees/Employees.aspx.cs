using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Website.dashboard.sections.employees
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            string selectQuery = "SELECT TOP (1000) peri.[id] " +
                    ",peri.[first_name] + ' ' + peri.[last_name] as Employee " +
                    ",peri.[email] as Email " +
                    ",d.[name] as Department " +
                    ",emps.[status] as [Status] " +
                    "FROM [STORE_DB].[dbo].[personal_information] as peri " +
                    "JOIN departments as d ON d.id = peri.department_id " +
                    "JOIN employee_status as emps ON emps.id = peri.employee_status_id";

            string searchCondition = "WHERE peri.first_name LIKE '%' + @SearchTerm + '%' " +
                    "OR peri.last_name LIKE '%' + @SearchTerm + '%' " +
                    "OR d.name LIKE '%' + @SearchTerm + '%' " +
                    "OR peri.email LIKE '%' + @SearchTerm + '%' " +
                    "OR emps.status LIKE '%' + @SearchTerm + '%'";

            if (string.IsNullOrEmpty(searchTerm))
            {
                Employees_SQL_DS.SelectCommand = selectQuery;
                Employees_SQL_DS.SelectParameters.Clear();
                
            } else
            {
                Employees_SQL_DS.SelectCommand = selectQuery + " " + searchCondition;
                Employees_SQL_DS.SelectParameters.Clear();
                Employees_SQL_DS.SelectParameters.Add("SearchTerm", searchTerm);
            }

            EmployeesGridView.DataBind();
        }

        protected void EmployeesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void EmployeesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteEmployee")
            {
                int empId = Convert.ToInt32(e.CommandArgument);

                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                string query = "DELETE FROM employees WHERE id = @id";

                using(SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@id", empId);

                    conn.Open();
                    int affected_rows = cmd.ExecuteNonQuery();

                    if (affected_rows > 0)
                        Master.ShowAlert("Employee deleted successfuly", "success");
                    else
                        Master.ShowAlert("Unable to delete employee", "danger");
                }
            }
        }
    }
}