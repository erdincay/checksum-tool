﻿/*
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
using CheckSumTool.Utils;
using NUnit.Framework;

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Implement FLV file handler. FLV files contain CRC32 checksums.
    /// File format specification:
    /// http://www.joegeluso.com/software/file-formats/sfv.htm
    /// </summary>
    public class SFVFile : IFile
    {
        /// <summary>
        /// Read data from file.
        /// </summary>
        /// <param name="reader">File for reading.</param>
        /// <returns>Readed items, checkSum and filename.</returns>
        public List<Pair<string>> ReadData(TextFileReader reader)
        {
            List<Pair<string>> itemList = reader.ReadSplittedLines(true);

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

            file.Write(relativePath);
            file.Write(separator);
            file.Write(checksum);
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
        const string TestFolder = @"../../TestData/SFVFiles/";

        int _testFile1RowCount = 13;

        /// <summary>
        /// Test reading file TestFile1.SFV and check checksum value.
        /// </summary>
        [Test]
        public void ReadFile()
        {
            TextFileReader reader = new TextFileReader(TestFolder + "TestFile1.SFV");

            SFVFile sumFile = new SFVFile();
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
        /// Test reading file TestFile1.SFV and checking row count
        /// </summary>
        [Test]
        public void ReadFileAndCheckRowCount()
        {
            TextFileReader reader = new TextFileReader(TestFolder + "TestFile1.SFV");

            SFVFile sumFile = new SFVFile();
            List<Pair<string>> itemList = sumFile.ReadData(reader);

            Assert.AreEqual(itemList.Count, _testFile1RowCount);
        }

        [Test]
        public void WriteHeader()
        {
            StreamWriter fileOut = new StreamWriter(@"../../TestData/UnitTestFolder/TestFileWrite1.SFV", false);
            SFVFile sumFile = new SFVFile();
            sumFile.Header(fileOut);
            fileOut.Close();

            Assert.IsTrue(File.Exists(@"../../TestData/UnitTestFolder/TestFileWrite1.SFV"));

            string[] lines = File.ReadAllLines(@"../../TestData/UnitTestFolder/TestFileWrite1.SFV");
            Assert.GreaterOrEqual(lines.Length, 1);

            File.Delete(@"../../TestData/UnitTestFolder/TestFileWrite1.SFV");
        }

        [Test]
        public void WriteSums()
        {
            StreamWriter fileOut = new StreamWriter(@"../../TestData/UnitTestFolder/TestFileWrite2.SFV", false);
            SFVFile sumFile = new SFVFile();
            sumFile.WriteDataRow(fileOut, "692ccfbd", "test.txt");
            fileOut.Close();

            Assert.IsTrue(File.Exists(@"../../TestData/UnitTestFolder/TestFileWrite2.SFV"));

            string[] lines = File.ReadAllLines(@"../../TestData/UnitTestFolder/TestFileWrite2.SFV");
            Assert.AreEqual(1, lines.Length);
            Assert.IsTrue(lines[0] == "test.txt 692ccfbd");

            File.Delete(@"../../TestData/UnitTestFolder/TestFileWrite2.SFV");
        }
    }
}