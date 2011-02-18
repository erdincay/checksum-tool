/*
The MIT License

Copyright (c) 2011 Kimmo Varis <kimmov@gmail.com>

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
using System.Windows.Forms;
using CheckSumTool.Utils;

namespace CheckSumTool
{
    /// <summary>
    /// A progress form showing progress bar and textual message.
    /// </summary>
    public partial class ProgressForm : Form
    {
        private ProgressInfo _progressInfo;
        
        /// <summary>
        /// Initialize the form.
        /// </summary>
        public ProgressForm(ProgressInfo info)
        {
            InitializeComponent();
            _progressInfo = info;
        }

        /// <summary>
        /// Set the form caption text.
        /// </summary>
        /// <param name="caption">Text to set as a caption.</param>
        public void SetCaption(string caption)
        {
            Text = caption;
        }

        /// <summary>
        /// Set the progress message.
        /// </summary>
        /// <param name="message">Progress message shown in the form.</param>
        public void SetMessage(string message)
        {
            lblStatus.Text = message;
        }

        /// <summary>
        /// Set maximum value for the progress steps.
        /// </summary>
        /// <param name="count">Maximum progress step value.</param>
        public void SetMaxProgress(int count)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = count;
        }

        /// <summary>
        /// Set current progress step/value.
        /// </summary>
        /// <param name="count">Current progress step/value.</param>
        public void SetCurrentProgress(int count)
        {
            progressBar.Value = count;
        }

        /// <summary>
        /// Set progress bar style to marquee or normal (continuous).
        /// </summary>
        /// <param name="marquee">If true the progress bar is marquee, otherwise normal.</param>
        public void SetMarquee(bool marquee)
        {
            if (marquee)
            {
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.Maximum = 100;
            }
            else
            {
                progressBar.Style = ProgressBarStyle.Continuous;
            }
        }

        /// <summary>
        /// Center the form to parent when it is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            CenterToParent();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _progressInfo.Stop();
        }
    }
}
