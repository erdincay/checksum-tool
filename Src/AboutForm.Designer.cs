/*
 * Created by SharpDevelop.
 * User: variski
 * Date: 21.9.2007
 * Time: 13:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CheckSumTool
{
    partial class AboutForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
        	this.btnOk = new System.Windows.Forms.Button();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.pictureIcon = new System.Windows.Forms.PictureBox();
        	this.linkHomepage = new System.Windows.Forms.LinkLabel();
        	this.labelVersion = new System.Windows.Forms.Label();
        	this.label3 = new System.Windows.Forms.Label();
        	((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// btnOk
        	// 
        	this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.btnOk.Location = new System.Drawing.Point(119, 134);
        	this.btnOk.Name = "btnOk";
        	this.btnOk.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.btnOk.Size = new System.Drawing.Size(75, 23);
        	this.btnOk.TabIndex = 0;
        	this.btnOk.Text = "OK";
        	this.btnOk.UseVisualStyleBackColor = true;
        	this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
        	// 
        	// label1
        	// 
        	this.label1.Location = new System.Drawing.Point(67, 19);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(89, 19);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "CheckSumTool";
        	// 
        	// label2
        	// 
        	this.label2.Location = new System.Drawing.Point(67, 42);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(196, 23);
        	this.label2.TabIndex = 2;
        	this.label2.Text = "(c) 2007 Ixonos Plc, Kimmo Varis";
        	// 
        	// pictureIcon
        	// 
        	this.pictureIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureIcon.Image")));
        	this.pictureIcon.Location = new System.Drawing.Point(12, 12);
        	this.pictureIcon.Name = "pictureIcon";
        	this.pictureIcon.Size = new System.Drawing.Size(45, 42);
        	this.pictureIcon.TabIndex = 3;
        	this.pictureIcon.TabStop = false;
        	// 
        	// linkHomepage
        	// 
        	this.linkHomepage.Location = new System.Drawing.Point(67, 99);
        	this.linkHomepage.Name = "linkHomepage";
        	this.linkHomepage.Size = new System.Drawing.Size(196, 23);
        	this.linkHomepage.TabIndex = 5;
        	this.linkHomepage.TabStop = true;
        	this.linkHomepage.Text = "Visit CheckSum Tool Homepage";
        	this.linkHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
        	// 
        	// labelVersion
        	// 
        	this.labelVersion.Location = new System.Drawing.Point(162, 19);
        	this.labelVersion.Name = "labelVersion";
        	this.labelVersion.Size = new System.Drawing.Size(138, 23);
        	this.labelVersion.TabIndex = 6;
        	this.labelVersion.Text = "Version";
        	// 
        	// label3
        	// 
        	this.label3.Location = new System.Drawing.Point(67, 65);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(233, 23);
        	this.label3.TabIndex = 7;
        	this.label3.Text = "All rights reserved.";
        	// 
        	// AboutForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(312, 169);
        	this.Controls.Add(this.label3);
        	this.Controls.Add(this.labelVersion);
        	this.Controls.Add(this.linkHomepage);
        	this.Controls.Add(this.pictureIcon);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.btnOk);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.Name = "AboutForm";
        	this.Text = "About CheckSum Tool";
        	((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).EndInit();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.PictureBox pictureIcon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel linkHomepage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
    }
}
