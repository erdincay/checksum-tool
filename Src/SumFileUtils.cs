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

namespace CheckSumTool
{
    /// <summary>
    /// Sum file types tool can handle.
    /// </summary>
    public enum SumFileType
    {
        /// <summary>
        /// File type not recognized.
        /// </summary>
        Unknown,
        /// <summary>
        /// File containing MD5 sums.
        /// </summary>
        MD5,
        /// <summary>
        /// File containing CRC32 sums.
        /// </summary>
        SFV,
        /// <summary>
        /// File containing SHA-1 sums.
        /// </summary>
        SHA1,
    }
    
    /// <summary>
    /// Utility methods for handlind checksum files.
    /// </summary>
    public class SumFileUtils
    {
        /// <summary>
        /// Try to determine (checksum) filetype for given file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        /// <returns>File's type if it could be determined.</returns>
        /// <exception cref="ArgumentNullException">Path was null.</exception>
        /// <exception cref="FileNotfoundException">
        /// Given file was not found.
        ///</exception>
        static public SumFileType FindFileType(string path)
        {
            if (path == null)
                throw new ArgumentNullException(path);
            
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
                throw new FileNotFoundException("Cannot find the file", path);
            
            SumFileType fileType = SumFileType.Unknown;
            if (fi.Extension == ".md5")
                fileType = SumFileType.MD5;
            else if (fi.Extension == ".sfv")
                fileType = SumFileType.SFV;
            else if (fi.Extension == ".sha1")
                fileType = SumFileType.SHA1;
            
            if (fileType == SumFileType.Unknown)
            {
                string name = fi.Name;
                name = name.ToLower();
                
                if (name.Contains("md5"))
                    fileType = SumFileType.MD5;
                else if (name.Contains("sha1"))
                    fileType = SumFileType.SHA1;
                else if (name.Contains("sfv") || name.Contains("crc"))
                    fileType = SumFileType.SFV;
            }
            return fileType;
        }
    }
}
