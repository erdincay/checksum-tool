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

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of Setting.
    /// </summary>
    public class Setting : ISetting
    {
        string _name;
        string _settingValue;
        int _type;

        enum Type {INT, STRING, BOOL};

        /// <summary>
        /// Name value for Setting.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Setting Constructor for int type value.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="settingValue"></param>
        public Setting(int type, string name, int settingValue)
        {
            _type = type;
            _name = name;

            SetInt(settingValue);
        }

        /// <summary>
        /// Setting Constructor for string type value.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="settingValue"></param>
        public Setting(int type, string name, string settingValue)
        {
            _type = type;
            _name = name;

            SetString(settingValue);
        }

        /// <summary>
        /// Setting Constructor for boolean type value.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="settingValue"></param>
        public Setting(int type, string name, bool settingValue)
        {
            _type = type;
            _name = name;

            SetBool(settingValue);
        }

        /// <summary>
        /// Set Setting int value.
        /// </summary>
        /// <param name="settingValue"></param>
        public void SetInt(int settingValue)
        {
            _settingValue = settingValue.ToString();
            _type = (int) Type.INT;
        }

        /// <summary>
        /// Get Setting int value.
        /// </summary>
        /// <returns></returns>
        public int GetInt()
        {
            return Convert.ToInt32(_settingValue);
        }

        /// <summary>
        /// Set Setting string value.
        /// </summary>
        /// <param name="settingValue"></param>
        public void SetString(string settingValue)
        {
            _settingValue = settingValue;
            _type = (int) Type.STRING;
        }

        /// <summary>
        /// Get Setting string value.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return _settingValue;
        }

        /// <summary>
        /// Set Setting boolean value.
        /// </summary>
        /// <param name="settingValue"></param>
        public void SetBool(bool settingValue)
        {
            _settingValue = settingValue.ToString();
            _type = (int) Type.BOOL;
        }

        /// <summary>
        /// Get Setting boolean value.
        /// </summary>
        /// <returns></returns>
        public bool GetBool()
        {
            return Convert.ToBoolean(_settingValue);
        }
    }
}
