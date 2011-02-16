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
using System.IO;
using System.Collections.Generic;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Interface for getting and setting component setting values.
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// Name value for Setting.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Set Setting int value.
        /// </summary>
        /// <param name="settingValue"></param>
        void SetInt(int settingValue);

        /// <summary>
        /// Get Setting int value.
        /// </summary>
        /// <returns></returns>
        int GetInt();

        /// <summary>
        /// Set Setting string value.
        /// </summary>
        /// <param name="settingValue"></param>
        void SetString(string settingValue);

        /// <summary>
        /// Get Setting string value.
        /// </summary>
        /// <returns></returns>
        string GetString();

        /// <summary>
        /// Set Setting boolean value.
        /// </summary>
        /// <param name="settingValue"></param>
        void SetBool(bool settingValue);

        /// <summary>
        /// Get Setting boolean value.
        /// </summary>
        /// <returns></returns>
        bool GetBool();
    }
}

