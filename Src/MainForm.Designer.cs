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
        	this.itemList = new System.Windows.Forms.ListView();
        	this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        	this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        	this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        	this.mainMenuFileAddFile = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuFileAddFolder = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        	this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuEditMenu = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuEditClear = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuEditRemoveSelected = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuChecksumsCalculateAll = new System.Windows.Forms.ToolStripMenuItem();
        	this.mainMenuChecksumsVerifyAll = new System.Windows.Forms.ToolStripMenuItem();
        	this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.aboutSHA1ToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMain = new System.Windows.Forms.ToolStrip();
        	this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
        	this.toolStripBtnSave = new System.Windows.Forms.ToolStripButton();
        	this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        	this.toolStripBtnCalculate = new System.Windows.Forms.ToolStripButton();
        	this.toolStripBtnVerify = new System.Windows.Forms.ToolStripButton();
        	this.toolStripComboSumTypes = new System.Windows.Forms.ToolStripComboBox();
        	this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        	this.statusbarLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
        	this.statusbarLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
        	this.menuStrip1.SuspendLayout();
        	this.toolStripMain.SuspendLayout();
        	this.statusStrip1.SuspendLayout();
        	this.SuspendLayout();
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
        	this.itemList.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.itemList.FullRowSelect = true;
        	this.itemList.Location = new System.Drawing.Point(0, 49);
        	this.itemList.MultiSelect = false;
        	this.itemList.Name = "itemList";
        	this.itemList.ShowItemToolTips = true;
        	this.itemList.Size = new System.Drawing.Size(631, 281);
        	this.itemList.TabIndex = 2;
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
        	// menuStrip1
        	// 
        	this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.fileToolStripMenuItem,
        	        	        	this.mainMenuEditMenu,
        	        	        	this.toolStripMenuItem1,
        	        	        	this.aboutToolStripMenuItem});
        	this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        	this.menuStrip1.Name = "menuStrip1";
        	this.menuStrip1.Size = new System.Drawing.Size(631, 24);
        	this.menuStrip1.TabIndex = 0;
        	this.menuStrip1.Text = "menuStrip1";
        	// 
        	// fileToolStripMenuItem
        	// 
        	this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.mainMenuFileOpen,
        	        	        	this.mainMenuFileSave,
        	        	        	this.toolStripSeparator2,
        	        	        	this.mainMenuFileAddFile,
        	        	        	this.mainMenuFileAddFolder,
        	        	        	this.toolStripSeparator1,
        	        	        	this.exitToolStripMenuItem});
        	this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        	this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
        	this.fileToolStripMenuItem.Text = "&File";
        	// 
        	// mainMenuFileOpen
        	// 
        	this.mainMenuFileOpen.AutoToolTip = true;
        	this.mainMenuFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileOpen.Image")));
        	this.mainMenuFileOpen.ImageTransparentColor = System.Drawing.Color.White;
        	this.mainMenuFileOpen.Name = "mainMenuFileOpen";
        	this.mainMenuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
        	this.mainMenuFileOpen.Size = new System.Drawing.Size(163, 22);
        	this.mainMenuFileOpen.Text = "Open...";
        	this.mainMenuFileOpen.Click += new System.EventHandler(this.MenuIFileOpenClick);
        	// 
        	// mainMenuFileSave
        	// 
        	this.mainMenuFileSave.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileSave.Image")));
        	this.mainMenuFileSave.ImageTransparentColor = System.Drawing.Color.White;
        	this.mainMenuFileSave.Name = "mainMenuFileSave";
        	this.mainMenuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
        	this.mainMenuFileSave.Size = new System.Drawing.Size(163, 22);
        	this.mainMenuFileSave.Text = "Save...";
        	this.mainMenuFileSave.Click += new System.EventHandler(this.MenuFileSaveClick);
        	// 
        	// toolStripSeparator2
        	// 
        	this.toolStripSeparator2.Name = "toolStripSeparator2";
        	this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
        	// 
        	// mainMenuFileAddFile
        	// 
        	this.mainMenuFileAddFile.Name = "mainMenuFileAddFile";
        	this.mainMenuFileAddFile.Size = new System.Drawing.Size(163, 22);
        	this.mainMenuFileAddFile.Text = "Add Files...";
        	this.mainMenuFileAddFile.Click += new System.EventHandler(this.MainMenuFileAddFileClick);
        	// 
        	// mainMenuFileAddFolder
        	// 
        	this.mainMenuFileAddFolder.Name = "mainMenuFileAddFolder";
        	this.mainMenuFileAddFolder.Size = new System.Drawing.Size(163, 22);
        	this.mainMenuFileAddFolder.Text = "Add Folder...";
        	this.mainMenuFileAddFolder.Click += new System.EventHandler(this.MainMenuFileAddFolderClick);
        	// 
        	// toolStripSeparator1
        	// 
        	this.toolStripSeparator1.Name = "toolStripSeparator1";
        	this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
        	// 
        	// exitToolStripMenuItem
        	// 
        	this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        	this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
        	this.exitToolStripMenuItem.Text = "&Exit";
        	this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
        	// 
        	// mainMenuEditMenu
        	// 
        	this.mainMenuEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.mainMenuEditClear,
        	        	        	this.mainMenuEditRemoveSelected});
        	this.mainMenuEditMenu.Name = "mainMenuEditMenu";
        	this.mainMenuEditMenu.Size = new System.Drawing.Size(37, 20);
        	this.mainMenuEditMenu.Text = "&Edit";
        	// 
        	// mainMenuEditClear
        	// 
        	this.mainMenuEditClear.Name = "mainMenuEditClear";
        	this.mainMenuEditClear.Size = new System.Drawing.Size(168, 22);
        	this.mainMenuEditClear.Text = "&Clear";
        	this.mainMenuEditClear.Click += new System.EventHandler(this.ClearToolStripMenuItemClick);
        	// 
        	// mainMenuEditRemoveSelected
        	// 
        	this.mainMenuEditRemoveSelected.Name = "mainMenuEditRemoveSelected";
        	this.mainMenuEditRemoveSelected.Size = new System.Drawing.Size(168, 22);
        	this.mainMenuEditRemoveSelected.Text = "&Remove Selected";
        	this.mainMenuEditRemoveSelected.Click += new System.EventHandler(this.MainMenuEditRemoveSelectedClick);
        	// 
        	// toolStripMenuItem1
        	// 
        	this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.mainMenuChecksumsCalculateAll,
        	        	        	this.mainMenuChecksumsVerifyAll});
        	this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        	this.toolStripMenuItem1.Size = new System.Drawing.Size(72, 20);
        	this.toolStripMenuItem1.Text = "&Checksums";
        	// 
        	// mainMenuChecksumsCalculateAll
        	// 
        	this.mainMenuChecksumsCalculateAll.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuChecksumsCalculateAll.Image")));
        	this.mainMenuChecksumsCalculateAll.Name = "mainMenuChecksumsCalculateAll";
        	this.mainMenuChecksumsCalculateAll.ShortcutKeys = System.Windows.Forms.Keys.F5;
        	this.mainMenuChecksumsCalculateAll.Size = new System.Drawing.Size(162, 22);
        	this.mainMenuChecksumsCalculateAll.Text = "&Calculate All";
        	this.mainMenuChecksumsCalculateAll.Click += new System.EventHandler(this.MainMenuChecksumsCalculateAllClick);
        	// 
        	// mainMenuChecksumsVerifyAll
        	// 
        	this.mainMenuChecksumsVerifyAll.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuChecksumsVerifyAll.Image")));
        	this.mainMenuChecksumsVerifyAll.Name = "mainMenuChecksumsVerifyAll";
        	this.mainMenuChecksumsVerifyAll.ShortcutKeys = System.Windows.Forms.Keys.F6;
        	this.mainMenuChecksumsVerifyAll.Size = new System.Drawing.Size(162, 22);
        	this.mainMenuChecksumsVerifyAll.Text = "&Verify All";
        	this.mainMenuChecksumsVerifyAll.Click += new System.EventHandler(this.MainMenuChecksumsVerifyAllClick);
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
        	// toolStripMain
        	// 
        	this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.toolStripBtnOpen,
        	        	        	this.toolStripBtnSave,
        	        	        	this.toolStripSeparator3,
        	        	        	this.toolStripBtnCalculate,
        	        	        	this.toolStripBtnVerify,
        	        	        	this.toolStripComboSumTypes});
        	this.toolStripMain.Location = new System.Drawing.Point(0, 24);
        	this.toolStripMain.Name = "toolStripMain";
        	this.toolStripMain.Size = new System.Drawing.Size(631, 25);
        	this.toolStripMain.TabIndex = 1;
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
        	this.toolStripBtnOpen.ToolTipText = "Open Sum File";
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
        	this.toolStripBtnSave.ToolTipText = "Save Sum File";
        	this.toolStripBtnSave.Click += new System.EventHandler(this.ToolStripBtnSaveClick);
        	// 
        	// toolStripSeparator3
        	// 
        	this.toolStripSeparator3.Name = "toolStripSeparator3";
        	this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
        	// 
        	// toolStripBtnCalculate
        	// 
        	this.toolStripBtnCalculate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripBtnCalculate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnCalculate.Image")));
        	this.toolStripBtnCalculate.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripBtnCalculate.Name = "toolStripBtnCalculate";
        	this.toolStripBtnCalculate.Size = new System.Drawing.Size(23, 22);
        	this.toolStripBtnCalculate.Text = "toolStripButton1";
        	this.toolStripBtnCalculate.ToolTipText = "Calculate Sums";
        	this.toolStripBtnCalculate.Click += new System.EventHandler(this.ToolStripBtnCalculateClick);
        	// 
        	// toolStripBtnVerify
        	// 
        	this.toolStripBtnVerify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripBtnVerify.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnVerify.Image")));
        	this.toolStripBtnVerify.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripBtnVerify.Name = "toolStripBtnVerify";
        	this.toolStripBtnVerify.Size = new System.Drawing.Size(23, 22);
        	this.toolStripBtnVerify.Text = "toolStripButton2";
        	this.toolStripBtnVerify.ToolTipText = "Verify Sums";
        	this.toolStripBtnVerify.Click += new System.EventHandler(this.ToolStripBtnVerifyClick);
        	// 
        	// toolStripComboSumTypes
        	// 
        	this.toolStripComboSumTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.toolStripComboSumTypes.Name = "toolStripComboSumTypes";
        	this.toolStripComboSumTypes.Size = new System.Drawing.Size(75, 25);
        	this.toolStripComboSumTypes.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboSumTypesSelectedIndexChanged);
        	// 
        	// statusStrip1
        	// 
        	this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.statusbarLabel1,
        	        	        	this.statusbarLabelCount});
        	this.statusStrip1.Location = new System.Drawing.Point(0, 308);
        	this.statusStrip1.Name = "statusStrip1";
        	this.statusStrip1.Size = new System.Drawing.Size(631, 22);
        	this.statusStrip1.TabIndex = 3;
        	this.statusStrip1.Text = "statusStrip1";
        	// 
        	// statusbarLabel1
        	// 
        	this.statusbarLabel1.Name = "statusbarLabel1";
        	this.statusbarLabel1.Size = new System.Drawing.Size(507, 17);
        	this.statusbarLabel1.Spring = true;
        	this.statusbarLabel1.Text = "toolStripStatusLabel1";
        	this.statusbarLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// statusbarLabelCount
        	// 
        	this.statusbarLabelCount.Name = "statusbarLabelCount";
        	this.statusbarLabelCount.Size = new System.Drawing.Size(109, 17);
        	this.statusbarLabelCount.Text = "toolStripStatusLabel1";
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(631, 330);
        	this.Controls.Add(this.statusStrip1);
        	this.Controls.Add(this.itemList);
        	this.Controls.Add(this.toolStripMain);
        	this.Controls.Add(this.menuStrip1);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MainMenuStrip = this.menuStrip1;
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "MainForm";
        	this.Text = "CheckSum Tool";
        	this.menuStrip1.ResumeLayout(false);
        	this.menuStrip1.PerformLayout();
        	this.toolStripMain.ResumeLayout(false);
        	this.toolStripMain.PerformLayout();
        	this.statusStrip1.ResumeLayout(false);
        	this.statusStrip1.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.ToolStripStatusLabel statusbarLabelCount;
        private System.Windows.Forms.ToolStripStatusLabel statusbarLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChecksumsVerifyAll;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChecksumsCalculateAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboSumTypes;
        private System.Windows.Forms.ToolStripButton toolStripBtnVerify;
        private System.Windows.Forms.ToolStripButton toolStripBtnCalculate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mainMenuEditRemoveSelected;
        private System.Windows.Forms.ToolStripMenuItem mainMenuEditMenu;
        private System.Windows.Forms.ToolStripMenuItem mainMenuEditClear;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileAddFolder;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileAddFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileOpen;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripBtnSave;
        private System.Windows.Forms.ToolStripButton toolStripBtnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripMenuItem aboutSHA1ToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListView itemList;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
