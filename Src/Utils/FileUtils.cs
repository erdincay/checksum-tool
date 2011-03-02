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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.Utils
{
    /// <summary>
    /// File handling utilies.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Path separator used internally.
        /// </summary>
        public static readonly string PathSeparator = "/";

        /// <summary>
        /// Convert path to use internal separators. We use Unix path
        /// separators as our internal path separators. Unix path separators
        /// are used also in checksum files.
        /// </summary>
        /// <param name="path">Path to convert.</param>
        /// <returns>Converted path.</returns>
        public static string FromNativeSeparators(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            if (path.IndexOf('\\') >= 0)
            {
                string newpath = path.Replace(@"\", PathSeparator);
                return newpath;
            }
            return path;
        }
    }

#if NUNIT
    /// <summary>
    /// An unit testing class for FileUtils class.
    /// </summary>
    [TestFixture]
    public class TestFileUtils
    {
        [Test]
        public void PathSeparator()
        {
            Assert.AreEqual("/", FileUtils.PathSeparator);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToInternalConvertNull()
        {
            string conv = FileUtils.FromNativeSeparators((string) null);
        }

        [Test]
        public void ToInternalConvertEmpty()
        {
            string conv = FileUtils.FromNativeSeparators("");
            Assert.IsEmpty(conv);
        }

        [Test]
        public void ToInternalConvertSimple()
        {
            string conv = FileUtils.FromNativeSeparators(@"c:\Temp\");
            Assert.AreEqual(@"c:/Temp/", conv);
        }
    }
#endif
}
