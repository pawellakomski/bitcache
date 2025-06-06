using Microsoft.Identity.Client;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.DirectoryServices.ActiveDirectory;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Drawing;
using System.Data;
using System.Text;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace bitcache
{

    public partial class MainWindow : Form
    {
        private readonly HttpClient _httpClient = new();
        private bool signedIn = false;
        private bool tenantConfigured = false;
        private bool dbconfigured = false;
        private IPublicClientApplication msalPublicClientApp;
        private AuthenticationResult? msalAuthenticationResult = null;
        private string TenantId;
        private string ClientId;


        public MainWindow()
        {

            InitializeComponent();

            System.Windows.Forms.ToolTip tooltipSearch = new System.Windows.Forms.ToolTip();
            tooltipSearch.SetToolTip(searchButton, "You can use part of computer names in your search.");
            string connectionString = "Data Source=.\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=True; MultipleActiveResultSets = true";

            //checking if the DB exists, if not create it
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name FROM sys.databases WHERE name = 'bitcache';";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Database exists.");
                            dbconfigured = true;
                            conn.Close();
                        }

                        else
                        {
                            MessageBox.Show("The DB for Bitcache does not exist. Creating it now...", "Bitcache");
                            string createDB = "CREATE DATABASE bitcache;";

                            using (SqlCommand cmdCreateDB = new SqlCommand(createDB, conn))
                            {
                                cmdCreateDB.ExecuteNonQuery();
                            }

                            conn.Close();

                            string connectionStringTable = "Data Source=.\\SQLEXPRESS;Database=bitcache;Integrated Security=True;TrustServerCertificate=True; MultipleActiveResultSets = true";
                            string createTableSqlKeys = "CREATE TABLE [dbo].[bitcachekeys]([bitcacheId] [int] IDENTITY(1,1) NOT NULL,[bitcacheHostname] [varchar](50) NOT NULL,[bitcacheKeyId] [varchar](100) NOT NULL,[bitcacheKeyContent] [varchar](100) NOT NULL,[bitcacheKeyDate] [varchar](50) NOT NULL,[bitcacheOS] [varchar](100) NOT NULL);CREATE TABLE bitcachemeta([bitcacheTenantId] [varchar](100) NULL,[bitcacheClientId] [varchar](100) NULL,[bitcacheSyncTime] [varchar](20) NULL)";


                            using (SqlConnection connTable = new SqlConnection(connectionStringTable))
                            {
                                connTable.Open();
                                using (SqlCommand cmdTable = new SqlCommand(createTableSqlKeys, connTable))
                                {
                                    cmdTable.ExecuteNonQuery();
                                }
                                connTable.Close();
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    conn.Close();
                    //MessageBox.Show("No connection to SQL server. Is SQL Express installed and running?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("No connection to SQL server. Is SQL Express installed and running?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Environment.Exit(1); // Exit with an error code
                }
                conn.Close();
            }

            string connectionStringTenantConfig = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";
            string sqlQueryTenantConfig = "SELECT bitcacheTenantId FROM bitcachemeta";
            SqlConnection conTenantConfig = new SqlConnection(connectionStringTenantConfig);
            conTenantConfig.Open();
            SqlCommand scTenantConfig = new SqlCommand(sqlQueryTenantConfig, conTenantConfig);
            object queryResultTenantConfig = scTenantConfig.ExecuteScalar();
            string sqlQueryClientConfig = "SELECT bitcacheClientId FROM bitcachemeta";
            SqlCommand scClientConfig = new SqlCommand(sqlQueryClientConfig, conTenantConfig);
            object queryResultClientConfig = scClientConfig.ExecuteScalar();
            conTenantConfig.Close();

            if (queryResultTenantConfig != null)
            {
                tenantConfigured = true;
            }


            mainPane.TabPages[0].Text = "Saved keys";
            mainPane.TabPages[1].Text = "Search";

            //Checking last sync status and number of keys in the db

            connectionString = "Data Source=.\\SQLEXPRESS;Database=bitcache;Integrated Security=True;TrustServerCertificate=True; MultipleActiveResultSets = true";

            string sqlQueryLastSync = "SELECT bitcacheSyncTime FROM bitcachemeta";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand scLastSync = new SqlCommand(sqlQueryLastSync, con);
            object queryResultLastSync = scLastSync.ExecuteScalar();

            string sqlQueryNumberKeys = "SELECT COUNT(*) FROM bitcachekeys";
            SqlCommand scNumberKeys = new SqlCommand(sqlQueryNumberKeys, con);
            object queryResultNumberKeys = scNumberKeys.ExecuteScalar();
            con.Close();

            numberOfKeys.Text = "Number of keys in Bitcache: " + queryResultNumberKeys.ToString();
            if (queryResultLastSync == null || queryResultLastSync.ToString() == "")
            {
                lastSync.Text = "Last sync: Never";
            }
            else
            {
                lastSync.Text = "Last sync: " + queryResultLastSync.ToString();
            }

            string sqlQueryAllKeys = "SELECT * FROM bitcachekeys ORDER BY bitcacheHostname";
            System.Data.DataTable dataTable = new System.Data.DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQueryAllKeys, connection))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
                resultsPane.DataSource = dataTable;
                resultsPane.Columns[0].HeaderText = "No.";
                resultsPane.Columns[1].HeaderText = "Hostname";
                resultsPane.Columns[2].HeaderText = "Key ID";
                resultsPane.Columns[3].HeaderText = "Recovery Key";
                resultsPane.Columns[4].HeaderText = "Key Stored On";
                resultsPane.Columns[5].HeaderText = "Operating System";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            if (tenantConfigured)
            {

                string connectionStringTenantConfigCheck = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";

                string sqlQueryTenantConfigCheck = "SELECT bitcacheTenantId FROM bitcachemeta";
                SqlConnection conTenantConfigCheck = new SqlConnection(connectionStringTenantConfigCheck);
                conTenantConfigCheck.Open();
                SqlCommand scTenantConfigCheck = new SqlCommand(sqlQueryTenantConfigCheck, conTenantConfigCheck);
                object queryResultTenantConfigCheck = scTenantConfigCheck.ExecuteScalar();

                string sqlQueryClientConfigCheck = "SELECT bitcacheClientId FROM bitcachemeta";
                SqlCommand scClientConfigCheck = new SqlCommand(sqlQueryClientConfigCheck, conTenantConfigCheck);
                object queryResultClientConfigCheck = scClientConfigCheck.ExecuteScalar();
                conTenantConfigCheck.Close();

                tenantConfigTenantBox.Text = queryResultTenantConfigCheck.ToString();
                tenantConfigClientBox.Text = queryResultClientConfigCheck.ToString();

                msalPublicClientApp = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(new PublicClientApplicationOptions
                {
                    TenantId = queryResultTenantConfigCheck.ToString(),
                    ClientId = queryResultClientConfigCheck.ToString()
                })
                .WithDefaultRedirectUri() // http://localhost
                .Build();
            }


        }

        public async void SignInButton_Click(object sender, EventArgs e)
        {

            string connectionStringTenantConfigCheck = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";

            string sqlQueryTenantConfigCheck = "SELECT bitcacheTenantId FROM bitcachemeta";
            SqlConnection conTenantConfigCheck = new SqlConnection(connectionStringTenantConfigCheck);
            conTenantConfigCheck.Open();
            SqlCommand scTenantConfigCheck = new SqlCommand(sqlQueryTenantConfigCheck, conTenantConfigCheck);
            object queryResultTenantConfigCheck = scTenantConfigCheck.ExecuteScalar();

            string sqlQueryClientConfigCheck = "SELECT bitcacheClientId FROM bitcachemeta";
            SqlCommand scClientConfigCheck = new SqlCommand(sqlQueryClientConfigCheck, conTenantConfigCheck);
            object queryResultClientConfigCheck = scClientConfigCheck.ExecuteScalar();
            conTenantConfigCheck.Close();

            tenantConfigTenantBox.Text = queryResultTenantConfigCheck.ToString();
            tenantConfigClientBox.Text = queryResultClientConfigCheck.ToString();

            msalPublicClientApp = PublicClientApplicationBuilder
            .CreateWithApplicationOptions(new PublicClientApplicationOptions
            {
                TenantId = queryResultTenantConfigCheck.ToString(),
                ClientId = queryResultClientConfigCheck.ToString()
            })
            .WithDefaultRedirectUri() // http://localhost
            .Build();

            if (tenantConfigured == false)
            {
                MessageBox.Show("You need to configure the tenant first");
                return;
            }

            else

            {

                if (signedIn == false)
                {



                    // Acquire a cached access token for Microsoft Graph if one is available from a prior
                    // execution of this authentication flow.


                    var accounts = await msalPublicClientApp.GetAccountsAsync();
                    if (accounts.Any())
                    {
                        try
                        {
                            // Will return a cached access token if available, refreshing if necessary.
                            msalAuthenticationResult = await msalPublicClientApp.AcquireTokenSilent(
                                new[] { "https://graph.microsoft.com/BitlockerKey.ReadBasic.All", "https://graph.microsoft.com/BitlockerKey.Read.All", "https://graph.microsoft.com/Device.Read.All" },
                                accounts.First())
                                .ExecuteAsync();
                        }
                        catch (MsalUiRequiredException)
                        {
                            // Nothing in cache for this account + scope, and interactive experience required.
                        }
                    }

                    if (msalAuthenticationResult == null)
                    {
                        // This is likely the first authentication request since application start or after
                        // Sign Out was clicked, so calling this will launch the user's default browser and
                        // send them through a login flow. After the flow is complete, the rest of this method
                        // will continue to execute.
                        msalAuthenticationResult = await msalPublicClientApp.AcquireTokenInteractive(
                            new[] { "https://graph.microsoft.com/BitlockerKey.Read.All", "https://graph.microsoft.com/BitlockerKey.ReadBasic.All", "https://graph.microsoft.com/Device.Read.All" })
                            .ExecuteAsync();
                    }

                    //Screen management
                    SignInButton.Text = "Sign Out";
                    signedIn = true;
                    signinStatus.Text = "Signed in as:";
                    signInUser.Visible = true;
                    signInUser.Text = msalAuthenticationResult.Account.Username;



                }

                else
                {
                    // Signing out is removing all cached tokens, meaning the next token request will
                    // require the user to sign in.
                    foreach (var account in (await msalPublicClientApp.GetAccountsAsync()).ToList())
                    {
                        await msalPublicClientApp.RemoveAsync(account);
                    }

                    // Show the call to action and hide the results.
                    signedIn = false;
                    SignInButton.Text = "Sign In";
                    signinStatus.Text = "No user signed-in";
                    signInUser.Visible = false;
                }
            }
        }

        public void UpdateTenantConfigState(bool configured)
        {
            tenantConfigured = configured;
        }

        public async void Sync(string token)
        {

            //setup
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";
            syncStatus.Text = "Sync started. Getting info...";

            // Getting key ids existing in the DB to the list to compare it with the key ids in the cloud
            List<string> bitcacheKeyIds = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT bitcacheKeyId FROM bitcachekeys";
                SqlCommand sc = new SqlCommand(sqlQuery, con);
                con.Open();

                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string keyId = reader["bitcacheKeyId"].ToString();
                        bitcacheKeyIds.Add(keyId);
                        GraphResultsTextBox.Text += "Bitcachekey: " + keyId;
                        GraphResultsTextBox.Text += "\n";
                    }
                }
            }

            //Caling MS Graph API

            using var graphRequest = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/informationProtection/bitlocker/recoveryKeys");
            graphRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            graphRequest.Headers.UserAgent.ParseAdd("Bitcache/0.1 (Windows)");
            var graphResponseMessage = await _httpClient.SendAsync(graphRequest);
            graphResponseMessage.EnsureSuccessStatusCode();
            using var graphResponseJson = JsonDocument.Parse(await graphResponseMessage.Content.ReadAsStreamAsync());
            var serializedJSON = System.Text.Json.JsonSerializer.Serialize(graphResponseJson, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            JObject jsonObject = JObject.Parse(serializedJSON);

            List<string> entraKeyIds = new List<string>();

            foreach (var item in jsonObject["value"])
            {
                string id = item["id"].ToString();
                entraKeyIds.Add(id);
                GraphResultsTextBox.Text += "Entra key: " + id;
                GraphResultsTextBox.Text += "\n";
            }

            //Now we have two lists, one with key ids from Bitcache and the second with key ids from Entra. We will compare them and if there is a key in Entra that is not in Bitcache, we will add it to the DB.

            List<string> filteredIKeyIds = entraKeyIds.Except(bitcacheKeyIds).ToList();

            //the filtered list will be used to update the DB with the new keys
            //number of elements to be used in the progress bar
            progressBar.Maximum = filteredIKeyIds.Count;
            progressBar.Value = 0;

            foreach (var item in jsonObject["value"])
            {

                if (!filteredIKeyIds.Contains(item["id"].ToString()))
                {
                    continue;
                }

                var URL = "https://graph.microsoft.com/v1.0/informationProtection/bitlocker/recoveryKeys/" + item["id"].ToString() + "?$select=key";
                using var graphRequestForKey = new HttpRequestMessage(HttpMethod.Get, URL);
                graphRequestForKey.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                graphRequestForKey.Headers.UserAgent.ParseAdd("Bitcache/0.1 (Windows)");
                var graphResponseMessageForKey = await _httpClient.SendAsync(graphRequestForKey);
                graphResponseMessageForKey.EnsureSuccessStatusCode();
                using var graphResponseJsonForKey = JsonDocument.Parse(await graphResponseMessageForKey.Content.ReadAsStreamAsync());
                var serializedJSONforKey = System.Text.Json.JsonSerializer.Serialize(graphResponseJsonForKey, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

                RecoveryKey recoveryKey = JsonConvert.DeserializeObject<RecoveryKey>(serializedJSONforKey);
                List<RecoveryKey> keyList = new List<RecoveryKey> { recoveryKey };

                foreach (var key in keyList)
                {
                    DateTime dateTime = DateTime.Parse(key.CreatedDateTime);
                    string formattedDate = dateTime.ToString("dd-MM-yy");
                    var URLforComputer = "https://graph.microsoft.com/v1.0/devices(deviceId='" + key.DeviceId + "')?$select=displayName,operatingSystem,operatingSystemVersion";
                    using var graphRequestForComputer = new HttpRequestMessage(HttpMethod.Get, URLforComputer);

                    graphRequestForComputer.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    graphRequestForComputer.Headers.UserAgent.ParseAdd("Bitcache/0.1 (Windows)");
                    var graphResponseMessageForComputer = await _httpClient.SendAsync(graphRequestForComputer);
                    graphResponseMessageForComputer.EnsureSuccessStatusCode();
                    using var graphResponseJsonForComputer = JsonDocument.Parse(await graphResponseMessageForComputer.Content.ReadAsStreamAsync());
                    var serializedJSONforComputer = System.Text.Json.JsonSerializer.Serialize(graphResponseJsonForComputer, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });


                    Computer computer = JsonConvert.DeserializeObject<Computer>(serializedJSONforComputer);
                    List<Computer> computerList = new List<Computer> { computer };

                    foreach (var comp in computerList)
                    {

                        progressBar.Value++;
                        syncStatus.Text = "Syncing... " + progressBar.Value + " of " + progressBar.Maximum + " keys.";

                        string connectionString1 = "Data Source=.\\SQLEXPRESS;Database=bitcache;Integrated Security=True;TrustServerCertificate=True; MultipleActiveResultSets = true";

                        SqlConnection conKeys = new SqlConnection(connectionString1);
                        string sqlQuery = "INSERT INTO bitcachekeys (bitcacheHostname, bitcacheKeyId, bitcacheKeyContent, bitcacheKeyDate, bitcacheOS) VALUES (@bitcacheHostname, @bitcacheKeyId, @bitcacheKeyContent, @bitcacheKeyDate, @bitcacheOS)";

                        conKeys.Open();
                        SqlCommand sc = new SqlCommand(sqlQuery, conKeys);

                        var bitcacheHostname = new SqlParameter("bitcacheHostname", System.Data.SqlDbType.VarChar);
                        bitcacheHostname.Value = comp.displayName;
                        sc.Parameters.Add(bitcacheHostname);

                        var bitcacheKeyId = new SqlParameter("bitcacheKeyId", System.Data.SqlDbType.VarChar);
                        bitcacheKeyId.Value = key.Id;
                        sc.Parameters.Add(bitcacheKeyId);

                        var bitcacheKeyContent = new SqlParameter("bitcacheKeyContent", System.Data.SqlDbType.VarChar);
                        bitcacheKeyContent.Value = key.Key;
                        sc.Parameters.Add(bitcacheKeyContent);

                        var bitcacheKeyDate = new SqlParameter("bitcacheKeyDate", System.Data.SqlDbType.VarChar);
                        bitcacheKeyDate.Value = formattedDate;
                        sc.Parameters.Add(bitcacheKeyDate);

                        var bitcacheOS = new SqlParameter("bitcacheOS", System.Data.SqlDbType.VarChar);
                        bitcacheOS.Value = comp.operatingSystem + " " + comp.operatingSystemVersion;
                        sc.Parameters.Add(bitcacheOS);

                        sc.ExecuteNonQuery();
                        conKeys.Close();

                        string sqlQueryAllKeys = "SELECT * FROM bitcachekeys ORDER BY bitcacheHostname";
                        System.Data.DataTable dataTable = new System.Data.DataTable();

                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQueryAllKeys, connection))
                                {
                                    dataAdapter.Fill(dataTable);
                                }
                            }
                            resultsPane.DataSource = dataTable;
                            resultsPane.Columns[0].HeaderText = "No.";
                            resultsPane.Columns[1].HeaderText = "Hostname";
                            resultsPane.Columns[2].HeaderText = "Key ID";
                            resultsPane.Columns[3].HeaderText = "Recovery Key";
                            resultsPane.Columns[4].HeaderText = "Key Stored On";
                            resultsPane.Columns[5].HeaderText = "Operating System";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }

                    }
                }


            }

            progressBar.Value = 0;
            syncStatus.Text = "Sync completed!";

            connectionString = "Data Source=.\\SQLEXPRESS;Database=bitcache;Integrated Security=True;TrustServerCertificate=True; MultipleActiveResultSets = true";
            string sqlQueryNumberKeys = "SELECT COUNT(*) FROM bitcachekeys";
            SqlConnection conSyncFinished = new SqlConnection(connectionString);
            conSyncFinished.Open();
            SqlCommand scNumberKeys = new SqlCommand(sqlQueryNumberKeys, conSyncFinished);
            object queryResultLastSync = scNumberKeys.ExecuteScalar();

            DateTime now = DateTime.Now;
            string nowFormatted = now.ToString("dd-MM-yyyy HH:mm");
            string sqlQueryInsertTime = "UPDATE bitcachemeta SET bitcacheSyncTime='" + nowFormatted + "'";

            SqlCommand scLastSync = new SqlCommand(sqlQueryInsertTime, conSyncFinished);
            object queryResultNumberKeys = scLastSync.ExecuteScalar();
            conSyncFinished.Close();

            lastSync.Text = "Last sync: " + nowFormatted;


        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void manSync_Click(object sender, EventArgs e)
        {
            if (signedIn == true)
            {
                Sync(msalAuthenticationResult.AccessToken);
            }
            else
            {
                MessageBox.Show("You need to sign in first");
                return;
            }


        }

        private void tenantConfigButton_Click(object sender, EventArgs e)
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

                string connectionStringDB = "Data Source=.\\SQLEXPRESS;Database=bitcache;Integrated Security=True;TrustServerCertificate=True; MultipleActiveResultSets = true";
                string sqlQueryTenantDelete = "DELETE FROM bitcachemeta;";
                string sqlQueryTenantUpdate = "INSERT INTO bitcachemeta (bitcacheTenantId, bitcacheClientId) VALUES ('" + newTenant + "', '" + newClient + "')";



                using (SqlConnection connTable = new SqlConnection(connectionStringDB))
                {
                    connTable.Open();
                    using (SqlCommand cmdTable = new SqlCommand(sqlQueryTenantDelete, connTable))
                    {
                        cmdTable.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdTable = new SqlCommand(sqlQueryTenantUpdate, connTable))
                    {
                        cmdTable.ExecuteNonQuery();
                    }
                    connTable.Close();
                }

                tenantConfigured = true;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchString = searchFileld.Text;
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bitcache;Integrated Security=True; TrustServerCertificate=True";

            string sqlQuery = "SELECT * FROM bitcachekeys WHERE bitcacheHostname LIKE @bitcacheHostname";
            System.Data.DataTable dataTableSearch = new System.Data.DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Add the parameter with the user input, including wildcard for LIKE clause
                        command.Parameters.Add(new SqlParameter("@bitcacheHostname", System.Data.SqlDbType.VarChar));
                        command.Parameters["@bitcacheHostname"].Value = $"%{searchString}%";

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            dataAdapter.Fill(dataTableSearch);
                        }
                    }
                }

                searchResultsPane.DataSource = dataTableSearch;
                searchResultsPane.Columns[0].HeaderText = "No.";
                searchResultsPane.Columns[1].HeaderText = "Hostname";
                searchResultsPane.Columns[2].HeaderText = "Key ID";
                searchResultsPane.Columns[3].HeaderText = "Recovery Key";
                searchResultsPane.Columns[4].HeaderText = "Key Stored On";
                searchResultsPane.Columns[5].HeaderText = "Operating System";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            if (resultsPane.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "keys.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(sfd.FileName))
                    {
                        try
                        {
                            System.IO.File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Problem with writing the data to disk" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = resultsPane.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[resultsPane.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += resultsPane.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < resultsPane.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += resultsPane.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            System.IO.File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data exported successfully", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No data to export", "Info");
            }
        }

        private void tenantConfig_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to remove all keys from Bitcache DB? The keys in your Entra ID tenant will remain intact but you will lose all keys in the local database.",
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

                string sqlQueryAllKeys = "SELECT * FROM bitcachekeys ORDER BY bitcacheHostname";
                System.Data.DataTable dataTable = new System.Data.DataTable();

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQueryAllKeys, connection))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                    resultsPane.DataSource = dataTable;
                    resultsPane.Columns[0].HeaderText = "No.";
                    resultsPane.Columns[1].HeaderText = "Hostname";
                    resultsPane.Columns[2].HeaderText = "Key ID";
                    resultsPane.Columns[3].HeaderText = "Recovery Key";
                    resultsPane.Columns[4].HeaderText = "Key Stored On";
                    resultsPane.Columns[5].HeaderText = "Operating System";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }

    public class RecoveryKey
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDateTime")]
        public string CreatedDateTime { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class Computer
    {
        [JsonProperty("displayName")]
        public string displayName { get; set; }

        [JsonProperty("operatingSystem")]
        public string operatingSystem { get; set; }

        [JsonProperty("operatingSystemVersion")]
        public string operatingSystemVersion { get; set; }
    }

    



    }
