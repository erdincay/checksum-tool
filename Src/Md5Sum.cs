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
    /// Calculates and verifies MD5 checksums.
    /// </summary>
    public class Md5Sum : ICheckSum
    {
        /// <summary>
        /// MD5 crypto service by system.
        /// </summary>
        private MD5CryptoServiceProvider _md5Sum;
            
        /// <summary>
        /// Constructor.
        /// </summary>
        public Md5Sum()
        {
            _md5Sum = new MD5CryptoServiceProvider();
            _md5Sum.Initialize();
        }
        
        /// <summary>
        /// Calculate checksum from byte table data.
        /// </summary>
        /// <param name="data">Data as byte table.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(byte[] data)
        {
            byte[] sum = _md5Sum.ComputeHash(data);
            return sum;
        }
        
        /// <summary>
        /// Calculate checksum from stream data.
        /// </summary>
        /// <param name="stream">Data as stream.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(Stream stream)
        {
            byte[] sum = _md5Sum.ComputeHash(stream);
            return sum;
        }
        
        /// <summary>
        /// Verify given data against given checksum.
        /// </summary>
        /// <param name="data">Data as byte table.</param>
        /// <param name="sum">Checksum.</param>
        /// <returns>true if data matches checksum, false otherwise.</returns>
        public bool Verify(byte[] data, byte[] sum)
        {
            byte[] newSum = _md5Sum.ComputeHash(data);
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
        /// Verify given data against given checksum.
        /// </summary>
        /// <param name="stream">Data as stream.</param>
        /// <param name="sum">Checksum.</param>
        /// <returns>true if data matches checksum, false otherwise.</returns>
        public bool Verify(Stream stream, byte[] sum)
        {
            byte[] newSum = _md5Sum.ComputeHash(stream);
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
