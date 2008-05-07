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
using System.IO;
using System.Collections.Generic;

namespace CheckSumTool.SumLib
{
    /// <summary>
    /// Interface for reading / writing list of filenames and checksums.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Read data from file.
        /// </summary>
        /// <param name="reader">File for reading.</param>
        /// <returns>Readed items, checkSum and filename.</returns>
        List<Pair<string>> ReadData(TextFileReader reader);

        /// <summary>
        /// Write header info.
        /// </summary>
        /// <param name="file">Target file to write.</param>
        void Header(StreamWriter file);

        /// <summary>
        /// Write items to the file.
        /// </summary>
        /// <param name="file">Target file to write.</param>
        /// <param name="checkSum">CheckSum value.</param>
        /// <param name="relativePath">Relative path value.</param>
        void WriteDataRow(StreamWriter file, string checksum, string relativePath);

        /// <summary>
        /// Write header info.
        /// </summary>
        /// <param name="file">Target file to write.</param>
        void Footer(StreamWriter file);
    }
}
