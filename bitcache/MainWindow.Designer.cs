namespace bitcache
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ExitButton = new Button();
            SignInButton = new Button();
            GraphResultsTextBox = new RichTextBox();
            progressBar = new ProgressBar();
            syncStatus = new Label();
            lastSync = new Label();
            numberOfKeys = new Label();
            manSync = new Button();
            groupSync = new GroupBox();
            groupSignIn = new GroupBox();
            tenantConfigButton = new Button();
            tenantConfig = new Label();
            signInUser = new Label();
            signinStatus = new Label();
            mainPane = new TabControl();
            tabPage1 = new TabPage();
            resultsPane = new DataGridView();
            tabPage2 = new TabPage();
            searchResultsPane = new DataGridView();
            searchFileld = new TextBox();
            searchButton = new Button();
            label2 = new Label();
            OS = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            RecoveryKey = new DataGridViewTextBoxColumn();
            KeyID = new DataGridViewTextBoxColumn();
            Hostname = new DataGridViewTextBoxColumn();
            label1 = new Label();
            btnExportToCSV = new Button();
            groupSync.SuspendLayout();
            groupSignIn.SuspendLayout();
            mainPane.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultsPane).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchResultsPane).BeginInit();
            SuspendLayout();
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Bottom;
            ExitButton.Location = new Point(16, 1096);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(112, 34);
            ExitButton.TabIndex = 3;
            ExitButton.Text = "E&xit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // SignInButton
            // 
            SignInButton.Anchor = AnchorStyles.Top;
            SignInButton.Location = new Point(18, 299);
            SignInButton.Name = "SignInButton";
            SignInButton.Size = new Size(113, 34);
            SignInButton.TabIndex = 0;
            SignInButton.Text = "Sign In";
            SignInButton.UseVisualStyleBackColor = true;
            SignInButton.Click += SignInButton_Click;
            // 
            // GraphResultsTextBox
            // 
            GraphResultsTextBox.Location = new Point(695, 932);
            GraphResultsTextBox.Name = "GraphResultsTextBox";
            GraphResultsTextBox.Size = new Size(396, 57);
            GraphResultsTextBox.TabIndex = 2;
            GraphResultsTextBox.Text = "";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(19, 213);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(224, 22);
            progressBar.TabIndex = 4;
            // 
            // syncStatus
            // 
            syncStatus.AutoSize = true;
            syncStatus.Location = new Point(19, 172);
            syncStatus.Name = "syncStatus";
            syncStatus.Size = new Size(120, 25);
            syncStatus.TabIndex = 5;
            syncStatus.Text = "Sync stopped";
            // 
            // lastSync
            // 
            lastSync.AutoSize = true;
            lastSync.Location = new Point(18, 131);
            lastSync.Name = "lastSync";
            lastSync.Size = new Size(174, 25);
            lastSync.TabIndex = 6;
            lastSync.Text = "Last Sync: checking...";
            // 
            // numberOfKeys
            // 
            numberOfKeys.AutoSize = true;
            numberOfKeys.Location = new Point(18, 92);
            numberOfKeys.Name = "numberOfKeys";
            numberOfKeys.Size = new Size(317, 25);
            numberOfKeys.TabIndex = 7;
            numberOfKeys.Text = "Number of keys in Bitcache: checking...";
            // 
            // manSync
            // 
            manSync.Location = new Point(19, 261);
            manSync.Name = "manSync";
            manSync.Size = new Size(112, 34);
            manSync.TabIndex = 8;
            manSync.Text = "Sync";
            manSync.UseVisualStyleBackColor = true;
            manSync.Click += manSync_Click;
            // 
            // groupSync
            // 
            groupSync.Controls.Add(manSync);
            groupSync.Controls.Add(numberOfKeys);
            groupSync.Controls.Add(lastSync);
            groupSync.Controls.Add(progressBar);
            groupSync.Controls.Add(syncStatus);
            groupSync.Location = new Point(16, 433);
            groupSync.Name = "groupSync";
            groupSync.Size = new Size(458, 384);
            groupSync.TabIndex = 9;
            groupSync.TabStop = false;
            groupSync.Text = "Synchronization";
            // 
            // groupSignIn
            // 
            groupSignIn.Controls.Add(tenantConfigButton);
            groupSignIn.Controls.Add(tenantConfig);
            groupSignIn.Controls.Add(signInUser);
            groupSignIn.Controls.Add(signinStatus);
            groupSignIn.Controls.Add(SignInButton);
            groupSignIn.Location = new Point(16, 27);
            groupSignIn.Name = "groupSignIn";
            groupSignIn.Size = new Size(458, 351);
            groupSignIn.TabIndex = 10;
            groupSignIn.TabStop = false;
            groupSignIn.Text = "Entra Sign-In Status";
            // 
            // tenantConfigButton
            // 
            tenantConfigButton.Location = new Point(19, 85);
            tenantConfigButton.Name = "tenantConfigButton";
            tenantConfigButton.Size = new Size(112, 34);
            tenantConfigButton.TabIndex = 7;
            tenantConfigButton.Text = "Configure";
            tenantConfigButton.UseVisualStyleBackColor = true;
            tenantConfigButton.Click += tenantConfigButton_Click;
            // 
            // tenantConfig
            // 
            tenantConfig.AutoSize = true;
            tenantConfig.Location = new Point(18, 46);
            tenantConfig.Name = "tenantConfig";
            tenantConfig.Size = new Size(174, 25);
            tenantConfig.TabIndex = 6;
            tenantConfig.Text = "Tenant config status:";
            // 
            // signInUser
            // 
            signInUser.AutoSize = true;
            signInUser.Location = new Point(18, 254);
            signInUser.Name = "signInUser";
            signInUser.Size = new Size(59, 25);
            signInUser.TabIndex = 5;
            signInUser.Text = "label1";
            signInUser.Visible = false;
            // 
            // signinStatus
            // 
            signinStatus.AutoSize = true;
            signinStatus.Location = new Point(17, 218);
            signinStatus.Name = "signinStatus";
            signinStatus.Size = new Size(153, 25);
            signinStatus.TabIndex = 4;
            signinStatus.Text = "No user signed-in";
            // 
            // mainPane
            // 
            mainPane.Controls.Add(tabPage1);
            mainPane.Controls.Add(tabPage2);
            mainPane.Location = new Point(505, 39);
            mainPane.Name = "mainPane";
            mainPane.SelectedIndex = 0;
            mainPane.Size = new Size(1208, 1091);
            mainPane.TabIndex = 12;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(resultsPane);
            tabPage1.Controls.Add(GraphResultsTextBox);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1200, 1053);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // resultsPane
            // 
            resultsPane.AllowUserToAddRows = false;
            resultsPane.AllowUserToDeleteRows = false;
            resultsPane.AllowUserToOrderColumns = true;
            resultsPane.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            resultsPane.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultsPane.Location = new Point(0, 0);
            resultsPane.Name = "resultsPane";
            resultsPane.ReadOnly = true;
            resultsPane.RowHeadersWidth = 62;
            resultsPane.Size = new Size(1200, 1053);
            resultsPane.TabIndex = 11;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(searchResultsPane);
            tabPage2.Controls.Add(searchFileld);
            tabPage2.Controls.Add(searchButton);
            tabPage2.Controls.Add(label2);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1200, 1053);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // searchResultsPane
            // 
            searchResultsPane.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchResultsPane.Location = new Point(27, 106);
            searchResultsPane.Name = "searchResultsPane";
            searchResultsPane.RowHeadersWidth = 62;
            searchResultsPane.Size = new Size(1029, 708);
            searchResultsPane.TabIndex = 3;
            // 
            // searchFileld
            // 
            searchFileld.Location = new Point(307, 42);
            searchFileld.Name = "searchFileld";
            searchFileld.Size = new Size(370, 31);
            searchFileld.TabIndex = 2;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(705, 44);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(112, 34);
            searchButton.TabIndex = 1;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 44);
            label2.Name = "label2";
            label2.Size = new Size(266, 25);
            label2.TabIndex = 0;
            label2.Text = "Search for computer in Bitcache:";
            // 
            // OS
            // 
            OS.HeaderText = "OS";
            OS.MinimumWidth = 8;
            OS.Name = "OS";
            OS.ReadOnly = true;
            OS.Width = 150;
            // 
            // Date
            // 
            Date.HeaderText = "Date";
            Date.MinimumWidth = 8;
            Date.Name = "Date";
            Date.ReadOnly = true;
            Date.Width = 150;
            // 
            // RecoveryKey
            // 
            RecoveryKey.HeaderText = "Recovery Key";
            RecoveryKey.MinimumWidth = 8;
            RecoveryKey.Name = "RecoveryKey";
            RecoveryKey.ReadOnly = true;
            RecoveryKey.Width = 150;
            // 
            // KeyID
            // 
            KeyID.HeaderText = "Key ID";
            KeyID.MinimumWidth = 8;
            KeyID.Name = "KeyID";
            KeyID.ReadOnly = true;
            KeyID.Width = 150;
            // 
            // Hostname
            // 
            Hostname.HeaderText = "Hostname";
            Hostname.MinimumWidth = 8;
            Hostname.Name = "Hostname";
            Hostname.ReadOnly = true;
            Hostname.Width = 150;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 6F);
            label1.Location = new Point(1510, 1133);
            label1.Name = "label1";
            label1.Size = new Size(204, 15);
            label1.TabIndex = 13;
            label1.Text = "Bitcache v0.1 © Pawel Lakomski 2025";
            // 
            // btnExportToCSV
            // 
            btnExportToCSV.Location = new Point(335, 1096);
            btnExportToCSV.Name = "btnExportToCSV";
            btnExportToCSV.Size = new Size(139, 34);
            btnExportToCSV.TabIndex = 14;
            btnExportToCSV.Text = "Export to CSV";
            btnExportToCSV.UseVisualStyleBackColor = true;
            btnExportToCSV.Click += btnExportToCSV_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = ExitButton;
            ClientSize = new Size(1725, 1152);
            Controls.Add(btnExportToCSV);
            Controls.Add(label1);
            Controls.Add(mainPane);
            Controls.Add(ExitButton);
            Controls.Add(groupSignIn);
            Controls.Add(groupSync);
            MaximizeBox = false;
            MinimumSize = new Size(800, 500);
            Name = "MainWindow";
            Text = "Bitcache 0.1";
            groupSync.ResumeLayout(false);
            groupSync.PerformLayout();
            groupSignIn.ResumeLayout(false);
            groupSignIn.PerformLayout();
            mainPane.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)resultsPane).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)searchResultsPane).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button ExitButton;
        private Button SignInButton;
        private RichTextBox GraphResultsTextBox;
        private ProgressBar progressBar;
        private Label syncStatus;
        private Label lastSync;
        private Label numberOfKeys;
        private Button manSync;
        private GroupBox groupSync;
        private GroupBox groupSignIn;
        private Label signinStatus;
        private TabControl mainPane;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private DataGridView resultsPane;
        private DataGridViewTextBoxColumn OS;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn RecoveryKey;
        private DataGridViewTextBoxColumn KeyID;
        private DataGridViewTextBoxColumn Hostname;
        private Label signInUser;
        private Button tenantConfigButton;
        private Label tenantConfig;
        private Label label1;
        private TextBox searchFileld;
        private Button searchButton;
        private Label label2;
        private DataGridView searchResultsPane;
        private Button btnExportToCSV;
    }
}