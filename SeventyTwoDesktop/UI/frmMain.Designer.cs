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
            this.gbCalendar = new System.Windows.Forms.GroupBox();
            this.btnNewProfile = new System.Windows.Forms.Button();
            this.grpExisting = new System.Windows.Forms.GroupBox();
            this.BtnLoadSelectedProfile = new System.Windows.Forms.Button();
            this.dtpSearch = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearchByDate = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblSearchByPatientName = new System.Windows.Forms.Label();
            this.lblPatientList = new System.Windows.Forms.Label();
            this.LstProfiles = new System.Windows.Forms.ListBox();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.BtnCloseAllOpenProfiles = new System.Windows.Forms.Button();
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
            this.tabMain.Size = new System.Drawing.Size(982, 508);
            this.tabMain.TabIndex = 1;
            // 
            // tabProfiles
            // 
            this.tabProfiles.Controls.Add(this.BtnCloseAllOpenProfiles);
            this.tabProfiles.Controls.Add(this.gbCalendar);
            this.tabProfiles.Controls.Add(this.btnNewProfile);
            this.tabProfiles.Controls.Add(this.grpExisting);
            this.tabProfiles.Location = new System.Drawing.Point(4, 24);
            this.tabProfiles.Name = "tabProfiles";
            this.tabProfiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabProfiles.Size = new System.Drawing.Size(974, 480);
            this.tabProfiles.TabIndex = 0;
            this.tabProfiles.Text = "Profiles";
            this.tabProfiles.UseVisualStyleBackColor = true;
            // 
            // gbCalendar
            // 
            this.gbCalendar.Location = new System.Drawing.Point(468, 11);
            this.gbCalendar.Name = "gbCalendar";
            this.gbCalendar.Size = new System.Drawing.Size(490, 462);
            this.gbCalendar.TabIndex = 9;
            this.gbCalendar.TabStop = false;
            this.gbCalendar.Text = "Appointment Calendar";
            // 
            // btnNewProfile
            // 
            this.btnNewProfile.BackColor = System.Drawing.Color.Transparent;
            this.btnNewProfile.Location = new System.Drawing.Point(20, 28);
            this.btnNewProfile.Name = "btnNewProfile";
            this.btnNewProfile.Size = new System.Drawing.Size(140, 43);
            this.btnNewProfile.TabIndex = 8;
            this.btnNewProfile.Text = "Create &New Profile";
            this.btnNewProfile.UseVisualStyleBackColor = false;
            this.btnNewProfile.Click += new System.EventHandler(this.BtnNewProfile_Click);
            // 
            // grpExisting
            // 
            this.grpExisting.Controls.Add(this.BtnLoadSelectedProfile);
            this.grpExisting.Controls.Add(this.dtpSearch);
            this.grpExisting.Controls.Add(this.btnSearch);
            this.grpExisting.Controls.Add(this.lblSearchByDate);
            this.grpExisting.Controls.Add(this.textBox1);
            this.grpExisting.Controls.Add(this.lblSearchByPatientName);
            this.grpExisting.Controls.Add(this.lblPatientList);
            this.grpExisting.Controls.Add(this.LstProfiles);
            this.grpExisting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpExisting.Location = new System.Drawing.Point(181, 11);
            this.grpExisting.Name = "grpExisting";
            this.grpExisting.Size = new System.Drawing.Size(265, 462);
            this.grpExisting.TabIndex = 7;
            this.grpExisting.TabStop = false;
            this.grpExisting.Text = "Existing Profiles";
            // 
            // BtnLoadSelectedProfile
            // 
            this.BtnLoadSelectedProfile.Location = new System.Drawing.Point(6, 429);
            this.BtnLoadSelectedProfile.Name = "BtnLoadSelectedProfile";
            this.BtnLoadSelectedProfile.Size = new System.Drawing.Size(250, 27);
            this.BtnLoadSelectedProfile.TabIndex = 13;
            this.BtnLoadSelectedProfile.Text = "Load Selected Profile";
            this.BtnLoadSelectedProfile.UseVisualStyleBackColor = true;
            this.BtnLoadSelectedProfile.Click += new System.EventHandler(this.BtnLoadSelectedProfile_Click);
            // 
            // dtpSearch
            // 
            this.dtpSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSearch.Location = new System.Drawing.Point(150, 37);
            this.dtpSearch.Name = "dtpSearch";
            this.dtpSearch.Size = new System.Drawing.Size(106, 23);
            this.dtpSearch.TabIndex = 12;
            this.dtpSearch.Value = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(164, 66);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 27);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            this.btnSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSearch_KeyDown);
            // 
            // lblSearchByDate
            // 
            this.lblSearchByDate.AutoSize = true;
            this.lblSearchByDate.Location = new System.Drawing.Point(146, 19);
            this.lblSearchByDate.Name = "lblSearchByDate";
            this.lblSearchByDate.Size = new System.Drawing.Size(110, 15);
            this.lblSearchByDate.TabIndex = 9;
            this.lblSearchByDate.Text = "Search By Visit Date";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(134, 23);
            this.textBox1.TabIndex = 8;
            // 
            // lblSearchByPatientName
            // 
            this.lblSearchByPatientName.AutoSize = true;
            this.lblSearchByPatientName.Location = new System.Drawing.Point(6, 19);
            this.lblSearchByPatientName.Name = "lblSearchByPatientName";
            this.lblSearchByPatientName.Size = new System.Drawing.Size(93, 15);
            this.lblSearchByPatientName.TabIndex = 7;
            this.lblSearchByPatientName.Text = "Search By Name";
            // 
            // lblPatientList
            // 
            this.lblPatientList.AutoSize = true;
            this.lblPatientList.Location = new System.Drawing.Point(6, 89);
            this.lblPatientList.Name = "lblPatientList";
            this.lblPatientList.Size = new System.Drawing.Size(62, 15);
            this.lblPatientList.TabIndex = 5;
            this.lblPatientList.Text = "Profile List";
            // 
            // LstProfiles
            // 
            this.LstProfiles.FormattingEnabled = true;
            this.LstProfiles.ItemHeight = 15;
            this.LstProfiles.Location = new System.Drawing.Point(6, 107);
            this.LstProfiles.Name = "LstProfiles";
            this.LstProfiles.Size = new System.Drawing.Size(250, 319);
            this.LstProfiles.TabIndex = 4;
            this.LstProfiles.DoubleClick += new System.EventHandler(this.LstProfiles_DoubleClick);
            // 
            // tabReports
            // 
            this.tabReports.Location = new System.Drawing.Point(4, 24);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(974, 480);
            this.tabReports.TabIndex = 1;
            this.tabReports.Text = "Reports";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // BtnCloseAllOpenProfiles
            // 
            this.BtnCloseAllOpenProfiles.BackColor = System.Drawing.Color.Transparent;
            this.BtnCloseAllOpenProfiles.Location = new System.Drawing.Point(20, 77);
            this.BtnCloseAllOpenProfiles.Name = "BtnCloseAllOpenProfiles";
            this.BtnCloseAllOpenProfiles.Size = new System.Drawing.Size(140, 43);
            this.BtnCloseAllOpenProfiles.TabIndex = 10;
            this.BtnCloseAllOpenProfiles.Text = "Close All Open Profiles";
            this.BtnCloseAllOpenProfiles.UseVisualStyleBackColor = false;
            this.BtnCloseAllOpenProfiles.Click += new System.EventHandler(this.BtnCloseAllOpenProfiles_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 511);
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
        private System.Windows.Forms.Label lblSearchByDate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblSearchByPatientName;
        private System.Windows.Forms.Label lblPatientList;
        private System.Windows.Forms.ListBox LstProfiles;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.DateTimePicker dtpSearch;
        private System.Windows.Forms.GroupBox gbCalendar;
        private System.Windows.Forms.Button BtnLoadSelectedProfile;
        private System.Windows.Forms.Button BtnCloseAllOpenProfiles;
    }
}