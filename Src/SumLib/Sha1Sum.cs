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
using System.Security.Cryptography;
using System.IO;
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Class for SHA1 checksum calculating and verifying.
    /// </summary>
    public class Sha1Sum : ICheckSum, IDisposable
    {
        /// <summary>
        /// Size of the byte table for SHA1 sum.
        /// </summary>
        public static readonly int SumSize = 20;

        /// <summary>
        /// SHA1 checksum calculator
        /// </summary>
        private SHA1Managed _sha1;

        /// <summary>
        /// Has the object been disposed?
        /// </summary>
        private bool _disposed;

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
        /// <returns>Checksum for data.</returns>
        public byte[] Calculate(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

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
            if (data == null)
                throw new ArgumentNullException("data");
            if (sum == null)
                throw new ArgumentNullException("sum");

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

        /// <summary>
        /// Implement IDisposable pattern for cleanup.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose resources.
        /// </summary>
        /// <param name="disposing">If true, cleanup managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sha1.Clear();
                }
                _disposed = true;
            }
        }
    }

#if NUNIT
    /// <summary>
    /// Unit testing class for Sha1Sum class.
    /// </summary>
    [TestFixture]
    public class TestSha1Sum
    {
        // Precalculated SHA1-sum for TestData/File1.txt
        // This sum was calculated by Cygwin's Sha1Sum -tool
           static byte[] File1Sum = { 0x94, 0xa3, 0x22, 0x5c, 0x6b, 0xac, 0x57,
                0x3a, 0x06, 0xda, 0x75, 0xb0, 0x5b, 0xcf, 0x6d, 0xe5, 0x9f,
                0x65, 0xdb, 0x2c };

        /// <summary>
        /// Test creating sum instance.
        /// </summary>
        [Test]
        public void Create()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);
        }

        /// <summary>
        /// Test giving null parameter to sum method.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateFromBytesNull()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            byte[] check = sum.Calculate((byte[])null);
            Assert.IsNull(check);
        }

        /// <summary>
        /// Test creating checksum for empty byte table.
        /// </summary>
        [Test]
        public void CalculateFromEmptyTable()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            byte[] array = new byte[0];
            byte[] check = sum.Calculate(array);
            Assert.IsNotNull(check);
            Assert.AreEqual(Sha1Sum.SumSize, check.Length);
        }

        /// <summary>
        /// Compare sum calculated by this class to sum calculated by
        /// Cygwin's Sha1Tool.
        /// </summary>
        [Test]
        public void CalculateFromTestFile1()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            byte[] check = sum.Calculate(array);

            Assert.IsNotNull(check);
            Assert.AreEqual(Sha1Sum.SumSize, check.Length);
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
        /// Compare sum calculated by this class to sum calculated by
        /// Cygwin's Sha1Tool.
        /// </summary>
        [Test]
        public void CalculateFromTestFile1Stream()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            byte[] check = sum.Calculate(file);
            file.Close();

            Assert.IsNotNull(check);
            Assert.AreEqual(Sha1Sum.SumSize, check.Length);
            for (int i = 0; i < File1Sum.Length; i++)
            {
                if (check[i] != File1Sum[i])
                {
                    Assert.Fail("Result byte {0} does not match! Was {1:x2}, expected {2:x2}",
                        i, check[i], File1Sum[i]);
                }
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullArray()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            bool match = sum.Verify((byte[])null, new byte[1]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNullArray2()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            bool match = sum.Verify(new byte[1], (byte[])null);
        }

        /// <summary>
        /// Verify sum calculated by this class to sum calculated by
        /// Cygwin's Sha1Tool.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            bool match = sum.Verify(array, File1Sum);
            Assert.IsTrue(match);
        }

        /// <summary>
        /// Verify sum calculated by this class to sum calculated by
        /// Cygwin's Sha1Tool.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1Stream()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            bool match = sum.Verify(file, File1Sum);
            file.Close();
            Assert.IsTrue(match);
        }

        /// <summary>
        /// Verify sum calculated by this class to sum calculated by
        /// Cygwin's Sha1Tool.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1Fail()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            // Create invalid checksum
            byte[] invalidSum = new byte[Sha1Sum.SumSize];
            Array.Copy(File1Sum, invalidSum, Sha1Sum.SumSize);
            invalidSum[3] = 0x00;

            byte[] array = File.ReadAllBytes("../../TestData/TextFile1.txt");
            bool match = sum.Verify(array, invalidSum);
            Assert.IsFalse(match);
        }

        /// <summary>
        /// Verify sum calculated by this class to sum calculated by
        /// Cygwin's Sha1Tool.
        /// </summary>
        [Test]
        public void VerifyFromTestFile1StreamFail()
        {
            Sha1Sum sum = new Sha1Sum();
            Assert.IsNotNull(sum);

            // Create invalid checksum
            byte[] invalidSum = new byte[Sha1Sum.SumSize];
            Array.Copy(File1Sum, invalidSum, Sha1Sum.SumSize);
            invalidSum[3] = 0x00;

            FileStream file = new FileStream("../../TestData/TextFile1.txt",
                FileMode.Open);
            bool match = sum.Verify(file, invalidSum);
            file.Close();
            Assert.IsFalse(match);
        }
    }
#endif
}
