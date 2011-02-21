/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
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
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CheckSumTool.Utils;

namespace CheckSumTool
{
    /// <summary>
    /// About-dialog of the application. Contains a link to program's website.
    /// </summary>
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Program version.
        /// </summary>
        private string _version;

        /// <summary>
        /// Homepage URL.
        /// </summary>
        private string _url;

        /// <summary>
        /// Constructor. Set version number and link.
        /// </summary>
        public AboutForm()
        {
            // The InitializeComponent() call is required for Windows Forms
            // designer support.
            InitializeComponent();

            GetInfo();
            labelVersion.Text += " ";
            labelVersion.Text += _version;

            // Set the homepage link
            linkHomepage.Links[0].LinkData = _url;
        }

        /// <summary>
        /// Close the dialog when user clicks OK-button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Get the program information.
        /// </summary>
        private void GetInfo()
        {
            // Get name of current executable and get info from it.
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            string filename = Path.GetFileName(curAssembly.Location);

            ProgramInfo info = new ProgramInfo(filename);
            _version = info.Version;
            _url = info.URL;
        }

        /// <summary>
        /// Called when homepage link in form is clicked. Opens homepage in browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkHomepageClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;

            // Check that link looks like an URL and open it to browser.
            if (target.StartsWith(@"http://") || target.StartsWith("www."))
            {
                System.Diagnostics.Process.Start(target);
            }
        }
    }
}
