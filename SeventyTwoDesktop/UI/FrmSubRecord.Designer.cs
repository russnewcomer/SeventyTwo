namespace SeventyTwoDesktop.UI
{
    partial class FrmSubRecord
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.LblRecordTitle = new System.Windows.Forms.Label();
            this.PnlControls = new System.Windows.Forms.Panel();
            this.TvViewNodes = new System.Windows.Forms.TreeView();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnFinished = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblRecordTitle
            // 
            this.LblRecordTitle.AutoSize = true;
            this.LblRecordTitle.Location = new System.Drawing.Point(15, 15);
            this.LblRecordTitle.Name = "LblRecordTitle";
            this.LblRecordTitle.Size = new System.Drawing.Size(38, 15);
            this.LblRecordTitle.TabIndex = 0;
            this.LblRecordTitle.Text = "label1";
            // 
            // PnlControls
            // 
            this.PnlControls.Location = new System.Drawing.Point(18, 34);
            this.PnlControls.Name = "PnlControls";
            this.PnlControls.Size = new System.Drawing.Size(550, 200);
            this.PnlControls.TabIndex = 1;
            // 
            // TvViewNodes
            // 
            this.TvViewNodes.Location = new System.Drawing.Point(575, 34);
            this.TvViewNodes.Name = "TvViewNodes";
            this.TvViewNodes.Size = new System.Drawing.Size(262, 257);
            this.TvViewNodes.TabIndex = 2;
            this.TvViewNodes.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvViewNodes_NodeMouseClick);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrevious.Location = new System.Drawing.Point(18, 240);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(278, 89);
            this.BtnPrevious.TabIndex = 3;
            this.BtnPrevious.Text = "&Previous";
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNext.Location = new System.Drawing.Point(302, 240);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(266, 89);
            this.BtnNext.TabIndex = 4;
            this.BtnNext.Text = "&Next";
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnFinished
            // 
            this.BtnFinished.Location = new System.Drawing.Point(575, 297);
            this.BtnFinished.Name = "BtnFinished";
            this.BtnFinished.Size = new System.Drawing.Size(262, 32);
            this.BtnFinished.TabIndex = 6;
            this.BtnFinished.Text = "&Finished";
            this.BtnFinished.UseVisualStyleBackColor = true;
            this.BtnFinished.Click += new System.EventHandler(this.BtnFinished_Click);
            // 
            // FrmSubRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 341);
            this.Controls.Add(this.BtnFinished);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.TvViewNodes);
            this.Controls.Add(this.PnlControls);
            this.Controls.Add(this.LblRecordTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSubRecord";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SubRecord";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblRecordTitle;
        private System.Windows.Forms.Panel PnlControls;
        private System.Windows.Forms.TreeView TvViewNodes;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button BtnFinished;
    }
}