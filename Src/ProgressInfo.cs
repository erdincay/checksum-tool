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

namespace CheckSumTool
{
    /// <summary>
    /// ProgresInfo is used when have to updare UI in other thread.
    /// </summary>
    public class ProgressInfo
    {
        int _max; 
        int _min;
        int _now;
        bool _ready;
        bool _stop;
        bool _run;
        string _filename;
        int _succeeded;
        
        public ProgressInfo()
        {
        }
        
        /// <summary>
        /// ProgresInfo Max value.
        /// </summary>
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }
        
        /// <summary>
        /// ProgresInfo Min value.
        /// </summary>
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }
        
        /// <summary>
        /// ProgresInfo Now value.
        /// </summary>
        public int Now
        {
            get { return _now; }
            set { _now = value; }
        }
        
        /// <summary>
        /// ProgresInfo Ready value.
        /// </summary>
        public bool Ready
        {
            get { return _ready; }
            set { _ready = value; }
        }
        
        /// <summary>
        /// ProgresInfo Stop value.
        /// </summary>
        public bool Stop
        {
            get { return _stop; }
            set { _stop = value; }
        }
        
        /// <summary>
        /// ProgresInfo Run value.
        /// </summary>
        public bool Run
        {
            get { return _run; }
            set { _run = value; }
        }
        
        /// <summary>
        /// ProgresInfo Filename value.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        
        /// <summary>
        /// ProgresInfo Succeeded value.
        /// </summary>
        public int Succeeded
        {
            get { return _succeeded; }
            set { _succeeded = value; }
        }
        
        /// <summary>
        /// Set default values for ProgresInfo.
        /// </summary>
        public void DefaultSetting()
        {
            _max = 0;
            _min = 0;
            _now = 0;
            _ready = false;
            _stop = false;
            _run = false;     
            _succeeded = -1;
        }
    }
}
