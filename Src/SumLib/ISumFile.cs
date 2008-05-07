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

namespace CheckSumTool
{
    /// <summary>
    /// Interface for reading / writing list of filenames and checksums.
    /// </summary>
    public interface ISumFile
    {
        /// <summary>
        /// Set the list into which items are added.
        /// </summary>
        /// <param name="list">List getting items.</param>
        void SetFileList(CheckSumFileList list);

        /// <summary>
        /// Read items from a file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        /// <returns>Number of items read.</returns>
        int ReadFile(string path, CheckSumType fileType);

        /// <summary>
        /// Write items to the file.
        /// </summary>
        /// <param name="path">Full path to the file.</param>
        void WriteFile(string path, CheckSumType currentSumType);
    }
}
