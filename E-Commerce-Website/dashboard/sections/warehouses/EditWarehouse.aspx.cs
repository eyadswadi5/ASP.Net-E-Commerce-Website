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

namespace E_Commerce_Website.dashboard.sections.warehouses
{
    public partial class EditWarehouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            
            int warehouseId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string query = @"SELECT wh.[name]
              ,wh.[city]
              ,wh.[address]
              ,wh.[phone]
              ,wh.[manager_id]
              FROM [STORE_DB].[dbo].[warehouses] as wh WHERE wh.id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", warehouseId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["name"].ToString();
                        txtCity.Text = reader["city"].ToString();
                        txtAddress.Text = reader["address"].ToString();
                        txtPhone.Text = reader["phone"].ToString();
                        ddlManager.SelectedValue = reader["manager_id"].ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int warehouseId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;

            string uploadedFileName = string.Empty;
            string hashedFileName = string.Empty;
            string filePath = string.Empty;
            string[] allowedExt = { ".pdf" };
            string[] allowedContType = { "application/pdf" };
            
            if (fileUploadBrochure != null && fileUploadBrochure.HasFile)
            {
                uploadedFileName = Path.GetFileName(fileUploadBrochure.FileName);
                string uploadedFileExt = Path.GetExtension(uploadedFileName).ToLower();
                string uploadedFileContType = fileUploadBrochure.PostedFile.ContentType;

                if (!allowedExt.Contains(uploadedFileExt) || !allowedContType.Contains(uploadedFileContType))
                {
                    Master.ShowAlert("Only PDF files are allowed.", "danger");
                    return;
                }

                hashedFileName = GetHashedFileName(uploadedFileName);
                var saveDir = Server.MapPath("~/storage/file/brochures/warehouses");
                Directory.CreateDirectory(saveDir);
                var savePath = Path.Combine(saveDir, hashedFileName);
                filePath = "/storage/file/brochures/warehouses/" + hashedFileName;
                fileUploadBrochure.SaveAs(savePath);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string query = "UPDATE warehouses SET " +
                "name = @name, city = @city, address = @address, phone = @phone, manager_id = @manager_id";

            query = !string.IsNullOrEmpty(hashedFileName) ? query + ", file_url = @file_path" : query;
            query = query + " WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@manager_id", Convert.ToInt32(ddlManager.SelectedValue));
                cmd.Parameters.AddWithValue("@id", warehouseId);

                if (!string.IsNullOrEmpty(hashedFileName))
                    cmd.Parameters.AddWithValue("@file_path", filePath);

                conn.Open();
                int affected_rows = cmd.ExecuteNonQuery();

                if (affected_rows > 0)
                {
                    Master.StoreSessionAlert("Warehouse updated successfully.", "success");
                    Response.Redirect("~/dashboard/sections/warehouses/Warehouses.aspx");
                }
                else
                    Master.ShowAlert("No changes were made to the warehouse.", "warning");
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