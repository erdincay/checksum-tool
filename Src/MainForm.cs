/*
The MIT License

Copyright (c) 2007 Ixonos Plc, Kimmo Varis <kimmo.varis@ixonos.com>

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

// $Id$

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CheckSumTool
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Relative path to manual file to open.
        /// </summary>
        static readonly string ManualFile = "Manual/Manual.html";

        /// <summary>
        /// Indices for list view columns
        /// </summary>
        enum ListIndices
        {
            FileName,
            CheckSum,
            Verified,
            FullPath,
            Count, // Keep this as last item to get count of items
        }

        /// <summary>
        /// List of files to handle.
        /// </summary>
        CheckSumFileList _checksumItemList = new CheckSumFileList();

        /// <summary>
        /// Currently selected/active checksum type.
        /// </summary>
        CheckSumImplList.SumImplementation _currentSumType;

        /// <summary>
        /// Do the file list has calculated sums?
        /// I.e. do it need to be cleared before calculating new sums?
        /// </summary>
        bool _listHasSums;

        /// <summary>
        /// User's last selected folder.
        /// We remember user's last selected folder and use that folder
        /// when opening new selection dialogs. Also sum file is most naturally
        /// saved to last selected folder.
        /// </summary>
        string _lastFolder;

        /// <summary>
        /// Current filename (full path) for the checksum file.
        /// </summary>
        string _filename;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            foreach (string name in CheckSumImplList.SumNames)
            {
                this.toolStripComboSumTypes.Items.Add(name);
            }

            statusbarLabel1.Text = "";
            statusbarLabelCount.Text = "0 items";
            this.toolStripComboSumTypes.SelectedIndex = 0;
            _currentSumType = CheckSumImplList.SumImplementation.SHA1;
            SetFilename("");
        }

        /// <summary>
        /// Set the program title.
        /// </summary>
        /// <param name="filename">Filename to add to title.</param>
        void SetTitle(string filename)
        {
            string title = filename + " - CheckSum Tool";
            this.Text = title;
        }

        /// <summary>
        /// Add new item to GUI list.
        /// </summary>
        /// <param name="fullpath">Full path to the file.</param>
        void AddItemToList(string fullpath)
        {
            AddItemToList(fullpath, "");
        }

        /// <summary>
        /// Add new item to the GUI list.
        /// </summary>
        /// <param name="fullpath">Full path to the file.</param>
        /// <param name="checksum">Checksum of the file.</param>
        void AddItemToList(string fullpath, string checksum)
        {
            FileInfo fi = new FileInfo(fullpath);
            ListViewItem listItem = itemList.Items.Add(fullpath, fi.Name, "");
            string[] listItems = new string[(int)ListIndices.Count - 1];
            listItems[(int)ListIndices.CheckSum - 1] = checksum;
            listItems[(int)ListIndices.Verified - 1] = "";
            listItems[(int)ListIndices.FullPath - 1] = fi.FullName;
            itemList.Items[listItem.Index].SubItems.AddRange(listItems);
        }

        /// <summary>
        /// Calculate checksums for items in the list.
        /// </summary>
        void CalculateCheckSums()
        {
            if (itemList.Items.Count == 0)
                return;

            this.UseWaitCursor = true;
            statusbarLabel1.Text = "Calculating checksums...";
            ListView.ListViewItemCollection items = itemList.Items;

            int sumSelection = toolStripComboSumTypes.SelectedIndex;

            Calculator sumCalculator = new Calculator(_currentSumType);
            sumCalculator.Calculate(_checksumItemList.FileList);

            List<CheckSumItem> list = _checksumItemList.FileList;

            foreach (CheckSumItem fi in list)
            {
                int index = itemList.Items.IndexOfKey(fi.FullPath);
                if (index != -1)
                {
                    itemList.Items[index].SubItems[(int)ListIndices.CheckSum].Text = fi.CheckSum.ToString();
                }
            }
            _listHasSums = true;
            statusbarLabel1.Text = "Ready.";
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Removes selected items from the list.
        /// </summary>
        void RemoveSelectedItems()
        {
            ListView.SelectedIndexCollection selections = itemList.SelectedIndices;
            int[] indices = new int[selections.Count];
            int ind = 0;

            // Put selected indexes in reversed order to new table
            foreach (int selindex in selections)
            {
                indices[selections.Count - ind - 1] = selindex;
                ind++;
            }
            for (int i = 0; i < indices.Length; i++)
            {
                _checksumItemList.Remove(itemList.Items[indices[i]].Name);
                itemList.Items.RemoveAt(indices[i]);
            }
        }

        /// <summary>
        /// Removes all items from list and from GUI.
        /// </summary>
        void ClearAllItems()
        {
            itemList.Items.Clear();
            _checksumItemList.RemoveAll();
            _listHasSums = false;
            statusbarLabelCount.Text = "0 items";
        }

        /// <summary>
        /// Opens About box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AboutCheckSumToolMainMenuItemClick(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show(this);
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Verify that checksums in the list match to file's actual checksums.
        /// </summary>
        void VerifyCheckSums()
        {
            if (itemList.Items.Count == 0)
                return;

            this.UseWaitCursor = true;
            statusbarLabel1.Text = "Verifying checksums...";
            ListView.ListViewItemCollection items = itemList.Items;

            Calculator sumCalculator = new Calculator(_currentSumType);
            sumCalculator.Verify(_checksumItemList.FileList);

            List<CheckSumItem> list = _checksumItemList.FileList;

            bool allOk = true;
            foreach (CheckSumItem fi in list)
            {
                int index = itemList.Items.IndexOfKey(fi.FullPath);
                if (index != -1)
                {
                    if (fi.Verified)
                        itemList.Items[index].SubItems[(int)ListIndices.Verified].Text = "OK";
                    else
                    {
                        itemList.Items[index].SubItems[(int)ListIndices.Verified].Text = "NOK";
                        allOk = false;
                    }
                }
            }

            if (allOk)
            {
                MessageBox.Show(this, "All items verified to match checksum.",
                                "Verification Succeeded", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "One ore more items could not be verified to match checksum.",
                                "Verification Failed", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            _listHasSums = true;
            statusbarLabel1.Text = "Ready.";
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Called when user selects File/Save from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuFileSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Save the file with previously selected filename. Or if no selected
        /// filename, asks for it.
        /// </summary>
        void SaveFile()
        {
            if (_checksumItemList.FileList.Count == 0 || _listHasSums == false)
            {
                MessageBox.Show(this, "No checksums to save!", "CheckSum Tool",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (_filename != null && _filename.Length > 0)
                SaveFile(_filename);
            else
                SaveFileAs();
        }

        /// <summary>
        /// Asks user filename to use when saving the checksums and saves
        /// checksums to the file.
        /// </summary>
        void SaveFileAs()
        {
            int sumtype = 0;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "MD5 File (*.MD5)|*.MD5";
            dlg.Filter += "|Simple File Verification File (*.SFV)|*.SFV";
            dlg.Filter += "|SHA-1 File (*.sha1)|*.sha1";
            dlg.AddExtension = true;
            dlg.InitialDirectory = _lastFolder;

            // Determine extension to select by default
            switch (_currentSumType)
            {
                case CheckSumImplList.SumImplementation.CRC32:
                    dlg.FilterIndex = 2;
                    sumtype = 2;
                    break;

                case CheckSumImplList.SumImplementation.MD5:
                    dlg.FilterIndex = 1;
                    sumtype = 1;
                    break;

                case CheckSumImplList.SumImplementation.SHA1:
                    dlg.FilterIndex = 3;
                    sumtype = 3;
                    break;

                default:
                    break;
            }

            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (sumtype != dlg.FilterIndex)
                {
                    string message = "Incompatible checksum type and filetype!";
                    MessageBox.Show(this, message, "CheckSum Tool",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileInfo fi = new FileInfo(dlg.FileName);
                SaveFile(fi.FullName);
            }
        }

        /// <summary>
        /// Save the file with given filename (full path).
        /// </summary>
        /// <param name="fileName">Filename to save to (full path).</param>
        void SaveFile(string fileName)
        {
            if (fileName == null || fileName.Length == 0)
            {
                MessageBox.Show(this, "No filename to save!",
                    "CheckSum Tool", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                switch (_currentSumType)
                {
                    case CheckSumImplList.SumImplementation.CRC32:
                        SFVFile SvfSumfile = new SFVFile();
                        SvfSumfile.SetFileList(_checksumItemList);
                        SvfSumfile.WriteFile(fileName);
                        SetFilename(fileName);
                        break;

                    case CheckSumImplList.SumImplementation.MD5:
                        MD5File Md5Sumfile = new MD5File();
                        Md5Sumfile.SetFileList(_checksumItemList);
                        Md5Sumfile.WriteFile(fileName);
                        SetFilename(fileName);
                        break;

                    case CheckSumImplList.SumImplementation.SHA1:
                        Sha1File Sha1Sumfile = new Sha1File();
                        Sha1Sumfile.SetFileList(_checksumItemList);
                        Sha1Sumfile.WriteFile(fileName);
                        SetFilename(fileName);
                        break;

                    default:
                        MessageBox.Show(this, "Unknown checksum file type!",
                                    "CheckSum Tool", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                string msg = "Failed to write the checksum file.\n\n";
                msg += "Maybe you don't have write priviledges to the folder?";
                MessageBox.Show(this, msg, "CheckSum Tool",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Create sumfile instance for checksum file.
        /// </summary>
        /// <param name="ext">File extension for sum file.</param>
        /// <returns>ISumFile instance.</returns>
        ISumFile InitSumFile(string ext)
        {
            ISumFile newSumFile = null;
            ext = ext.ToLower();
            if (ext == ".md5")
            {
                MD5File sumfile = new MD5File();
                newSumFile = sumfile;
                int ind = toolStripComboSumTypes.FindStringExact("MD5");
                toolStripComboSumTypes.SelectedIndex = ind;
            }
            else if (ext == ".sfv")
            {
                SFVFile sumfile = new SFVFile();
                newSumFile = sumfile;
                int ind = toolStripComboSumTypes.FindStringExact("CRC32");
                toolStripComboSumTypes.SelectedIndex = ind;
            }
            else if (ext == ".sha1")
            {
                Sha1File sumfile = new Sha1File();
                newSumFile = sumfile;
                int ind = toolStripComboSumTypes.FindStringExact("SHA-1");
                toolStripComboSumTypes.SelectedIndex = ind;
            }
            return newSumFile;
        }

        /// <summary>
        /// Select correct sumtype into the sumtype combobox, based on loaded
        /// sumfile.
        /// </summary>
        /// <param name="ext">File extension of the loaded file.</param>
        /// <remarks>
        /// Currently this method uses filename extension, but it should
        /// use real sumfile type instead. As extension in some cases does not
        /// tell the file type.
        /// </remarks>
        void SetSumTypeCombo(string ext)
        {
            if (ext == ".md5")
            {
                int ind = toolStripComboSumTypes.FindStringExact("MD5");
                toolStripComboSumTypes.SelectedIndex = ind;
            }
            else if (ext == ".sfv")
            {
                int ind = toolStripComboSumTypes.FindStringExact("CRC32");
                toolStripComboSumTypes.SelectedIndex = ind;
            }
            else if (ext == ".sha1")
            {
                int ind = toolStripComboSumTypes.FindStringExact("SHA-1");
                toolStripComboSumTypes.SelectedIndex = ind;
            }
        }

        /// <summary>
        /// Checks that given path and filename exists.
        /// </summary>
        /// <param name="filename">Filename to check.</param>
        /// <param name="fullpath">Returns full absolute path to the file.</param>
        /// <param name="ext">Returns filename's extension.</param>
        /// <returns>true if file and path exists, false otherwise.</returns>
        bool CheckFileAndPathExists(string filename, out string fullpath,
            out string ext)
        {
            bool retval = true;

            FileInfo fi = null;
            try
            {
                fi = new FileInfo(filename);
                fullpath = fi.FullName;
                ext = fi.Extension;
            }
            catch (Exception)
            {
                String msg = "Cannot open selected file!\n\n";
                msg += "Please select a valid checksum file!";
                MessageBox.Show(this, msg, "CheckSum Tool", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                fullpath = "";
                ext = "";
                retval = false;
            }
            return retval;
        }

        /// <summary>
        /// Called when user selecs Open-item from File-menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuFileOpenClick(object sender, EventArgs e)
        {
            LoadFile();
        }

        /// <summary>
        /// Shows Open-dialog for user to select the file to load. And opens
        /// the file user selected.
        /// </summary>
        void LoadFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                string fullpath;
                string ext;
                bool fileOk = CheckFileAndPathExists(dlg.FileName,
                    out fullpath, out ext);

                if (fileOk)
                {
                    // Clear all items before adding new ones
                    ClearAllItems();

                    ISumFile newSumFile = InitSumFile(ext);

                    if (newSumFile != null)
                    {
                        newSumFile.SetFileList(_checksumItemList);
                        int items = newSumFile.ReadFile(fullpath);

                        List<CheckSumItem> itemlist = _checksumItemList.FileList;
                        foreach (CheckSumItem it in itemlist)
                        {
                            AddItemToList(it.FullPath, it.CheckSum.ToString());
                        }

                        SetSumTypeCombo(ext);
                        _listHasSums = true;
                        string statustext = string.Format("{0} items", items);
                        statusbarLabelCount.Text = statustext;

                        string filename = Path.GetFileName(dlg.FileName);
                        SetFilename(filename);
                    }
                    else
                    {
                        MessageBox.Show(this, "Unknown checksum file type!",
                            "CheckSum Tool", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Asks user to select one or more files to add to checksum list.
        /// </summary>
        void AddFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Title = "Select files to add to the list";
            dlg.InitialDirectory = _lastFolder;

            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                foreach (string path in dlg.FileNames)
                {
                    _checksumItemList.AddFile(path);

                    if (path.Length > 0)
                    {
                        AddItemToList(path);
                    }
                }
                string statustext = string.Format("{0} items", _checksumItemList.Count);
                statusbarLabelCount.Text = statustext;
                _lastFolder = Path.GetDirectoryName(dlg.FileNames[0]);
            }
        }

        /// <summary>
        /// Asks user to select a folder whose contents will be added to the
        /// checksum list. Adds folder contents to the list.
        /// </summary>
        void AddFolder()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder whose contents to add to the list.";
            dlg.SelectedPath = _lastFolder;
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                string path = dlg.SelectedPath;
                _checksumItemList.AddFolder(path);
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();

                foreach (FileInfo fi in files)
                {
                    AddItemToList(fi.FullName);
                }
                string statustext = string.Format("{0} items", _checksumItemList.Count);
                statusbarLabelCount.Text = statustext;
                _lastFolder = path;
            }
        }

        /// <summary>
        /// Set the file list's column name.
        /// </summary>
        /// <param name="column">Column to change.</param>
        /// <param name="text">New column name.</param>
        void SetListColumnHeader(ListIndices column, string text)
        {
            itemList.Columns[(int)column].Text = text;
        }

        /// <summary>
        /// Set the checsum tyle for GUI list.
        /// </summary>
        /// <param name="sumtype">Checksum type.</param>
        void SetListSumType(CheckSumImplList.SumImplementation sumtype)
        {
            string colname = CheckSumImplList.GetImplementationName(sumtype);
            SetListColumnHeader(ListIndices.CheckSum, colname);
        }

        /// <summary>
        /// Clear checksums and verification status from list's items.
        /// </summary>
        void ClearSums()
        {
            List<CheckSumItem> list = _checksumItemList.FileList;
            foreach (CheckSumItem fi in list)
            {
                fi.CheckSum.Clear();
                fi.Verified = false;
            }

            int itemcount = itemList.Items.Count;
            //foreach (ListViewItem li in itemList)
            for (int i = 0; i < itemcount; i++)
            {
                itemList.Items[i].SubItems[(int)ListIndices.CheckSum].Text = "";
                itemList.Items[i].SubItems[(int)ListIndices.Verified].Text = "";
            }
        }

        /// <summary>
        /// Called when user selects Open-icon from toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToolStripBtnOpenClick(object sender, EventArgs e)
        {
            LoadFile();
        }

        /// <summary>
        /// Called when user selects Save-icon from toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToolStripBtnSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Called when user selects File/Add File from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuFileAddFileClick(object sender, EventArgs e)
        {
            AddFile();
        }

        /// <summary>
        /// Called when user selects File/Add Folder from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuFileAddFolderClick(object sender, EventArgs e)
        {
            AddFolder();
        }

        /// <summary>
        /// Called when user selects Edit/Clear from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClearToolStripMenuItemClick(object sender, EventArgs e)
        {
            ClearAllItems();
        }

        /// <summary>
        /// Called when user selects Edit/Remove Selected from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuEditRemoveSelectedClick(object sender, EventArgs e)
        {
            RemoveSelectedItems();
        }

        /// <summary>
        /// Called when user clicks Calculate-button in toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToolStripBtnCalculateClick(object sender, EventArgs e)
        {
            CalculateCheckSums();
        }

        /// <summary>
        /// Called when user clicks Verify-button in toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToolStripBtnVerifyClick(object sender, EventArgs e)
        {
            VerifyCheckSums();
        }

        /// <summary>
        /// Called when selection in checksum type combo is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToolStripComboSumTypesSelectedIndexChanged(object sender, EventArgs e)
        {
             if (_listHasSums)
                ClearSums();

            int sumSelection = toolStripComboSumTypes.SelectedIndex;
            SetCurrentSumType((CheckSumImplList.SumImplementation) sumSelection);
        }

        /// <summary>
        /// Set current checksum type.
        /// </summary>
        /// <param name="sumtype">Checksumtype to set as current.</param>
        void SetCurrentSumType(CheckSumImplList.SumImplementation sumtype)
        {
            _currentSumType = sumtype;
            SetListSumType(sumtype);
        }

        /// <summary>
        /// Called when user selects Checksums/Calculate All from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuChecksumsCalculateAllClick(object sender, EventArgs e)
        {
            CalculateCheckSums();
        }

        /// <summary>
        /// Called when user selects Checksums/Verify All from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuChecksumsVerifyAllClick(object sender, EventArgs e)
        {
            VerifyCheckSums();
        }

        /// <summary>
        /// Called when user selects File/New from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuFileNewClick(object sender, EventArgs e)
        {
            ClearAllItems();
            SetFilename("");

        }

        /// <summary>
        /// Set the current filename.
        /// </summary>
        /// <param name="filename">Filename to use, null or empty if no file.</param>
        void SetFilename(string filename)
        {
            _filename = filename;
            if (filename == null || filename == "")
            {
                SetTitle("Untitled");
            }
            else
            {
                string file = Path.GetFileName(filename);
                SetTitle(file);
            }
        }

        /// <summary>
        /// Called when user selects File/Save As from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuFileSaveAsClick(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        /// <summary>
        /// Show the user manual for user in the browser.
        /// </summary>
        static void OpenManual()
        {
            FileInfo fi = new FileInfo(ManualFile);
            if (fi.Exists)
            {
                // Format local URL for the manual file
                string path = "file://";
                path += fi.FullName;
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                string message = string.Format("Cannot find file:\n{0}", ManualFile);
                MessageBox.Show(message, "CheckSum Tool", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when user selects Help/Manual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMenuHelpManualClick(object sender, EventArgs e)
        {
            OpenManual();
        }
    }
}
