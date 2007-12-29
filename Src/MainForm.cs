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
        /// User's last selected folder.
        /// We remember user's last selected folder and use that folder
        /// when opening new selection dialogs. Also sum file is most naturally
        /// saved to last selected folder.
        /// </summary>
        string _lastFolder;

        /// <summary>
        /// One and only instance of document having list of checksum items.
        /// </summary>
        SumDocument _document = new SumDocument();

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
            _document.SumType = CheckSumType.SHA1;
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
        /// Add item to the GUI item list.
        /// </summary>
        /// <param name="item">Checksum item to add.</param>
        void AddItemToList(CheckSumItem item)
        {
            ListViewItem listItem = itemList.Items.Add(item.FullPath,
                    item.FileName, "");
            string[] listItems = new string[(int)ListIndices.Count - 1];

            if (item.CheckSum != null)
            {
                listItems[(int)ListIndices.CheckSum - 1] =
                        item.CheckSum.ToString();
            }
            else
                listItems[(int)ListIndices.CheckSum - 1] = "";

            if (item.Verified)
                listItems[(int)ListIndices.Verified - 1] = "OK";
            else
                listItems[(int)ListIndices.Verified - 1] = "NO";

            listItems[(int)ListIndices.FullPath - 1] = item.FullPath;
            itemList.Items[listItem.Index].SubItems.AddRange(listItems);
        }

        /// <summary>
        /// Clears GUI list and adds items from docment to it.
        /// </summary>
        void UpdateGUIListFromDoc()
        {
            itemList.Items.Clear();
            foreach (CheckSumItem item in _document.Items.FileList)
            {
                AddItemToList(item);
            }
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

            _document.CalculateSums();

            UpdateGUIListFromDoc();
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
                _document.Items.Remove(itemList.Items[indices[i]].Name);
            }
            UpdateGUIListFromDoc();
        }

        /// <summary>
        /// Removes all items from list and from GUI.
        /// </summary>
        void ClearAllItems()
        {
            itemList.Items.Clear();
            _document.Items.RemoveAll();
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

            _document.VerifySums();
            UpdateGUIListFromDoc();

            bool allOk = true;
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
            if (_document.Items.FileList.Count == 0 ||
                _document.Items.HasCheckSums == false)
            {
                MessageBox.Show(this, "No checksums to save!", "CheckSum Tool",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (_document.Filename != null && _document.Filename.Length > 0)
                SaveFile(_document.Filename);
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
            dlg.Title = "Save the checksum list as";

            // Determine extension to select by default
            switch (_document.SumType)
            {
                case CheckSumType.CRC32:
                    dlg.FilterIndex = 2;
                    sumtype = 2;
                    break;

                case CheckSumType.MD5:
                    dlg.FilterIndex = 1;
                    sumtype = 1;
                    break;

                case CheckSumType.SHA1:
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

            _document.Filename = fileName;
            try
            {
                bool success = _document.SaveToFile();
                if (success)
                {
                    SetFilename(_document.Filename);
                }
                else
                {
                    string msg = "Unknown checksum type.";
                    MessageBox.Show(this, msg, "CheckSum Tool",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Select correct sumtype into the sumtype combobox, based on loaded
        /// sumfile type.
        /// </summary>
        /// <param name="fileType">File type loaded.</param>
        void SetSumTypeCombo(SumFileType fileType)
        {
            int ind;
            switch (fileType)
            {
                case SumFileType.MD5:
                    ind = toolStripComboSumTypes.FindStringExact("MD5");
                    toolStripComboSumTypes.SelectedIndex = ind;
                    break;

                case SumFileType.SFV:
                    ind = toolStripComboSumTypes.FindStringExact("CRC32");
                    toolStripComboSumTypes.SelectedIndex = ind;
                    break;
                case SumFileType.SHA1:
                    ind = toolStripComboSumTypes.FindStringExact("SHA-1");
                    toolStripComboSumTypes.SelectedIndex = ind;
                    break;

                default:
                    throw new NotImplementedException("Unknown checksum file type");
            }
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
            dlg.Filter = "MD5 Files (*.MD5)|*.MD5";
            dlg.Filter += "|Simple File Verification Files (*.SFV)|*.SFV";
            dlg.Filter += "|SHA-1 Files (*.sha1)|*.sha1";
            dlg.Filter += "|All Files (*.*)|*.*";
            dlg.Title = "Open checksum file";
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                SumFileType fileType;

                try
                {
                    fileType = SumFileUtils.FindFileType(dlg.FileName);
                }
                catch (FileNotFoundException ex)
                {
                    string message = "Cannot find/open the file:\n";
                    message += ex.FileName;
                    MessageBox.Show(this, message, "CheckSum Tool",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (fileType != SumFileType.Unknown)
                {
                    // Clear all items before adding new ones
                    ClearAllItems();

                    _document.Items.RemoveAll();
                    bool success = _document.LoadFile(dlg.FileName, fileType);

                    if (success)
                    {
                        UpdateGUIListFromDoc();
                        SetSumTypeCombo(fileType);
                        string statustext = string.Format("{0} items",
                                _document.Items.FileList.Count);
                        statusbarLabelCount.Text = statustext;

                        string filename = Path.GetFileName(dlg.FileName);
                        SetFilename(filename);
                    }
                    else
                    {
                        string message = "Cannot determine checksum file type.";
                        message += "\n\nPlease rename the file to have a proper";
                        message += "filename extension (.md5, .sfv, or .sha1)";
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
                    _document.Items.AddFile(path);
                }

                UpdateGUIListFromDoc();
                string statustext = string.Format("{0} items",
                    _document.Items.Count);
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
                _document.Items.AddFolder(path);

                UpdateGUIListFromDoc();
                string statustext = string.Format("{0} items",
                        _document.Items.Count);
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
        void SetListSumType(CheckSumType sumtype)
        {
            string colname = CheckSumImplList.GetImplementationName(sumtype);
            SetListColumnHeader(ListIndices.CheckSum, colname);
        }

        /// <summary>
        /// Clear checksums and verification status from list's items.
        /// </summary>
        void ClearSums()
        {
            _document.ClearAllSums();

            int itemcount = itemList.Items.Count;
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
             if (_document.Items.HasCheckSums)
                ClearSums();

            int sumSelection = toolStripComboSumTypes.SelectedIndex;
            SetCurrentSumType((CheckSumType) sumSelection);
        }

        /// <summary>
        /// Set current checksum type.
        /// </summary>
        /// <param name="sumtype">Checksumtype to set as current.</param>
        void SetCurrentSumType(CheckSumType sumtype)
        {
            _document.SumType = sumtype;
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
            _document.Filename = filename;
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
