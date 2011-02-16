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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of ToolbarSetting.
    /// </summary>
    public class ToolbarSetting : IComponentSetting
    {
        List<Setting> _list = new List<Setting>();

        XPathHandler _handler;

        /// <summary>
        /// Constructor. Set default values and handler.
        /// </summary>
        /// <param name="handler"></param>
        public ToolbarSetting(XPathHandler handler)
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
            SettingUtils.GetSetting(_list, _handler, this, name, "toolbar");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting()
        {
            //TODO: Save first position values and then other.
            SettingUtils.SaveSetting(_handler, this, this.Name, "toolbar");
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting(string name, int x, int y, bool visible)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            this.Visible = visible;

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
        /// <param name="visible"></param>
        public void SetValues(bool visible)
        {
           Setting settingVisible = new Setting(2, "Visible", visible);
           _list.Add(settingVisible);
        }
    }

#if NUNIT
    [TestFixture]
    public class TestStatusbarSetting
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
        /// Test getting Statusbar values and comparing.
        /// </summary>
        [Test]
        public void TestToolbarGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/config.xml");
            ToolbarSetting settingToolStripFile = new ToolbarSetting(handler);
            settingToolStripFile.GetSetting("toolStripFile");

            Assert.AreEqual("toolStripFile", settingToolStripFile.Name);
            Assert.AreEqual(3, settingToolStripFile.X);
            Assert.AreEqual(24, settingToolStripFile.Y);
            Assert.AreEqual(true, settingToolStripFile.Visible);
        }

        /// <summary>
        /// Testing saving and reading.
        /// First test gets Statusbar values, then increase values
        /// and after that gets values again and compare values.
        /// </summary>
        [Test]
        public void TestToolbarSaveSettinAndGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/configWriteTemp.xml");
            ToolbarSetting settingToolStripFileFirst = new ToolbarSetting(handler);
            settingToolStripFileFirst.GetSetting("toolStripFile");

            ToolbarSetting settingToolStripFileSave = new ToolbarSetting(handler);
            settingToolStripFileSave.Name = "toolStripFile";
            settingToolStripFileSave.X = settingToolStripFileFirst.X + 1;
            settingToolStripFileSave.Y = settingToolStripFileFirst.Y + 1;
            settingToolStripFileSave.Visible = !settingToolStripFileFirst.Visible;
            settingToolStripFileSave.SaveSetting();

            ToolbarSetting settingToolStripFileSecond = new ToolbarSetting(handler);
            settingToolStripFileSecond.GetSetting("toolStripFile");

            Assert.AreEqual(settingToolStripFileSave.Name, settingToolStripFileSecond.Name);
            Assert.AreEqual(settingToolStripFileSave.X, settingToolStripFileSecond.X);
            Assert.AreEqual(settingToolStripFileSave.Y, settingToolStripFileSecond.Y);
            Assert.AreEqual(settingToolStripFileSave.Visible, settingToolStripFileSecond.Visible);
        }
    }
#endif
}
