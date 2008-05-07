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
using System.IO;
using System.Collections.Generic;

namespace CheckSumTool.SumLib
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
                try
                {
                    fi.CheckSum.Clear();
                    fi.Verified = VerificationState.NotVerified;
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// Calculate checksums for all items in the list.
        /// </summary>
        /// <param name="progressInfo">Returns information about progress.</param>
        public void CalculateSums(ref ProgressInfo progressInfo)
        {
            progressInfo.Max = _checksumItemList.Count;
            Calculator sumCalculator = new Calculator(_currentSumType);
            sumCalculator.Calculate(_checksumItemList.FileList, ref progressInfo);

            // Set list changed as even existing checksum values could change
            // during calculation (file in disc changed between calculations).
            _checksumItemList.SetChanged();
            progressInfo.Complete();
        }

        /// <summary>
        /// Verify checksums for all items in the list.
        /// </summary>
        /// <param name="progressInfo">Returns information about progress.</param>
        /// <returns>
        /// true if all items were verified successfully, false if
        /// one or more items failed the verification.
        ///</returns>
        public bool VerifySums(ref ProgressInfo progressInfo)
        {
            progressInfo.Max = _checksumItemList.Count;
            Calculator sumCalculator = new Calculator(_currentSumType);
            bool succeeded = sumCalculator.Verify(_checksumItemList.FileList,
                ref progressInfo);
            progressInfo.Complete();
            return succeeded;
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
            SumFile newSumFile = new SumFile();

            if (newSumFile != null)
            {
                SetSumTypeFromFileType(fileType);
                newSumFile.SetFileList(_checksumItemList);

                int items = 0;
                try
                {
                    items = newSumFile.ReadFile(path, _currentSumType);
                }
                catch (Exception)
                {

                }

                if (items > 0)
                {
                    success = true;
                    // File just loaded, reset change tracking.
                    _checksumItemList.ResetChanges();
                }
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

            try
            {
                SumFile newSumFile = new SumFile();
                newSumFile.SetFileList(_checksumItemList);
                newSumFile.WriteFile(_filename, _currentSumType);
            }
            catch
            {
                success = false;
            }

            return success;
        }
    }
}
