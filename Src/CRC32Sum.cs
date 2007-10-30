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
using System.IO;

namespace CheckSumTool
{
    /// <summary>
    /// Calculates and verifies CRC32 checksums.
    /// </summary>
    public class CRC32Sum : ICheckSum
    {
        /// <summary>
        /// Calculate checksum from byte table data.
        /// </summary>
        /// <param name="data">Data as byte table.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(byte[] data)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Calculate checksum from stream data.
        /// </summary>
        /// <param name="stream">Data as stream.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(Stream stream)
        {
            uint sum = Crc32.GetStreamCRC32(stream);
            byte[] hexSum = new byte[4];
            hexSum[0] = (byte)((sum & 0xff000000) >> 24);
            hexSum[1] = (byte)((sum & 0x00ff0000) >> 16);
            hexSum[2] = (byte)((sum & 0x0000ff00) >> 8);
            hexSum[0] = (byte)(sum & 0x000000ff);
            return hexSum;
        }
        
        /// <summary>
        /// Verify given byte data against given checksum.
        /// </summary>
        /// <param name="data">Data to check as byte table.</param>
        /// <param name="sum">Checksum.</param>
        /// <returns>true if data matches checksum, false otherwise.</returns>
        public bool Verify(byte[] data, byte[] sum)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Verify given stream data against given checksum.
        /// </summary>
        /// <param name="stream">Stream data to check.</param>
        /// <param name="sum">Checksum.</param>
        /// <returns>true if data matches checksum, false otherwise.</returns>
        public bool Verify(Stream stream, byte[] sum)
        {
            // CRC32 sum is 4 bytes
            if (sum.Length != 4)
                return false;
            
            uint newsum = Crc32.GetStreamCRC32(stream);
            byte[] hexSum = new byte[4];
            hexSum[0] = (byte)((newsum & 0xff000000) >> 24);
            hexSum[1] = (byte)((newsum & 0x00ff0000) >> 16);
            hexSum[2] = (byte)((newsum & 0x0000ff00) >> 8);
            hexSum[0] = (byte)(newsum & 0x000000ff);
            
            for (int i = 0; i < hexSum.Length; i++)
            {
                if (hexSum[i] != sum[i])
                    return false;
            }
            return true;
        }
    }
}
