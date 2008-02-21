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
    /// Description of ComponentSetting.
    /// </summary>
    public class FormSetting : IComponentSetting
    {
        string _name;
        int _x;
        int _y;
        int _width;
        int _height;

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
        /// Width value for component.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Height value for component.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Constructor. Set default values and handler.
        /// </summary>
        /// <param name="handler"></param>
        public FormSetting(XPathHandler handler)
        {
            _handler = handler;

            _x = -1;
            _y = -1;
            _width = -1;
            _height = -1;
        }

        /// <summary>
        /// Get component position and other values.
        /// </summary>
        /// <param name="name"></param>
        public void GetSetting(string name)
        {
            _name = name;

            string path = @"/configuration/form[@name=""$formName""]/*".Replace("$formName", name);
            XPathNodeIterator iterator = _handler.GetComponentSettings(path);
            _handler.SetFormValues(this, iterator.Clone());
            _handler.SetPositionValues(this, iterator.Clone());

        }

        /// <summary>
        /// Saving component position and other values.
        /// </summary>
        public void SaveSetting()
        {
            //TODO: Save first position values and then other.

            _handler.SaveForm(@"/configuration/form[@name=""$formName""]".Replace("$formName", this.Name), this);
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
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetValues(int width, int height)
        {
             _width = width;
             _height = height;
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

            Assert.AreEqual(mainFormSetting.Name, "MainForm");
            Assert.AreEqual(mainFormSetting.X, 23);
            Assert.AreEqual(mainFormSetting.Y, 1);
            Assert.AreEqual(mainFormSetting.Width, 617);
            Assert.AreEqual(mainFormSetting.Height, 498);
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
