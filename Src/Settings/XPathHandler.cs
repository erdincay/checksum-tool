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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.Settings
{
    /// <summary>
    /// Description of XPathHandler.
    /// </summary>
    public class XPathHandler
    {
        /// <summary>
        /// A full path to the XML configuration file.
        /// </summary>
        string _fileName;

        /// <summary>
        /// Constructor. Sets configuration file path.
        /// </summary>
        /// <param name="path">Configuration file's path.</param>
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
            XmlNode parentNode = oldForm.ParentNode;

            XmlElement newForm = doc.CreateElement("form");
            newForm.SetAttribute("name", form.Name);

            newForm.InnerXml = "<X>" + form.X + "</X>" +
                               "<Y>" + form.Y + "</Y>" +
                               "<Width>" + form.Width + "</Width>" +
                               "<Height>" + form.Height + "</Height>";

            //root.ReplaceChild(newForm, oldForm);
            parentNode.ReplaceChild(newForm, oldForm);
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
            XmlNode parentNode = oldForm.ParentNode;

            XmlElement newForm = doc.CreateElement("toolbar");
            newForm.SetAttribute("name", toolbar.Name);

            newForm.InnerXml = "<X>" + toolbar.X + "</X>" +
                               "<Y>" + toolbar.Y + "</Y>" +
                               "<Visible>" + toolbar.Visible + "</Visible>";

            // If existing element was not found, append setting as new element
            if (oldForm != null)
            {
                parentNode.ReplaceChild(newForm, oldForm);
            }
            else
            {
                parentNode.AppendChild(newForm);
            }
            doc.Save(_fileName);
        }

        /// <summary>
        /// Saves statusbar values to config file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="toolbar"></param>
        public void SaveStatusbar(string path, StatusbarSetting statusbar)
        {
            XmlTextReader reader = new XmlTextReader(_fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode oldForm;
            XmlElement root = doc.DocumentElement;
            oldForm = root.SelectSingleNode(path);
            XmlNode parentNode = oldForm.ParentNode;

            XmlElement newForm = doc.CreateElement("statusbar");
            newForm.SetAttribute("name", statusbar.Name);

            newForm.InnerXml = "<Visible>" + statusbar.Visible + "</Visible>";

            // If existing element was not found, append setting as new element
            if (oldForm != null)
            {
                parentNode.ReplaceChild(newForm, oldForm);
            }
            else
            {
                parentNode.AppendChild(newForm);
            }
            doc.Save(_fileName);
        }

        /// <summary>
        /// Saves column values to config file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="toolbar"></param>
        public void SaveColumn(string path, ColumnSetting column)
        {
            XmlTextReader reader = new XmlTextReader(_fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode oldForm;
            XmlElement root = doc.DocumentElement;
            oldForm = root.SelectSingleNode(path);
            XmlNode parentNode = oldForm.ParentNode;

            XmlElement newForm = doc.CreateElement("column");
            newForm.SetAttribute("name", column.Name);

            newForm.InnerXml = "<DisplayIndex>" + column.DisplayIndex + "</DisplayIndex>" +
                               "<Width>" + column.Width + "</Width>";

            // If existing element was not found, append setting as new element
            if (oldForm != null)
            {
                parentNode.ReplaceChild(newForm, oldForm);
            }
            else
            {
                parentNode.AppendChild(newForm);
            }
            doc.Save(_fileName);
        }
    }

#if NUNIT
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
                        Assert.AreEqual("23", navigator.Value);
                        break;
                    case "Y":
                        Assert.AreEqual("1", navigator.Value);
                        break;
                    case "Width":
                        Assert.AreEqual("617", navigator.Value);
                        break;
                    case "Height":
                        Assert.AreEqual("498", navigator.Value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
#endif
}
