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

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Interface for checksum data. This interface defines methods for
    /// handling checksum data. And all checksum data containers implement
    /// this interface.
    /// </summary>
    public interface ICheckSumData
    {
        /// <summary>
        /// Returns data as a string.
        /// </summary>
        /// <returns>String containing the checksum data.</returns>
        string GetAsString();

        /// <summary>
        /// Returns data as a byte array.
        /// </summary>
        /// <returns>Byte array containing the checksum data</returns>
        byte[] GetAsByteArray();

        /// <summary>
        /// Set checksum data from a string.
        /// </summary>
        /// <param name="data">String containing checksum data.</param>
        void Set(string data);

        /// <summary>
        /// Set checksum data from a byte array.
        /// </summary>
        /// <param name="data">Byte array containing the checksum data.</param>
        void Set(byte[] data);

        /// <summary>
        /// Clear the data.
        /// </summary>
        void Clear();
    }
}
