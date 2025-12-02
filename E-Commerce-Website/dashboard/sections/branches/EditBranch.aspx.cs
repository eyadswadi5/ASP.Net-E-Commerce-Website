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
    public partial class EditBranch : System.Web.UI.Page
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

            if (IsPostBack)
                return;

            int branchId = Convert.ToInt32(Request.QueryString["id"]);
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string query = "SELECT * FROM stores WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", branchId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    txtName.Text = Convert.ToString(reader["name"].ToString());
                    txtAddress.Text = Convert.ToString(reader["address"].ToString());
                    txtCity.Text = Convert.ToString(reader["city"].ToString());
                    txtPhone.Text = Convert.ToString(reader["phone"].ToString());
                    ddlManager.SelectedValue = Convert.ToString(reader["manager_id"].ToString());
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int branchId = Convert.ToInt32(Request.QueryString["id"]);

            string uploadedFileName = null;
            string hashedFileName = null;
            string filePath = null;
            string[] allowedExtensions = { ".pdf" };
            string[] allowedContentTypes = { "application/pdf" };

            if (fileUploadBrochure != null && fileUploadBrochure.HasFile)
            {
                uploadedFileName = Path.GetFileName(fileUploadBrochure.FileName);
                string uploadedFileExtention = Path.GetExtension(uploadedFileName).ToLower();
                string uploadedContentType = fileUploadBrochure.PostedFile.ContentType;

                if (!allowedExtensions.Contains(uploadedFileExtention) || !allowedContentTypes.Contains(uploadedContentType))
                {
                    Master.ShowAlert("Only PDF file are allowed.", "danger");
                    return;
                }

                hashedFileName = GetHashedFileName(uploadedFileName);

                var saveDir = Server.MapPath("~/storage/file/brochures/branches");
                Directory.CreateDirectory(saveDir);
                var savePath = Path.Combine(saveDir, hashedFileName);
                filePath = "/storage/file/brochures/branches/" + hashedFileName;
                fileUploadBrochure.SaveAs(savePath);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string query = @"UPDATE stores SET 
                name = @name, 
                address = @address, 
                city = @city, 
                phone = @phone, 
                manager_id = @manager_id";
            query = string.IsNullOrEmpty(filePath) ? query : query + ", file_url = @file_url";
            query += " WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@manager_id", ddlManager.SelectedValue);
                cmd.Parameters.AddWithValue("@id", branchId);
                if (!string.IsNullOrEmpty(filePath))
                    cmd.Parameters.AddWithValue("@file_url", filePath);

                conn.Open();
                int affected_rows = cmd.ExecuteNonQuery();

                if (affected_rows > 0)
                {
                    Master.StoreSessionAlert("Branch updated successfully", "success");
                    Response.Redirect("~/dashboard/sections/branches/Branches.aspx");
                }
                else
                    Master.ShowAlert("Error updating branch", "danger");
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