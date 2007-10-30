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
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.lblVersion = new System.Windows.Forms.Label();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// btnOk
        	// 
        	this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.btnOk.Location = new System.Drawing.Point(89, 80);
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
        	this.label1.Location = new System.Drawing.Point(65, 19);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(89, 19);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "CheckSumTool";
        	// 
        	// label2
        	// 
        	this.label2.Location = new System.Drawing.Point(65, 43);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(249, 23);
        	this.label2.TabIndex = 2;
        	this.label2.Text = "(c) 2007 Ixonos Plc, Kimmo Varis";
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
        	this.pictureBox1.Location = new System.Drawing.Point(13, 13);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(33, 33);
        	this.pictureBox1.TabIndex = 3;
        	this.pictureBox1.TabStop = false;
        	// 
        	// lblVersion
        	// 
        	this.lblVersion.Location = new System.Drawing.Point(160, 19);
        	this.lblVersion.Name = "lblVersion";
        	this.lblVersion.Size = new System.Drawing.Size(100, 24);
        	this.lblVersion.TabIndex = 4;
        	this.lblVersion.Text = "version";
        	// 
        	// AboutForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(253, 115);
        	this.Controls.Add(this.lblVersion);
        	this.Controls.Add(this.pictureBox1);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.btnOk);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.Name = "AboutForm";
        	this.Text = "AboutForm";
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
    }
}
