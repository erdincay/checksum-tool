/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc, Kimmo Varis <kimmo.varis@ixonos.com>

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
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using NUnit.Framework;


namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of ToolbarSetting.
    /// </summary>
    public class StatusbarSetting : IComponentSetting
    {
        List<Setting> _list = new List<Setting>();

        XPathHandler _handler;

        /// <summary>
        /// Constructor. Set default values and handler.
        /// </summary>
        /// <param name="handler"></param>
        public StatusbarSetting(XPathHandler handler)
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
        /// Visible value for component.
        /// </summary>
        public bool Visible
        {
            get
            {
                Setting item = SettingUtils.GetSettingFromList(_list, "Visible");
                return item.GetBool();
            }

            set
            {
                SettingUtils.SetSettingFromList(_list, "Visible", value);
            }
        }

        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        public void GetSetting(string name)
        {
            SettingUtils.GetSetting(_list, _handler, this, name, "statusbar");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting()
        {
            //TODO: Save first position values and then other.
            SettingUtils.SaveSetting(_handler, this, this.Name, "statusbar");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting(string name, bool visible)
        {
            this.Name = name;
            this.Visible = visible;

            SaveSetting();
        }

        /// <summary>
        /// Setting component other then position values.
        /// </summary>
        /// <param name="visible"></param>
        public void SetValues(bool visible)
        {
           Setting settingVisible = new Setting(2, "Visible", visible);
           _list.Add(settingVisible);
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

    [TestFixture]
    public class TestToolbarSetting
    {
        [SetUp]
        public void InitTest()
        {
            File.Copy(@"../../TestData/configWrite.xml",
                      @"../../TestData/configWriteTemp.xml");
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(@"../../TestData/configWriteTemp.xml");
        }

        /// <summary>
        /// Test getting Toolbar values and comparing.
        /// </summary>
        [Test]
        public void TestToolbarGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/config.xml");
            StatusbarSetting settingStatusStrip1 = new StatusbarSetting(handler);
            settingStatusStrip1.GetSetting("statusStrip1");

            Assert.AreEqual("statusStrip1", settingStatusStrip1.Name);
            Assert.AreEqual(true, settingStatusStrip1.Visible);
        }

        /// <summary>
        /// Testing saving and reading.
        /// First test gets Toolbar values, then increase values
        /// and after that gets values again and compare values.
        /// </summary>
        [Test]
        public void TestToolbarSaveSettinAndGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/configWriteTemp.xml");
            StatusbarSetting settingStatusStrip1First = new StatusbarSetting(handler);
            settingStatusStrip1First.GetSetting("statusStrip1");

            StatusbarSetting settingStatusStrip1Save = new StatusbarSetting(handler);
            settingStatusStrip1Save.Name = "statusStrip1";
            settingStatusStrip1Save.Visible = !settingStatusStrip1First.Visible;
            settingStatusStrip1Save.SaveSetting();

            StatusbarSetting settingStatusStrip1Second = new StatusbarSetting(handler);
            settingStatusStrip1Second.GetSetting("statusStrip1");

            Assert.AreEqual(settingStatusStrip1Save.Name, settingStatusStrip1Second.Name);
            Assert.AreEqual(settingStatusStrip1Save.Visible, settingStatusStrip1Second.Visible);
        }
    }
}
