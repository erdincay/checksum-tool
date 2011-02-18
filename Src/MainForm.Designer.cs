/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
Copyright (c) 2007-2011 Kimmo Varis <kimmov@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.itemList = new System.Windows.Forms.ListView();
            this.columnFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVerified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFullpath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuFileAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuEditClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuEditRemoveSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuViewToolbars = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuViewToolbarsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuViewToolbarsSums = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuChecksumsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuChecksumsCalculateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuChecksumsVerifyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuChecksumsStop = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpManual = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpContributors = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFile = new System.Windows.Forms.ToolStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSums = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnAddFiles = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnAddFolders = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusbarLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusbarLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripSums = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnCalculate = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnVerify = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboSumTypes = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1.SuspendLayout();
            this.toolStripFile.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripSums.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemList
            // 
            this.itemList.AllowColumnReorder = true;
            this.itemList.AllowDrop = true;
            this.itemList.AutoArrange = false;
            this.itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFilename,
            this.columnType,
            this.columnVerified,
            this.columnFullpath,
            this.columnSize});
            this.itemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemList.FullRowSelect = true;
            this.itemList.Location = new System.Drawing.Point(0, 0);
            this.itemList.Name = "itemList";
            this.itemList.ShowItemToolTips = true;
            this.itemList.Size = new System.Drawing.Size(631, 234);
            this.itemList.TabIndex = 2;
            this.itemList.UseCompatibleStateImageBehavior = false;
            this.itemList.View = System.Windows.Forms.View.Details;
            this.itemList.SizeChanged += new System.EventHandler(this.ItemListSizeChanged);
            this.itemList.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemListDragDrop);
            this.itemList.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemListDragOver);
            // 
            // columnFilename
            // 
            this.columnFilename.Text = "Filename";
            this.columnFilename.Width = 150;
            // 
            // columnType
            // 
            this.columnType.Text = "SHA1";
            this.columnType.Width = 266;
            // 
            // columnVerified
            // 
            this.columnVerified.Text = "Verified";
            // 
            // columnFullpath
            // 
            this.columnFullpath.Text = "Full Path";
            this.columnFullpath.Width = 120;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Size";
            this.columnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnSize.Width = 80;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mainMenuEditMenu,
            this.mainMenuViewMenu,
            this.mainMenuChecksumsMenu,
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
            this.mainMenuFileNew,
            this.mainMenuFileOpen,
            this.toolStripSeparator5,
            this.mainMenuFileSave,
            this.mainMenuFileSaveAs,
            this.toolStripSeparator2,
            this.mainMenuFileAddFile,
            this.mainMenuFileAddFolder,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mainMenuFileNew
            // 
            this.mainMenuFileNew.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileNew.Image")));
            this.mainMenuFileNew.Name = "mainMenuFileNew";
            this.mainMenuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mainMenuFileNew.Size = new System.Drawing.Size(155, 22);
            this.mainMenuFileNew.Text = "&New";
            this.mainMenuFileNew.ToolTipText = "Create new checksum file";
            this.mainMenuFileNew.Click += new System.EventHandler(this.MainMenuFileNewClick);
            // 
            // mainMenuFileOpen
            // 
            this.mainMenuFileOpen.AutoToolTip = true;
            this.mainMenuFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileOpen.Image")));
            this.mainMenuFileOpen.ImageTransparentColor = System.Drawing.Color.White;
            this.mainMenuFileOpen.Name = "mainMenuFileOpen";
            this.mainMenuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mainMenuFileOpen.Size = new System.Drawing.Size(155, 22);
            this.mainMenuFileOpen.Text = "&Open...";
            this.mainMenuFileOpen.ToolTipText = "Open checksum file";
            this.mainMenuFileOpen.Click += new System.EventHandler(this.MenuFileOpenClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
            // 
            // mainMenuFileSave
            // 
            this.mainMenuFileSave.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileSave.Image")));
            this.mainMenuFileSave.ImageTransparentColor = System.Drawing.Color.White;
            this.mainMenuFileSave.Name = "mainMenuFileSave";
            this.mainMenuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mainMenuFileSave.Size = new System.Drawing.Size(155, 22);
            this.mainMenuFileSave.Text = "&Save...";
            this.mainMenuFileSave.ToolTipText = "Save checksum file";
            this.mainMenuFileSave.Click += new System.EventHandler(this.MenuFileSaveClick);
            // 
            // mainMenuFileSaveAs
            // 
            this.mainMenuFileSaveAs.Name = "mainMenuFileSaveAs";
            this.mainMenuFileSaveAs.Size = new System.Drawing.Size(155, 22);
            this.mainMenuFileSaveAs.Text = "Sa&ve As...";
            this.mainMenuFileSaveAs.ToolTipText = "Save the file with new name";
            this.mainMenuFileSaveAs.Click += new System.EventHandler(this.MainMenuFileSaveAsClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // mainMenuFileAddFile
            // 
            this.mainMenuFileAddFile.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileAddFile.Image")));
            this.mainMenuFileAddFile.Name = "mainMenuFileAddFile";
            this.mainMenuFileAddFile.Size = new System.Drawing.Size(155, 22);
            this.mainMenuFileAddFile.Text = "&Add Files...";
            this.mainMenuFileAddFile.ToolTipText = "Add files to checksum list";
            this.mainMenuFileAddFile.Click += new System.EventHandler(this.MainMenuFileAddFileClick);
            // 
            // mainMenuFileAddFolder
            // 
            this.mainMenuFileAddFolder.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuFileAddFolder.Image")));
            this.mainMenuFileAddFolder.Name = "mainMenuFileAddFolder";
            this.mainMenuFileAddFolder.Size = new System.Drawing.Size(155, 22);
            this.mainMenuFileAddFolder.Text = "A&dd Folder...";
            this.mainMenuFileAddFolder.ToolTipText = "Add files in folder to checsum list";
            this.mainMenuFileAddFolder.Click += new System.EventHandler(this.MainMenuFileAddFolderClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.ToolTipText = "Exit the program";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // mainMenuEditMenu
            // 
            this.mainMenuEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuEditCopy,
            this.mainMenuEditClear,
            this.mainMenuEditRemoveSelected,
            this.toolStripSeparator4,
            this.mainMenuEditSelectAll});
            this.mainMenuEditMenu.Name = "mainMenuEditMenu";
            this.mainMenuEditMenu.Size = new System.Drawing.Size(39, 20);
            this.mainMenuEditMenu.Text = "&Edit";
            // 
            // mainMenuEditCopy
            // 
            this.mainMenuEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuEditCopy.Image")));
            this.mainMenuEditCopy.Name = "mainMenuEditCopy";
            this.mainMenuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mainMenuEditCopy.Size = new System.Drawing.Size(188, 22);
            this.mainMenuEditCopy.Text = "&Copy";
            this.mainMenuEditCopy.ToolTipText = "Copy selected items to clipboard";
            this.mainMenuEditCopy.Click += new System.EventHandler(this.MainMenuEditCopyClick);
            // 
            // mainMenuEditClear
            // 
            this.mainMenuEditClear.Name = "mainMenuEditClear";
            this.mainMenuEditClear.Size = new System.Drawing.Size(188, 22);
            this.mainMenuEditClear.Text = "Cl&ear";
            this.mainMenuEditClear.ToolTipText = "Remove all the files from the list";
            this.mainMenuEditClear.Click += new System.EventHandler(this.ClearToolStripMenuItemClick);
            // 
            // mainMenuEditRemoveSelected
            // 
            this.mainMenuEditRemoveSelected.Name = "mainMenuEditRemoveSelected";
            this.mainMenuEditRemoveSelected.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mainMenuEditRemoveSelected.Size = new System.Drawing.Size(188, 22);
            this.mainMenuEditRemoveSelected.Text = "&Remove Selected";
            this.mainMenuEditRemoveSelected.ToolTipText = "Remove selected items from the list";
            this.mainMenuEditRemoveSelected.Click += new System.EventHandler(this.MainMenuEditRemoveSelectedClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(185, 6);
            // 
            // mainMenuEditSelectAll
            // 
            this.mainMenuEditSelectAll.Name = "mainMenuEditSelectAll";
            this.mainMenuEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mainMenuEditSelectAll.Size = new System.Drawing.Size(188, 22);
            this.mainMenuEditSelectAll.Text = "&Select All";
            this.mainMenuEditSelectAll.ToolTipText = "Select all items in the list";
            this.mainMenuEditSelectAll.Click += new System.EventHandler(this.MainMenuEditSelectAllClick);
            // 
            // mainMenuViewMenu
            // 
            this.mainMenuViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuViewToolbars,
            this.mainMenuViewStatusBar});
            this.mainMenuViewMenu.Name = "mainMenuViewMenu";
            this.mainMenuViewMenu.Size = new System.Drawing.Size(44, 20);
            this.mainMenuViewMenu.Text = "&View";
            // 
            // mainMenuViewToolbars
            // 
            this.mainMenuViewToolbars.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuViewToolbarsFile,
            this.mainMenuViewToolbarsSums});
            this.mainMenuViewToolbars.Name = "mainMenuViewToolbars";
            this.mainMenuViewToolbars.Size = new System.Drawing.Size(126, 22);
            this.mainMenuViewToolbars.Text = "Toolbars";
            // 
            // mainMenuViewToolbarsFile
            // 
            this.mainMenuViewToolbarsFile.Name = "mainMenuViewToolbarsFile";
            this.mainMenuViewToolbarsFile.Size = new System.Drawing.Size(103, 22);
            this.mainMenuViewToolbarsFile.Text = "File";
            this.mainMenuViewToolbarsFile.Click += new System.EventHandler(this.MainMenuViewToolsFileClick);
            // 
            // mainMenuViewToolbarsSums
            // 
            this.mainMenuViewToolbarsSums.Name = "mainMenuViewToolbarsSums";
            this.mainMenuViewToolbarsSums.Size = new System.Drawing.Size(103, 22);
            this.mainMenuViewToolbarsSums.Text = "Sums";
            this.mainMenuViewToolbarsSums.Click += new System.EventHandler(this.MainMenuViewToolsSumsClick);
            // 
            // mainMenuViewStatusBar
            // 
            this.mainMenuViewStatusBar.Name = "mainMenuViewStatusBar";
            this.mainMenuViewStatusBar.Size = new System.Drawing.Size(126, 22);
            this.mainMenuViewStatusBar.Text = "Status Bar";
            this.mainMenuViewStatusBar.Click += new System.EventHandler(this.StatusBarToolStripMenuItemClick);
            // 
            // mainMenuChecksumsMenu
            // 
            this.mainMenuChecksumsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuChecksumsCalculateAll,
            this.mainMenuChecksumsVerifyAll,
            this.mainMenuChecksumsStop});
            this.mainMenuChecksumsMenu.Name = "mainMenuChecksumsMenu";
            this.mainMenuChecksumsMenu.Size = new System.Drawing.Size(80, 20);
            this.mainMenuChecksumsMenu.Text = "&Checksums";
            this.mainMenuChecksumsMenu.DropDownOpening += new System.EventHandler(this.MainMenuChecksumsMenuDropDownOpening);
            // 
            // mainMenuChecksumsCalculateAll
            // 
            this.mainMenuChecksumsCalculateAll.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuChecksumsCalculateAll.Image")));
            this.mainMenuChecksumsCalculateAll.Name = "mainMenuChecksumsCalculateAll";
            this.mainMenuChecksumsCalculateAll.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mainMenuChecksumsCalculateAll.Size = new System.Drawing.Size(159, 22);
            this.mainMenuChecksumsCalculateAll.Text = "&Calculate All";
            this.mainMenuChecksumsCalculateAll.ToolTipText = "Calculate checksum for all files in the list";
            this.mainMenuChecksumsCalculateAll.Click += new System.EventHandler(this.MainMenuChecksumsCalculateAllClick);
            // 
            // mainMenuChecksumsVerifyAll
            // 
            this.mainMenuChecksumsVerifyAll.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuChecksumsVerifyAll.Image")));
            this.mainMenuChecksumsVerifyAll.Name = "mainMenuChecksumsVerifyAll";
            this.mainMenuChecksumsVerifyAll.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mainMenuChecksumsVerifyAll.Size = new System.Drawing.Size(159, 22);
            this.mainMenuChecksumsVerifyAll.Text = "&Verify All";
            this.mainMenuChecksumsVerifyAll.ToolTipText = "Verify checksum of all files in the list";
            this.mainMenuChecksumsVerifyAll.Click += new System.EventHandler(this.MainMenuChecksumsVerifyAllClick);
            // 
            // mainMenuChecksumsStop
            // 
            this.mainMenuChecksumsStop.Enabled = false;
            this.mainMenuChecksumsStop.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuChecksumsStop.Image")));
            this.mainMenuChecksumsStop.Name = "mainMenuChecksumsStop";
            this.mainMenuChecksumsStop.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.mainMenuChecksumsStop.Size = new System.Drawing.Size(159, 22);
            this.mainMenuChecksumsStop.Text = "&Stop";
            this.mainMenuChecksumsStop.ToolTipText = "Stop the current processing";
            this.mainMenuChecksumsStop.Click += new System.EventHandler(this.ToolStripBtnStopClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuHelpManual,
            this.mainMenuHelpContributors,
            this.mainMenuHelpAbout});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // mainMenuHelpManual
            // 
            this.mainMenuHelpManual.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuHelpManual.Image")));
            this.mainMenuHelpManual.Name = "mainMenuHelpManual";
            this.mainMenuHelpManual.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mainMenuHelpManual.Size = new System.Drawing.Size(194, 22);
            this.mainMenuHelpManual.Text = "&User Manual";
            this.mainMenuHelpManual.ToolTipText = "Open user manual";
            this.mainMenuHelpManual.Click += new System.EventHandler(this.MainMenuHelpManualClick);
            // 
            // mainMenuHelpContributors
            // 
            this.mainMenuHelpContributors.Name = "mainMenuHelpContributors";
            this.mainMenuHelpContributors.Size = new System.Drawing.Size(194, 22);
            this.mainMenuHelpContributors.Text = "&Contributors";
            this.mainMenuHelpContributors.ToolTipText = "Shows a list of contributors";
            this.mainMenuHelpContributors.Click += new System.EventHandler(this.MainMenuHelpContributorsClick);
            // 
            // mainMenuHelpAbout
            // 
            this.mainMenuHelpAbout.Name = "mainMenuHelpAbout";
            this.mainMenuHelpAbout.Size = new System.Drawing.Size(194, 22);
            this.mainMenuHelpAbout.Text = "&About CheckSum Tool";
            this.mainMenuHelpAbout.ToolTipText = "Show the about box";
            this.mainMenuHelpAbout.Click += new System.EventHandler(this.AboutCheckSumToolMainMenuItemClick);
            // 
            // toolStripFile
            // 
            this.toolStripFile.ContextMenuStrip = this.contextMenuStrip1;
            this.toolStripFile.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnOpen,
            this.toolStripBtnSave,
            this.toolStripSeparator3,
            this.toolStripBtnAddFiles,
            this.toolStripBtnAddFolders});
            this.toolStripFile.Location = new System.Drawing.Point(3, 24);
            this.toolStripFile.Name = "toolStripFile";
            this.toolStripFile.Size = new System.Drawing.Size(110, 25);
            this.toolStripFile.TabIndex = 1;
            this.toolStripFile.Text = "toolStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuFile,
            this.contextMenuSums});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(111, 48);
            // 
            // contextMenuFile
            // 
            this.contextMenuFile.Name = "contextMenuFile";
            this.contextMenuFile.Size = new System.Drawing.Size(110, 22);
            this.contextMenuFile.Text = "File";
            this.contextMenuFile.Click += new System.EventHandler(this.MainMenuViewToolsFileClick);
            // 
            // contextMenuSums
            // 
            this.contextMenuSums.Name = "contextMenuSums";
            this.contextMenuSums.Size = new System.Drawing.Size(110, 22);
            this.contextMenuSums.Text = "Sums";
            this.contextMenuSums.Click += new System.EventHandler(this.MainMenuViewToolsSumsClick);
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
            // toolStripBtnAddFiles
            // 
            this.toolStripBtnAddFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnAddFiles.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnAddFiles.Image")));
            this.toolStripBtnAddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAddFiles.Name = "toolStripBtnAddFiles";
            this.toolStripBtnAddFiles.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnAddFiles.Text = "toolStripButton1";
            this.toolStripBtnAddFiles.ToolTipText = "Add files to the list";
            this.toolStripBtnAddFiles.Click += new System.EventHandler(this.ToolStripBtnAddFilesClick);
            // 
            // toolStripBtnAddFolders
            // 
            this.toolStripBtnAddFolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnAddFolders.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnAddFolders.Image")));
            this.toolStripBtnAddFolders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAddFolders.Name = "toolStripBtnAddFolders";
            this.toolStripBtnAddFolders.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnAddFolders.Text = "toolStripButton2";
            this.toolStripBtnAddFolders.ToolTipText = "Add files in folder to the list";
            this.toolStripBtnAddFolders.Click += new System.EventHandler(this.ToolStripBtnAddFoldersClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusbarLabel1,
            this.statusbarLabelCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusbarLabel1
            // 
            this.statusbarLabel1.Name = "statusbarLabel1";
            this.statusbarLabel1.Size = new System.Drawing.Size(616, 17);
            this.statusbarLabel1.Spring = true;
            this.statusbarLabel1.Text = "toolStripStatusLabel1";
            this.statusbarLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusbarLabelCount
            // 
            this.statusbarLabelCount.Name = "statusbarLabelCount";
            this.statusbarLabelCount.Size = new System.Drawing.Size(118, 17);
            this.statusbarLabelCount.Text = "toolStripStatusLabel1";
            this.statusbarLabelCount.Visible = false;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.itemList);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(631, 234);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(631, 330);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripFile);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripSums);
            // 
            // toolStripSums
            // 
            this.toolStripSums.ContextMenuStrip = this.contextMenuStrip1;
            this.toolStripSums.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripSums.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnCalculate,
            this.toolStripBtnVerify,
            this.toolStripBtnStop,
            this.toolStripComboSumTypes});
            this.toolStripSums.Location = new System.Drawing.Point(3, 49);
            this.toolStripSums.Name = "toolStripSums";
            this.toolStripSums.Size = new System.Drawing.Size(158, 25);
            this.toolStripSums.TabIndex = 2;
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
            // toolStripBtnStop
            // 
            this.toolStripBtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnStop.Enabled = false;
            this.toolStripBtnStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnStop.Image")));
            this.toolStripBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnStop.Name = "toolStripBtnStop";
            this.toolStripBtnStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnStop.Text = "toolStripBtnStop";
            this.toolStripBtnStop.ToolTipText = "Stop";
            this.toolStripBtnStop.Click += new System.EventHandler(this.ToolStripBtnStopClick);
            this.toolStripBtnStop.MouseEnter += new System.EventHandler(this.ToolStripBtnStopMouseEnter);
            this.toolStripBtnStop.MouseHover += new System.EventHandler(this.ToolStripBtnStopMouseHover);
            // 
            // toolStripComboSumTypes
            // 
            this.toolStripComboSumTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboSumTypes.Name = "toolStripComboSumTypes";
            this.toolStripComboSumTypes.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboSumTypes.SelectedIndexChanged += new System.EventHandler(this.ToolBarSumTypesSelectionChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 330);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckSum Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripFile.ResumeLayout(false);
            this.toolStripFile.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripSums.ResumeLayout(false);
            this.toolStripSums.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ColumnHeader columnFilename;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnVerified;
        private System.Windows.Forms.ColumnHeader columnFullpath;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChecksumsStop;
        private System.Windows.Forms.ToolStripButton toolStripBtnStop;
        private System.Windows.Forms.ToolStripMenuItem mainMenuViewStatusBar;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSums;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuViewToolbars;
        private System.Windows.Forms.ToolStripMenuItem mainMenuViewToolbarsFile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuViewToolbarsSums;
        private System.Windows.Forms.ToolStripMenuItem mainMenuViewMenu;
        private System.Windows.Forms.ToolStrip toolStripFile;
        private System.Windows.Forms.ToolStrip toolStripSums;
        private System.Windows.Forms.ToolStripButton toolStripBtnAddFolders;
        private System.Windows.Forms.ToolStripButton toolStripBtnAddFiles;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpContributors;
        private System.Windows.Forms.ToolStripMenuItem mainMenuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mainMenuEditSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChecksumsMenu;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpManual;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileNew;
        private System.Windows.Forms.ToolStripStatusLabel statusbarLabelCount;
        private System.Windows.Forms.ToolStripStatusLabel statusbarLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChecksumsVerifyAll;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChecksumsCalculateAll;
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
        private System.Windows.Forms.ToolStripButton toolStripBtnSave;
        private System.Windows.Forms.ToolStripButton toolStripBtnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListView itemList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}
