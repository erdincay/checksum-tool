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

namespace CheckSumTool
{
    /// <summary>
    /// Class containing list of the available checksum implementations.
    /// </summary>
    public class CheckSumImplList
    {
        /// <summary>
        /// List of available checksum implementations.
        /// </summary>
        public enum SumImplementation
        {
            SHA1,
            MD5,
            CRC32,
            Count,  // Keep this as last item to get count of items
        }
        
        /// <summary>
        /// String names for available checksum implementations.
        /// </summary>
        public static readonly string[] SumNames = { "SHA-1", "MD5", "CRC32", };

    
        /// <summary>
        /// Get checksum calculator for given checksum type.
        /// </summary>
        /// <param name="impl">Checksum type.</param>
        /// <returns>Checksum calculator.</returns>
        public static ICheckSum GetImplementation(SumImplementation impl)
        {
            ICheckSum sum;
            switch (impl)
            {
                case SumImplementation.SHA1:
                    sum = new Sha1Sum();
                    break;
                case SumImplementation.MD5:
                    sum = new Md5Sum();
                    break;
                case SumImplementation.CRC32:
                    sum = new CRC32Sum();
                    break;
                default:
                    sum = new Sha1Sum();
                    break;
            }
            return sum;            
        }
        
        /// <summary>
        /// Get name for checksum implementation.
        /// </summary>
        /// <param name="impl">Checkcum implementation.</param>
        /// <returns>Name of checksum implementation.</returns>
        public static string GetImplementationName(SumImplementation impl)
        {
            string name = SumNames[(int) impl];
            return name;
        }
    }
}
