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
    /// This class combines information of file and checksum.
    /// </summary>
    public class CheckSumItem
    {
        /// <summary>
        /// Full path (full path + filename) for the item
        /// </summary>
        private string _fullPath;

        /// <summary>
        /// Checksum calculated for the item
        /// </summary>
        private CheckSumData _checkSum;

        /// <summary>
        /// Is item verified against checksum?
        /// </summary>
        private bool _verified;

        /// <summary>
        /// Filename (without path) for the item.
        /// </summary>
        public string FileName
        {
            get
            { 
                int ind = _fullPath.LastIndexOf('\\');
                string filename = _fullPath.Substring(ind + 1);
                return filename;
            }
        }
        
        /// <summary>
        /// Full path (inc. filename) for the item.
        /// </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set { _fullPath = value; }
        }
       
        /// <summary>
        /// Checksum calculated for the item.
        /// </summary>
        public CheckSumData CheckSum
        {
            get { return _checkSum; }
        }
        
        /// <summary>
        /// Is item verified against checksum?
        /// </summary>
        public bool Verified
        {
            get { return _verified; }
            set { _verified = value; }
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Full path to file.</param>
        public CheckSumItem(string path)
        {
            _fullPath = path;
        }
        
        /// <summary>
        /// Set the checksum.
        /// </summary>
        /// <param name="data">Checksum as byte table.</param>
        public void SetSum(byte[] data)
        {
            _checkSum = new CheckSumData(data);
        }
        
        /// <summary>
        /// Set the checksum.
        /// </summary>
        /// <param name="data">Checksum as string.</param>
        public void SetSum(string data)
        {
            _checkSum = new CheckSumData(data);
        }
    }
}
