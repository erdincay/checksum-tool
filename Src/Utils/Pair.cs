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
using System.Collections.Generic;

namespace CheckSumTool
{
    /// <summary>
    /// This class holds two (related) items together.
    /// </summary>
    public class Pair<T>
    {
        /// <summary>
        /// First item.
        /// </summary>
        T _item1;
        /// <summary>
        /// Second item.
        /// </summary>
        T _item2;

        /// <summary>
        /// First item of the pair.
        /// </summary>
        public T Item1
        {
            get { return _item1; }
            set { _item1 = value; }
        }
        
        /// <summary>
        /// Second item of the pair.
        /// </summary>
        public T Item2
        {
            get { return _item2; }
            set { _item2 = value; }
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Pair()
        {
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="item1">First item.</param>
        /// <param name="item2">Second item.</param>
        public Pair(T item1, T item2)
        {
            _item1 = item1;
            _item2 = item2;
        }
    }
}
