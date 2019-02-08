namespace SeventyTwoDesktop.UI
{
    partial class CtlCalendar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) ) {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Patient A - +93770998343 - Maternal Antenatal Visit");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Patient 1 - 9374783843  - Maternal Antenatal Visit");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Scheduled", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Patient B - 888349343 - Maternal Postpartum Visit");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Confirmed", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Patient C - 934838843 - Maternal Antenatal Visit");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Completed", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Saturday, 9 Feb 2019", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Sunday, 10 Feb 2019");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Monday, 11 Feb 2019");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Tuesday, 12 Feb 2019");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Wednesday, 13 Feb 2019");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Thursday, 14 Feb 2019");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Friday, 15 Feb 2019");
            this.BtnPrevWeek = new System.Windows.Forms.Button();
            this.LblApptWeek = new System.Windows.Forms.Label();
            this.BtnNextWeek = new System.Windows.Forms.Button();
            this.TvCalendarItems = new System.Windows.Forms.TreeView();
            this.CalDate7 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.CalDate6 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.CalDate5 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.CalDate4 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.CalDate3 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.CalDate2 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.CalDate1 = new SeventyTwoDesktop.UI.CtlCalDate();
            this.SuspendLayout();
            // 
            // BtnPrevWeek
            // 
            this.BtnPrevWeek.Location = new System.Drawing.Point(3, 3);
            this.BtnPrevWeek.Name = "BtnPrevWeek";
            this.BtnPrevWeek.Size = new System.Drawing.Size(56, 32);
            this.BtnPrevWeek.TabIndex = 0;
            this.BtnPrevWeek.Text = "<--";
            this.BtnPrevWeek.UseVisualStyleBackColor = true;
            // 
            // LblApptWeek
            // 
            this.LblApptWeek.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblApptWeek.Location = new System.Drawing.Point(65, 3);
            this.LblApptWeek.Name = "LblApptWeek";
            this.LblApptWeek.Size = new System.Drawing.Size(370, 32);
            this.LblApptWeek.TabIndex = 1;
            this.LblApptWeek.Text = "Week Beginning 9-Feb-2019";
            this.LblApptWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnNextWeek
            // 
            this.BtnNextWeek.Location = new System.Drawing.Point(441, 3);
            this.BtnNextWeek.Name = "BtnNextWeek";
            this.BtnNextWeek.Size = new System.Drawing.Size(56, 32);
            this.BtnNextWeek.TabIndex = 2;
            this.BtnNextWeek.Text = "-->";
            this.BtnNextWeek.UseVisualStyleBackColor = true;
            // 
            // TvCalendarItems
            // 
            this.TvCalendarItems.Location = new System.Drawing.Point(4, 140);
            this.TvCalendarItems.Name = "TvCalendarItems";
            treeNode1.Name = "Node9";
            treeNode1.Text = "Patient A - +93770998343 - Maternal Antenatal Visit";
            treeNode2.Name = "Node15";
            treeNode2.Text = "Patient 1 - 9374783843  - Maternal Antenatal Visit";
            treeNode3.Name = "Node8";
            treeNode3.Text = "Scheduled";
            treeNode4.Name = "Node12";
            treeNode4.Text = "Patient B - 888349343 - Maternal Postpartum Visit";
            treeNode5.Name = "Node10";
            treeNode5.Text = "Confirmed";
            treeNode6.Name = "Node13";
            treeNode6.Text = "Patient C - 934838843 - Maternal Antenatal Visit";
            treeNode7.Name = "Node11";
            treeNode7.Text = "Completed";
            treeNode8.Name = "Node0";
            treeNode8.Text = "Saturday, 9 Feb 2019";
            treeNode9.Name = "Node1";
            treeNode9.Text = "Sunday, 10 Feb 2019";
            treeNode10.Name = "Node2";
            treeNode10.Text = "Monday, 11 Feb 2019";
            treeNode11.Name = "Node3";
            treeNode11.Text = "Tuesday, 12 Feb 2019";
            treeNode12.Name = "Node4";
            treeNode12.Text = "Wednesday, 13 Feb 2019";
            treeNode13.Name = "Node5";
            treeNode13.Text = "Thursday, 14 Feb 2019";
            treeNode14.Name = "Node6";
            treeNode14.Text = "Friday, 15 Feb 2019";
            this.TvCalendarItems.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            this.TvCalendarItems.Size = new System.Drawing.Size(493, 317);
            this.TvCalendarItems.TabIndex = 11;
            // 
            // CalDate7
            // 
            this.CalDate7.Location = new System.Drawing.Point(432, 41);
            this.CalDate7.Name = "CalDate7";
            this.CalDate7.Size = new System.Drawing.Size(65, 85);
            this.CalDate7.TabIndex = 10;
            this.CalDate7.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate6
            // 
            this.CalDate6.Location = new System.Drawing.Point(360, 41);
            this.CalDate6.Name = "CalDate6";
            this.CalDate6.Size = new System.Drawing.Size(65, 85);
            this.CalDate6.TabIndex = 9;
            this.CalDate6.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate5
            // 
            this.CalDate5.Location = new System.Drawing.Point(288, 41);
            this.CalDate5.Name = "CalDate5";
            this.CalDate5.Size = new System.Drawing.Size(65, 85);
            this.CalDate5.TabIndex = 8;
            this.CalDate5.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate4
            // 
            this.CalDate4.Location = new System.Drawing.Point(216, 41);
            this.CalDate4.Name = "CalDate4";
            this.CalDate4.Size = new System.Drawing.Size(65, 85);
            this.CalDate4.TabIndex = 7;
            this.CalDate4.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate3
            // 
            this.CalDate3.Location = new System.Drawing.Point(144, 41);
            this.CalDate3.Name = "CalDate3";
            this.CalDate3.Size = new System.Drawing.Size(65, 85);
            this.CalDate3.TabIndex = 6;
            this.CalDate3.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate2
            // 
            this.CalDate2.Location = new System.Drawing.Point(72, 41);
            this.CalDate2.Name = "CalDate2";
            this.CalDate2.Size = new System.Drawing.Size(65, 85);
            this.CalDate2.TabIndex = 5;
            this.CalDate2.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate1
            // 
            this.CalDate1.Location = new System.Drawing.Point(0, 41);
            this.CalDate1.Name = "CalDate1";
            this.CalDate1.Size = new System.Drawing.Size(65, 85);
            this.CalDate1.TabIndex = 4;
            this.CalDate1.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CtlCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TvCalendarItems);
            this.Controls.Add(this.CalDate7);
            this.Controls.Add(this.CalDate6);
            this.Controls.Add(this.CalDate5);
            this.Controls.Add(this.CalDate4);
            this.Controls.Add(this.CalDate3);
            this.Controls.Add(this.CalDate2);
            this.Controls.Add(this.CalDate1);
            this.Controls.Add(this.BtnNextWeek);
            this.Controls.Add(this.LblApptWeek);
            this.Controls.Add(this.BtnPrevWeek);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CtlCalendar";
            this.Size = new System.Drawing.Size(500, 460);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnPrevWeek;
        private System.Windows.Forms.Label LblApptWeek;
        private System.Windows.Forms.Button BtnNextWeek;
        private CtlCalDate CalDate1;
        private CtlCalDate CalDate2;
        private CtlCalDate CalDate3;
        private CtlCalDate CalDate4;
        private CtlCalDate CalDate5;
        private CtlCalDate CalDate6;
        private CtlCalDate CalDate7;
        private System.Windows.Forms.TreeView TvCalendarItems;
    }
}
