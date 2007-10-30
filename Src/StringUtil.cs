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

using System;

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
        /// <returns>Byte array.</returns>
        public static byte[] StringToArray(string text)
        {
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
            string dump = "";
            foreach (byte by in hex)
            {
                dump += string.Format("{0:x2} ",  by);
            }
            return dump;
        }
    }
}
