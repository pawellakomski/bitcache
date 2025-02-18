using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bitcache
{
    public partial class tenantConfigOK : Form
    {
        public tenantConfigOK()
        {
            InitializeComponent();

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";
            string sqlQueryTenant = "SELECT bitcacheTenantId FROM bitcachemeta";
            SqlConnection conTenant = new SqlConnection(connectionString);
            conTenant.Open();
            SqlCommand scTenant = new SqlCommand(sqlQueryTenant, conTenant);
            object queryResultTenant = scTenant.ExecuteScalar();
            string sqlQueryClient = "SELECT bitcacheClientId FROM bitcachemeta";
            SqlCommand scClient = new SqlCommand(sqlQueryClient, conTenant);
            object queryResultClient = scClient.ExecuteScalar();
            conTenant.Close();

            if (queryResultTenant != null)
            {
                tenantConfigTenantBox.Text = queryResultTenant.ToString();
            }

            if (queryResultClient != null)
            {
                tenantConfigClientBox.Text = queryResultClient.ToString();
            }
        }

        private void clearDB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to execute this command?",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.OK)
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";
                SqlConnection conDBClear = new SqlConnection(connectionString);
                conDBClear.Open();
                string sqlQueryDBClear = "DELETE FROM dbo.bitcachekeys";
                SqlCommand scDBClear = new SqlCommand(sqlQueryDBClear, conDBClear);
                scDBClear.ExecuteNonQuery();
                conDBClear.Close();
            }

        }

        private void confirmConfig_Click(object sender, EventArgs e)
        {
            string newTenant = tenantConfigTenantBox.Text;
            string newClient = tenantConfigClientBox.Text;
            string pattern = "^[a-f0-9]{8}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{12}$";

            if (!(Regex.IsMatch(newTenant, pattern)) || !(Regex.IsMatch(newClient, pattern)))
            {
                MessageBox.Show("Please enter a valid Tenant ID and Client ID");
                return;
            }

            else
            {
                string sqlQueryTenantUpdate = "";
                string sqlQueryClientUpdate = "";

                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";

                SqlConnection conTenant = new SqlConnection(connectionString);
                conTenant.Open();
                string sqlQueryTenant = "SELECT COUNT(*) FROM bitcachemeta";
                SqlCommand scTenant = new SqlCommand(sqlQueryTenant, conTenant);
                object queryResultTenant = scTenant.ExecuteScalar();

                if (queryResultTenant.ToString() != "0")
                {
                    sqlQueryTenantUpdate = "UPDATE bitcachemeta SET bitcacheTenantId = '" + newTenant + "'";
                }
                else
                {
                    sqlQueryTenantUpdate = "INSERT INTO bitcachemeta (bitcacheTenantId) VALUES ('" + newTenant + "')";
                }

                sqlQueryClientUpdate = "UPDATE bitcachemeta SET bitcacheClientId = '" + newClient + "'";

                SqlCommand scTenantUpdate = new SqlCommand(sqlQueryTenantUpdate, conTenant);
                scTenantUpdate.ExecuteNonQuery();
                SqlCommand scClientUpdate = new SqlCommand(sqlQueryClientUpdate, conTenant);
                scClientUpdate.ExecuteNonQuery();
                conTenant.Close();

                MainWindow MainWindow = new MainWindow();
                MainWindow.UpdateTenantLabel("Tenant config status: Configured");
                MainWindow.UpdateTenantConfigState(true);

                this.Close();
            }


        }

        private void tenantConfigCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=your_server;User Id=your_user;Password=your_password;"; // Replace with your actual credentials

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name FROM sys.databases WHERE name = 'pawel';";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            Console.WriteLine("Database exists.");
                        else
                            Console.WriteLine("Database does not exist.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }


        }
    }
}
