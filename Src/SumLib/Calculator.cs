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
using System.IO;
using CheckSumTool.Utils;

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Class calculating and verifying checksums.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Type of checksums used for calculation/verifying.
        /// </summary>
        private CheckSumType _sumType;

        /// <summary>
        /// Type of checksum used.
        /// </summary>
        public CheckSumType SumType
        {
            get { return _sumType; }
            set { _sumType = value; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Calculator()
        {
        }

        /// <summary>
        /// Constructor selecting checksum type.
        /// </summary>
        /// <param name="sumtype">Checksum type.</param>
        public Calculator(CheckSumType sumtype)
        {
            _sumType = sumtype;
        }

        /// <summary>
        /// Calculate checksums.
        /// </summary>
        /// <param name="itemList">List of items for which to calculate.</param>
        /// <param name="progressInfo">Returns information about calculation
        /// progress.</param>
        public void Calculate(List<CheckSumItem> itemList,
                ref ProgressInfo progressInfo)
        {
            ICheckSum sumImpl = CheckSumImplList.GetImplementation(_sumType);
            int i = 0;
            foreach (CheckSumItem ci in itemList)
            {
                progressInfo.Filename = ci.FullPath;

                // Check if fhe file is found and accessible
                try
                {
                    FileInfo fi = new FileInfo(ci.FullPath);
                    if (!fi.Exists)
                        continue;
                }
                catch
                {
                    // Ignore non-existing and non-accessible files
                    // TODO: Should we inform user?
                    continue;
                }

                try
                {
                    using (FileStream filestream = new FileStream(ci.FullPath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] hash = sumImpl.Calculate(filestream);
                        filestream.Close();
                        ci.SetSum(hash);
                    }
                }
                catch (IOException)
                {
                    //TODO: Set failure status to checksum item.
                }

                // Update progress information after item completed
                i++;
                progressInfo.Now = i;

                if (progressInfo.IsStopping())
                {
                    progressInfo.ActivityEnded();
                    break;
                }
            }
        }

        /// <summary>
        /// Verify checksums.
        /// </summary>
        /// <param name="itemList">List of items to verify.</param>
        /// <param name="progressInfo">Returns information about calculation
        /// progress.</param>
        /// <returns>
        /// true if all items succeeded the verification, false if
        /// one or more items failed verification.
        /// </returns>
        public bool Verify(List<CheckSumItem> itemList,
                ref ProgressInfo progressInfo)
        {
            bool verifysucceeded = true;
            ICheckSum sumImpl = CheckSumImplList.GetImplementation(_sumType);
            int i = 0;
            foreach (CheckSumItem ci in itemList)
            {
                progressInfo.Filename = ci.FullPath;

                // If verification finds an item which does not have checksum
                // (not calculated yet?) just ignore the item. Or maybe we
                // should calculate checksum first?
                if (ci.CheckSum == null)
                {
                    verifysucceeded = false;
                    continue;
                }

                // Check if fhe file is found and accessible
                try
                {
                    FileInfo fi = new FileInfo(ci.FullPath);
                    if (!fi.Exists)
                    {
                        verifysucceeded = false;
                        continue;
                    }
                }
                catch
                {
                    // Ignore non-existing and non-accessible files
                    // TODO: Should we inform user?
                    verifysucceeded = false;
                    continue;
                }

                bool verified = false;

                try
                {
                    using(FileStream filestream = new FileStream(ci.FullPath, FileMode.Open, FileAccess.Read))
                    {
                        verified = sumImpl.Verify(filestream, ci.CheckSum.GetAsByteArray());
                        filestream.Close();
                    }
                }
                catch (IOException)
                {
                    // TODO: Set failure status to checksum item.

                    // Let verification just fail when not implemented

                    // TODO: Need to inform the user, but how?
                    // For debug builds just throw the exception for reminder..
                    #if DEBUG
                    throw;
                    #endif
                }

                if (verified)
                {
                    ci.Verified = VerificationState.VerifyOK;
                }
                else
                {
                    ci.Verified = VerificationState.VerifyFailed;
                    if (verifysucceeded == true)
                    {
                        // Set to false when we find first failed item
                        verifysucceeded = false;
                    }
                }

                // Update progress information after item completed
                i++;
                progressInfo.Now = i;
                
                if (progressInfo.IsStopping())
                {
                    progressInfo.ActivityEnded();
                    break;
                }
            }
            return verifysucceeded;
        }
    }
}
