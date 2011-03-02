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
using System.Diagnostics;
using System.Reflection;
using System.IO;
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.Utils
{
    /// <summary>
    /// A class for getting information from the program itself. Like
    /// product name, version, homepage URl.
    /// </summary>
    public class ProgramInfo
    {
        /// <summary>
        /// Program's homepage URL.
        /// </summary>
        const string _URL = @"http://checksumtool.sourceforge.net/";

        /// <summary>
        /// The program version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// The program product name.
        /// </summary>
        public string ProgramName { get; private set; }

        /// <summary>
        /// Return the program homepage URL.
        /// </summary>
        public string URL
        {
            get { return _URL; }
        }

        /// <summary>
        /// The default constructor. Reads info from the current dll file.
        /// Read info may not be the same as calling exe!
        /// </summary>
        public ProgramInfo()
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            string filename = curAssembly.Location;

            ReadVersionInfo(filename);
        }

        /// <summary>
        /// Constructor, for reading information from given file (exe or dll).
        /// </summary>
        /// <param name="filename">Filename to read info from.</param>
        public ProgramInfo(string filename)
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(curAssembly.Location);
            string fullpath = FileUtils.ConcatPaths(path, filename);

            ReadVersionInfo(fullpath);
        }

        /// <summary>
        /// Get file information from given file.
        /// </summary>
        /// <param name="path">Filename from which to get the info.</param>
        void ReadVersionInfo(string path)
        {
            path = FileUtils.FromNativeSeparators(path);
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(path);
            Version = info.FileVersion;
            ProgramName = info.ProductName;
        }
    }

#if NUNIT
    /// <summary>
    /// A unit testing class for ProgramInfo class.
    /// </summary>
    [TestFixture]
    public class TestProgramInfo
    {
        /// <summary>
        /// Initialize testing, copy the test executable to correct folder.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string path = GetTestFilePath();
            File.Copy(@"../../TestData/CheckSumTool.exe", path);
        }

        /// <summary>
        /// Cleanup after test, remove test executable from test run folder.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            string path = GetTestFilePath();
            File.Delete(path);
        }

        /// <summary>
        /// Get a path to the test executable in test run folder.
        /// </summary>
        /// <returns>Path to the executable.</returns>
        private string GetTestFilePath()
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(curAssembly.Location);
            string file = FileUtils.ConcatPaths(path, "CheckSumTool.exe");
            return file;
        }

        /// <summary>
        /// A test reading version data from its own dll file.
        /// </summary>
        [Test]
        public void Construct()
        {
            ProgramInfo info = new ProgramInfo();
            Assert.IsNotNull(info);
        }

        /// <summary>
        /// Read test executable.
        /// </summary>
        [Test]
        public void ReadExe()
        {
            ProgramInfo info = new ProgramInfo("CheckSumTool.exe");
            Assert.IsNotNull(info);
        }

        /// <summary>
        /// Read information from the test executable.
        /// </summary>
        [Test]
        public void ReadExeInfo()
        {
            ProgramInfo info = new ProgramInfo("CheckSumTool.exe");
            Assert.IsNotNull(info);

            Assert.AreEqual("0.6.0.13406", info.Version);
            Assert.AreEqual("CheckSumTool", info.ProgramName);
            Assert.AreEqual(@"http://checksumtool.sourceforge.net/", info.URL);
        }
    }
#endif
}
