namespace SeventyTwoDesktop.UI
{
    partial class CtlCalDate
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
            this.LblDay = new System.Windows.Forms.Label();
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.LblScheduled = new System.Windows.Forms.Label();
            this.LblConfirmed = new System.Windows.Forms.Label();
            this.LblCompleted = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblDay
            // 
            this.LblDay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblDay.Location = new System.Drawing.Point(0, 0);
            this.LblDay.Name = "LblDay";
            this.LblDay.Size = new System.Drawing.Size(40, 15);
            this.LblDay.TabIndex = 1;
            this.LblDay.Text = "9 Feb";
            this.LblDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblDay.Click += new System.EventHandler(this.Control_Click);
            // 
            // lblDayOfWeek
            // 
            this.lblDayOfWeek.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDayOfWeek.Location = new System.Drawing.Point(40, 0);
            this.lblDayOfWeek.Name = "lblDayOfWeek";
            this.lblDayOfWeek.Size = new System.Drawing.Size(25, 15);
            this.lblDayOfWeek.TabIndex = 2;
            this.lblDayOfWeek.Text = "Sa";
            this.lblDayOfWeek.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDayOfWeek.Click += new System.EventHandler(this.Control_Click);
            // 
            // LblScheduled
            // 
            this.LblScheduled.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblScheduled.Location = new System.Drawing.Point(0, 20);
            this.LblScheduled.Name = "LblScheduled";
            this.LblScheduled.Size = new System.Drawing.Size(65, 15);
            this.LblScheduled.TabIndex = 3;
            this.LblScheduled.Text = "X Sched";
            this.LblScheduled.Click += new System.EventHandler(this.Control_Click);
            // 
            // LblConfirmed
            // 
            this.LblConfirmed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblConfirmed.Location = new System.Drawing.Point(0, 40);
            this.LblConfirmed.Name = "LblConfirmed";
            this.LblConfirmed.Size = new System.Drawing.Size(65, 13);
            this.LblConfirmed.TabIndex = 4;
            this.LblConfirmed.Text = "X Conf";
            this.LblConfirmed.Click += new System.EventHandler(this.Control_Click);
            // 
            // LblCompleted
            // 
            this.LblCompleted.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblCompleted.Location = new System.Drawing.Point(0, 60);
            this.LblCompleted.Name = "LblCompleted";
            this.LblCompleted.Size = new System.Drawing.Size(65, 15);
            this.LblCompleted.TabIndex = 5;
            this.LblCompleted.Text = "X Complete";
            this.LblCompleted.Click += new System.EventHandler(this.Control_Click);
            // 
            // CtlCalDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LblCompleted);
            this.Controls.Add(this.LblConfirmed);
            this.Controls.Add(this.LblScheduled);
            this.Controls.Add(this.lblDayOfWeek);
            this.Controls.Add(this.LblDay);
            this.Name = "CtlCalDate";
            this.Size = new System.Drawing.Size(65, 80);
            this.Click += new System.EventHandler(this.CtlCalDate_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblDay;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.Label LblScheduled;
        private System.Windows.Forms.Label LblConfirmed;
        private System.Windows.Forms.Label LblCompleted;
    }
}
