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
using System.Security.Cryptography;
using System.IO;

namespace CheckSumTool
{
    /// <summary>
    /// Class for SHA1 checksum calculating and verifying.
    /// </summary>
    public class Sha1Sum : ICheckSum
    {
        /// <summary>
        /// SHA1 checksum calculator
        /// </summary>
        private SHA1Managed _sha1;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Sha1Sum()
        {
            _sha1 = new SHA1Managed();
            _sha1.Initialize();
        }
        
        /// <summary>
        /// Calculate SHA1 checksum for given data.
        /// </summary>
        /// <param name="data">Data for which to calculate checksum</param>
        /// <returns>Checksum for data</returns>
        public byte[] Calculate(byte[] data)
        {
            byte[] hash = _sha1.ComputeHash(data);
            return hash;
        }
        
        /// <summary>
        /// Calculate SHA1 checksum for given stream data.
        /// </summary>
        /// <param name="stream">Stream for which to calc checksum.</param>
        /// <returns>Checksum for data.</returns>
        public byte[] Calculate(Stream stream)
        {
            byte[] hash = _sha1.ComputeHash(stream);
            return hash;
        }
        
        /// <summary>
        /// Check if given data matches given SHA1-checksum.
        /// </summary>
        /// <param name="data">Data to check</param>
        /// <param name="sum">SHA1 checksum to verify against</param>
        /// <returns>true if checksum matches, false otherwise</returns>
        public bool Verify(byte[] data, byte[] sum)
        {
            byte[] newSum = Calculate(data);
            if (sum.Length != newSum.Length)
                return false;
            
            for (int i = 0; i < newSum.Length; i++)
            {
                if (newSum[i] != sum[i])
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Check if given data matches given SHA1-checksum.
        /// </summary>
        /// <param name="stream">Stream data to check.</param>
        /// <param name="sum">Known checksum to verify.</param>
        /// <returns>true if checksum matches, false otherwise</returns>
        public bool Verify(Stream stream, byte[] sum)
        {
            byte[] newSum = Calculate(stream);
            if (sum.Length != newSum.Length)
                return false;
            
            for (int i = 0; i < newSum.Length; i++)
            {
                if (newSum[i] != sum[i])
                    return false;
            }
            return true;
        }
    }
}
