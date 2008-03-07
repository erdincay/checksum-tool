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
using System.IO;
using System.Collections.Generic;

namespace CheckSumTool
{
    /// <summary>
    /// Description of SumFile.
    /// </summary>
    public class SumFile : ISumFile
    {
        /// <summary>
        /// List where items are added.
        /// </summary>
        CheckSumFileList _list;

        /// <summary>
        /// Set the list into which items are added.
        /// </summary>
        /// <param name="list">List getting items.</param>
        public void SetFileList(CheckSumFileList list)
        {
            _list = list;
        }

         /// <summary>
        /// Create sumfile instance for checksum file.
        /// </summary>
        /// <param name="fileType">Type of checksum file to initialize.</param>
        /// <returns>ISumFile instance.</returns>
        IFile InitSumFile(CheckSumType currentSumType)
        {
            IFile newFile = null;
             switch (currentSumType)
             {
                 case CheckSumType.MD5:
                     newFile = (IFile) new MD5File();
                     break;

                 case CheckSumType.SHA1:
                     newFile = (IFile) new Sha1File();
                     break;

                 case CheckSumType.CRC32:
                     newFile = (IFile) new SFVFile();
                     break;

                 default:
                     break;
            }
            return newFile;
        }

        /// <summary>
        /// Read items from a file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        /// <returns>Number of items read.</returns>
        public int ReadFile(string path, CheckSumType currentSumType)
        {
            TextFileReader reader = new TextFileReader(path);
            IFile sumFile = InitSumFile(currentSumType);

            List<Pair<string>> itemList = sumFile.ReadData(reader);

            int items = 0;
            foreach (Pair<string> item in itemList)
            {
                // TODO: must validity-check values!
                string filename = item.Item2;
                FileInfo fi = new FileInfo(filename);

                // Check if fhe file is found and accessible
                try
                {
                    if (fi.Exists)
                    {
                        string fullpath = Path.Combine(fi.DirectoryName, fi.Name);

                        _list.AddFile(fullpath, item.Item1);
                        ++items;
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return items;
        }

        /// <summary>
        /// Write items to the file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        public void WriteFile(string path, CheckSumType currentSumType)
        {
            StreamWriter file = new StreamWriter(path, false,
                System.Text.Encoding.ASCII);

            IFile sumFile = InitSumFile(currentSumType);

            sumFile.Header(file);

            for (int i = 0; i < _list.FileList.Count; i++)
            {
                string filename = _list.FileList[i].FileName;

                if (_list.FileList[i].CheckSum == null)
                    continue;

                DirectoryInfo rootDirector = Directory.GetParent(path);

                string relativePath = "";
                string checksum = _list.FileList[i].CheckSum.ToString();
                if (rootDirector.Parent == null)
                {
                    relativePath = _list.FileList[i].FullPath.Replace(rootDirector.Name, "");
                }
                else
                {
                    relativePath = _list.FileList[i].FullPath.Replace(Path.GetDirectoryName(path) +
                            Path.DirectorySeparatorChar, "");
                }
                relativePath = FileUtils.GetUnixPathFormat(relativePath);

                sumFile.WriteDataRow(file, checksum,relativePath);
            }
            file.Flush();
            file.Close();
        }
    }
}
