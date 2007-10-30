/*
 * Created by SharpDevelop.
 * User: variski
 * Date: 19.9.2007
 * Time: 9:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CheckSumTool
{
    partial class MainForm
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        	this.btnProcess = new System.Windows.Forms.Button();
        	this.itemList = new System.Windows.Forms.ListView();
        	this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        	this.btnRemove = new System.Windows.Forms.Button();
        	this.btnClear = new System.Windows.Forms.Button();
        	this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        	this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        	this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.aboutSHA1ToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.btnVerify = new System.Windows.Forms.Button();
        	this.comboCheckSumType = new System.Windows.Forms.ComboBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.btnAddFile = new System.Windows.Forms.Button();
        	this.btnAddFolder = new System.Windows.Forms.Button();
        	this.toolStripMain = new System.Windows.Forms.ToolStrip();
        	this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
        	this.toolStripBtnSave = new System.Windows.Forms.ToolStripButton();
        	this.menuStrip1.SuspendLayout();
        	this.toolStripMain.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// btnProcess
        	// 
        	this.btnProcess.Location = new System.Drawing.Point(222, 326);
        	this.btnProcess.Name = "btnProcess";
        	this.btnProcess.Size = new System.Drawing.Size(75, 23);
        	this.btnProcess.TabIndex = 4;
        	this.btnProcess.Text = "Calculate";
        	this.btnProcess.UseVisualStyleBackColor = true;
        	this.btnProcess.Click += new System.EventHandler(this.BtnProcessClick);
        	// 
        	// itemList
        	// 
        	this.itemList.AllowColumnReorder = true;
        	this.itemList.AutoArrange = false;
        	this.itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
        	        	        	this.columnHeader1,
        	        	        	this.columnHeader2,
        	        	        	this.columnHeader3,
        	        	        	this.columnHeader4});
        	this.itemList.Location = new System.Drawing.Point(12, 89);
        	this.itemList.MultiSelect = false;
        	this.itemList.Name = "itemList";
        	this.itemList.ShowItemToolTips = true;
        	this.itemList.Size = new System.Drawing.Size(607, 230);
        	this.itemList.TabIndex = 0;
        	this.itemList.UseCompatibleStateImageBehavior = false;
        	this.itemList.View = System.Windows.Forms.View.Details;
        	// 
        	// columnHeader1
        	// 
        	this.columnHeader1.Text = "Filename";
        	this.columnHeader1.Width = 150;
        	// 
        	// columnHeader2
        	// 
        	this.columnHeader2.Text = "SHA1";
        	this.columnHeader2.Width = 266;
        	// 
        	// columnHeader3
        	// 
        	this.columnHeader3.Text = "Verified";
        	// 
        	// columnHeader4
        	// 
        	this.columnHeader4.Text = "Full Path";
        	this.columnHeader4.Width = 120;
        	// 
        	// btnRemove
        	// 
        	this.btnRemove.Location = new System.Drawing.Point(174, 60);
        	this.btnRemove.Name = "btnRemove";
        	this.btnRemove.Size = new System.Drawing.Size(75, 23);
        	this.btnRemove.TabIndex = 10;
        	this.btnRemove.Text = "Remove";
        	this.btnRemove.UseVisualStyleBackColor = true;
        	this.btnRemove.Click += new System.EventHandler(this.BtnRemoveClick);
        	// 
        	// btnClear
        	// 
        	this.btnClear.Location = new System.Drawing.Point(255, 60);
        	this.btnClear.Name = "btnClear";
        	this.btnClear.Size = new System.Drawing.Size(75, 23);
        	this.btnClear.TabIndex = 11;
        	this.btnClear.Text = "Clear";
        	this.btnClear.UseVisualStyleBackColor = true;
        	this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
        	// 
        	// menuStrip1
        	// 
        	this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.fileToolStripMenuItem,
        	        	        	this.aboutToolStripMenuItem});
        	this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        	this.menuStrip1.Name = "menuStrip1";
        	this.menuStrip1.Size = new System.Drawing.Size(631, 24);
        	this.menuStrip1.TabIndex = 13;
        	this.menuStrip1.Text = "menuStrip1";
        	// 
        	// fileToolStripMenuItem
        	// 
        	this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.toolStripMenuItem2,
        	        	        	this.toolStripMenuItem1,
        	        	        	this.toolStripSeparator1,
        	        	        	this.exitToolStripMenuItem});
        	this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        	this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
        	this.fileToolStripMenuItem.Text = "&File";
        	// 
        	// toolStripMenuItem2
        	// 
        	this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
        	this.toolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.White;
        	this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        	this.toolStripMenuItem2.Size = new System.Drawing.Size(123, 22);
        	this.toolStripMenuItem2.Text = "Open...";
        	this.toolStripMenuItem2.Click += new System.EventHandler(this.MenuIFileOpenClick);
        	// 
        	// toolStripMenuItem1
        	// 
        	this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
        	this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.White;
        	this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        	this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
        	this.toolStripMenuItem1.Text = "Save...";
        	this.toolStripMenuItem1.Click += new System.EventHandler(this.MenuFileSaveClick);
        	// 
        	// toolStripSeparator1
        	// 
        	this.toolStripSeparator1.Name = "toolStripSeparator1";
        	this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
        	// 
        	// exitToolStripMenuItem
        	// 
        	this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        	this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
        	this.exitToolStripMenuItem.Text = "&Exit";
        	this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
        	// 
        	// aboutToolStripMenuItem
        	// 
        	this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.aboutSHA1ToolToolStripMenuItem});
        	this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        	this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
        	this.aboutToolStripMenuItem.Text = "&About";
        	// 
        	// aboutSHA1ToolToolStripMenuItem
        	// 
        	this.aboutSHA1ToolToolStripMenuItem.Name = "aboutSHA1ToolToolStripMenuItem";
        	this.aboutSHA1ToolToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
        	this.aboutSHA1ToolToolStripMenuItem.Text = "&About SHA1Tool";
        	this.aboutSHA1ToolToolStripMenuItem.Click += new System.EventHandler(this.AboutSHA1ToolToolStripMenuItemClick);
        	// 
        	// btnVerify
        	// 
        	this.btnVerify.Location = new System.Drawing.Point(303, 326);
        	this.btnVerify.Name = "btnVerify";
        	this.btnVerify.Size = new System.Drawing.Size(75, 23);
        	this.btnVerify.TabIndex = 14;
        	this.btnVerify.Text = "Verify";
        	this.btnVerify.UseVisualStyleBackColor = true;
        	this.btnVerify.Click += new System.EventHandler(this.BtnVerifyClick);
        	// 
        	// comboCheckSumType
        	// 
        	this.comboCheckSumType.FormattingEnabled = true;
        	this.comboCheckSumType.Location = new System.Drawing.Point(107, 326);
        	this.comboCheckSumType.Name = "comboCheckSumType";
        	this.comboCheckSumType.Size = new System.Drawing.Size(97, 21);
        	this.comboCheckSumType.TabIndex = 15;
        	this.comboCheckSumType.SelectedIndexChanged += new System.EventHandler(this.ComboCheckSumTypeSelectedIndexChanged);
        	// 
        	// label1
        	// 
        	this.label1.Location = new System.Drawing.Point(12, 329);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(89, 23);
        	this.label1.TabIndex = 16;
        	this.label1.Text = "CheckSum type:";
        	// 
        	// btnAddFile
        	// 
        	this.btnAddFile.Location = new System.Drawing.Point(12, 60);
        	this.btnAddFile.Name = "btnAddFile";
        	this.btnAddFile.Size = new System.Drawing.Size(75, 23);
        	this.btnAddFile.TabIndex = 17;
        	this.btnAddFile.Text = "Add File";
        	this.btnAddFile.UseVisualStyleBackColor = true;
        	this.btnAddFile.Click += new System.EventHandler(this.BtnAddFileClick);
        	// 
        	// btnAddFolder
        	// 
        	this.btnAddFolder.Location = new System.Drawing.Point(93, 60);
        	this.btnAddFolder.Name = "btnAddFolder";
        	this.btnAddFolder.Size = new System.Drawing.Size(75, 23);
        	this.btnAddFolder.TabIndex = 18;
        	this.btnAddFolder.Text = "Add Folder";
        	this.btnAddFolder.UseVisualStyleBackColor = true;
        	this.btnAddFolder.Click += new System.EventHandler(this.BtnAddFolderClick);
        	// 
        	// toolStripMain
        	// 
        	this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        	this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.toolStripBtnOpen,
        	        	        	this.toolStripBtnSave});
        	this.toolStripMain.Location = new System.Drawing.Point(0, 24);
        	this.toolStripMain.Name = "toolStripMain";
        	this.toolStripMain.Size = new System.Drawing.Size(631, 25);
        	this.toolStripMain.TabIndex = 19;
        	this.toolStripMain.Text = "toolStrip1";
        	// 
        	// toolStripBtnOpen
        	// 
        	this.toolStripBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnOpen.Image")));
        	this.toolStripBtnOpen.ImageTransparentColor = System.Drawing.Color.White;
        	this.toolStripBtnOpen.Name = "toolStripBtnOpen";
        	this.toolStripBtnOpen.Size = new System.Drawing.Size(23, 22);
        	this.toolStripBtnOpen.Text = "toolStripButton1";
        	this.toolStripBtnOpen.Click += new System.EventHandler(this.ToolStripBtnOpenClick);
        	// 
        	// toolStripBtnSave
        	// 
        	this.toolStripBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnSave.Image")));
        	this.toolStripBtnSave.ImageTransparentColor = System.Drawing.Color.White;
        	this.toolStripBtnSave.Name = "toolStripBtnSave";
        	this.toolStripBtnSave.Size = new System.Drawing.Size(23, 22);
        	this.toolStripBtnSave.Text = "toolStripButton1";
        	this.toolStripBtnSave.Click += new System.EventHandler(this.ToolStripBtnSaveClick);
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(631, 362);
        	this.Controls.Add(this.toolStripMain);
        	this.Controls.Add(this.btnAddFolder);
        	this.Controls.Add(this.btnAddFile);
        	this.Controls.Add(this.itemList);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.comboCheckSumType);
        	this.Controls.Add(this.btnVerify);
        	this.Controls.Add(this.menuStrip1);
        	this.Controls.Add(this.btnClear);
        	this.Controls.Add(this.btnRemove);
        	this.Controls.Add(this.btnProcess);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MainMenuStrip = this.menuStrip1;
        	this.Name = "MainForm";
        	this.Text = "CheckSum Tool";
        	this.menuStrip1.ResumeLayout(false);
        	this.menuStrip1.PerformLayout();
        	this.toolStripMain.ResumeLayout(false);
        	this.toolStripMain.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripBtnSave;
        private System.Windows.Forms.ToolStripButton toolStripBtnOpen;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.ComboBox comboCheckSumType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.ToolStripMenuItem aboutSHA1ToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListView itemList;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
