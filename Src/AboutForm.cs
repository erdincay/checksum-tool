/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
Copyright (c) 2007-2008 Kimmo Varis <kimmov@winmerge.org>

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
using System.Drawing;
using System.Windows.Forms;

namespace CheckSumTool
{
    /// <summary>
    /// About-dialog of the application. Contains a link to program's website.
    /// </summary>
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Constructor. Set version number and link.
        /// </summary>
        public AboutForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            string version = GetVersion();
            labelVersion.Text += " ";
            labelVersion.Text += version;

            // Set the homepage link
            linkHomepage.Links[0].LinkData = SumFileInfo.URL;
        }

        /// <summary>
        /// Close the dialog when user clicks OK-button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BtnOkClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Read version info from current assembly.
        /// </summary>
        /// <returns>Version number as a string.</returns>
        public static string GetVersion()
        {
            return SumFileInfo.GetVersion();
        }

        /// <summary>
        /// Called when link in form is clicked. Opens link in browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;

            //Check that link looks like an URL and open it to browser.
            if (target.StartsWith(@"http://") || target.StartsWith("www."))
            {
                System.Diagnostics.Process.Start(target);
            }
        }
    }
}
