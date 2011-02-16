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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.SumLib
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
        /// <exception cref="ArgumentException">Path was empty.</exception>
        /// <exception cref="FileNotfoundException">
        /// Given file was not found.
        ///</exception>
        static public SumFileType FindFileType(string path)
        {
            if (path == null)
                throw new ArgumentNullException(path);
            if (path == String.Empty)
                throw new ArgumentException("Path is empty", "path");

            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
                throw new FileNotFoundException("Cannot find the file", path);

            // First try checking for filename extension
            SumFileType fileType = SumFileType.Unknown;
            if (fi.Extension == ".md5")
                fileType = SumFileType.MD5;
            else if (fi.Extension == ".sfv")
                fileType = SumFileType.SFV;
            else if (fi.Extension == ".sha1")
                fileType = SumFileType.SHA1;

            // If we could not determine file type from filename extension,
            // try to find some hints from filename itself.
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

#if NUNIT
    /// <summary>
    /// Unit test class for SumFileUtils.FindFileType()
    /// </summary>
    [TestFixture]
    public class TestSumFileUtilsFindFileType
    {
        /// <summary>
        /// Test we get expection from null argument.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFilename()
        {
            SumFileType ft = SumFileUtils.FindFileType(null);
        }

        /// <summary>
        /// Test we get exception from empty argument.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFileName()
        {
            SumFileType ft = SumFileUtils.FindFileType(String.Empty);
        }

        /// <summary>
        /// Test we get exception from file that is not found.
        /// </summary>
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileNotFound()
        {
            // Expect user doen't have this file..
            SumFileType ft = SumFileUtils.FindFileType(@"C:\unique.txt");
        }

        /// <summary>
        /// Test that SHA-1 file is recognized by filename extension.
        /// </summary>
        [Test]
        public void ShaFile1()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/SHA1Files/TestFile1.sha1");
            Assert.AreEqual(SumFileType.SHA1, ft);
        }

        /// <summary>
        /// Test that SHA-1 file is recognized by filename.
        /// </summary>
        [Test]
        public void ShaFile2()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/SHA1Files/file_sha1.txt");
            Assert.AreEqual(SumFileType.SHA1, ft);
        }

        /// <summary>
        /// Test that MD5 file is recognized by filename extension.
        /// </summary>
        [Test]
        public void MD5File1()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/MD5Files/TestFile1.md5");
            Assert.AreEqual(SumFileType.MD5, ft);
        }

        /// <summary>
        /// Test that MD5 file is recognized from filename.
        /// </summary>
        [Test]
        public void MD5File2()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/MD5Files/File_md5.txt");
            Assert.AreEqual(SumFileType.MD5, ft);
        }

        /// <summary>
        /// Test that CRC32 file is recognized by filename extension.
        /// </summary>
        [Test]
        public void CRC32File1()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/SFVFiles/TestFile1.sfv");
            Assert.AreEqual(SumFileType.SFV, ft);
        }

        /// <summary>
        /// Test that CRC32 file is recognized by filename.
        /// </summary>
        [Test]
        public void CRC32File2()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/SFVFiles/file_crc.txt");
            Assert.AreEqual(SumFileType.SFV, ft);
        }

        /// <summary>
        /// Test that CRC32 file is recognized by filename.
        /// </summary>
        [Test]
        public void CRC32File3()
        {
            SumFileType ft = SumFileUtils.FindFileType(@"../../TestData/SFVFiles/file_sfv.txt");
            Assert.AreEqual(SumFileType.SFV, ft);
        }
    }
#endif
}
