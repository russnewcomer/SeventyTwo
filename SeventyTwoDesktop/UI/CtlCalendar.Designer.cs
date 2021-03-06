﻿namespace SeventyTwoDesktop.UI
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
            this.components = new System.ComponentModel.Container();
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
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Saturday, 9-Feb-2019", new System.Windows.Forms.TreeNode[] {
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
            this.cmsCalStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCalStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnPrevWeek
            // 
            this.BtnPrevWeek.Location = new System.Drawing.Point(3, 3);
            this.BtnPrevWeek.Name = "BtnPrevWeek";
            this.BtnPrevWeek.Size = new System.Drawing.Size(56, 49);
            this.BtnPrevWeek.TabIndex = 0;
            this.BtnPrevWeek.Text = "<--";
            this.BtnPrevWeek.UseVisualStyleBackColor = true;
            this.BtnPrevWeek.Click += new System.EventHandler(this.BtnPrevWeek_Click);
            // 
            // LblApptWeek
            // 
            this.LblApptWeek.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblApptWeek.Location = new System.Drawing.Point(65, 3);
            this.LblApptWeek.Name = "LblApptWeek";
            this.LblApptWeek.Size = new System.Drawing.Size(621, 49);
            this.LblApptWeek.TabIndex = 1;
            this.LblApptWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblApptWeek.Click += new System.EventHandler(this.LblApptWeek_Click);
            // 
            // BtnNextWeek
            // 
            this.BtnNextWeek.Location = new System.Drawing.Point(692, 3);
            this.BtnNextWeek.Name = "BtnNextWeek";
            this.BtnNextWeek.Size = new System.Drawing.Size(56, 49);
            this.BtnNextWeek.TabIndex = 2;
            this.BtnNextWeek.Text = "-->";
            this.BtnNextWeek.UseVisualStyleBackColor = true;
            this.BtnNextWeek.Click += new System.EventHandler(this.BtnNextWeek_Click);
            // 
            // TvCalendarItems
            // 
            this.TvCalendarItems.Location = new System.Drawing.Point(3, 242);
            this.TvCalendarItems.Name = "TvCalendarItems";
            treeNode1.Name = "09-Feb-2019-Scheduled-0";
            treeNode1.Text = "Patient A - +93770998343 - Maternal Antenatal Visit";
            treeNode2.Name = "09-Feb-2019-Scheduled-1";
            treeNode2.Text = "Patient 1 - 9374783843  - Maternal Antenatal Visit";
            treeNode3.Name = "09-Feb-2019-Scheduled";
            treeNode3.Text = "Scheduled";
            treeNode4.Name = "09-Feb-2019-Confirmed-0";
            treeNode4.Text = "Patient B - 888349343 - Maternal Postpartum Visit";
            treeNode5.Name = "09-Feb-2019-Confirmed";
            treeNode5.Text = "Confirmed";
            treeNode6.Name = "09-Feb-2019-Completed-0";
            treeNode6.Text = "Patient C - 934838843 - Maternal Antenatal Visit";
            treeNode7.Name = "09-Feb-2019-Completed";
            treeNode7.Text = "Completed";
            treeNode8.Name = "09-Feb-2019-All";
            treeNode8.Text = "Saturday, 9-Feb-2019";
            treeNode9.Name = "10-Feb-2019-All";
            treeNode9.Text = "Sunday, 10 Feb 2019";
            treeNode10.Name = "11-Feb-2019-All";
            treeNode10.Text = "Monday, 11 Feb 2019";
            treeNode11.Name = "12-Feb-2019-All";
            treeNode11.Text = "Tuesday, 12 Feb 2019";
            treeNode12.Name = "13-Feb-2019-All";
            treeNode12.Text = "Wednesday, 13 Feb 2019";
            treeNode13.Name = "14-Feb-2019-All";
            treeNode13.Text = "Thursday, 14 Feb 2019";
            treeNode14.Name = "15-Feb-2019-All";
            treeNode14.Text = "Friday, 15 Feb 2019";
            this.TvCalendarItems.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            this.TvCalendarItems.ShowNodeToolTips = true;
            this.TvCalendarItems.Size = new System.Drawing.Size(745, 274);
            this.TvCalendarItems.TabIndex = 11;
            this.TvCalendarItems.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvCalendarItems_NodeMouseClick);
            this.TvCalendarItems.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvCalendarItems_NodeMouseDoubleClick);
            // 
            // CalDate7
            // 
            this.CalDate7.Location = new System.Drawing.Point(648, 84);
            this.CalDate7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate7.Name = "CalDate7";
            this.CalDate7.Size = new System.Drawing.Size(100, 150);
            this.CalDate7.TabIndex = 10;
            this.CalDate7.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate6
            // 
            this.CalDate6.Location = new System.Drawing.Point(540, 84);
            this.CalDate6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate6.Name = "CalDate6";
            this.CalDate6.Size = new System.Drawing.Size(100, 150);
            this.CalDate6.TabIndex = 9;
            this.CalDate6.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate5
            // 
            this.CalDate5.Location = new System.Drawing.Point(432, 84);
            this.CalDate5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate5.Name = "CalDate5";
            this.CalDate5.Size = new System.Drawing.Size(100, 150);
            this.CalDate5.TabIndex = 8;
            this.CalDate5.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate4
            // 
            this.CalDate4.Location = new System.Drawing.Point(324, 84);
            this.CalDate4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate4.Name = "CalDate4";
            this.CalDate4.Size = new System.Drawing.Size(100, 150);
            this.CalDate4.TabIndex = 7;
            this.CalDate4.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate3
            // 
            this.CalDate3.Location = new System.Drawing.Point(216, 84);
            this.CalDate3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate3.Name = "CalDate3";
            this.CalDate3.Size = new System.Drawing.Size(100, 150);
            this.CalDate3.TabIndex = 6;
            this.CalDate3.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate2
            // 
            this.CalDate2.Location = new System.Drawing.Point(108, 84);
            this.CalDate2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate2.Name = "CalDate2";
            this.CalDate2.Size = new System.Drawing.Size(100, 150);
            this.CalDate2.TabIndex = 5;
            this.CalDate2.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // CalDate1
            // 
            this.CalDate1.Location = new System.Drawing.Point(0, 84);
            this.CalDate1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CalDate1.Name = "CalDate1";
            this.CalDate1.Size = new System.Drawing.Size(100, 150);
            this.CalDate1.TabIndex = 4;
            this.CalDate1.Clicked += new System.EventHandler(this.CalDate_ClickedEventHandler);
            // 
            // cmsCalStrip
            // 
            this.cmsCalStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsCalStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpen,
            this.tsmCompleted});
            this.cmsCalStrip.Name = "cmsCalStrip";
            this.cmsCalStrip.Size = new System.Drawing.Size(198, 68);
            // 
            // tsmOpen
            // 
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.Size = new System.Drawing.Size(197, 32);
            this.tsmOpen.Text = "Open Record";
            this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // tsmCompleted
            // 
            this.tsmCompleted.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduledToolStripMenuItem,
            this.confirmedToolStripMenuItem,
            this.completedToolStripMenuItem,
            this.deletedToolStripMenuItem});
            this.tsmCompleted.Name = "tsmCompleted";
            this.tsmCompleted.Size = new System.Drawing.Size(197, 32);
            this.tsmCompleted.Text = "Change Status";
            // 
            // scheduledToolStripMenuItem
            // 
            this.scheduledToolStripMenuItem.Name = "scheduledToolStripMenuItem";
            this.scheduledToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.scheduledToolStripMenuItem.Text = "Scheduled";
            this.scheduledToolStripMenuItem.Click += new System.EventHandler(this.scheduledToolStripMenuItem_Click);
            // 
            // confirmedToolStripMenuItem
            // 
            this.confirmedToolStripMenuItem.Name = "confirmedToolStripMenuItem";
            this.confirmedToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.confirmedToolStripMenuItem.Text = "Confirmed";
            this.confirmedToolStripMenuItem.Click += new System.EventHandler(this.confirmedToolStripMenuItem_Click);
            // 
            // completedToolStripMenuItem
            // 
            this.completedToolStripMenuItem.Name = "completedToolStripMenuItem";
            this.completedToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.completedToolStripMenuItem.Text = "Completed";
            this.completedToolStripMenuItem.Click += new System.EventHandler(this.completedToolStripMenuItem_Click);
            // 
            // deletedToolStripMenuItem
            // 
            this.deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
            this.deletedToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.deletedToolStripMenuItem.Text = "Cancelled";
            this.deletedToolStripMenuItem.Click += new System.EventHandler(this.deletedToolStripMenuItem_Click);
            // 
            // CtlCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
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
            this.Size = new System.Drawing.Size(755, 519);
            this.cmsCalStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip cmsCalStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmCompleted;
        private System.Windows.Forms.ToolStripMenuItem scheduledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletedToolStripMenuItem;
    }
}
