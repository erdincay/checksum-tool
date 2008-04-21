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
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using NUnit.Framework;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of ComponentSetting.
    /// </summary>
    public class FormSetting : IComponentSetting
    {
        List<Setting> _list = new List<Setting>();

        XPathHandler _handler;

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
        /// X value for component.
        /// </summary>
        public int X
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "X");
                return item.GetInt();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "X", value);
            }
        }

        /// <summary>
        /// Y value for component.
        /// </summary>
        public int Y
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "Y");
                return item.GetInt();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "Y", value);
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
                return item.GetInt();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "Width", value);
            }
        }

        /// <summary>
        /// Height value for component.
        /// </summary>
        public int Height
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "Height");
                return item.GetInt();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "Height", value);
            }
        }

        /// <summary>
        /// Constructor. Set default values and handler.
        /// </summary>
        /// <param name="handler"></param>
        public FormSetting(XPathHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        public void GetSetting(string name)
        {
            SettingUtils.GetSetting(_list, _handler, this, name, "form");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting()
        {
            //TODO: Save first position values and then other.
            SettingUtils.SaveSetting(_handler, this, this.Name, "form");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting(string name, int x, int y, int width, int height)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            SaveSetting();
        }

        /// <summary>
        /// Setting component position values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
           Setting settingX = new Setting(0, "X", x);
           _list.Add(settingX);

           Setting settingY = new Setting(0, "Y", y);
           _list.Add(settingY);
        }

        /// <summary>
        /// Setting component other then position values.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetValues(int width, int height)
        {
            Setting settingWidth = new Setting(0, "Width", width);
           _list.Add(settingWidth);

           Setting settingHeight = new Setting(0, "Height", height);
           _list.Add(settingHeight);
        }
    }

    [TestFixture]
    public class TestFormSetting
    {
        /// <summary>
        /// Test getting Form values and comparing.
        /// </summary>
        [Test]
        public void TestFormGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/config.xml");
            FormSetting mainFormSetting = new FormSetting(handler);
            mainFormSetting.GetSetting("MainForm");

            Assert.AreEqual("MainForm", mainFormSetting.Name);
            Assert.AreEqual(23, mainFormSetting.X);
            Assert.AreEqual(15, mainFormSetting.Y);
            Assert.AreEqual(617, mainFormSetting.Width);
            Assert.AreEqual(498, mainFormSetting.Height);
        }

        /// <summary>
        /// Testing saving and reading.
        /// First test gets Form values, then increase values
        /// and after that gets valuesa again and compare values.
        /// </summary>
        [Test]
        public void TestFormSaveSettinAndGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/configWrite.xml");
            FormSetting mainFormSettingFirst = new FormSetting(handler);
            mainFormSettingFirst.GetSetting("MainForm");

            FormSetting mainFormSettingSave = new FormSetting(handler);
            mainFormSettingSave.Name = "MainForm";
            mainFormSettingSave.X = mainFormSettingFirst.X + 1;
            mainFormSettingSave.Y = mainFormSettingFirst.Y + 1;
            mainFormSettingSave.Width = mainFormSettingFirst.Width + 1;
            mainFormSettingSave.Height = mainFormSettingFirst.Height + 1;
            mainFormSettingSave.SaveSetting();

            FormSetting mainFormSettingSecond = new FormSetting(handler);
            mainFormSettingSecond.GetSetting("MainForm");

            Assert.AreEqual(mainFormSettingSecond.Name, mainFormSettingSave.Name);
            Assert.AreEqual(mainFormSettingSecond.X,  mainFormSettingSave.X);
            Assert.AreEqual(mainFormSettingSecond.Y,  mainFormSettingSave.Y);
            Assert.AreEqual(mainFormSettingSecond.Width,  mainFormSettingSave.Width);
            Assert.AreEqual(mainFormSettingSecond.Height,  mainFormSettingSave.Height);
        }
    }
}
