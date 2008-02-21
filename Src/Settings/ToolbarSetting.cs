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
using NUnit.Framework;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of ToolbarSetting.
    /// </summary>
    public class ToolbarSetting : IComponentSetting
    {
        string _name;
        int _x;
        int _y;
        bool _visible;

        XPathHandler _handler;

        /// <summary>
        /// Name value for component.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// X value for component.
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Y value for component.
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Visible value for component.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// Constructor. Set default values and handler.
        /// </summary>
        /// <param name="handler"></param>
        public ToolbarSetting(XPathHandler handler)
        {
            _handler = handler;

            _x = -1;
            _y = -1;
            _visible = true;
        }

        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        public void GetSetting(string name)
        {
            _name = name;

            string path = @"/configuration/toolbar[@name=""$toolbarName""]/*".Replace("$toolbarName", name);
            XPathNodeIterator iterator = _handler.GetComponentSettings(path);
            _handler.SetPositionValues(this, iterator.Clone());
            _handler.SetToolbarValues(this, iterator.Clone());
        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting()
        {
            //TODO: Save first position values and then other.

            _handler.SaveToolbar(@"/configuration/toolbar[@name=""$toolbarName""]".Replace("$toolbarName", this.Name), this);
        }

        /// <summary>
        /// Setting component position values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
             _x = x;
             _y = y;
        }

        /// <summary>
        /// Setting component other then position values.
        /// </summary>
        /// <param name="visible"></param>
        public void SetValues(bool visible)
        {
            _visible = visible;
        }
    }

    [TestFixture]
    public class TestToolbarSetting
    {
        /// <summary>
        /// Test getting Toolbar values and comparing.
        /// </summary>
        [Test]
        public void TestToolbarGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/config.xml");
            ToolbarSetting settingToolStripFile = new ToolbarSetting(handler);
            settingToolStripFile.GetSetting("toolStripFile");

            Assert.AreEqual(settingToolStripFile.Name, "toolStripFile");
            Assert.AreEqual(settingToolStripFile.X, 3);
            Assert.AreEqual(settingToolStripFile.Y, 24);
            Assert.AreEqual(settingToolStripFile.Visible, true);
        }

        /// <summary>
        /// Testing saving and reading.
        /// First test gets Toolbar values, then increase values
        /// and after that gets values again and compare values.
        /// </summary>
        [Test]
        public void TestToolbarSaveSettinAndGetSetting()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/configWrite.xml");
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

            Assert.AreEqual(settingToolStripFileSecond.Name, settingToolStripFileSave.Name);
            Assert.AreEqual(settingToolStripFileSecond.X,  settingToolStripFileSave.X);
            Assert.AreEqual(settingToolStripFileSecond.Y,  settingToolStripFileSave.Y);
            Assert.AreEqual(settingToolStripFileSecond.Visible,  settingToolStripFileSave.Visible);
        }
    }
}
