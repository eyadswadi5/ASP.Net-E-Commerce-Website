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

namespace E_Commerce_Website.dashboard.sections.branches
{
    public partial class AddBranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Department"].ToString().ToLower() == "branches")
            {
                if (Session["Role"].ToString().ToLower() != "manager" && Session["Role"].ToString().ToLower() != "employee")
                {
                    Master.StoreSessionAlert("You'r not permitted to access this feature.", "danger");
                    Response.Redirect("~/dashboard/sections/branches/Branches.aspx");
                }
            }
            else if (Session["Role"].ToString().ToLower() != "owner")
            {
                Master.StoreSessionAlert("You'r not permitted to access this feature.", "danger");
                Response.Redirect("~/dashboard/sections/branches/Branches.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string branchName = txtName?.Text.Trim() ?? string.Empty;
            string city = txtCity?.Text.Trim() ?? string.Empty;
            string address = txtAddress?.Text.Trim() ?? string.Empty;
            string phoneNumber = txtPhone?.Text.Trim() ?? string.Empty;
            int managerId = Convert.ToInt32(ddlManager?.SelectedValue);            

            string uploadedFileName = null;
            string hashedFileName = null;
            string[] allowedExtensions = { ".pdf"};
            string[] allowedContentTypes = { "application/pdf" };
            int uploadedFileSize = 0;

            if (fileUploadBrochure != null && fileUploadBrochure.HasFile)
            {
                uploadedFileName = Path.GetFileName(fileUploadBrochure.FileName);
                uploadedFileSize = fileUploadBrochure.PostedFile.ContentLength;
                string uploadedFileExtention = Path.GetExtension(uploadedFileName).ToLower();
                string uploadedContentType = fileUploadBrochure.PostedFile.ContentType;

                if (!allowedExtensions.Contains(uploadedFileExtention) || !allowedContentTypes.Contains(uploadedContentType))
                {
                    lblFileUploadMessage.Text = "Only PDF file are allowed.";
                    return;
                }

                hashedFileName = GetHashedFileName(uploadedFileName);

                var saveDir = Server.MapPath("~/storage/file/brochures/branches");
                Directory.CreateDirectory(saveDir);
                var savePath = Path.Combine(saveDir, hashedFileName);
                fileUploadBrochure.SaveAs(savePath);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) as num FROM personal_information JOIN roles ON personal_information.role_id = roles.id WHERE personal_information.id = @Manager_ID AND roles.type = 'manager'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Manager_ID", managerId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Master.ShowAlert("Error: The provided id didn't belongs to a manager.", "warning");
                    return;
                }

                reader.Close();

                query = "INSERT INTO stores VALUES (@Name, @City, @Address, @Phone, @File_url, @Manager_ID);";
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@Name", branchName);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                
                string filePath = string.Empty;
                if (!string.IsNullOrEmpty(hashedFileName))
                {
                    filePath = "/storage/files/brochures/branches/" + hashedFileName;
                }
                cmd.Parameters.AddWithValue("@File_url", filePath);

                int created = cmd.ExecuteNonQuery();

                if (created > 0)
                {
                    Master.StoreSessionAlert("Store created successfully!", "success");
                    Response.Redirect("~/dashboard/sections/branches/Branches.aspx");
                } else
                    Master.StoreSessionAlert("Error Creating store branch", "danger");
                return;
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