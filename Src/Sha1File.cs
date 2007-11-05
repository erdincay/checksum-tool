﻿/*
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

using System;
using System.IO;
using System.Collections.Generic;

namespace CheckSumTool
{
    /// <summary>
    /// A class for reading and writing SHA1 files.
    /// </summary>
    public class Sha1File : ISumFile
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
        /// Read items from a file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        /// <returns>Number of items read.</returns>
        public int ReadFile(string path)
        {
            TextFileReader reader = new TextFileReader(path);
            List<Pair<string>> itemList = reader.ReadSplittedLines();
            
            int items = 0;
            foreach (Pair<string> item in itemList)
            {
                // TODO: must validity-check values!
                string filename = item.Item2;
                FileInfo fi = new FileInfo(filename);
                string fullpath = Path.Combine(fi.DirectoryName, filename);
                
                _list.AddFile(fullpath, item.Item1);
                ++items;
            }
            return items;
        }
        
        /// <summary>
        /// Write items to the file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        public void WriteFile(string path)
        {
            const string separator = "|";
            StreamWriter file = new StreamWriter(path, false,
                System.Text.Encoding.ASCII);

            for (int i = 0; i < _list.FileList.Count; i++)
            {
                string filename = _list.FileList[i].FileName;
                string checksum = _list.FileList[i].CheckSum.ToString();

                file.Write(checksum);
                file.Write(separator);
                file.Write(filename);
                file.Write(Environment.NewLine);
            }
            file.Close();
        }
    }
}
