namespace SeventyTwoDesktop
{
    partial class frmMain
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
            this.tabPatients = new System.Windows.Forms.TabPage();
            this.btnNewPatient = new System.Windows.Forms.Button();
            this.grpExisting = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblSearchByDate = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblSearchByPatientName = new System.Windows.Forms.Label();
            this.lblPatientList = new System.Windows.Forms.Label();
            this.lstPatients = new System.Windows.Forms.ListBox();
            this.tabCalendar = new System.Windows.Forms.TabPage();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.tabMain.SuspendLayout();
            this.tabPatients.SuspendLayout();
            this.grpExisting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPatients);
            this.tabMain.Controls.Add(this.tabCalendar);
            this.tabMain.Controls.Add(this.tabReports);
            this.tabMain.Location = new System.Drawing.Point(1, 1);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(798, 449);
            this.tabMain.TabIndex = 1;
            // 
            // tabPatients
            // 
            this.tabPatients.Controls.Add(this.btnNewPatient);
            this.tabPatients.Controls.Add(this.grpExisting);
            this.tabPatients.Location = new System.Drawing.Point(4, 22);
            this.tabPatients.Name = "tabPatients";
            this.tabPatients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatients.Size = new System.Drawing.Size(790, 423);
            this.tabPatients.TabIndex = 0;
            this.tabPatients.Text = "Patients";
            this.tabPatients.UseVisualStyleBackColor = true;
            // 
            // btnNewPatient
            // 
            this.btnNewPatient.Location = new System.Drawing.Point(663, 356);
            this.btnNewPatient.Name = "btnNewPatient";
            this.btnNewPatient.Size = new System.Drawing.Size(120, 58);
            this.btnNewPatient.TabIndex = 8;
            this.btnNewPatient.Text = "&New Patient";
            this.btnNewPatient.UseVisualStyleBackColor = true;
            // 
            // grpExisting
            // 
            this.grpExisting.Controls.Add(this.btnSearch);
            this.grpExisting.Controls.Add(this.textBox2);
            this.grpExisting.Controls.Add(this.lblSearchByDate);
            this.grpExisting.Controls.Add(this.textBox1);
            this.grpExisting.Controls.Add(this.lblSearchByPatientName);
            this.grpExisting.Controls.Add(this.lblPatientList);
            this.grpExisting.Controls.Add(this.lstPatients);
            this.grpExisting.Location = new System.Drawing.Point(7, 6);
            this.grpExisting.Name = "grpExisting";
            this.grpExisting.Size = new System.Drawing.Size(776, 309);
            this.grpExisting.TabIndex = 7;
            this.grpExisting.TabStop = false;
            this.grpExisting.Text = "Existing Patients";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 121);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(150, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 95);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(150, 20);
            this.textBox2.TabIndex = 10;
            // 
            // lblSearchByDate
            // 
            this.lblSearchByDate.AutoSize = true;
            this.lblSearchByDate.Location = new System.Drawing.Point(3, 79);
            this.lblSearchByDate.Name = "lblSearchByDate";
            this.lblSearchByDate.Size = new System.Drawing.Size(104, 13);
            this.lblSearchByDate.TabIndex = 9;
            this.lblSearchByDate.Text = "Search By Visit Date";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 8;
            // 
            // lblSearchByPatientName
            // 
            this.lblSearchByPatientName.AutoSize = true;
            this.lblSearchByPatientName.Location = new System.Drawing.Point(3, 23);
            this.lblSearchByPatientName.Name = "lblSearchByPatientName";
            this.lblSearchByPatientName.Size = new System.Drawing.Size(123, 13);
            this.lblSearchByPatientName.TabIndex = 7;
            this.lblSearchByPatientName.Text = "Search By Patient Name";
            // 
            // lblPatientList
            // 
            this.lblPatientList.AutoSize = true;
            this.lblPatientList.Location = new System.Drawing.Point(159, 23);
            this.lblPatientList.Name = "lblPatientList";
            this.lblPatientList.Size = new System.Drawing.Size(59, 13);
            this.lblPatientList.TabIndex = 5;
            this.lblPatientList.Text = "Patient List";
            // 
            // lstPatients
            // 
            this.lstPatients.FormattingEnabled = true;
            this.lstPatients.Location = new System.Drawing.Point(162, 39);
            this.lstPatients.Name = "lstPatients";
            this.lstPatients.Size = new System.Drawing.Size(608, 264);
            this.lstPatients.TabIndex = 4;
            // 
            // tabCalendar
            // 
            this.tabCalendar.Location = new System.Drawing.Point(4, 22);
            this.tabCalendar.Name = "tabCalendar";
            this.tabCalendar.Size = new System.Drawing.Size(790, 423);
            this.tabCalendar.TabIndex = 2;
            this.tabCalendar.Text = "Calendar";
            this.tabCalendar.UseVisualStyleBackColor = true;
            // 
            // tabReports
            // 
            this.tabReports.Location = new System.Drawing.Point(4, 22);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(790, 423);
            this.tabReports.TabIndex = 1;
            this.tabReports.Text = "Reports";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // frmInitial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabMain);
            this.Name = "frmInitial";
            this.Text = "frmInitial";
            this.tabMain.ResumeLayout(false);
            this.tabPatients.ResumeLayout(false);
            this.grpExisting.ResumeLayout(false);
            this.grpExisting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPatients;
        private System.Windows.Forms.Button btnNewPatient;
        private System.Windows.Forms.GroupBox grpExisting;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblSearchByDate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblSearchByPatientName;
        private System.Windows.Forms.Label lblPatientList;
        private System.Windows.Forms.ListBox lstPatients;
        private System.Windows.Forms.TabPage tabCalendar;
        private System.Windows.Forms.TabPage tabReports;
    }
}