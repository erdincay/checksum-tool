/*
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
            MemoryStream ms = new MemoryStream(data);
            byte[] hexSum = Crc32.GetStreamCRC32(ms);
            ms.Close();
            return hexSum;
        }

        /// <summary>
        /// Calculate checksum from stream data.
        /// </summary>
        /// <param name="stream">Data as stream.</param>
        /// <returns>Checksum.</returns>
        public byte[] Calculate(Stream stream)
        {
            byte[] hexSum = Crc32.GetStreamCRC32(stream);
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
            if (data == null)
                throw new ArgumentNullException("data");
            if (sum == null)
                throw new ArgumentNullException("sum");

            // CRC32 sum is 4 bytes
            if (sum.Length != 4)
                return false;

            MemoryStream msData = new MemoryStream(data);
            MemoryStream msSum = new MemoryStream(sum);

            byte[] hexSum = Crc32.GetStreamCRC32(msData);
            int ind = 0;
            bool valid = true;
            while (ind < hexSum.Length && valid)
            {
                if (hexSum[ind] != sum[ind])
                    valid = false;
                else
                    ++ind;
            }
            msData.Close();
            msSum.Close();
            return valid;
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

            byte[] hexSum = Crc32.GetStreamCRC32(stream);
            for (int i = 0; i < hexSum.Length; i++)
            {
                if (hexSum[i] != sum[i])
                    return false;
            }
            return true;
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
        /// Test calculating CRC32 for TextFile1.txt using stream interface.
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
        /// Test calculating with null byte array.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateNullArray()
        {
            CRC32Sum sum = new CRC32Sum();
            byte[] check = sum.Calculate((byte[])null);

        }

        /// <summary>
        /// Test calculating CRC32 for TextFile1.txt using byte array interface.
        /// </summary>
        [Test]
        public void CalculateFile1ByteArray()
        {
            CRC32Sum sum = new CRC32Sum();
            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            byte[] check = sum.Calculate(array);

            Assert.IsNotNull(check);
            Assert.AreEqual(4, check.Length);
            for (int i = 0; i < File1Sum.Length; i++)
            {
                if (check[i] != File1Sum[i])
                {
                    Assert.Fail("Result byte {0} does not match! Was {1}, expected {2}",
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
        /// Test verifying null filedata.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullByteData()
        {
            CRC32Sum sum = new CRC32Sum();
            bool correct = sum.Verify((byte[])null, new byte[4]);
        }

        /// <summary>
        /// Test verifying null checksum.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullSumData()
        {
            byte[] fileArray = File.ReadAllBytes("../../TestData/TextFile1.txt");
            CRC32Sum sum = new CRC32Sum();
            bool correct = sum.Verify(fileArray, null);
        }

        /// <summary>
        /// Test verifying TextFile1.txt using file stream.
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
        /// Test verifying TextFile1.txt using byte array interface.
        /// </summary>
        [Test]
        public void VerifyFile1ByteArray()
        {
            byte[] fileArray = File.ReadAllBytes("../../TestData/TextFile1.txt");
            CRC32Sum sum = new CRC32Sum();
            bool correct = sum.Verify(fileArray, File1Sum);
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
