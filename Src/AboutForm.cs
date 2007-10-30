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

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace CheckSumTool
{
    /// <summary>
    /// About-dialog of the application.
    /// </summary>
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AboutForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            string version = GetVersion();
            lblVersion.Text = version;
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
        string GetVersion()
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            return curAssembly.GetName().Version.ToString();
        }
    }
}
