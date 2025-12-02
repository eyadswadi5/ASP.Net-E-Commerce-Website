using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace E_Commerce_Website.dashboard.sections.employees
{
    public partial class AddEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString().ToLower() != "employee")
            {
                Session["auth_error"] = "You'r not permitted to access this feature.";
                Response.Redirect("~/dashboard/sections/employees/Employees.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName?.Text.Trim() ?? string.Empty;
            string lastName = txtLastName?.Text.Trim() ?? string.Empty;
            string motherName = txtMother?.Text.Trim() ?? string.Empty;
            string fatherName = txtFather?.Text.Trim() ?? string.Empty;
            string phone = txtMobile?.Text.Trim() ?? string.Empty;
            string email = txtEmail?.Text.Trim() ?? string.Empty;
            string address = txtAddress?.Text.Trim() ?? string.Empty;
            string gender = ddlGender?.SelectedValue ?? string.Empty;

            string employeeUsername = txtEmployeeUsername?.Text.Trim() ?? string.Empty;
            string employeeIDNumber = txtEmployeeIDNumber?.Text.Trim() ?? string.Empty;
            string departmentId = ddlDepartment?.SelectedValue ?? string.Empty;
            string jobTitleId = ddlJobTitle?.SelectedValue ?? string.Empty;
            string employeePassword = txtEmployeePassword?.Text ?? string.Empty;

            string salaryText = txtSalary?.Text.Trim() ?? string.Empty;
            int salary = 0;
            if (!string.IsNullOrEmpty(salaryText))
            {
                int.TryParse(salaryText, out salary);
            }

            string statusId = ddlStatus?.SelectedValue ?? string.Empty;

            string uploadedFileName = null;
            string hashedFileName = null;
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            string[] allowedContentTypes = { "image/jpeg", "image/png" };
            int uploadedFileSize = 0;

            if (fileUploadImage != null && fileUploadImage.HasFile)
            {
                uploadedFileName = Path.GetFileName(fileUploadImage.FileName);
                uploadedFileSize = fileUploadImage.PostedFile.ContentLength;
                string uploadedFileExtention = Path.GetExtension(uploadedFileName).ToLower();
                string uploadedContentType = fileUploadImage.PostedFile.ContentType;

                if (!allowedExtensions.Contains(uploadedFileExtention) || !allowedContentTypes.Contains(uploadedContentType))
                {
                    lblFileUploadMessage.Text = "Only JPG, JPEG, and PNG files are allowed.";
                    return;
                }

                hashedFileName = GetHashedFileName(uploadedFileName);

                var saveDir = Server.MapPath("~/storage/photos/employees/");
                Directory.CreateDirectory(saveDir);
                var savePath = Path.Combine(saveDir, hashedFileName);
                fileUploadImage.SaveAs(savePath);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO users (username, password, account_status) VALUES (@Username, @Password, 'active'); SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Username", employeeUsername);
                    cmd.Parameters.AddWithValue("@Password", employeePassword);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        lblStatusMessage.Text = "Error creating user account.";
                        return;
                    }
                    int createdUserId = Convert.ToInt32(result);

                    cmd.Parameters.Clear();

                    query = "INSERT INTO personal_information " +
                            "(first_name, last_name, mother, father, phone, " +
                            "birthday, email, address, gender, national_number, salary, " +
                            "photo_url, role_id, user_id, department_id, employee_status_id) " +
                            "VALUES (@FirstName, @LastName, @Mother, @Father, @Phone, " +
                            "@Birthday, @Email, @Address, @Gender, @NationalNumber, @Salary, " +
                            "@PhotoUrl, @Role_id, @User_id, @Department_id, @Employee_status_id)";

                    cmd.CommandText = query;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? (object)DBNull.Value : firstName);
                    cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(lastName) ? (object)DBNull.Value : lastName);
                    cmd.Parameters.AddWithValue("@Mother", string.IsNullOrEmpty(motherName) ? (object)DBNull.Value : motherName);
                    cmd.Parameters.AddWithValue("@Father", string.IsNullOrEmpty(fatherName) ? (object)DBNull.Value : fatherName);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                    cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
                    cmd.Parameters.AddWithValue("@NationalNumber", string.IsNullOrEmpty(employeeIDNumber) ? (object)DBNull.Value : employeeIDNumber);

                    DateTime birthdayValue;
                    if (DateTime.TryParse(txtBithDate?.Text, out birthdayValue))
                        cmd.Parameters.AddWithValue("@Birthday", birthdayValue);
                    else
                        cmd.Parameters.AddWithValue("@Birthday", DBNull.Value);

                    if (salary > 0)
                        cmd.Parameters.AddWithValue("@Salary", salary);
                    else
                        cmd.Parameters.AddWithValue("@Salary", DBNull.Value);

                    if (!string.IsNullOrEmpty(hashedFileName))
                    {
                        string relativePhotoUrl = "/storage/photos/employees/" + hashedFileName;
                        cmd.Parameters.AddWithValue("@PhotoUrl", relativePhotoUrl);
                    }
                    else
                        cmd.Parameters.AddWithValue("@PhotoUrl", DBNull.Value);

                    int roleIdParsed;
                    if (int.TryParse(jobTitleId, out roleIdParsed) && roleIdParsed > 0)
                        cmd.Parameters.AddWithValue("@Role_id", roleIdParsed);
                    else
                        cmd.Parameters.AddWithValue("@Role_id", DBNull.Value);

                    cmd.Parameters.AddWithValue("@User_id", createdUserId);

                    int deptIdParsed;
                    if (int.TryParse(departmentId, out deptIdParsed) && deptIdParsed > 0)
                        cmd.Parameters.AddWithValue("@Department_id", deptIdParsed);
                    else
                        cmd.Parameters.AddWithValue("@Department_id", DBNull.Value);

                    int statusIdParsed;
                    if (int.TryParse(statusId, out statusIdParsed) && statusIdParsed > 0)
                        cmd.Parameters.AddWithValue("@Employee_status_id", statusIdParsed);
                    else
                        cmd.Parameters.AddWithValue("@Employee_status_id", DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        lblStatusMessage.Text = "Employee saved successfully.";
                    else
                        lblStatusMessage.Text = "Failed to save employee personal information.";
                }
                catch (Exception ex)
                {
                    lblStatusMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        private string GetHashedFileName(string originalFileName)
        {
            if (string.IsNullOrEmpty(originalFileName))
                return originalFileName;

            string extension = Path.GetExtension(originalFileName).ToLowerInvariant();
            string nameToHash = Path.GetFileNameWithoutExtension(originalFileName) + "|" + Guid.NewGuid().ToString("N") + "|" + DateTime.UtcNow.Ticks.ToString();

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