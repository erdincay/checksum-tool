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
using System.IO;
using NUnit.Framework;

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
            byte[] hexSum = UintToByteTable(sum);
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
            if (sum == null)
                throw new ArgumentNullException("sum");

            // CRC32 sum is 4 bytes
            if (sum.Length != 4)
                return false;

            uint newsum = Crc32.GetStreamCRC32(stream);
            byte[] hexSum = UintToByteTable(newsum);

            for (int i = 0; i < hexSum.Length; i++)
            {
                if (hexSum[i] != sum[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Convert integer to byte array.
        /// </summary>
        /// <param name="number">Number to convert.</param>
        /// <returns>Number as byte array.</returns>
        byte[] UintToByteTable(uint number)
        {
            byte[] hexSum = new byte[4];
            hexSum[0] = (byte)((number & 0xff000000) >> 24);
            hexSum[1] = (byte)((number & 0x00ff0000) >> 16);
            hexSum[2] = (byte)((number & 0x0000ff00) >> 8);
            hexSum[3] = (byte)(number & 0x000000ff);
            return hexSum;
        }
    }

    /// <summary>
    /// Unit testing class for CRC32Sum class.
    /// </summary>
    [TestFixture]
    public class TestCrc32Sum
    {
        /// <summary>
        /// CRC32-checksum calculated for TextFile1.txt with fsum tool.
        /// </summary>
        static readonly byte[] File1Sum = { 0xb0, 0x46, 0xe6, 0xf2 };

        /// <summary>
        /// Test calculating with null stream.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateNullStream()
        {
            CRC32Sum sum = new CRC32Sum();
            byte[] check = sum.Calculate((Stream)null);
        }

        /// <summary>
        /// Test calculating CRC32 for TextFile1.txt
        /// </summary>
        [Test]
        public void CalculateFile1Stream()
        {
            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);

            CRC32Sum sum = new CRC32Sum();
            byte[] check = sum.Calculate(file);
            file.Close();
            Assert.IsNotNull(check);
            Assert.AreEqual(4, check.Length);
            for (int i = 0; i < File1Sum.Length; i++)
            {
                if (check[i] != File1Sum[i])
                {
                    Assert.Fail("Result byte {0} does not match! Was {1:x2}, expected {2:x2}",
                        i, check[i], File1Sum[i]);
                }
            }
        }

        /// <summary>
        /// Test verifying null stream.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullStream()
        {
            CRC32Sum sum = new CRC32Sum();
            bool correct = sum.Verify((Stream)null, new byte[4]);
        }

        /// <summary>
        /// Test verifying null checksum.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullSumStream()
        {
            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            CRC32Sum sum = new CRC32Sum();
            try
            {
                bool correct = sum.Verify(file, null);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Make sure stream is closed.
                file.Close();
            }
        }

        /// <summary>
        /// Test verifying TextFile1.txt
        /// </summary>
        [Test]
        public void VerifyFile1Stream()
        {
            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            CRC32Sum sum = new CRC32Sum();
            bool correct = sum.Verify(file, File1Sum);
            file.Close();
            Assert.IsTrue(correct);
        }

        /// <summary>
        /// Test verifying against invalid checksum.
        /// </summary>
        [Test]
        public void VerifyFile1StreamFail()
        {
            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            CRC32Sum sum = new CRC32Sum();

            // Create invalid checksum
            byte[] invalidSum = new byte[4];
            Array.Copy(File1Sum, invalidSum, 4);
            invalidSum[1] = 0x00;

            bool correct = sum.Verify(file, invalidSum);
            file.Close();
            Assert.IsFalse(correct);
        }
    }
}
