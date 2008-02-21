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
using System.Xml;
using System.Xml.XPath;
using NUnit.Framework;

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of XPathHandler.
    /// </summary>
    public class XPathHandler
    {
        string _fileName;

        /// <summary>
        /// Constructor. Set _fileName value.
        /// </summary>
        /// <param name="path"></param>
        public XPathHandler(string path)
        {
            _fileName = path;
        }

        /// <summary>
        /// Getting node, where is component specific info.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public XPathNodeIterator GetComponentSettings(string path)
        {
            XPathDocument doc = new XPathDocument(_fileName);
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr = nav.Compile(path);

            XPathNodeIterator iterator = nav.Select(expr);

            return iterator;
        }

        /// <summary>
        /// Setting position info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public void SetPositionValues(IComponentSetting component, XPathNodeIterator iterator)
        {
            int x = -1;
            int y = -1;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "X":
                         try
                        {
                            x = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            x = -1;
                        }
                        break;
                    case "Y":
                        try
                        {
                            y = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            y = -1;
                        }
                        break;
                    default:
                        break;
                  }
             }
            component.SetPosition(x, y);
        }

        /// <summary>
        /// Setting form specific info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public void SetFormValues(FormSetting component, XPathNodeIterator iterator)
        {
            int height = -1;
            int width = -1;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "Width":
                         try
                        {
                            width = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            width = -1;
                        }
                        break;
                    case "Height":
                        try
                        {
                            height = Convert.ToInt32(navigator.Value);
                        }
                        catch(Exception)
                        {
                            height = -1;
                        }
                        break;
                    default:
                        break;
                  }
             }

            component.SetValues(width, height);
        }

        /// <summary>
        /// Setting toolbar specific info.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="iterator"></param>
        public void SetToolbarValues(ToolbarSetting component, XPathNodeIterator iterator)
        {
            bool visible = false;

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "Visible":
                         try
                        {
                            visible = Convert.ToBoolean(navigator.Value);
                        }
                        catch(Exception)
                        {
                            visible = false;
                        }
                        break;
                    default:
                        break;
                  }
             }

            component.SetValues(visible);
        }

        /// <summary>
        /// Not in use.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SavePosition(string path, int x, int y)
        {
            XmlTextReader reader = new XmlTextReader(_fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode oldForm;
            XmlElement root = doc.DocumentElement;
            oldForm = root.SelectSingleNode(path);

            XmlElement newForm = doc.CreateElement("form");
            newForm.SetAttribute("name","MainForm");

            newForm.InnerXml = "<X>" + x + "</X>" +
                               "<Y>" + y + "</Y>" +
                               "<Width>400</Width>" +
                               "<Height>600</Height>";



            root.ReplaceChild(newForm, oldForm);
            //root.RemoveChild(oldForm);
            doc.Save(_fileName);
        }

        /// <summary>
        /// Saves form values to config file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="form"></param>
        public void SaveForm(string path, FormSetting form)
        {
            XmlTextReader reader = new XmlTextReader(_fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode oldForm;
            XmlElement root = doc.DocumentElement;
            oldForm = root.SelectSingleNode(path);

            XmlElement newForm = doc.CreateElement("form");
            newForm.SetAttribute("name", form.Name);

            newForm.InnerXml = "<X>" + form.X + "</X>" +
                               "<Y>" + form.Y + "</Y>" +
                               "<Width>" + form.Width + "</Width>" +
                               "<Height>" + form.Height + "</Height>";

            root.ReplaceChild(newForm, oldForm);
            doc.Save(_fileName);
        }

        /// <summary>
        /// Saves toolbar values to config file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="toolbar"></param>
        public void SaveToolbar(string path, ToolbarSetting toolbar)
        {
            XmlTextReader reader = new XmlTextReader(_fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode oldForm;
            XmlElement root = doc.DocumentElement;
            oldForm = root.SelectSingleNode(path);

            XmlElement newForm = doc.CreateElement("toolbar");
            newForm.SetAttribute("name", toolbar.Name);

            newForm.InnerXml = "<X>" + toolbar.X + "</X>" +
                               "<Y>" + toolbar.Y + "</Y>" +
                               "<Visible>" + toolbar.Visible + "</Visible>";

            root.ReplaceChild(newForm, oldForm);
            doc.Save(_fileName);
        }
    }

    [TestFixture]
    public class TestXPathHandler
    {
        /// <summary>
        /// Test getting Form node
        /// </summary>
        [Test]
        public void TestGetComponentSettings()
        {
            XPathHandler handler = new XPathHandler(@"../../TestData/config.xml");
            XPathNodeIterator iterator = handler.GetComponentSettings(@"/configuration/form[@name=""MainForm""]/*");

            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current.Clone();

                switch (navigator.Name)
                {
                    case "X":
                        Assert.AreEqual(navigator.Value, "23");
                        break;
                    case "Y":
                        Assert.AreEqual(navigator.Value, "1");
                        break;
                    case "Width":
                        Assert.AreEqual(navigator.Value, "617");
                        break;
                    case "Height":
                        Assert.AreEqual(navigator.Value, "498");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
