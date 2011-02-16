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

using System;
using System.IO;
using System.Collections.Generic;
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.Utils
{
    /// <summary>
    /// Class for reading checksum text files.
    /// </summary>
    public class TextFileReader
    {
        /// <summary>
        /// Full path of the file.
        /// </summary>
        string _path;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Full path to the file to read.</param>
        public TextFileReader(string path)
        {
            // Fileinfo checks path validity and file existence + priviledges
            FileInfo fi = new FileInfo(path);
            _path = path;
        }

        /// <summary>
        /// Read file contents to list of strings.
        /// </summary>
        /// <returns>Lines in file as a string list.</returns>
        List<string> ReadLineList()
        {
            string[] lines = File.ReadAllLines(_path);
            List<string> processlines = new List<string>();

            foreach (string line in lines)
            {
                line.Trim();

                // Ignore empty lines
                if (line.Length == 0)
                    continue;

                // Ignore comment lines
                if (line[0] == ';')
                    continue;

                processlines.Add(line);
            }
            return processlines;
        }

        /// <summary>
        /// Read all valid lines from the text file. Ingnore invalid lines,
        /// like comment-lines and empty lines.
        /// </summary>
        /// <returns>Valid lines as array of strings.</returns>
        public string[] ReadLines()
        {
            List<string> lines = ReadLineList();

            // Copy strings to one array for returning for the caller
            string[] retlines = new string[lines.Count];
            int index = 0;
            foreach (string line in lines)
            {
                retlines[index] = line;
                ++index;
            }
            return retlines;
        }

        /// <summary>
        /// Read lines to list of Pairs, using whitespace separators.
        /// </summary>
        /// <returns>Lines as list of Pairs.</returns>
        public List<Pair<string>> ReadSplittedLines()
        {
            char[] separators = new char[] {' ', '\t',};
            List<Pair<string>> items = ReadSplittedLines(separators, false);
            return items;
        }

        /// <summary>
        /// Read lines to list of Pairs using custom separator chars.
        /// </summary>
        /// <param name="separators">Chars used as separators.</param>
        /// <returns>Lines as list of Pairs.</returns>
        public List<Pair<string>> ReadSplittedLines(char [] separators)
        {
            List<Pair<string>> items = ReadSplittedLines(separators, false);
            return items;
        }

        /// <summary>
        /// Read lines to list of Pairs, reversing line parsing.
        /// </summary>
        /// <param name="reverse">If true, separator chars are searched
        /// from end of line.</param>
        /// <returns>Lines as list of Pairs.</returns>
        public List<Pair<string>> ReadSplittedLines(bool reverse)
        {
            char[] separators = new char[] {' ', '\t',};
            List<Pair<string>> items = ReadSplittedLines(separators, reverse);
            return items;
        }

        /// <summary>
        /// Read lines to list of Pairs.
        /// </summary>
        /// <param name="separators">Chars used as separators.</param>
        /// <param name="reverse">Search separators from end of line.</param>
        /// <returns>Lines as list of Pairs.</returns>
        public List<Pair<string>> ReadSplittedLines(char [] separators,
            bool reverse)
        {
            List<string> lines = ReadLineList();
            List<Pair<string>> list = new List<Pair<string>>();

            foreach (string line in lines)
            {
                Pair<string> pair = SplitLine(line, separators, reverse);
                if (pair != null)
                    list.Add(pair);
            }
            return list;
        }

        /// <summary>
        /// Split line to two items (one Pair).
        /// </summary>
        /// <param name="line">Line to split.</param>
        /// <param name="separators">Separator characters.</param>
        /// <returns>Line items as a Pair.</returns>
        Pair<string> SplitLine(string line, char [] separators)
        {
            Pair<String> pair = SplitLine(line, separators, false);
            return pair;
        }

        /// <summary>
        /// Split line to two items (one Pair).
        /// </summary>
        /// <param name="line">Line to Split.</param>
        /// <param name="separators">Separator characters.</param>
        /// <param name="reverse">Find separator characters from end of line.</param>
        /// <returns>Line items as a Pair.</returns>
        Pair<string> SplitLine(string line, char [] separators, bool reverse)
        {
            int separatorInd = 0;
            if (reverse)
                separatorInd = line.LastIndexOfAny(separators);
            else
                separatorInd = line.IndexOfAny(separators);

            Pair<string> lineItem = null;

            if (separatorInd != -1)
            {
                string first = "";
                string second = "";
                if (separatorInd > 0)
                {
                    first = line.Substring(0, separatorInd);
                    first = first.Trim();
                    second = line.Substring(separatorInd + 1);
                    second = second.Trim();

                    if (first.Length > 0 && second.Length > 0 && reverse == false)
                        lineItem = new Pair<string>(first, second);
                    else if (first.Length > 0 && second.Length > 0 && reverse == true)
                        lineItem = new Pair<string>(second, first);
                }
            }
            return lineItem;
        }
    }

#if NUNIT
    [TestFixture]
    public class TestTextFileReader
    {
         /// <summary>
         /// Test creating TextFileReader.
         /// </summary>
        [Test]
        public void Create()
        {
            TextFileReader textFile = new TextFileReader("../../TestData/TextFile2.txt");
            Assert.IsNotNull(textFile);
        }

        /// <summary>
         /// Test reading lines from TextFile2.txt
         /// </summary>
        [Test]
        public void ReadLinesFromTestFile2()
        {
            TextFileReader textFile = new TextFileReader("../../TestData/TextFile2.txt");
            string[] lines = textFile.ReadLines();
            Assert.AreEqual(3, lines.Length);
        }

        /// <summary>
        /// Test giving wrong file string.
        /// </summary>
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadSplittedLinesFromTestFile()
        {
            TextFileReader textFile = new TextFileReader("../../TestData/TextFile4.txt");
            List<Pair<string>> items = textFile.ReadSplittedLines();
        }

        /// <summary>
        /// Test reading splitted lines from TextFile3.txt
        /// </summary>
        [Test]
        public void ReadSplittedLinesFromTestFile3()
        {
            TextFileReader textFile = new TextFileReader("../../TestData/TextFile3.txt");
            List<Pair<string>> items = textFile.ReadSplittedLines();

            Pair<string> pairItems = items[0];

            Assert.AreEqual(pairItems.Item1.ToString(), "1");
            Assert.AreEqual(pairItems.Item2.ToString(), "2");
        }
    }
#endif
}
