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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using CheckSumTool.SumLib;
using CheckSumTool.Settings;
using CheckSumTool.Utils;

namespace CheckSumTool
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Relative path for folder where user manual is.
        /// </summary>
        private const string ManualFolder = "Manual";

        /// <summary>
        /// Filename of the user manual.
        /// </summary>
        private const string ManualFile = "Manual.html";

        /// <summary>
        /// Relative path to folder containing documents.
        /// </summary>
        private const string DocsFolder = "Docs";

        /// <summary>
        /// Filename for the contributors list file.
        /// </summary>
        private const string ContributorsFile = "Contributors.txt";

        /// <summary>
        /// A timer for updating GUI when doing background processing. This
        /// processing can be adding (lots of) items, calculating or verifying
        /// checksums.
        /// </summary>
        private System.Windows.Forms.Timer _progressTimer;

        /// <summary>
        /// A shared class for sharing progress data between backround
        /// processing and GUI.
        /// </summary>
        private ProgressInfo _progressInfo;

        private delegate void DelegateCalculateSums(ref ProgressInfo progressInfo);
        private delegate void DelegateAddFiles(ref ProgressInfo progressInfo, string path);

        /// <summary>
        /// Indices for list view columns
        /// </summary>
        private enum ListIndices
        {
            /// <summary>
            /// Column for the filename.
            /// </summary>
            FileName,

            /// <summary>
            /// Column for the checksum.
            /// </summary>
            CheckSum,

            /// <summary>
            /// Column telling if the checksum is verified and if verification
            /// succeeded or failed.
            /// </summary>
            Verified,

            /// <summary>
            /// Column for item's full path.
            /// </summary>
            FullPath,

            /// <summary>
            /// Column for file size.
            /// </summary>
            Size,
            Count, // Keep this as last item to get count of items
        }

        /// <summary>
        /// User's last selected folder.
        /// We remember user's last selected folder and use that folder
        /// when opening new selection dialogs. Also sum file is most naturally
        /// saved to last selected folder.
        /// </summary>
        private string _lastFolder;

        /// <summary>
        /// One and only instance of document having list of checksum items.
        /// </summary>
        private SumDocument _document = new SumDocument();

        private XPathHandler _handler;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            CheckConfigFile();

            _progressTimer = new System.Windows.Forms.Timer();
            _progressTimer.Interval = 500;
            _progressTimer.Tick += new EventHandler(Timer_Tick);
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            InitChecksumsGui();

            statusbarLabel1.Text = String.Empty;
            statusbarLabelCount.Text = "0 items";
            this.toolStripComboSumTypes.SelectedIndex = 0;
            _document.SumType = CheckSumType.SHA1;

            InitNewList();

            // Setup idle even handler.
            Application.Idle += Application_Idle;
        }

        /// <summary>
        /// Checking if the config file exist and is of correct version. If
        /// the config file is not found or it is unknown version, then the
        /// default config file (from program folder) is copied as new config
        /// file.
        /// </summary>
        private void CheckConfigFile()
        {
            bool success = true;
            string appDataPath = EnvironmentUtils.GetAppDataPath();
            ConfigFile cfile = new ConfigFile(Application.StartupPath,
                    appDataPath);
            if (!cfile.FileExists())
                success = cfile.CreateDefaultFile();

            // Check config file version
            // If incompatible version found, overwrite it.
            if (!cfile.IsCompatibleVersion())
                success = cfile.ReplaceWithDefault();

            if (success)
                _handler = new XPathHandler(cfile.FilePath);
        }

        /// <summary>
        /// Event called when application becomes idle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// This idle event handler is used to set statuses of various GUI
        /// controls, based on program state. E.g. Save* controls are disabled
        /// when there is no changed data to save.
        /// </remarks>
        private void Application_Idle(Object sender, EventArgs e)
        {
            if (_document.Items.Count == 0)
            {
                toolStripBtnCalculate.Enabled = false;
                mainMenuChecksumsCalculateAll.Enabled = false;
            }
            else
            {
                toolStripBtnCalculate.Enabled = true;
                mainMenuChecksumsCalculateAll.Enabled = true;
            }

            // Verify-items are active only when there are checksums in the list
            if (_document.Items.HasCheckSums == false)
            {
                toolStripBtnVerify.Enabled = false;
                mainMenuChecksumsVerifyAll.Enabled = false;
            }
            else
            {
                toolStripBtnVerify.Enabled = true;
                mainMenuChecksumsVerifyAll.Enabled = true;
            }

            if (_document.Items.HasChanged)
            {
                toolStripBtnSave.Enabled = true;

                // Add star to caption if changed
                if (!this.Text.StartsWith("*"))
                    this.Text = this.Text.Insert(0, "* ");
            }
            else
            {
                toolStripBtnSave.Enabled = false;
                mainMenuFileSave.Enabled = false;

                // Remove star from caption it not changed
                if (this.Text.StartsWith("*"))
                    this.Text = this.Text.Remove(0, 2);
            }
        }

        /// <summary>
        /// Initialize various GUI items related to different checksums.
        /// </summary>
        private void InitChecksumsGui()
        {
            mainMenuChecksumsMenu.DropDownItems.Add(new ToolStripSeparator());

            foreach (string name in CheckSumImplList.SumNames)
            {
                // Add checksum name to toolbar dropdown
                toolStripComboSumTypes.Items.Add(name);

                // Add checksum name to Checksums -menu
                mainMenuChecksumsMenu.DropDownItems.Add(name, null,
                    new System.EventHandler(this.MenuCheckSumtype_OnClick));
            }
        }

        /// <summary>
        /// Initialize new file list.
        /// </summary>
        private void InitNewList()
        {
            ClearAllItems();
            //SetFilename(null);
            SetFilename(String.Empty);
            _document.Items.ResetChanges();
        }

        /// <summary>
        /// Called when of of checksum type menuitems is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MenuCheckSumtype_OnClick(System.Object sender,
                System.EventArgs e)
        {
            ToolStripMenuItem menuitem = (ToolStripMenuItem) sender;

            int ind = toolStripComboSumTypes.FindStringExact(menuitem.Text);
            toolStripComboSumTypes.SelectedIndex = ind;
        }

        /// <summary>
        /// Set the program title.
        /// </summary>
        /// <param name="filename">Filename to add to title.</param>
        private void SetTitle(string filename)
        {
            string title = filename + " - CheckSum Tool";
            this.Text = title;
        }

        /// <summary>
        /// Add item to the GUI item list.
        /// </summary>
        /// <param name="item">Checksum item to add.</param>
        private void AddItemToList(CheckSumItem item)
        {
            ListViewItem listItem = itemList.Items.Add(item.FullPath,
                    item.FileName, String.Empty);

            string[] listItems = new string[(int)ListIndices.Count - 1];

            if (item.CheckSum != null)
            {
                listItems[(int)ListIndices.CheckSum - 1] =
                        item.CheckSum.ToString();
            }
            else
                listItems[(int)ListIndices.CheckSum - 1] = String.Empty;

            switch (item.Verified)
            {
                case VerificationState.NotVerified:
                    listItems[(int)ListIndices.Verified - 1] = String.Empty;
                    break;
                case VerificationState.VerifyOK:
                    listItems[(int)ListIndices.Verified - 1] = "OK";
                    break;
                case VerificationState.VerifyFailed:
                    listItems[(int)ListIndices.Verified - 1] = "FAIL";
                    break;
                default:
                    throw new ApplicationException();
                    break;
            }

            if (item.Size != 0)
            {
                string size = Locality.GetShortFileSize(item.Size);
                listItems[(int)ListIndices.Size - 1] = size;
            }
            else
                listItems[(int)ListIndices.Size - 1] = String.Empty;

            listItems[(int)ListIndices.FullPath - 1] = item.FullPath;
            itemList.Items[listItem.Index].SubItems.AddRange(listItems);
        }

        /// <summary>
        /// Clears GUI list and adds items from docment to it.
        /// </summary>
        private void UpdateGUIListFromDoc()
        {
            itemList.Items.Clear();
            try
            {
                foreach (CheckSumItem item in _document.Items.FileList)
                {
                    AddItemToList(item);
                }
            }
            catch (Exception)
            {
                ///Raising when Stopping File Adding..
            }
        }

        /// <summary>
        /// Calculate checksums for items in the list.
        /// </summary>
        private void CalculateCheckSums()
        {
            if (itemList.Items.Count == 0)
                return;

            StartProgress("Calculating checksums...", false);
            statusbarLabelProgressBar.Maximum = _document.Items.Count;

            _progressInfo.DefaultSetting();
            _progressInfo.Start();

            DelegateCalculateSums delInstance = new DelegateCalculateSums(
                    _document.CalculateSums);
            delInstance.BeginInvoke(ref _progressInfo, null, null);
        }

        /// <summary>
        /// Removes selected items from the list.
        /// </summary>
        private void RemoveSelectedItems()
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
        private void ClearAllItems()
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
        private void AboutCheckSumToolMainMenuItemClick(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show(this);
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Verify that checksums in the list match to file's actual checksums.
        /// </summary>
        private void VerifyCheckSums()
        {
            if (itemList.Items.Count == 0)
                return;

            _progressInfo.DefaultSetting();
            _progressInfo.Start();

            StartProgress("Verifying checksums...", false);
            statusbarLabelProgressBar.Maximum = _document.Items.Count;

            DelegateCalculateSums delInstance = new DelegateCalculateSums(DoVerifyCheckSums);
            delInstance.BeginInvoke(ref _progressInfo, null, null);
        }

        /// <summary>
        /// Calculate checksums - this is the asynchronous method.
        /// </summary>
        /// <param name="progressInfo">Progress information.</param>
        private void DoVerifyCheckSums(ref ProgressInfo progressInfo)
        {
            bool allSucceeded = _document.VerifySums(ref progressInfo);

            if (allSucceeded)
            {
                progressInfo.Succeeded = ProgressInfo.Result.Success;
            }
            else
            {
                progressInfo.Succeeded = ProgressInfo.Result.PartialSuccess;
            }
        }

        /// <summary>
        /// Called when user selects File/Save from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFileSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Save the file with previously selected filename. Or if no selected
        /// filename, asks for it.
        /// </summary>
        private void SaveFile()
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
        private void SaveFileAs()
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
        private void SaveFile(string fileName)
        {
            // No changes, don't bother saving.
            if (!_document.Items.HasChanged)
                return;

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
                    _document.Items.ResetChanges();
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
        private void SetSumTypeCombo(SumFileType fileType)
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
        private void MenuFileOpenClick(object sender, EventArgs e)
        {
            LoadFile();
        }

        /// <summary>
        /// Shows Open-dialog for user to select the file to load. And opens
        /// the file user selected. If there are unsaved changes, we ask
        /// from user if one wants to save changes first.
        /// </summary>
        private void LoadFile()
        {
            if (_document.Items.HasChanged)
            {
                string message = "The list contains unsaved data.\n\n";
                message += "Do you want to save current list first?";
                DialogResult result = MessageBox.Show(this, message, "CheckSum Tool",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                switch (result)
                {
                    // User wants to cancel so exit from saving
                    case DialogResult.Cancel:
                        return;

                    // User does not want to change so just continue to opening
                    case DialogResult.No:
                        break;

                    // User wants to save
                    case DialogResult.Yes:
                        SaveFile();
                        break;
                }
            }

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

                        string filename = Path.GetFullPath(dlg.FileName);
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
        private void AddFile()
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
                _lastFolder = Path.GetDirectoryName(dlg.FileNames[0]);
            }
        }

        /// <summary>
        /// Asks user to select a folder whose contents will be added to the
        /// checksum list. Adds folder contents to the list. If there are
        /// subfolders, ask if the user wants to add all files in subfolders
        /// also.
        /// </summary>
        private void AddFolder()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder whose contents to add to the list.";
            dlg.SelectedPath = _lastFolder;
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                string path = dlg.SelectedPath;

                if (Directory.GetDirectories(path).Length > 0)
                {
                    const string Message = "The selected folder contains one " +
                        "or more subfolders. Do you want to add all files " +
                        "from all the subfolders?\n\nSelecting No adds only " +
                        "files in the selected folder.";
                    DialogResult result = MessageBox.Show(Message,
                            "CheckSum Tool", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                    // We can't start progress before showing the dialog!
                    StartProgress("Adding files...", true);
                    if (result == DialogResult.Yes)
                    {
                        DelegateAddFiles delInstance = new DelegateAddFiles(_document.Items.AddSubFolders);
                        delInstance.BeginInvoke(ref _progressInfo, path, null, null);
                    }
                    else
                    {
                        DelegateAddFiles delInstance = new DelegateAddFiles(_document.Items.AddFolder);
                        delInstance.BeginInvoke(ref _progressInfo, path, null, null);
                    }
                }
                else
                {
                    StartProgress("Adding files...", true);
                    DelegateAddFiles delInstance = new DelegateAddFiles(_document.Items.AddFolder);
                    delInstance.BeginInvoke(ref _progressInfo, path, null, null);
                }

                _lastFolder = path;
            }
        }

        /// <summary>
        /// Set the file list's column name.
        /// </summary>
        /// <param name="column">Column to change.</param>
        /// <param name="text">New column name.</param>
        private void SetListColumnHeader(ListIndices column, string text)
        {
            itemList.Columns[(int)column].Text = text;
        }

        /// <summary>
        /// Set the checsum tyle for GUI list.
        /// </summary>
        /// <param name="sumtype">Checksum type.</param>
        private void SetListSumType(CheckSumType sumtype)
        {
            string colname = CheckSumImplList.GetImplementationName(sumtype);
            SetListColumnHeader(ListIndices.CheckSum, colname);
        }

        /// <summary>
        /// Clear checksums and verification status from list's items.
        /// </summary>
        private void ClearSums()
        {
            _document.ClearAllSums();

            int itemcount = itemList.Items.Count;
            for (int i = 0; i < itemcount; i++)
            {
                itemList.Items[i].SubItems[(int)ListIndices.CheckSum].Text = String.Empty;
                itemList.Items[i].SubItems[(int)ListIndices.Verified].Text = String.Empty;
            }
        }

        /// <summary>
        /// Called when user selects Open-icon from toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnOpenClick(object sender, EventArgs e)
        {
            LoadFile();
        }

        /// <summary>
        /// Called when user selects Save-icon from toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Called when user selects File/Add File from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuFileAddFileClick(object sender, EventArgs e)
        {
            AddFile();
        }

        /// <summary>
        /// Called when user selects File/Add Folder from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuFileAddFolderClick(object sender, EventArgs e)
        {
            AddFolder();
        }

        /// <summary>
        /// Called when user selects Edit/Clear from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearToolStripMenuItemClick(object sender, EventArgs e)
        {
            ClearAllItems();
        }

        /// <summary>
        /// Called when user selects Edit/Remove Selected from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuEditRemoveSelectedClick(object sender, EventArgs e)
        {
            RemoveSelectedItems();
        }

        /// <summary>
        /// Called when user clicks Calculate-button in toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnCalculateClick(object sender, EventArgs e)
        {
            CalculateCheckSums();
        }

        /// <summary>
        /// Called when user clicks Verify-button in toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnVerifyClick(object sender, EventArgs e)
        {
            VerifyCheckSums();
        }

        /// <summary>
        /// Called when selection in checksum type combo is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBarSumTypesSelectionChanged(object sender, EventArgs e)
        {
            int sumSelection = toolStripComboSumTypes.SelectedIndex;
            SetCurrentSumType((CheckSumType) sumSelection);
        }

        /// <summary>
        /// Set current checksum type.
        /// </summary>
        /// <param name="sumtype">Checksumtype to set as current.</param>
        private void SetCurrentSumType(CheckSumType sumtype)
        {
            // If sumtype already matches, it means we are loading sum
            // file and so don't want to clear existing items!
            // TODO: This is a bit hacky fix - we'll need to handle this
            // sum type change in some better way.
            if (sumtype != _document.SumType)
            {
                // Clear existing sums as they are now of wrong type
                if (_document.Items.HasCheckSums)
                    ClearSums();

                _document.SumType = sumtype;
            }

            SetListSumType(sumtype);

            // Clear filename so we ask it while saving
            // and don't override file with wrong (old) name.
            SetFilename(String.Empty);
        }

        /// <summary>
        /// Called when user selects Checksums/Calculate All from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuChecksumsCalculateAllClick(object sender, EventArgs e)
        {
            CalculateCheckSums();
        }

        /// <summary>
        /// Called when user selects Checksums/Verify All from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuChecksumsVerifyAllClick(object sender, EventArgs e)
        {
            VerifyCheckSums();
        }

        /// <summary>
        /// Called when user selects File/New from menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuFileNewClick(object sender, EventArgs e)
        {
            InitNewList();
        }

        /// <summary>
        /// Set the current filename.
        /// </summary>
        /// <param name="filename">Filename to use, null or empty if no file.</param>
        private void SetFilename(string filename)
        {
            _document.Filename = filename;
            if (filename == null || filename == String.Empty)
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
        private void MainMenuFileSaveAsClick(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        /// <summary>
        /// Get full path to the manual file.
        /// </summary>
        /// <returns>Full path to the manual file.</returns>
        private static string GetManualFile()
        {
            string programPath = Assembly.GetExecutingAssembly().Location;
            string programFile = Path.GetFileName(programPath);
            int index = programPath.LastIndexOf(programFile);
            programPath = programPath.Remove(index);
            string manualRelative = Path.Combine(ManualFolder, ManualFile);
            string manual = Path.Combine(programPath, manualRelative);
            return manual;
        }

        /// <summary>
        /// Get full path to the contributors list file.
        /// </summary>
        /// <returns>Full path to the contributors list file.</returns>
        private static string GetContributorsFile()
        {
            string programPath = Assembly.GetExecutingAssembly().Location;
            string programFile = Path.GetFileName(programPath);
            int index = programPath.LastIndexOf(programFile);
            programPath = programPath.Remove(index);
            string contribsRelative = Path.Combine(DocsFolder, ContributorsFile);
            string contributors = Path.Combine(programPath, contribsRelative);
            return contributors;
        }

        /// <summary>
        /// Show the user manual for user in the browser.
        /// </summary>
        private static void OpenManual()
        {
            string manualPath = GetManualFile();
            FileInfo fi = new FileInfo(manualPath);
            if (fi.Exists)
            {
                // Format local URL for the manual file
                string path = "file://";
                path += fi.FullName;
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                string message = string.Format("Cannot find the manual file:\n{0}",
                        manualPath);
                MessageBox.Show(message, "CheckSum Tool", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when user selects Help/Manual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuHelpManualClick(object sender, EventArgs e)
        {
            OpenManual();
        }

        /// <summary>
        /// Opens a list of contributors.
        /// </summary>
        private static void OpenContributors()
        {
            string contribPath = GetContributorsFile();
            FileInfo fi = new FileInfo(contribPath);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(fi.FullName);
            }
            else
            {
                string message = string.Format("Cannot find the contributors file:\n{0}",
                        contribPath);
                MessageBox.Show(message, "CheckSum Tool", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Adds checkmark to current cheksum menuitem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuChecksumsMenuDropDownOpening(object sender, EventArgs e)
        {
            string name = CheckSumImplList.GetImplementationName(_document.SumType);
            foreach (ToolStripItem item in mainMenuChecksumsMenu.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menuitem = (ToolStripMenuItem)item;
                    foreach (string sum in CheckSumImplList.SumNames)
                    {
                        if (menuitem.Text == sum)
                            menuitem.CheckState = CheckState.Unchecked;
                    }

                    if (menuitem.Text == name)
                        menuitem.CheckState = CheckState.Checked;
                }
            }
        }

        /// <summary>
        /// Selects all items in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuEditSelectAllClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in itemList.Items)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// Called when user selects Edit/Copy from main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuEditCopyClick(object sender, EventArgs e)
        {
            CopySumsToClipboard();
        }

        /// <summary>
        /// Copy calculated and selected checksums to clipboard with filenames.
        /// </summary>
        private void CopySumsToClipboard()
        {
            ListView.SelectedIndexCollection selections = itemList.SelectedIndices;
            int[] indices = new int[selections.Count];
            StringBuilder selectedItems = new StringBuilder();

            foreach (int selindex in selections)
            {
                string filename = itemList.Items[selindex].SubItems[(int)ListIndices.FileName].Text;
                string sum = itemList.Items[selindex].SubItems[(int)ListIndices.CheckSum].Text;
                if (filename.Length > 0 && sum.Length > 0)
                {
                    selectedItems.Append(string.Format("{0}\t{1}{2}", sum,
                            filename, Environment.NewLine));
                }
            }

            if (selectedItems.Length > 0)
                Clipboard.SetText(selectedItems.ToString());
            else
                Clipboard.Clear();
        }

        /// <summary>
        /// Called when user selects Help / Contributors from main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuHelpContributorsClick(object sender, EventArgs e)
        {
            OpenContributors();
        }

        /// <summary>
        /// Called when Add File -toolbar button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnAddFilesClick(object sender, EventArgs e)
        {
            AddFile();
        }

        /// <summary>
        /// Called when Add Folder -toolbar button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnAddFoldersClick(object sender, EventArgs e)
        {
            AddFolder();
        }

        /// <summary>
        /// Called when item amount in the list is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemListSizeChanged(object sender, System.EventArgs e)
        {
            string statustext = string.Format("{0} items", _document.Items.Count);
            statusbarLabelCount.Text = statustext;
        }

        /// <summary>
        /// Called when MainForm is closing.
        /// Setting positio and visible values in user settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            // Setting MainForm ending values
            FormSetting mainFormSetting = new FormSetting(_handler);
            mainFormSetting.SaveSetting("MainForm",
                                        this.Location.X,
                                        this.Location.Y,
                                        this.Width,
                                        this.Height);
            // Setting toolStripFile ending values
            ToolbarSetting toolStripFileSetting = new ToolbarSetting(_handler);
            toolStripFileSetting.SaveSetting("toolStripFile",
                                             toolStripFile.Location.X,
                                             toolStripFile.Location.Y,
                                             toolStripFile.Visible);

            // Setting toolStripSums ending values
            ToolbarSetting toolStripSumsSetting = new ToolbarSetting(_handler);
            toolStripSumsSetting.SaveSetting("toolStripSums",
                                             toolStripSums.Location.X,
                                             toolStripSums.Location.Y,
                                             toolStripSums.Visible);

            //Setting settingStatusStrip1 ending values
            StatusbarSetting settingStatusStrip1 = new StatusbarSetting(_handler);
            settingStatusStrip1.SaveSetting("statusStrip1", statusStrip1.Visible);

            //Setting columnSize starting ending values
            ColumnSetting settingColumnSize = new ColumnSetting(_handler);
            settingColumnSize.SaveSetting("columnSize", columnSize.DisplayIndex, columnSize.Width);

            //Setting columnFullpath starting ending values
            ColumnSetting settingColumnFullpath = new ColumnSetting(_handler);
            settingColumnFullpath.SaveSetting("columnFullpath", columnFullpath.DisplayIndex, columnFullpath.Width);

            //Setting columnVerified starting ending values
            ColumnSetting settingColumnVerified = new ColumnSetting(_handler);
            settingColumnVerified.SaveSetting("columnVerified", columnVerified.DisplayIndex, columnVerified.Width);

            //Setting columnType starting ending values
            ColumnSetting settingColumnType = new ColumnSetting(_handler);
            settingColumnType.SaveSetting("columnType", columnType.DisplayIndex, columnType.Width);

            //Setting columnFilename starting ending values
            ColumnSetting settingColumnFilename = new ColumnSetting(_handler);
            settingColumnFilename.SaveSetting("columnFilename", columnFilename.DisplayIndex, columnFilename.Width);
        }

        /// <summary>
        /// Called when MainForm is starting.
        /// Setting positions for MainForm and Toolbars.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            // Setting MainForm starting values
            FormSetting mainFormSetting = new FormSetting(_handler);
            mainFormSetting.GetSetting("MainForm");

            if (mainFormSetting.X == -1 || mainFormSetting.Y == -1)
                StartPosition = FormStartPosition.CenterScreen;
            else
                Location = new Point(mainFormSetting.X, mainFormSetting.Y);

            Width = mainFormSetting.Width;
            Height = mainFormSetting.Height;

            // Setting toolStripFile starting values
            ToolbarSetting settingToolStripFile = new ToolbarSetting(_handler);
            settingToolStripFile.GetSetting("toolStripFile");

            if (settingToolStripFile.X == -1 || settingToolStripFile.Y == -1)
                toolStripFile.Location = new Point(3, 24);
            else
                toolStripFile.Location = new Point(settingToolStripFile.X, settingToolStripFile.Y);
            toolStripFile.Visible = settingToolStripFile.Visible;

            mainMenuViewToolbarsFile.Checked  = toolStripFile.Visible;
            contextMenuFile.Checked  = toolStripFile.Visible;

            // Setting toolStripSums starting values
            ToolbarSetting settingToolStripSums = new ToolbarSetting(_handler);
            settingToolStripSums.GetSetting("toolStripSums");

            if (settingToolStripSums.X == -1 || settingToolStripSums.Y == -1)
                toolStripSums.Location = new Point(3, 49);
            else
                toolStripSums.Location = new Point(settingToolStripSums.X, settingToolStripSums.Y);
            toolStripSums.Visible = settingToolStripSums.Visible;

            mainMenuViewToolbarsSums.Checked  = toolStripSums.Visible;
            contextMenuSums.Checked  = toolStripSums.Visible;

            // Setting statusStrip1 starting values
            StatusbarSetting settingStatusStrip1 = new StatusbarSetting(_handler);
            settingStatusStrip1.GetSetting("statusStrip1");

            statusStrip1.Visible = settingStatusStrip1.Visible;
            mainMenuViewStatusBar.Checked  = statusStrip1.Visible;

            // Setting columnSize starting values
            ColumnSetting settingColumnSize = new ColumnSetting(_handler);
            settingColumnSize.GetSetting("columnSize");

            columnSize.DisplayIndex = settingColumnSize.DisplayIndex;
            columnSize.Width = settingColumnSize.Width;

            // Setting columnFullpath starting values
            ColumnSetting settingColumnFullpath = new ColumnSetting(_handler);
            settingColumnFullpath.GetSetting("columnFullpath");

            columnFullpath.DisplayIndex = settingColumnFullpath.DisplayIndex;
            columnFullpath.Width = settingColumnFullpath.Width;

            //Setting columnVerified starting values
            ColumnSetting settingColumnVerified = new ColumnSetting(_handler);
            settingColumnVerified.GetSetting("columnVerified");

            columnVerified.DisplayIndex = settingColumnVerified.DisplayIndex;
            columnVerified.Width = settingColumnVerified.Width;

            // Setting columnType starting values
            ColumnSetting settingColumnType = new ColumnSetting(_handler);
            settingColumnType.GetSetting("columnType");

            columnType.DisplayIndex = settingColumnType.DisplayIndex;
            columnType.Width = settingColumnType.Width;

            // Setting columnFilename starting values
            ColumnSetting settingColumnFilename = new ColumnSetting(_handler);
            settingColumnFilename.GetSetting("columnFilename");

            columnFilename.DisplayIndex = settingColumnFilename.DisplayIndex;
            columnFilename.Width = settingColumnFilename.Width;
        }

        /// <summary>
        /// Called when MainMenuViewToolsFile is clicked.
        /// Setting Visible value for toolStripFile and Checked value for mainMenuViewToolbarsFile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuViewToolsFileClick(object sender, EventArgs e)
        {
            toolStripFile.Visible = !toolStripFile.Visible;
            mainMenuViewToolbarsFile.Checked  = toolStripFile.Visible;
            contextMenuFile.Checked  = toolStripFile.Visible;
        }

        /// <summary>
        /// Called when MainMenuViewToolsSums is clicked.
        /// Setting Visible value for toolStripSums and Checked value for mainMenuViewToolbarsSums.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuViewToolsSumsClick(object sender, EventArgs e)
        {
            toolStripSums.Visible = !toolStripSums.Visible;
            mainMenuViewToolbarsSums.Checked  = toolStripSums.Visible;
            contextMenuSums.Checked  = toolStripSums.Visible;
        }

        /// <summary>
        /// Called when mainMenuViewStatusBar is clicked.
        /// Setting Visible value for statusStrip1 and Checked value for mainMenuViewStatusBar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarToolStripMenuItemClick(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
            mainMenuViewStatusBar.Checked  = statusStrip1.Visible;
        }

        /// <summary>
        /// Sets up GUI for the asynchronous processing.
        /// </summary>
        /// <param name="message">Message to show in GUI.</param>
        /// <param name="noCount">Count of items not known, use the
        /// marquee style progress bar.</param>
        private void StartProgress(string message, bool noCount)
        {
            _progressTimer.Start();
            this.UseWaitCursor = true;
            statusbarLabel1.Text = message;

            if (noCount)
            {
                statusbarLabelProgressBar.ProgressBar.Style =
                        ProgressBarStyle.Marquee;
            }
            else
            {
                statusbarLabelProgressBar.ProgressBar.Style =
                        ProgressBarStyle.Blocks;
            }
            statusbarLabelCount.Visible = false;
            statusbarLabelProgressBar.Visible = true;
            EnableStop();

            if (_progressInfo == null)
                _progressInfo = new ProgressInfo();
        }

        /// <summary>
        /// Called when timer event is received.
        /// </summary>
        /// <param name="sender">The timer that fired.</param>
        /// <param name="args"></param>
        private void Timer_Tick(object sender, EventArgs args)
        {
            if (sender == _progressTimer)
            {
                OnProgressTimer();
            }
        }

        /// <summary>
        /// Called when progress timer is fired.
        /// </summary>
        private void OnProgressTimer()
        {
            if (_progressInfo.IsStopping())
            {
                _progressInfo.Stopped();
                EndProgress("Stopped.");

            }
            else
            {
                if (_progressInfo.IsRunning())
                {
                    if (statusbarLabelProgressBar.Value >= 100)
                        statusbarLabelProgressBar.Value = 0;

                    // If Max is 0, we have a marquee progress bar which
                    // updates itself.
                    if (_progressInfo.Max != 0)
                    {
                        statusbarLabelProgressBar.Value = _progressInfo.Now;
                        statusbarLabel1.Text = _progressInfo.Filename;
                    }
                }
                else if (_progressInfo.IsReady())
                {
                    OnProgressReady();
                }
            }
        }

        /// <summary>
        /// Called when progress finishes.
        /// </summary>
        private void OnProgressReady()
        {
            EndProgress("Ready.");

            switch (_progressInfo.Succeeded)
            {
                case ProgressInfo.Result.Failed:
                    break;

                case ProgressInfo.Result.PartialSuccess:
                    MessageBox.Show(this, "One ore more items could not be verified to match their checksums.",
                        "Verification Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    break;

                case ProgressInfo.Result.Success:
                    MessageBox.Show(this, "All items verified to match their checksums.",
                        "Verification Succeeded", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;

                default:
#if _DEBUG
                    throw new ApplicationException("Unknown result value");
#endif
                    break;
            }

        }

        /// <summary>
        /// Called when Timer_Tick is starting to progress process.
        /// </summary>
        private void EnableStop()
        {
            toolStripFile.Enabled = false;
            toolStripBtnCalculate.Enabled = false;
            toolStripBtnVerify.Enabled = false;
            toolStripComboSumTypes.Enabled = false;

            // Menu items
            foreach(object objectItem in fileToolStripMenuItem.DropDownItems)
            {
                if (objectItem is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = (ToolStripDropDownItem) objectItem;
                    item.Enabled = false;
                }
            }

            foreach (object objectItem in mainMenuEditMenu.DropDownItems)
            {
                if (objectItem is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = (ToolStripDropDownItem) objectItem;
                    item.Enabled = false;
                }
            }

            foreach (object objectItem in mainMenuChecksumsMenu.DropDownItems)
            {
                if (objectItem is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = (ToolStripDropDownItem) objectItem;
                    item.Enabled = false;
                }
            }

            exitToolStripMenuItem.Enabled = true;

            // Enable Stop buttons
            toolStripBtnStop.Enabled = true;
            mainMenuChecksumsStop.Enabled = true;
        }

        /// <summary>
        /// Called when Timer_Tick is Ready or Stopping process
        /// </summary>
        private void DisableStop()
        {
            toolStripFile.Enabled = true;
            toolStripBtnCalculate.Enabled = true;
            toolStripBtnVerify.Enabled = true;
            toolStripComboSumTypes.Enabled = true;

            // Menu items
            foreach (object objectItem in fileToolStripMenuItem.DropDownItems)
            {
                if (objectItem is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = (ToolStripDropDownItem) objectItem;
                    item.Enabled = true;
                }
            }

            foreach (object objectItem in mainMenuEditMenu.DropDownItems)
            {
                if (objectItem is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = (ToolStripDropDownItem) objectItem;
                    item.Enabled = true;
                }
            }

            foreach (object objectItem in mainMenuChecksumsMenu.DropDownItems)
            {
                if (objectItem is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = (ToolStripDropDownItem) objectItem;
                    item.Enabled = true;
                }
            }

            toolStripBtnStop.Enabled = false;
            mainMenuChecksumsStop.Enabled = false;
        }

        /// <summary>
        /// End asynchronous processing.
        /// </summary>
        /// <param name="text">Text shown in statusbar when done.</param>
        private void EndProgress(string text)
        {
            _progressTimer.Stop();
            statusbarLabelProgressBar.Visible = false;
            statusbarLabelProgressBar.Value = 0;
            statusbarLabel1.Text = text;
            statusbarLabelCount.Visible = true;
            UpdateGUIListFromDoc();
            DisableStop();
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Called when user clicks Stop-button in toolbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnStopClick(object sender, EventArgs e)
        {
            if (_progressInfo != null)
                _progressInfo.Stop();
        }

        /// <summary>
        /// Stop button event. Changing cursor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnStopMouseEnter(object sender, EventArgs e)
        {
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Stop button event. Changing cursor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripBtnStopMouseHover(object sender, EventArgs e)
        {
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Determine if d&d is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemListDragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// Handle dropping items to list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemListDragDrop(object sender, DragEventArgs e)
        {
            bool itemsAdded = false;
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string path = (string)e.Data.GetData(DataFormats.Text);
                _document.Items.Add(path, ref _progressInfo);
                itemsAdded = true;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Handle dropping files and folders from Windows Explorer.
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string path in paths)
                {
                    _document.Items.Add(path, ref _progressInfo);
                }
                itemsAdded = true;
            }

            if (itemsAdded)
                UpdateGUIListFromDoc();
        }
    }
}
