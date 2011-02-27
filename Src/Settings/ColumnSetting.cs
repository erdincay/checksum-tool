/*
The MIT License

Copyright (c) 2007 Ixonos Plc
Copyright (c) 2007-2011 Kimmo Varis <kimmov@gmail.com>

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
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of ColumnSetting.
    /// </summary>
    public class ColumnSetting : IComponentSetting
    {
        List<Setting> _list = new List<Setting>();

        XPathHandler _handler;

        /// <summary>
        /// Constructor. Set default values and handler.
        /// </summary>
        /// <param name="handler"></param>
        public ColumnSetting(XPathHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Name value for component.
        /// </summary>
        public string Name
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "Name");
                return item.GetString();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "Name", value);
            }
        }

        /// <summary>
        /// DisplayIndex value for component.
        /// </summary>
        public int DisplayIndex
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "DisplayIndex");
                return item.GetInt();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "DisplayIndex", value);
            }
        }

        /// <summary>
        /// Width value for component.
        /// </summary>
        public int Width
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "Width");
                int width = item.GetInt();
                
                if (width <= 0)
                    width = 50;
                if (width > 500)
                    width = 50;
                        
                return width;
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "Width", value);
            }
        }

        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        public void GetSetting(string name)
        {
            SettingUtils.GetSetting(_list, _handler, this, name, "column");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting()
        {
            SettingUtils.SaveSetting(_handler, this, this.Name, "column");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayIndex"></param>
        /// <param name="width"></param>
        public void SaveSetting(string name, int displayIndex, int width)
        {
            this.Name = name;
            this.DisplayIndex = displayIndex;

            if(width<50)
                width=50;

            this.Width = width;

            SaveSetting();
        }

        /// <summary>
        /// Setting component position values.
        /// </summary>
        /// <param name="displayIndex"></param>
        public void SetValues(int displayIndex, int width)
        {
           Setting settingDisplayIndex = new Setting(0, "DisplayIndex", displayIndex);
           _list.Add(settingDisplayIndex);

           Setting settingWidth = new Setting(0, "Width", width);
           _list.Add(settingWidth);
        }

        /// <summary>
        /// Setting component position values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {

        }
    }
}
