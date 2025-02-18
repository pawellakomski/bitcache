namespace bitcache
{
    partial class tenantConfigOK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tenantConfigTenantID = new Label();
            tenantConfigClientID = new Label();
            tenantConfigTenantBox = new TextBox();
            tenantConfigClientBox = new TextBox();
            confirmConfig = new Button();
            tenantConfigCancel = new Button();
            clearDB = new Button();
            SuspendLayout();
            // 
            // tenantConfigTenantID
            // 
            tenantConfigTenantID.AutoSize = true;
            tenantConfigTenantID.Location = new Point(66, 27);
            tenantConfigTenantID.Name = "tenantConfigTenantID";
            tenantConfigTenantID.Size = new Size(248, 25);
            tenantConfigTenantID.TabIndex = 0;
            tenantConfigTenantID.Text = "Tenant ID currenly configured:";
            // 
            // tenantConfigClientID
            // 
            tenantConfigClientID.AutoSize = true;
            tenantConfigClientID.Location = new Point(73, 149);
            tenantConfigClientID.Name = "tenantConfigClientID";
            tenantConfigClientID.Size = new Size(241, 25);
            tenantConfigClientID.TabIndex = 1;
            tenantConfigClientID.Text = "Client ID currenly configured:";
            // 
            // tenantConfigTenantBox
            // 
            tenantConfigTenantBox.Location = new Point(73, 67);
            tenantConfigTenantBox.Name = "tenantConfigTenantBox";
            tenantConfigTenantBox.Size = new Size(518, 31);
            tenantConfigTenantBox.TabIndex = 2;
            // 
            // tenantConfigClientBox
            // 
            tenantConfigClientBox.Location = new Point(74, 199);
            tenantConfigClientBox.Name = "tenantConfigClientBox";
            tenantConfigClientBox.Size = new Size(517, 31);
            tenantConfigClientBox.TabIndex = 3;
            // 
            // confirmConfig
            // 
            confirmConfig.Location = new Point(73, 290);
            confirmConfig.Name = "confirmConfig";
            confirmConfig.Size = new Size(112, 34);
            confirmConfig.TabIndex = 4;
            confirmConfig.Text = "OK";
            confirmConfig.UseVisualStyleBackColor = true;
            confirmConfig.Click += confirmConfig_Click;
            // 
            // tenantConfigCancel
            // 
            tenantConfigCancel.Location = new Point(219, 290);
            tenantConfigCancel.Name = "tenantConfigCancel";
            tenantConfigCancel.Size = new Size(112, 34);
            tenantConfigCancel.TabIndex = 5;
            tenantConfigCancel.Text = "Cancel";
            tenantConfigCancel.UseVisualStyleBackColor = true;
            tenantConfigCancel.Click += tenantConfigCancel_Click;
            // 
            // clearDB
            // 
            clearDB.Location = new Point(665, 395);
            clearDB.Name = "clearDB";
            clearDB.Size = new Size(112, 34);
            clearDB.TabIndex = 6;
            clearDB.Text = "Clear DB";
            clearDB.UseVisualStyleBackColor = true;
            clearDB.Click += clearDB_Click;
            // 
            // tenantConfigOK
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(clearDB);
            Controls.Add(tenantConfigCancel);
            Controls.Add(confirmConfig);
            Controls.Add(tenantConfigClientBox);
            Controls.Add(tenantConfigTenantBox);
            Controls.Add(tenantConfigClientID);
            Controls.Add(tenantConfigTenantID);
            Name = "tenantConfigOK";
            Text = "Entra connection configuration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tenantConfigTenantID;
        private Label tenantConfigClientID;
        private TextBox tenantConfigTenantBox;
        private TextBox tenantConfigClientBox;
        private Button confirmConfig;
        private Button tenantConfigCancel;
        private Button clearDB;
    }
}