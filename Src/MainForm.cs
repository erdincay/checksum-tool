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
        /// Do the file list has calculated sums?
        /// I.e. do it need to be cleared before calculating new sums?
        /// </summary>
        bool _listHasSums;
        
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
                this.comboCheckSumType.Items.Add(name);
            }
            
            this.comboCheckSumType.SelectedIndex = 0;
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
        /// Create checksum calculator instance for selected checksum type.
        /// </summary>
        /// <returns>Checksum calculator instance.</returns>
        ICheckSum CreateSumCalculator()
        {
            int sumSelection = comboCheckSumType.SelectedIndex;
            
            ICheckSum impl = CheckSumImplList.GetImplementation(
                (CheckSumImplList.SumImplementation) sumSelection);
            return impl;
        }
        
        /// <summary>
        /// Called when user clicks Process -button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnProcessClick(object sender, EventArgs e)
        {
            if (itemList.Items.Count == 0)
                return;

            ListView.ListViewItemCollection items = itemList.Items;
            
            int sumSelection = comboCheckSumType.SelectedIndex;
            
            ICheckSum sum = CreateSumCalculator();
            _checksumItemList.CalcSums(sum);
            
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
        }
        
        /// <summary>
        /// Called when user selects the Remove-button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnRemoveClick(object sender, EventArgs e)
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
        /// Called when user selects the Clear-button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnClearClick(object sender, EventArgs e)
        {
            ClearAllItems();
            _listHasSums = false;
        }
        
        /// <summary>
        /// Removes all items from list and from GUI.
        /// </summary>
        void ClearAllItems()
        {
            itemList.Items.Clear();
            _checksumItemList.RemoveAll();
        }
        
        /// <summary>
        /// Opens About box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AboutSHA1ToolToolStripMenuItemClick(object sender, EventArgs e)
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
        /// Verify checksums for listed files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnVerifyClick(object sender, EventArgs e)
        {
            if (itemList.Items.Count == 0)
                return;
            
            ListView.ListViewItemCollection items = itemList.Items;
            
            ICheckSum sum = CreateSumCalculator();
            _checksumItemList.CheckSums(sum);
            
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
        /// Asks user filename to use when saving the checksums and saves
        /// checksums to the file.
        /// </summary>
        void SaveFile()
        {
            if (_checksumItemList.FileList.Count == 0 ||
                _listHasSums == false)
            {
                MessageBox.Show(this, "No checksums to save!", "CheckSum Tool",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "MD5 File (*.MD5)|*.MD5 ";
            dlg.Filter += "|Simple File Verification File (*.SFV)|*.SFV ";
            dlg.Filter += "|SHA-1 File (*.sha1)|*.sha1";
            dlg.AddExtension = true;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    FileInfo fi = new FileInfo(dlg.FileName);
                    if (dlg.FilterIndex == 1)
                    {
                        MD5File sumfile = new MD5File();
                        sumfile.SetFileList(_checksumItemList);
                        sumfile.WriteFile(fi.FullName);
                    }
                    else if (dlg.FilterIndex == 2)
                    {
                        SFVFile sumfile = new SFVFile();
                        sumfile.SetFileList(_checksumItemList);
                        sumfile.WriteFile(fi.FullName);
                    }
                    else if (dlg.FilterIndex == 3)
                    {
                        Sha1File sumfile = new Sha1File();
                        sumfile.SetFileList(_checksumItemList);
                        sumfile.WriteFile(fi.FullName);
                    }
                    else
                    {
                        MessageBox.Show(this, "Unknown checksum file type!",
                            "CheckSum Tool", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
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
                int ind = comboCheckSumType.FindStringExact("MD5");
                comboCheckSumType.SelectedIndex = ind;
            }
            else if (ext == ".sfv")
            {
                SFVFile sumfile = new SFVFile();
                newSumFile = sumfile;
                int ind = comboCheckSumType.FindStringExact("CRC32");
                comboCheckSumType.SelectedIndex = ind;
            }
            else if (ext == ".sha1")
            {
                Sha1File sumfile = new Sha1File();
                newSumFile = sumfile;
                int ind = comboCheckSumType.FindStringExact("SHA-1");
                comboCheckSumType.SelectedIndex = ind;
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
                int ind = comboCheckSumType.FindStringExact("MD5");
                comboCheckSumType.SelectedIndex = ind;
            }
            else if (ext == ".sfv")
            {
                int ind = comboCheckSumType.FindStringExact("CRC32");
                comboCheckSumType.SelectedIndex = ind;
            }
            else if (ext == ".sha1")
            {
                int ind = comboCheckSumType.FindStringExact("SHA-1");
                comboCheckSumType.SelectedIndex = ind;
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
        void MenuIFileOpenClick(object sender, EventArgs e)
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
        /// Add a file to list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnAddFileClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            string path = "";
            if (res == DialogResult.OK)
            {
                path = dlg.FileName;
                _checksumItemList.AddFile(path);
            }
            if (path.Length > 0)
            {
                AddItemToList(path);
            }
        }
        
        /// <summary>
        /// Add files in folder to the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnAddFolderClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            string path = "";
            if (res == DialogResult.OK)
            {
                path = dlg.SelectedPath;
                _checksumItemList.AddFolder(path);
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();
                
                foreach (FileInfo fi in files)
                {
                    AddItemToList(fi.FullName);
                }
            }
        }
        
        /// <summary>
        /// Called when checksum type selection changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ComboCheckSumTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listHasSums)
                ClearSums();
            
            int sumSelection = comboCheckSumType.SelectedIndex;
            SetListSumType((CheckSumImplList.SumImplementation) sumSelection);
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
    }
}
