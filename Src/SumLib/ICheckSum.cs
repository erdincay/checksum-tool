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
using System.IO;

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Interface for calculating and verifying checksums for data.
    /// </summary>
    public interface ICheckSum
    {
        /// <summary>
        /// Calculate a checksum from the data.
        /// </summary>
        /// <param name="data">Data to process.</param>
        /// <returns>Checksum from the data.</returns>
        byte[] Calculate(byte[] data);
        
        /// <summary>
        /// Calculate a checksum from the data.
        /// </summary>
        /// <param name="stream">Stream data to process.</param>
        /// <returns>Checksum from the data.</returns>
        byte[] Calculate(Stream stream);
                
        /// <summary>
        /// Verify data against checksum.
        /// </summary>
        /// <param name="data">Data to verify.</param>
        /// <param name="sum">Checksum for the data.</param>
        /// <returns>true if data and checksum match, false otherwise.</returns>
        bool Verify(byte[] data, byte[] sum);
        
        /// <summary>
        /// Verify stream data against checksum.
        /// </summary>
        /// <param name="stream">Stream data to verify.</param>
        /// <param name="sum">Checksum for the data.</param>
        /// <returns>true if data and checksum match, false otherwise.</returns>
        bool Verify(Stream stream, byte[] sum);
    }
}
