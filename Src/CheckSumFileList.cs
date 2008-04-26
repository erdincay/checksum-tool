/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
Copyright (c) 2007-2008 Kimmo Varis <kimmov@winmerge.org>

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
using System.IO;

namespace CheckSumTool
{
    /// <summary>
    /// This class calculates checksums for list of files or verifies
    /// checksums against given checksums.
    /// </summary>
    public class CheckSumFileList
    {
        /// <summary>
        /// List of files to process.
        /// </summary>
        private List<CheckSumItem> _fileList = new List<CheckSumItem>();

        /// <summary>
        /// Has list contents been changed since last ResetChanges().
        /// </summary>
        private bool _listChanged;

        /// <summary>
        /// List of files to process.
        /// </summary>
        public List<CheckSumItem> FileList
        {
            get { return _fileList; }
        }

        /// <summary>
        /// Count of items in the list.
        /// </summary>
        public int Count
        {
            get { return _fileList.Count; }
        }

        /// <summary>
        /// Check if the list has calculated checksums.
        /// Returns true if one or more items have checksums,
        /// and false if all items are without checksum.
        /// </summary>
        public bool HasCheckSums
        {
            get
            {
                foreach (CheckSumItem item in _fileList)
                {
                    if (item.CheckSum != null)
                    {
                        byte[] data = item.CheckSum.GetAsByteArray();
                        if (data != null && data.Length > 0)
                            return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Return if list has been changed since last ResetChanges().
        /// </summary>
        public bool HasChanged
        {
            get { return _listChanged; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CheckSumFileList()
        {
        }

        /// <summary>
        /// Add file or folder (files in that folder) to list of files to
        /// process.
        /// </summary>
        /// <param name="path">File or folder path to add.</param>
        /// <param name="progressInfo">Returns information about progress.</param>
        public void Add(string path, ref ProgressInfo progressInfo)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            bool isDir = ((info.Attributes & FileAttributes.Directory) != 0);

            if (isDir)
            {
                AddFolder(path, ref progressInfo);
            }
            else
            {
                AddFile(path);
            }
        }

        /// <summary>
        /// Add file to list of items to process.
        /// </summary>
        /// <param name="file">Path to file to add.</param>
        public void AddFile(string file)
        {
            AddFile(file, "");
        }

        /// <summary>
        /// Add file and its checksum for verification.
        /// </summary>
        /// <param name="file">Path to file to add.</param>
        /// <param name="checksum">Checksum of the file.</param>
        public void AddFile(string file, string checksum)
        {
            CheckSumItem newItem = new CheckSumItem(file);

            FileInfo fileInfo = new FileInfo(file);
            newItem.Size = Convert.ToInt32(fileInfo.Length);

            if (checksum != null && checksum != "")
                newItem.SetSum(checksum);

            _listChanged = true;
            _fileList.Add(newItem);
        }

        /// <summary>
        /// Add files in folder to items to process.
        /// </summary>
        /// <param name="folder">Path to folder to add.</param>
        /// <param name="progressInfo">Returns information about progress.</param>
        public void AddFolder(ref ProgressInfo progressInfo, string path)
        {
            progressInfo.DefaultSetting();
            progressInfo.Start();

            AddFolder(path, ref progressInfo);

            progressInfo.Complete();
        }

        /// <summary>
        /// Add files in folder to items to process.
        /// </summary>
        /// <param name="folder">Path to folder to add.</param>
        /// <param name="progressInfo">Returns information about progress.</param>
        void AddFolder(string folder, ref ProgressInfo progressInfo)
        {
            DirectoryInfo info = new DirectoryInfo(folder);
            FileInfo[] files = info.GetFiles();

            _listChanged = true;
            foreach (FileInfo fi in files)
            {
                CheckSumItem newItem = new CheckSumItem(fi.FullName);

                FileInfo fileInfo = new FileInfo(fi.FullName);
                newItem.Size = Convert.ToInt32(fileInfo.Length);

                _fileList.Add(newItem);
            }
        }

        /// <summary>
        /// Add folder and it´s subfolders.
        /// </summary>
        /// <param name="folder">Path to folder to add.</param>
        /// <param name="progressInfo">Returns information about progress.</param>
        public void AddSubFolders(ref ProgressInfo progressInfo, string path)
        {
            progressInfo.DefaultSetting();
            progressInfo.Start();

            AddSubFolders(path, ref progressInfo);

            progressInfo.Complete();
        }

        /// <summary>
        /// Add folder and it´s subfolders.
        /// </summary>
        /// <param name="folder">Path to folder to add.</param>
        /// <param name="progressInfo">Returns information about progress.</param>
        void AddSubFolders(string folder, ref ProgressInfo progressInfo)
        {
            AddFolder(folder, ref progressInfo);

            string[] subFolders = Directory.GetDirectories(folder);

            for (int i = 0; i < subFolders.Length; i++)
            {
                string currentSubFolder = subFolders[i];
                try
                {
                    if (Directory.GetDirectories(currentSubFolder).Length > 0)
                        AddSubFolders(currentSubFolder, ref progressInfo);
                    else
                        AddFolder(currentSubFolder, ref progressInfo);
                }
                catch (Exception)
                {
                    //TODO: Tell user he did not get all subfolders.
                    //Some windows hidden folders throw this exception.
                }

                if(progressInfo.IsStopping())
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Remove file from items to process.
        /// </summary>
        /// <param name="path">File to remove</param>
        public void Remove(string path)
        {
            _listChanged = true;
            foreach (CheckSumItem fi in _fileList)
            {
                if (fi.FullPath == path)
                {
                    int index = _fileList.IndexOf(fi);
                    _fileList.RemoveAt(index);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove all items from list to process.
        /// </summary>
        public void RemoveAll()
        {
            _listChanged = true;
            _fileList.RemoveRange(0, _fileList.Count);
        }

        /// <summary>
        /// Reset change tracking.
        /// </summary>
        public void ResetChanges()
        {
            _listChanged = false;
        }

        /// <summary>
        /// Set list status to changed.
        /// </summary>
        public void SetChanged()
        {
            _listChanged = true;
        }
    }
}
