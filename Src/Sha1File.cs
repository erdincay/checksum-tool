﻿/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc, Kimmo Varis <kimmo.varis@ixonos.com>

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
    /// A class for reading and writing SHA1 files.
    /// </summary>
    public class Sha1File : IFile
    {
        /// <summary>
        /// Read data from file.
        /// </summary>
        /// <param name="reader">File for reading.</param>
        /// <returns>Readed items, checkSum and filename.</returns>
        public List<Pair<string>> ReadData(TextFileReader reader)
        {
            List<Pair<string>> itemList = reader.ReadSplittedLines();

            return itemList;
        }

        /// <summary>
        /// Write header info.
        /// </summary>
        /// <param name="file">Target file to write.</param>
        public void Header(StreamWriter file)
        {
            // Write program name and version as comment
            SumFileInfo info = new SumFileInfo();
            string prognameline = "; SHA-1 sums generated by ";
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
        }

        /// <summary>
        /// Write items to the file.
        /// </summary>
        /// <param name="file">Target file to write.</param>
        /// <param name="checkSum">CheckSum value.</param>
        /// <param name="relativePath">Relative path value.</param>
        public void WriteDataRow(StreamWriter file, string checksum, string relativePath)
        {
            string separator = " ";

            file.Write(checksum);
            file.Write(separator);
            file.Write(relativePath);
            file.Write(Environment.NewLine);
        }

        /// <summary>
        /// Write header info.
        /// </summary>
        /// <param name="file">Target file to write.</param>
        public void Footer(StreamWriter file)
        {

        }
    }

    [TestFixture]
    public class TestSha1FileNew
    {
        // Precalculated SHA-1 checkSum
        string[] _checkSum = {"94a3225c6bac573a06da75b05bcf6de59f65db2c",
                              "3fa6b918ad3fb6ac645cdd3caa17cb6d1492c99b",
                              "9f88f5c451feb859ff48ffe6c613437ac25c2b01",
                              "d3486ae9136e7856bc42212385ea797094475802",
                              "c9094305e83cb09ff11a8477e99d4ebf499bc74f",
                              "48332327f98549fd2782ebc4622981cc99514067"};

        int _testFile1RowCount = 6;

        /// <summary>
        /// Test reading file TestFile1.sha1 and check checksum value.
        /// </summary>
        [Test]
        public void ReadFile()
        {
            TextFileReader reader = new TextFileReader(@"../../TestData/UnitTestFolder/TestFile1.sha1");

            Sha1File sumFile = new Sha1File();
            List<Pair<string>> itemList = sumFile.ReadData(reader);

            int i = 0;
            foreach (Pair<string> item in itemList)
            {
                // TODO: must validity-check values!
                Assert.AreEqual(item.Item1.ToString(), _checkSum[i]);
                i++;
            }
        }
        /// <summary>
        /// Test reading file TestFile1.sha1 and checking row count
        /// </summary>
        [Test]
        public void ReadFileAndCheckRowCount()
        {
            TextFileReader reader = new TextFileReader(@"../../TestData/UnitTestFolder/TestFile1.sha1");

            Sha1File sumFile = new Sha1File();
            List<Pair<string>> itemList = sumFile.ReadData(reader);

            Assert.AreEqual(itemList.Count, _testFile1RowCount);
        }

        [Test]
        public void WriteHeader()
        {
            StreamWriter fileOut = new StreamWriter(@"../../TestData/UnitTestFolder/TestFileWrite1.Sha1", false);
            Sha1File sumFile = new Sha1File();
            sumFile.Header(fileOut);
            fileOut.Close();

            Assert.IsTrue(File.Exists(@"../../TestData/UnitTestFolder/TestFileWrite1.sha1"));

            string[] lines = File.ReadAllLines(@"../../TestData/UnitTestFolder/TestFileWrite1.sha1");
            Assert.GreaterOrEqual(lines.Length, 1);

            File.Delete(@"../../TestData/UnitTestFolder/TestFileWrite1.sha1");
        }

        [Test]
        public void WriteSums()
        {
            StreamWriter fileOut = new StreamWriter(@"../../TestData/UnitTestFolder/TestFileWrite2.sha1", false);
            Sha1File sumFile = new Sha1File();
            sumFile.WriteDataRow(fileOut, "94a3225c6bac573a06da75b05bcf6de59f65db2c",
                                 "test.txt");
            fileOut.Close();

            Assert.IsTrue(File.Exists(@"../../TestData/UnitTestFolder/TestFileWrite2.sha1"));

            string[] lines = File.ReadAllLines(@"../../TestData/UnitTestFolder/TestFileWrite2.sha1");
            Assert.AreEqual(1, lines.Length);
            Assert.IsTrue(lines[0] == "94a3225c6bac573a06da75b05bcf6de59f65db2c test.txt");

            File.Delete(@"../../TestData/UnitTestFolder/TestFileWrite2.sha1");
        }
    }
}
