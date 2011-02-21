/*
The MIT License

Copyright (c) 2011 Kimmo Varis <kimmov@gmail.com>

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
    /// This class tracks what kind of items are visible in the UI.
    /// The items have different states and the visibility is determined based
    /// on the item state and the selected menu items in the GUI.
    /// </summary>
    public class VisibleItemStates
    {
        /// <summary>
        /// Are items that have checksum calculated visible
        /// </summary>
        private bool _calculated;

        /// <summary>
        /// Are items withouth checksum calculated visible?
        /// </summary>
        private bool _nonCalculated;

        /// <summary>
        /// Are verified items visible?
        /// </summary>
        private bool _verified;

        /// <summary>
        /// Are items that have checksum calculated visible
        /// </summary>
        public bool Calculated
        {
            get { return _calculated; }
            set { _calculated = value; }
        }

        /// <summary>
        /// Are items withouth checksum calculated visible?
        /// </summary>
        public bool NonCalculated
        {
            get { return _nonCalculated; }
            set { _nonCalculated = value; }
        }

        /// <summary>
        /// Are verified items visible?
        /// </summary>
        public bool Verified
        {
            get { return _verified; }
            set { _verified = value; }
        }

        /// <summary>
        /// The constructor sets all items visible by default.
        /// </summary>
        public VisibleItemStates()
        {
            _nonCalculated = true;
            _calculated = true;
            _verified = true;
        }
    }
}
