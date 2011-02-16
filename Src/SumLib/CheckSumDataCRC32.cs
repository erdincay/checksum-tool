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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// A class holding CRC32 checksum data.
    /// </summary>
    public class CheckSumDataCRC32 : CheckSumData
    {
        /// <summary>
        /// Length of the CRC32 checksum (in bytes).
        /// </summary>
        public static readonly int Length = 4;

        /// <summary>
        /// Constructor taking the checksum data as a byte array.
        /// </summary>
        /// <param name="data">Checksum data as a byte array.</param>
        public CheckSumDataCRC32(byte[] data)
        {
            DataSize = Length;
            Set(data);
        }

        /// <summary>
        /// Constructor taking the checksum data as a string.
        /// </summary>
        /// <param name="data">Checksum data as a string.</param>
        public CheckSumDataCRC32(string data)
        {
            DataSize = Length;
            Set(data);
        }
    }

#if NUNIT
    /// <summary>
    /// A unit test class for CheckSumDataCRC32 class.
    /// </summary>
    [TestFixture]
    public class TestCheckSumDataCRC32
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructNull()
        {
            byte[] data = null;
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructEmpty()
        {
            byte[] data = new byte[0];
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructSmall()
        {
            byte[] data = { 2, 3 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructSmall2()
        {
            byte[] data = { 2, 3, 4 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructSLarge()
        {
            byte[] data = { 2, 3, 4, 5, 6 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        public void Construct()
        {
            byte[] data = { 2, 3, 4, 5 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);
            Assert.AreEqual("02030405", csData.ToString());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructNullStr()
        {
            string data = null;
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructEmptyStr()
        {
            string data = "";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructSmallStr()
        {
            string data = "0203";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructSmallStr2()
        {
            string data = "020304";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructLargeStr()
        {
            string data = "0203040506";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
        }

        [Test]
        public void ConstructStr()
        {
            string data = "02030405";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);
            Assert.AreEqual("02030405", csData.ToString());
        }

        [Test]
        public void GetStringFromBytes()
        {
            byte[] data = { 2, 3, 4, 5 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);

            string strData = csData.ToString();
            Assert.IsNotNull(strData);
            Assert.AreEqual("02030405", strData);
        }

        [Test]
        public void GetStringFromString()
        {
            string data = "02030405";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);

            string strData = csData.ToString();
            Assert.IsNotNull(strData);
            Assert.AreEqual("02030405", strData);
        }

        [Test]
        public void GetBytesFromBytes()
        {
            byte[] data = { 2, 3, 4, 5 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);

            byte[] byData = csData.GetAsByteArray();
            Assert.IsNotNull(byData);
            Assert.AreEqual(byData.Length, CheckSumDataCRC32.Length);
            Assert.AreEqual(data, byData);
        }

        [Test]
        public void GetBytesFromString()
        {
            string data = "02030405";
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);

            byte[] byData = csData.GetAsByteArray();
            byte[] veData = { 2, 3, 4, 5 };
            Assert.IsNotNull(byData);
            Assert.AreEqual(byData.Length, CheckSumDataCRC32.Length);
            Assert.AreEqual(veData, byData);
        }

        [Test]
        public void ClearingForBytes()
        {
            byte[] data = { 2, 3, 4, 5 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);

            csData.Clear();
            byte[] byData = csData.GetAsByteArray();
            Assert.IsNull(byData);
        }

        [Test]
        public void ClearingForString()
        {
            byte[] data = { 2, 3, 4, 5 };
            CheckSumDataCRC32 csData = new CheckSumDataCRC32(data);
            Assert.IsNotNull(csData);

            csData.Clear();
            string strData = csData.GetAsString();
            Assert.IsNotNull(strData);
            Assert.AreEqual("", strData);
        }
    }
#endif
}
