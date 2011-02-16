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
using System.Globalization;
using System.Text;

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// An abstract class holding checksum data. This class is a base class
    /// for checksum data storage classes.
    /// </summary>
    public abstract class CheckSumData : ICheckSumData
    {
        /// <summary>
        /// Byte table of checksum data.
        /// </summary>
        private byte[] _data;

        /// <summary>
        /// Size if the data. Data must be of this size. Set by the derived
        /// class.
        /// </summary>
        private int _size;

        /// <summary>
        /// Protected property for derived classes to set the allowed data size.
        /// </summary>
        protected int DataSize
        {
            set { _size = value; }
        }

        /// <summary>
        /// Return checksum as a string.
        /// </summary>
        /// <returns>Checksum as a string, empty string if no data.</returns>
        public override string ToString()
        {
            return GetAsString();
        }

        /// <summary>
        /// Set checksum.
        /// </summary>
        /// <param name="data">Data as a byte table.</param>
        /// <exception cref="ArgumentNullException">Data array is null.</exception>
        /// <exception cref="ArgumentException">Data array is empty.</exception>
        public virtual void Set(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length == 0)
                throw new ArgumentException("Empty table as parameter.", "data");
            if (data.Length != _size)
            {
                string msg = String.Format("Data size is invalid, must be {0} bytes",
                    _size);
                throw new ArgumentException(msg, "data");
            }

            _data = new byte[data.Length];
            Array.Copy(data, _data, data.Length);
        }

        /// <summary>
        /// Set checksum.
        /// </summary>
        /// <param name="data">Data as a string.</param>
        /// <exception cref="ArgumentNullException">Data is null.</exception>
        /// <exception cref="ArgumentException">Data is empty.</exception>
        public virtual void Set(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length == 0)
                throw new ArgumentException("Empty string as parameter.", "data");
            if ((data.Length / 2) != _size)
            {
                string msg = String.Format("Data size is invalid, must be {0} bytes",
                    _size);
                throw new ArgumentException(msg, "data");
            }

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
        public virtual void Clear()
        {
            _data = null;
        }

        /// <summary>
        /// Get checksum data as a string.
        /// </summary>
        /// <returns>Sum as string.</returns>
        public virtual string GetAsString()
        {
            if (_data == null)
                return "";

            StringBuilder retString = new StringBuilder();
            foreach (byte by in _data)
            {
                retString.AppendFormat("{0:x2}", by);
            }
            return retString.ToString();
        }

        /// <summary>
        /// Get checksum data as a byte array.
        /// </summary>
        /// <returns>Sum as byte array.</returns>
        public virtual byte[] GetAsByteArray()
        {
            return _data;
        }
    }
}
