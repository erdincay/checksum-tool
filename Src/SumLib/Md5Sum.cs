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

// $Id$

using System;
using System.Security.Cryptography;
using System.IO;
using NUnit.Framework;

namespace CheckSumTool.SumLib
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
            if (data == null)
                throw new ArgumentNullException("data");
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
            if (data == null)
                throw new ArgumentNullException("data");
            if (sum == null)
                throw new ArgumentNullException("sum");

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

    [TestFixture]
    public class TestMd5Sum
    {
        // Precalculated MD5-sum for TestData/File1.txt
        // This sum was calculated by Cygwin's md5Sum -tool
        static byte[] File1Sum = { 0x78, 0xcf, 0x91, 0xda, 0xf3, 0x73, 0xe2,
            0x86, 0x41, 0x5c, 0x36, 0xa8, 0xb0, 0x35, 0xdb, 0xa9 };

         /// <summary>
         /// Test creating Md5Sum.
         /// </summary>
        [Test]
        public void Create()
        {
            Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);
        }

        /// <summary>
        /// Test giving null parameter for calculate.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateBytesFromNull()
        {
            Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            byte[] check = sum.Calculate((byte[])null);
        }

        /// <summary>
        /// Test giving empty data array for calculate.
        /// </summary>
        [Test]
        public void CalculateBytesFromEmpty()
        {
            Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            byte[] data = new byte[0];
            byte[] check = sum.Calculate(data);
            Assert.IsNotNull(check);
            Assert.AreEqual(16, check.Length);
        }

        /// <summary>
        /// Test calculating Md5 for known test file, and compare to Md5
        /// calculated by Cygwin's md5sum.
        /// </summary>
        [Test]
        public void CalculateFromTestFile1()
        {
            Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            byte[] check = sum.Calculate(array);

            Assert.IsNotNull(check);
            Assert.AreEqual(16, check.Length);
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
        /// Test calculating Md5 for known test file, and compare to Md5
        /// calculated by Cygwin's md5sum.
        /// </summary>
        [Test]
        public void CalculateFromTestFile1Stream()
        {
            Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            byte[] check = sum.Calculate(file);
            file.Close();

            Assert.IsNotNull(check);
            Assert.AreEqual(16, check.Length);
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
        /// Test givin empty array for verification.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullArray()
        {
             Md5Sum sum = new Md5Sum();
             Assert.IsNotNull(sum);

            bool match = sum.Verify((byte[])null, new byte[1]);
        }

        /// <summary>
        /// Test giving null array for verification.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullArray2()
        {
             Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            bool match = sum.Verify(new byte[1], (byte[])null);
        }

        /// <summary>
        /// Test verifying known file against known md5.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1()
        {
             Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            bool match = sum.Verify(array, File1Sum);
            Assert.IsTrue(match);
        }

        /// <summary>
        /// Test verifying known file against known md5.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1Stream()
        {
            Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            bool match = sum.Verify(file, File1Sum);
            file.Close();
            Assert.IsTrue(match);
        }

        /// <summary>
        /// Test verifying against wrong md5 sum.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1Fail()
        {
             Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            // Create invalid checksum
            byte[] invalidSum = new byte[16];
            Array.Copy(File1Sum, invalidSum, 16);
            invalidSum[3] = 0x00;

            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            bool match = sum.Verify(array, invalidSum);
            Assert.IsFalse(match);
        }

        /// <summary>
        /// Test verifying against wrong md5 sum.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1StreamFail()
        {
             Md5Sum sum = new Md5Sum();
            Assert.IsNotNull(sum);

            // Create invalid checksum
            byte[] invalidSum = new byte[16];
            Array.Copy(File1Sum, invalidSum, 16);
            invalidSum[3] = 0x00;

            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            bool match = sum.Verify(file, invalidSum);
            file.Close();
            Assert.IsFalse(match);
        }
    }
}
