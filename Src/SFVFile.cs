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

// $Id$

using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

namespace CheckSumTool
{
    /// <summary>
    /// Implement FLV file handler. FLV files contain CRC32 checksums.
    /// File format specification:
    /// http://www.joegeluso.com/software/file-formats/sfv.htm
    /// </summary>
    public class SFVFile : ISumFile
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
            List<Pair<string>> itemList = reader.ReadSplittedLines(true);

            int items = 0;
            foreach (Pair<string> item in itemList)
            {
                // TODO: must validity-check values!
                string filename = item.Item1;
                FileInfo fi = new FileInfo(filename);
                string fullpath = Path.Combine(fi.DirectoryName, fi.Name);

                _list.AddFile(fullpath, item.Item2);
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
            const string separator = " ";
            StreamWriter file = new StreamWriter(path, false,
                System.Text.Encoding.ASCII);

            // Write program name and version as comment
            SumFileInfo info = new SumFileInfo();
            string prognameline = "; CRC-32 sums generated by ";
            prognameline += info.ProgramName;
            prognameline += " version ";
            prognameline += info.ProgramVersion;
            prognameline += Environment.NewLine;
            file.Write(prognameline);

            // Write program url as comment
            string urlline = "; ";
            urlline += info.ProgramUrl;
            urlline += Environment.NewLine;
            file.Write(urlline);

            // Write file creation time as comment
            string timeline = "; Created ";
            timeline += Convert.ToString(DateTime.Now);
            timeline += Environment.NewLine;
            file.Write(timeline);

            for (int i = 0; i < _list.FileList.Count; i++)
            {
                string filename = _list.FileList[i].FileName;
                string checksum = _list.FileList[i].CheckSum.ToString();
                string relativePath = _list.FileList[i].FullPath.Replace(Path.GetDirectoryName(path) + Path.DirectorySeparatorChar, "");
                relativePath = FileUtils.GetUnixPathFormat(relativePath);

                file.Write(relativePath);
                file.Write(separator);
                file.Write(checksum);
                file.Write(Environment.NewLine);
            }
            file.Flush();
            file.Close();
        }
    }

    [TestFixture]
    public class TestSFVFile
    {
        // Precalculated SFV-checkSum
        string[] _checkSum = {"692ccfbd",
                              "4fa6e929",
                              "8ca66b7b",
                              "8ca66b7b",
                              "bc654d09",
                              "b046e6f2",
                              "72935002",
                              "69dbc00f",
                              "1b851995",
                              "210c4839",
                              "25d0bd72",
                              "0e2b82e8",
                              "e4f410fe"};

        int _testFile1RowCount = 13;

        /// <summary>
        /// Test reading file TestFile1.SFV
        /// </summary>
        [Test]
        public void ReadFile()
        {
            SumDocument document = new SumDocument();
            bool success = document.LoadFile(@"../../TestData/UnitTestFolder/TestFile1.SFV", SumFileType.SFV);

            Assert.AreEqual(success, true);
        }

        /// <summary>
        /// Trying read file TestFile1.SFV
        /// Throw FileNotFoundException
        /// </summary>
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileFileNotFoundException()
        {
            SumDocument document = new SumDocument();
            bool success = document.LoadFile(@"../../TestData/UnitTestFolder/TestFile1.SF", SumFileType.SHA1);
        }

        /// <summary>
        /// Test reading file TestFile1.SFV
        /// </summary>
        [Test]
        public void ReadFileIsExcists()
        {
            SumDocument document = new SumDocument();

            string filePath = @"../../TestData/UnitTestFolder/TestFile1.SFV";

            SumFileType fileType;
            fileType = SumFileUtils.FindFileType(filePath);

            document.Items.RemoveAll();

            bool success = document.LoadFile(filePath, fileType);

            foreach (CheckSumItem item in document.Items.FileList)
            {
                string fullPath = item.FullPath.Replace(@"Build\Debug", @"TestData\UnitTestFolder");

                Assert.AreEqual(File.Exists(fullPath), true);
            }
        }

        /// <summary>
        /// Test reading file TestFile1.SFV and checking checkSum
        /// </summary>
        [Test]
        public void ReadFileAndCheckCheckSum()
        {
            SumDocument document = new SumDocument();

            string filePath = @"../../TestData/UnitTestFolder/TestFile1.SFV";

            SumFileType fileType;
            fileType = SumFileUtils.FindFileType(filePath);

            document.Items.RemoveAll();

            bool success = document.LoadFile(filePath, fileType);

            int i = 0;
            foreach (CheckSumItem item in document.Items.FileList)
            {
                string checkSum = item.CheckSum.ToString();

                Assert.AreEqual(checkSum, _checkSum[i]);
                i++;
            }
        }

        /// <summary>
        /// Test reading not standard file NotStandardTestFile1.SFV and checking checkSum
        /// Exception -> CheckSumItem.cs -> SetSum
        /// </summary>
        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void ReadNotStantardFile()
        {
            SumDocument document = new SumDocument();

            string filePath = @"../../TestData/UnitTestFolder/NotStandardTestFile1.SFV";

            SumFileType fileType;
            fileType = SumFileUtils.FindFileType(filePath);

            document.Items.RemoveAll();

            bool success = document.LoadFile(filePath, fileType);

            int i = 0;
            foreach (CheckSumItem item in document.Items.FileList)
            {
                string checkSum = item.CheckSum.ToString();

                Assert.AreEqual(checkSum, _checkSum[i]);
                i++;
            }
        }

        /// <summary>
        /// Test reading file TestFile1.dat
        /// It´s not sha1 syntax file.
        /// </summary>
        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void ReadDatFile()
        {
            SumDocument document = new SumDocument();

            string filePath = @"../../TestData/UnitTestFolder/TestFile1.dat";

            SumFileType fileType;
            fileType = SumFileUtils.FindFileType(filePath);

            document.Items.RemoveAll();

            bool success = document.LoadFile(filePath, fileType);
        }

        /// <summary>
        /// Test reading file TestFile1.md5 and checking checkSum
        /// </summary>
        [Test]
        public void ReadFileAndCheckRowCount()
        {
            SumDocument document = new SumDocument();

            string filePath = @"../../TestData/UnitTestFolder/TestFile1.SFV";

            SumFileType fileType;
            fileType = SumFileUtils.FindFileType(filePath);

            document.Items.RemoveAll();

            bool success = document.LoadFile(filePath, fileType);

            Assert.AreEqual(_testFile1RowCount, document.Items.Count);
        }
    }
}
