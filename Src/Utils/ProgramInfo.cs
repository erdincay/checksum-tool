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
using System.Diagnostics;
using System.Reflection;
using System.IO;

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
        /// Program version.
        /// </summary>
        string _version;
        /// <summary>
        /// Program's product name.
        /// </summary>
        string _progName;

        /// <summary>
        /// Return the program version.
        /// </summary>
        public string Version
        {
            get { return _version; }
        }

        /// <summary>
        /// Return the program product name.
        /// </summary>
        public string ProgramName
        {
            get { return _progName; }
        }

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
        /// Construtor, for reading information from given file (exe or dll).
        /// </summary>
        /// <param name="filename">Filename to read info from.</param>
        public ProgramInfo(string filename)
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(curAssembly.Location);
            string fullpath = path + FileUtils.PathSeparator + filename;

            ReadVersionInfo(fullpath);
        }

        /// <summary>
        /// Get file information from given file.
        /// </summary>
        /// <param name="path">Filename from which to get the info.</param>
        void ReadVersionInfo(string path)
        {
            path = FileUtils.GetUnixPathFormat(path);
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(path);
            _version = info.FileVersion;
            _progName = info.ProductName;
        }
    }
}
