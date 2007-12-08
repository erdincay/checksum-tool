/*
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
using NUnit.Framework;

namespace CheckSumTool
{
    /// <summary>
    /// Some general string utils.
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// Convert string to a byte array.
        /// </summary>
        /// <param name="text">String to convert.</param>
        /// <returns>Byte array. null if text is null, and empty array if
        /// the string is empty.
        ///</returns>
        public static byte[] StringToByteArray(string text)
        {
            if (text == null)
                return null;
            
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(text);
        }
        /// <summary>
        /// Format hex decimal string from a byte table.
        /// </summary>
        /// <param name="hex">Byte table to format to string.</param>
        /// <returns>Byte table as a string.</returns>
        public static string FormatHexDump(byte[] hex)
        {
            if (hex == null)
                return null;
            if (hex.Length == 0)
                return "";
            
            string dump = "";
            foreach (byte by in hex)
            {
                dump += string.Format("{0:x2} ",  by);
            }
            return dump;
        }
    }
    
    /// <summary>
    /// Unit test class for StringToByteArray.
    /// </summary>
    [TestFixture]
    public class TestStringToByteArray
    {
        /// <summary>
        /// Test with null parameter conversion.
        /// </summary>
        [Test]
        public void NullStringToArray()
        {
            byte[] arr = StringUtil.StringToByteArray((string)null);
            Assert.IsNull(arr);
        }
        
        /// <summary>
        /// Test that empty string converts to empty byte array.
        /// </summary>
        [Test]
        public void EmptyStringToArray()
        {
            string testStr = "";
            byte[] arr = StringUtil.StringToByteArray(testStr);
            Assert.IsNotNull(arr);
            Assert.AreEqual(0, arr.Length);
        }
        
        /// <summary>
        /// Test converting one-char length string to byte array
        /// </summary>
        [Test]
        public void StringToArray1()
        {
            string testStr = "1";
            byte[] arr = StringUtil.StringToByteArray(testStr);
            Assert.IsNotNull(arr);
            Assert.AreEqual(1, arr.Length);
            Assert.AreEqual(49, arr[0]);
        }

        /// <summary>
        /// Tet converting 5-char lenght string to byte array.
        /// </summary>
        [Test]
        public void StringToArray5()
        {
            string testStr = "12345";
            byte[] arr = StringUtil.StringToByteArray(testStr);
            Assert.IsNotNull(arr);
            Assert.AreEqual(5, arr.Length);
            Assert.AreEqual(49, arr[0]);
            Assert.AreEqual(50, arr[1]);
            Assert.AreEqual(51, arr[2]);
            Assert.AreEqual(52, arr[3]);
            Assert.AreEqual(53, arr[4]);
        }
    }
    
    /// <summary>
    /// Unit test class for FormatHexDump.
    /// </summary>
    [TestFixture]
    public class TestFormatHexDump
    {
        /// <summary>
        /// Test formatting null array.
        /// </summary>
        [Test]
        public void FormatNullArray()
        {
            string str = StringUtil.FormatHexDump((byte[])null);
            Assert.IsNull(str);
        }

        /// <summary>
        /// Test formatting empty array.
        /// </summary>
        [Test]
        public void FormatEmptyArray()
        {
            string str = StringUtil.FormatHexDump(new byte[0] {});
            Assert.IsNotNull(str);
            Assert.AreEqual(0, str.Length);
        }
        
        /// <summary>
        /// Test formatting one decimal byte.
        /// </summary>
        [Test]
        public void FormatShort1Array()
        {
            string str = StringUtil.FormatHexDump(new byte[1] {1});
            Assert.IsNotNull(str);
            Assert.AreEqual(3, str.Length);
            Assert.AreEqual("01 ", str);
        }
        
        /// <summary>
        /// Test formatting two decimal bytes.
        /// </summary>
        [Test]
        public void FormatShort2Array()
        {
            string str = StringUtil.FormatHexDump(new byte[2] {1, 2});
            Assert.IsNotNull(str);
            Assert.AreEqual(6, str.Length);
            Assert.AreEqual("01 02 ", str);
        }

        /// <summary>
        /// Test formatting one decimal and one hexa byte.
        /// </summary>
        [Test]
        public void FormatHex2Array()
        {
            string str = StringUtil.FormatHexDump(new byte[2] {1, 0xa});
            Assert.IsNotNull(str);
            Assert.AreEqual(6, str.Length);
            Assert.AreEqual("01 0a ", str);
        }

        /// <summary>
        /// Test formatting two hexa bytes.
        /// </summary>
        [Test]
        public void FormatHex2Array2()
        {
            string str = StringUtil.FormatHexDump(new byte[2] {0xb, 0xa});
            Assert.IsNotNull(str);
            Assert.AreEqual(6, str.Length);
            Assert.AreEqual("0b 0a ", str);
        }
    }
}
