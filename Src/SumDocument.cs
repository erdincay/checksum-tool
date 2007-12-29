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

namespace CheckSumTool
{
    /// <summary>
    /// A class holding checksum file list and other list-related information.
    /// </summary>
    public class SumDocument
    {
        /// <summary>
        /// List of files to handle.
        /// </summary>
        CheckSumFileList _checksumItemList = new CheckSumFileList();

        /// <summary>
        /// Currently selected/active checksum type.
        /// </summary>
        CheckSumType _currentSumType;

        /// <summary>
        /// Current filename (full path) for the checksum file.
        /// </summary>
        string _filename;

        /// <summary>
        /// Checksum type for the document.
        /// </summary>
        public CheckSumType SumType
        {
            get { return _currentSumType; }
            set { _currentSumType = value; }
        }

        /// <summary>
        /// Filename for the document.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// List of checksum items in document.
        /// </summary>
        public CheckSumFileList Items
        {
            get { return _checksumItemList; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SumDocument()
        {
        }

        /// <summary>
        /// Clear all checksums from items in the document.
        /// </summary>
        public void ClearAllSums()
        {
            List<CheckSumItem> list = _checksumItemList.FileList;
            foreach (CheckSumItem fi in list)
            {
                fi.CheckSum.Clear();
                fi.Verified = VerificationState.NotVerified;
            }
        }

        /// <summary>
        /// Calculate checksums for all items in the list.
        /// </summary>
        public void CalculateSums()
        {
            Calculator sumCalculator = new Calculator(_currentSumType);
            sumCalculator.Calculate(_checksumItemList.FileList);
        }

        /// <summary>
        /// Verify checksums for all items in the list.
        /// </summary>
        public void VerifySums()
        {
            Calculator sumCalculator = new Calculator(_currentSumType);
            sumCalculator.Verify(_checksumItemList.FileList);
        }

        /// <summary>
        /// Create sumfile instance for checksum file.
        /// </summary>
        /// <param name="fileType">Type of checksum file to initialize.</param>
        /// <returns>ISumFile instance.</returns>
        ISumFile InitSumFile(SumFileType fileType)
        {
            ISumFile newSumFile = null;
            if (fileType == SumFileType.MD5)
            {
                MD5File sumfile = new MD5File();
                newSumFile = sumfile;
            }
            else if (fileType == SumFileType.SFV)
            {
                SFVFile sumfile = new SFVFile();
                newSumFile = sumfile;
            }
            else if (fileType == SumFileType.SHA1)
            {
                Sha1File sumfile = new Sha1File();
                newSumFile = sumfile;
            }
            return newSumFile;
        }

        /// <summary>
        /// Convert sumfile type to sumtype.
        /// </summary>
        /// <param name="fileType">Filetype converted to sumtype</param>
        void SetSumTypeFromFileType(SumFileType fileType)
        {
            switch (fileType)
            {
                case SumFileType.MD5:
                    _currentSumType = CheckSumType.MD5;
                    break;
                case SumFileType.SFV:
                    _currentSumType = CheckSumType.CRC32;
                    break;
                case SumFileType.SHA1:
                    _currentSumType = CheckSumType.SHA1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Load items from the file.
        /// </summary>
        /// <param name="path">Full path to the file to load.</param>
        /// <param name="fileType">Type of the file to load.</param>
        /// <returns></returns>
        public bool LoadFile(string path, SumFileType fileType)
        {
            bool success = false;
            ISumFile newSumFile = InitSumFile(fileType);

            if (newSumFile != null)
            {
                SetSumTypeFromFileType(fileType);
                newSumFile.SetFileList(_checksumItemList);
                int items = newSumFile.ReadFile(path);
                if (items > 0)
                    success = true;
            }
            return success;
        }

        /// <summary>
        /// Save items to the file.
        /// </summary>
        /// <returns></returns>
        public bool SaveToFile()
        {
            bool success = true;
            switch (_currentSumType)
            {
                case CheckSumType.CRC32:
                    SFVFile SvfSumfile = new SFVFile();
                    SvfSumfile.SetFileList(_checksumItemList);
                    SvfSumfile.WriteFile(_filename);
                    break;

                case CheckSumType.MD5:
                    MD5File Md5Sumfile = new MD5File();
                    Md5Sumfile.SetFileList(_checksumItemList);
                    Md5Sumfile.WriteFile(_filename);
                    break;

                case CheckSumType.SHA1:
                    Sha1File Sha1Sumfile = new Sha1File();
                    Sha1Sumfile.SetFileList(_checksumItemList);
                    Sha1Sumfile.WriteFile(_filename);
                    break;

                default:
#if DEBUG
                    throw new NotImplementedException();
#endif
                    success = false;
                    break;
            }
            return success;
        }
    }
}
