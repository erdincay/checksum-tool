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
using System.Globalization;

namespace CheckSumTool
{
    /// <summary>
    /// A class holding checksum data. This class is a general checksum data
    /// storage, that can hold any type of checksum data in byte table.
    /// </summary>
    /// <remarks>
    /// Should replace this class with a class using generics. 
    /// </remarks>
    public class CheckSumData
    {
        /// <summary>
        /// Byte table of checksum data.
        /// </summary>
        private byte[] _data;
        
        /// <summary>
        /// Return current data.
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Checksumdata as byte table.</param>
        public CheckSumData(byte[] data)
        {
            Set(data);
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Checksumdata as string.</param>
        public CheckSumData(string data)
        {
            Set(data);
        }
        
        /// <summary>
        /// Return checksum as a string.
        /// </summary>
        /// <returns>Checksum as a string, empty string if no data.</returns>
        public override string ToString()
        {
            string retStr = "";
            if (_data == null)
                return "";

            foreach (byte by in _data)
            {
                retStr += string.Format("{0:x2}",  by);
            }
            return retStr;
        }
        
        /// <summary>
        /// Set checksum.
        /// </summary>
        /// <param name="data">Data as a byte table.</param>
        public void Set(byte[] data)
        {
            _data = new byte[data.Length];
            Array.Copy(data, _data, data.Length);
        }

        /// <summary>
        /// Set checksum.
        /// </summary>
        /// <param name="data">Data as a string.</param>
        public void Set(string data)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] bytes = new byte[data.Length / 2];
            int newIndex = 0; 
            for (int i = 0; i < data.Length; i += 2)
            {
                string conv = data.Substring(i, 2);
                byte by = byte.Parse(conv, NumberStyles.HexNumber);
                bytes[newIndex++] = by;
            }
            _data = bytes;
        }
        
        /// <summary>
        /// Clear data.
        /// </summary>
        public void Clear()
        {
            _data = null;
        }
    }
}
