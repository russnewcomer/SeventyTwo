namespace SeventyTwoDesktop
{
    partial class FrmMain
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabProfiles = new System.Windows.Forms.TabPage();
            this.ctlCalendar1 = new SeventyTwoDesktop.UI.CtlCalendar();
            this.BtnSendLogs = new System.Windows.Forms.Button();
            this.BtnCloseAllOpenProfiles = new System.Windows.Forms.Button();
            this.btnNewProfile = new System.Windows.Forms.Button();
            this.grpExisting = new System.Windows.Forms.GroupBox();
            this.BtnShowAll = new System.Windows.Forms.Button();
            this.BtnLoadSelectedProfile = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchByPatientName = new System.Windows.Forms.Label();
            this.LblProfileList = new System.Windows.Forms.Label();
            this.LstProfiles = new System.Windows.Forms.ListBox();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.tabMain.SuspendLayout();
            this.tabProfiles.SuspendLayout();
            this.grpExisting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabProfiles);
            this.tabMain.Controls.Add(this.tabReports);
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabMain.Location = new System.Drawing.Point(1, 1);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1450, 850);
            this.tabMain.TabIndex = 1;
            // 
            // tabProfiles
            // 
            this.tabProfiles.Controls.Add(this.ctlCalendar1);
            this.tabProfiles.Controls.Add(this.BtnSendLogs);
            this.tabProfiles.Controls.Add(this.BtnCloseAllOpenProfiles);
            this.tabProfiles.Controls.Add(this.btnNewProfile);
            this.tabProfiles.Controls.Add(this.grpExisting);
            this.tabProfiles.Location = new System.Drawing.Point(4, 34);
            this.tabProfiles.Name = "tabProfiles";
            this.tabProfiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabProfiles.Size = new System.Drawing.Size(1442, 812);
            this.tabProfiles.TabIndex = 0;
            this.tabProfiles.Text = "Profiles";
            this.tabProfiles.UseVisualStyleBackColor = true;
            // 
            // ctlCalendar1
            // 
            this.ctlCalendar1.ActiveWeekBeginningDate = new System.DateTime(2019, 2, 9, 0, 0, 0, 0);
            this.ctlCalendar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlCalendar1.Location = new System.Drawing.Point(681, 14);
            this.ctlCalendar1.Name = "ctlCalendar1";
            this.ctlCalendar1.Size = new System.Drawing.Size(755, 520);
            this.ctlCalendar1.TabIndex = 12;
            this.ctlCalendar1.AppointmentClicked += new System.EventHandler(this.ctlCalendar1_AppointmentClicked);
            // 
            // BtnSendLogs
            // 
            this.BtnSendLogs.BackColor = System.Drawing.Color.Transparent;
            this.BtnSendLogs.Location = new System.Drawing.Point(20, 502);
            this.BtnSendLogs.Name = "BtnSendLogs";
            this.BtnSendLogs.Size = new System.Drawing.Size(140, 32);
            this.BtnSendLogs.TabIndex = 11;
            this.BtnSendLogs.Text = "Send Log Files";
            this.BtnSendLogs.UseVisualStyleBackColor = false;
            this.BtnSendLogs.Click += new System.EventHandler(this.BtnSendLogs_Click);
            // 
            // BtnCloseAllOpenProfiles
            // 
            this.BtnCloseAllOpenProfiles.BackColor = System.Drawing.Color.Transparent;
            this.BtnCloseAllOpenProfiles.Location = new System.Drawing.Point(20, 94);
            this.BtnCloseAllOpenProfiles.Name = "BtnCloseAllOpenProfiles";
            this.BtnCloseAllOpenProfiles.Size = new System.Drawing.Size(140, 75);
            this.BtnCloseAllOpenProfiles.TabIndex = 10;
            this.BtnCloseAllOpenProfiles.Text = "Close All Open Profiles";
            this.BtnCloseAllOpenProfiles.UseVisualStyleBackColor = false;
            this.BtnCloseAllOpenProfiles.Click += new System.EventHandler(this.BtnCloseAllOpenProfiles_Click);
            // 
            // btnNewProfile
            // 
            this.btnNewProfile.BackColor = System.Drawing.Color.Transparent;
            this.btnNewProfile.Location = new System.Drawing.Point(20, 28);
            this.btnNewProfile.Name = "btnNewProfile";
            this.btnNewProfile.Size = new System.Drawing.Size(140, 60);
            this.btnNewProfile.TabIndex = 8;
            this.btnNewProfile.Text = "Create &New Profile";
            this.btnNewProfile.UseVisualStyleBackColor = false;
            this.btnNewProfile.Click += new System.EventHandler(this.BtnNewProfile_Click);
            // 
            // grpExisting
            // 
            this.grpExisting.Controls.Add(this.BtnShowAll);
            this.grpExisting.Controls.Add(this.BtnLoadSelectedProfile);
            this.grpExisting.Controls.Add(this.btnSearch);
            this.grpExisting.Controls.Add(this.TxtSearch);
            this.grpExisting.Controls.Add(this.lblSearchByPatientName);
            this.grpExisting.Controls.Add(this.LblProfileList);
            this.grpExisting.Controls.Add(this.LstProfiles);
            this.grpExisting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpExisting.Location = new System.Drawing.Point(181, 10);
            this.grpExisting.Name = "grpExisting";
            this.grpExisting.Size = new System.Drawing.Size(494, 524);
            this.grpExisting.TabIndex = 7;
            this.grpExisting.TabStop = false;
            this.grpExisting.Text = "Existing Profiles";
            // 
            // BtnShowAll
            // 
            this.BtnShowAll.Location = new System.Drawing.Point(380, 95);
            this.BtnShowAll.Name = "BtnShowAll";
            this.BtnShowAll.Size = new System.Drawing.Size(108, 61);
            this.BtnShowAll.TabIndex = 14;
            this.BtnShowAll.Text = "Show All";
            this.BtnShowAll.UseVisualStyleBackColor = true;
            this.BtnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // BtnLoadSelectedProfile
            // 
            this.BtnLoadSelectedProfile.Location = new System.Drawing.Point(6, 442);
            this.BtnLoadSelectedProfile.Name = "BtnLoadSelectedProfile";
            this.BtnLoadSelectedProfile.Size = new System.Drawing.Size(482, 76);
            this.BtnLoadSelectedProfile.TabIndex = 13;
            this.BtnLoadSelectedProfile.Text = "Load Selected Profile";
            this.BtnLoadSelectedProfile.UseVisualStyleBackColor = true;
            this.BtnLoadSelectedProfile.Click += new System.EventHandler(this.BtnLoadSelectedProfile_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(261, 95);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(113, 61);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(10, 47);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(478, 31);
            this.TxtSearch.TabIndex = 8;
            // 
            // lblSearchByPatientName
            // 
            this.lblSearchByPatientName.AutoSize = true;
            this.lblSearchByPatientName.Location = new System.Drawing.Point(6, 19);
            this.lblSearchByPatientName.Name = "lblSearchByPatientName";
            this.lblSearchByPatientName.Size = new System.Drawing.Size(140, 25);
            this.lblSearchByPatientName.TabIndex = 7;
            this.lblSearchByPatientName.Text = "Search By Name";
            // 
            // LblProfileList
            // 
            this.LblProfileList.AutoSize = true;
            this.LblProfileList.Location = new System.Drawing.Point(6, 134);
            this.LblProfileList.Name = "LblProfileList";
            this.LblProfileList.Size = new System.Drawing.Size(93, 25);
            this.LblProfileList.TabIndex = 5;
            this.LblProfileList.Text = "Profile List";
            // 
            // LstProfiles
            // 
            this.LstProfiles.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstProfiles.FormattingEnabled = true;
            this.LstProfiles.ItemHeight = 45;
            this.LstProfiles.Location = new System.Drawing.Point(9, 162);
            this.LstProfiles.Name = "LstProfiles";
            this.LstProfiles.Size = new System.Drawing.Size(479, 274);
            this.LstProfiles.TabIndex = 4;
            this.LstProfiles.DoubleClick += new System.EventHandler(this.LstProfiles_DoubleClick);
            // 
            // tabReports
            // 
            this.tabReports.Location = new System.Drawing.Point(4, 34);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(1241, 540);
            this.tabReports.TabIndex = 1;
            this.tabReports.Text = "Reports";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 844);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmMain";
            this.Text = "SeventyTwo";
            this.tabMain.ResumeLayout(false);
            this.tabProfiles.ResumeLayout(false);
            this.grpExisting.ResumeLayout(false);
            this.grpExisting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabProfiles;
        private System.Windows.Forms.Button btnNewProfile;
        private System.Windows.Forms.GroupBox grpExisting;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label lblSearchByPatientName;
        private System.Windows.Forms.Label LblProfileList;
        private System.Windows.Forms.ListBox LstProfiles;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.Button BtnLoadSelectedProfile;
        private System.Windows.Forms.Button BtnCloseAllOpenProfiles;
        private System.Windows.Forms.Button BtnShowAll;
        private System.Windows.Forms.Button BtnSendLogs;
        private UI.CtlCalendar ctlCalendar1;
    }
}