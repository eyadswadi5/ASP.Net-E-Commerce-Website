using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Website.dashboard.sections.employees
{
    public partial class EditEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Department"].ToString().ToLower() == "human-resources")
            {
                if (Session["Role"].ToString().ToLower() != "manager" && Session["Role"].ToString().ToLower() != "employee")
                {
                    Master.StoreSessionAlert("You'r not permitted to access this feature.", "danger");
                    Response.Redirect("~/dashboard/sections/employees/Employees.aspx");
                }
            }
            else if (Session["Role"].ToString().ToLower() != "owner")
            {
                Master.StoreSessionAlert("You'r not permitted to access this feature.", "danger");
                Response.Redirect("~/dashboard/sections/employees/Employees.aspx");
            }

            if (IsPostBack)
                return;

            int employeeId = Convert.ToInt32(Request.QueryString["id"]);

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string query = "SELECT * FROM personal_information WHERE user_id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", employeeId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    txtFirstName.Text = Convert.ToString(reader["first_name"].ToString());
                    txtLastName.Text = Convert.ToString(reader["last_name"].ToString());
                    txtMother.Text = Convert.ToString(reader["mother"].ToString());
                    txtFather.Text = Convert.ToString(reader["father"].ToString());
                    txtMobile.Text = Convert.ToString(reader["phone"].ToString());
                    txtEmail.Text = Convert.ToString(reader["email"].ToString());
                    txtAddress.Text = Convert.ToString(reader["address"].ToString());

                    string gender = Convert.ToString(reader["gender"].ToString());
                    if (!string.IsNullOrEmpty(gender))
                    {
                        try { ddlGender.SelectedValue = gender.ToLower(); } catch { }
                    }

                    txtEmployeeIDNumber.Text = Convert.ToString(reader["national_number"].ToString());
                    txtSalary.Text = Convert.ToString(reader["salary"].ToString());

                    if (!reader.IsDBNull(reader.GetOrdinal("birthday")))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(Convert.ToString(reader["birthday"].ToString()), out dt))
                        {
                            txtBithDate.Text = dt.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            txtBithDate.Text = Convert.ToString(reader["birthday"].ToString());
                        }
                    }

                    string deptId = Convert.ToString(reader["department_id"].ToString());
                    if (!string.IsNullOrEmpty(deptId))
                    {
                        try { ddlDepartment.SelectedValue = deptId; } catch { }
                    }

                    string roleId = Convert.ToString(reader["role_id"].ToString());
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        try { ddlJobTitle.SelectedValue = roleId; } catch { }
                    }

                    string statusId = Convert.ToString(reader["employee_status_id"].ToString());
                    if (!string.IsNullOrEmpty(statusId))
                    {
                        try { ddlStatus.SelectedValue = statusId; } catch { }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(Request.QueryString["id"]);

            string uploadedFileName = null;
            string hashedFileName = null;
            string filePath = null;
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            string[] allowedContentTypes = { "image/jpeg", "image/png" };

            if (fileUploadImage != null && fileUploadImage.HasFile)
            {
                uploadedFileName = Path.GetFileName(fileUploadImage.FileName);
                string uploadedFileExtention = Path.GetExtension(uploadedFileName).ToLower();
                string uploadedContentType = fileUploadImage.PostedFile.ContentType;

                if (!allowedExtensions.Contains(uploadedFileExtention) || !allowedContentTypes.Contains(uploadedContentType))
                {
                    Master.ShowAlert("Only JPG, JPEG, and PNG files are allowed.", "warning");
                    return;
                }

                hashedFileName = GetHashedFileName(uploadedFileName);

                var saveDir = Server.MapPath("~/storage/photos/employees/");
                Directory.CreateDirectory(saveDir);
                var savePath = Path.Combine(saveDir, hashedFileName);
                filePath = "/storage/photos/employees/" + hashedFileName;
                fileUploadImage.SaveAs(savePath);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string query = @"UPDATE personal_information SET 
                first_name = @first_name,
                last_name = @last_name,
                mother = @mother,
                father = @father,
                birthday = @birthday,
                gender = @gender,
                national_number = @national_number,
                phone = @phone,
                email = @email,
                address = @address,
                salary = @salary,
                role_id = @role_id,
                department_id = @department_id,
                employee_status_id = @employee_status_id";
            query = string.IsNullOrEmpty(hashedFileName) ? query : query + ", photo_url = @photo_path";
            query += " WHERE user_id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@mother", txtMother.Text.Trim());
                cmd.Parameters.AddWithValue("@father", txtFather.Text.Trim());
                cmd.Parameters.AddWithValue("@birthday", string.IsNullOrEmpty(txtBithDate.Text.Trim()) ? (object)DBNull.Value : Convert.ToDateTime(txtBithDate.Text.Trim()));
                cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@national_number", txtEmployeeIDNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@salary", string.IsNullOrEmpty(txtSalary.Text.Trim()) ? 0 : Convert.ToInt32(txtSalary.Text.Trim()));
                cmd.Parameters.AddWithValue("@role_id", ddlJobTitle.SelectedValue);
                cmd.Parameters.AddWithValue("@department_id", ddlDepartment.SelectedValue);
                cmd.Parameters.AddWithValue("@employee_status_id", ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@id", employeeId);
                if (!string.IsNullOrEmpty(hashedFileName))
                    cmd.Parameters.AddWithValue("@photo_path", filePath);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Master.StoreSessionAlert("Employee information updated successfully.", "success");
                    Response.Redirect("~/dashboard/sections/employees/Employees.aspx");
                }
                else
                    Master.ShowAlert("No changes were made to the employee information.", "warning");
            }
        }

        private string GetHashedFileName(string originalFileName)
        {
            if (string.IsNullOrEmpty(originalFileName))
                return originalFileName;

            string extension = Path.GetExtension(originalFileName).ToLowerInvariant();
            string nameToHash = Path.GetFileNameWithoutExtension(originalFileName) + " | " + Guid.NewGuid().ToString("N") + "|" + DateTime.UtcNow.Ticks.ToString();

            using (SHA256 sha = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(nameToHash);
                byte[] hashBytes = sha.ComputeHash(inputBytes);
                var sb = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString() + extension;
            }
        }
    }
}