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
            this.BtnPrevWeek = new System.Windows.Forms.Button();
            this.LblApptWeek = new System.Windows.Forms.Label();
            this.BtnNextWeek = new System.Windows.Forms.Button();
            this.PnlDay1 = new System.Windows.Forms.Panel();
            this.LblDay1 = new System.Windows.Forms.Label();
            this.PnlDay1.SuspendLayout();
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
            // PnlDay1
            // 
            this.PnlDay1.Controls.Add(this.LblDay1);
            this.PnlDay1.Location = new System.Drawing.Point(5, 50);
            this.PnlDay1.Name = "PnlDay1";
            this.PnlDay1.Size = new System.Drawing.Size(65, 80);
            this.PnlDay1.TabIndex = 3;
            // 
            // LblDay1
            // 
            this.LblDay1.Location = new System.Drawing.Point(0, 0);
            this.LblDay1.Name = "LblDay1";
            this.LblDay1.Size = new System.Drawing.Size(65, 15);
            this.LblDay1.TabIndex = 0;
            this.LblDay1.Text = "S 9 Feb";
            // 
            // CtlCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PnlDay1);
            this.Controls.Add(this.BtnNextWeek);
            this.Controls.Add(this.LblApptWeek);
            this.Controls.Add(this.BtnPrevWeek);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CtlCalendar";
            this.Size = new System.Drawing.Size(500, 460);
            this.PnlDay1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnPrevWeek;
        private System.Windows.Forms.Label LblApptWeek;
        private System.Windows.Forms.Button BtnNextWeek;
        private System.Windows.Forms.Panel PnlDay1;
        private System.Windows.Forms.Label LblDay1;
    }
}
