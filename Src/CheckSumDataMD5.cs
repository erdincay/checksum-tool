﻿/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc, Kimmo Varis <kimmo.varis@ixonos.com>

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

namespace CheckSumTool
{
    /// <summary>
    /// A class holding MD5 checksum data.
    /// </summary>
    public class CheckSumDataMD5 : CheckSumData
    {
        /// <summary>
        ///  Length of the MD5 checksum data (in bytes).
        /// </summary>
        public static readonly int Length = 16;

        /// <summary>
        /// Constructor setting data as a byte array.
        /// </summary>
        /// <param name="data">Checksum data as a byte array.</param>
        public CheckSumDataMD5(byte[] data)
        {
            DataSize = Length;
            Set(data);
        }

        /// <summary>
        /// Constructor setting data as a string.
        /// </summary>
        /// <param name="data">Checksum data as a string.</param>
        public CheckSumDataMD5(string data)
        {
            DataSize = Length;
            Set(data);
        }
    }
}
