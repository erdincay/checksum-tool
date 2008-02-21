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
using System.IO;
using System.Collections.Generic;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Interface for getting and setting component config values.
    /// </summary>
    public interface IComponentSetting
    {
        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        void GetSetting(string name);

        /// <summary>
        /// Setting component position values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SetPosition(int x, int y);

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        void SaveSetting();
    }
}

