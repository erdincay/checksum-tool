/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
Copyright (c) 2007-2009 Kimmo Varis <kimmov@winmerge.org>

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
using System.Globalization;
using NUnit.Framework;

namespace CheckSumTool
{
    /// <summary>
    /// Collection of utility methods for localization.
    /// </summary>
    public static class Locality
    {
        const int Kilo = 1024;

        /// <summary>
        /// Get a localized short file size.
        /// </summary>
        /// <param name="bytes">File size as a plain number.</param>
        /// <returns>File size as localized short number string.</returns>
        public static string GetShortFileSize(long bytes)
        {
            if (bytes < 0)
                return Convert.ToString(bytes);

            double fsize = 0;
            string postfix = "";
            if (bytes < Kilo)
            {
                fsize = bytes;
                postfix = " B";
            }
            else if (bytes < Kilo * Kilo)
            {
                fsize = (double) bytes / Kilo;
                postfix = " KB";
            }
            else if (bytes < Kilo * Kilo * Kilo)
            {
                fsize = (double) bytes / (Kilo * Kilo);
                postfix = " MB";
            }
            else
            {
                fsize = (double) bytes / (Kilo * Kilo * Kilo);
                postfix = " GB";
            }

            NumberFormatInfo numberInfo = CultureInfo.CurrentCulture.NumberFormat;
            fsize = Math.Round(fsize, 2);
            string size = Convert.ToString(fsize, numberInfo);
            size += postfix;
            return size;
        }
    }

    /// <summary>
    /// An unit testing class for the Locality class.
    /// </summary>
    [TestFixture]
    public class TestLocality
    {
        [Test]
        public void ShortSizeNegative()
        {
            string size = Locality.GetShortFileSize(-3);
            Assert.AreEqual("-3", size);
        }

        [Test]
        public void ShortSizeZero()
        {
            string size = Locality.GetShortFileSize(0);
            Assert.IsTrue(size.EndsWith(" B"));
        }

        [Test]
        public void ShortSizeBytes()
        {
            string size = Locality.GetShortFileSize(100);
            Assert.IsTrue(size.EndsWith(" B"));
        }

        [Test]
        public void ShortSizeKilo()
        {
            string size = Locality.GetShortFileSize(1100);
            Assert.IsTrue(size.EndsWith(" KB"));
        }

        [Test]
        public void ShortSizeMega()
        {
            string size = Locality.GetShortFileSize(1100000);
            Assert.IsTrue(size.EndsWith(" MB"));
        }

        [Test]
        public void ShortSizeGiga()
        {
            string size = Locality.GetShortFileSize(1100000000);
            Assert.IsTrue(size.EndsWith(" GB"));
        }

        [Test]
        public void ShortSizeGiga2()
        {
            string size = Locality.GetShortFileSize(5100000000);
            Assert.IsTrue(size.EndsWith(" GB"));
        }
    }
}
