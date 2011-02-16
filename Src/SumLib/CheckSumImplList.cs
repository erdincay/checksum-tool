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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Checksum types tool handles.
    /// </summary>
    public enum CheckSumType
    {
        SHA1,
        MD5,
        CRC32,
    }

    /// <summary>
    /// Class containing list of the available checksum implementations.
    /// </summary>
    public class CheckSumImplList
    {
        /// <summary>
        /// String names for available checksum implementations.
        /// </summary>
        public static readonly string[] SumNames = { "SHA-1", "MD5", "CRC32", };

        /// <summary>
        /// Get checksum calculator for given checksum type.
        /// </summary>
        /// <param name="impl">Checksum type.</param>
        /// <returns>Checksum calculator.</returns>
        public static ICheckSum GetImplementation(CheckSumType impl)
        {
            ICheckSum sum;
            switch (impl)
            {
                case CheckSumType.SHA1:
                    sum = new Sha1Sum();
                    break;
                case CheckSumType.MD5:
                    sum = new Md5Sum();
                    break;
                case CheckSumType.CRC32:
                    sum = new CRC32Sum();
                    break;
                default:
                    sum = new Sha1Sum();
#if DEBUG
                    throw new ApplicationException("Unknown Sum type!");
#else
                    break;
#endif
            }
            return sum;
        }

        /// <summary>
        /// Get name for checksum implementation.
        /// </summary>
        /// <param name="impl">Checkcum implementation.</param>
        /// <returns>Name of checksum implementation.</returns>
        public static string GetImplementationName(CheckSumType impl)
        {
            string name = SumNames[(int) impl];
            return name;
        }
    }

#if NUNIT
    /// <summary>
    /// A unit testing class for CheckSumImplList -class.
    /// </summary>
    [TestFixture]
    public class TestCheckSumImplList
    {
        [Test]
        public void GetSHA1Imp()
        {
            ICheckSum impl = CheckSumImplList.GetImplementation(
                CheckSumType.SHA1);

            Assert.IsNotNull(impl);
            Assert.IsInstanceOfType(typeof(Sha1Sum), impl);
        }

        [Test]
        public void GetMD5Imp()
        {
            ICheckSum impl = CheckSumImplList.GetImplementation(
                CheckSumType.MD5);

            Assert.IsNotNull(impl);
            Assert.IsInstanceOfType(typeof(Md5Sum), impl);
        }

        [Test]
        public void GetCRC32Imp()
        {
            ICheckSum impl = CheckSumImplList.GetImplementation(
                CheckSumType.CRC32);

            Assert.IsNotNull(impl);
            Assert.IsInstanceOfType(typeof(CRC32Sum), impl);
        }

        [Test]
        public void GetSHA1Name()
        {
            string name = CheckSumImplList.GetImplementationName(
                CheckSumType.SHA1);

            Assert.IsNotNull(name);
            Assert.AreEqual("SHA-1", name);
        }

        [Test]
        public void GetMD5Name()
        {
            string name = CheckSumImplList.GetImplementationName(
                CheckSumType.MD5);

            Assert.IsNotNull(name);
            Assert.AreEqual("MD5", name);
        }

        [Test]
        public void GetCRC32Name()
        {
            string name = CheckSumImplList.GetImplementationName(
                CheckSumType.CRC32);

            Assert.IsNotNull(name);
            Assert.AreEqual("CRC32", name);
        }
    }
#endif
}
