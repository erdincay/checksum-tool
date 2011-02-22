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
#if NUNIT
using NUnit.Framework;
#endif

namespace CheckSumTool.Utils
{
    /// <summary>
    /// ProgresInfo is used to share progress information between threads.
    /// Usually this means sharing progress data between backend processing
    /// and GUI.
    /// 
    /// There are two different states. First is the progress state as telling
    /// if we are doing something, waiting it to be stopped etc. Second is
    /// the activity indicator telling if we have some active processing going
    /// on.
    /// </summary>
    public class ProgressInfo
    {
        /// <summary>
        /// States in which our backend processing can be.
        /// </summary>
        public enum State
        {
            /// <summary>
            /// No processing is happening. The default state.
            /// </summary>
            Idle,
            /// <summary>
            /// The processing is currently running.
            /// </summary>
            Running,
            /// <summary>
            /// The progress is stopping (stopped by the user?).
            /// </summary>
            Stopping,
            /// <summary>
            /// The processing is ready.
            /// </summary>
            Ready,
        }

        /// <summary>
        /// Progress result values.
        /// </summary>
        public enum Result
        {
            Failed = 0,
            PartialSuccess,
            Success,
        }

        /// <summary>
        /// Progress state.
        /// </summary>
        State _state;
        /// <summary>
        /// Maximum value.
        /// </summary>
        int _max;
        /// <summary>
        /// Minimum value.
        /// </summary>
        int _min;
        /// <summary>
        /// Current value.
        /// </summary>
        int _now;
        /// <summary>
        /// Associated filename.
        /// </summary>
        string _filename;
        /// <summary>
        /// Result of the backend processing.
        /// </summary>
        Result _succeeded;
        /// <summary>
        /// Is the activity going on?
        /// </summary>
        bool _activity;

        /// <summary>
        /// Default constructor.
        /// </summary>
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
        /// Is there activity currently?
        /// </summary>
        /// <returns>true if there is activity currently, false otherwise.</returns>
        public bool HasActivity()
        {
            return _activity;
        }
        
        /// <summary>
        /// Is the processing running?
        /// </summary>
        /// <returns>true if the processing is running.</returns>
        public bool IsRunning()
        {
            return _state == State.Running;
        }

        /// <summary>
        /// Is the processing stopping (about to be stopped soon)?
        /// </summary>
        /// <returns>true if the processing is being stopped.</returns>
        public bool IsStopping()
        {
            return _state == State.Stopping;
        }

        /// <summary>
        /// Is the processing ready?
        /// </summary>
        /// <returns>true if the processing is ready.</returns>
        public bool IsReady()
        {
            return _state == State.Ready;
        }

        /// <summary>
        /// Start the processing.
        /// </summary>
        public void Start()
        {
            if (_state != State.Idle)
                throw new ApplicationException("Cannot start already running process!");

            _state = State.Running;
            _activity = true;
        }

        /// <summary>
        /// Comple the processing. This stops the processing when it becomes
        /// ready in natural way.
        /// </summary>
        public void Complete()
        {
            if (_state != State.Running && _state != State.Stopping)
                throw new ApplicationException("Cannot complete non-running process!");

            _state = State.Ready;
        }

        /// <summary>
        /// Stop the processing. This stops the processing before it is ready.
        /// </summary>
        public void Stop()
        {
            if (_state != State.Running)
                throw new ApplicationException("Cannot stop non-running process!");
            _state = State.Stopping;
        }

        /// <summary>
        /// Inform that the current activity is now ended.
        /// </summary>
        public void ActivityEnded()
        {
            if (_state != State.Running && _state != State.Stopping)
                throw new ApplicationException("Must be stopping when activity is stopped!");
            _activity = false;
        }

        /// <summary>
        /// Finish stopping the process that started stopping by calling Stop().
        /// </summary>
        public void Stopped()
        {
            if (_state != State.Stopping)
                throw new ApplicationException("Cannot stop-complete non-stopped process!");
            if (_activity == true)
                throw new ApplicationException("Cannot stop when activity is not ended!");
            _state = State.Ready;
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
        public Result Succeeded
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
            _state = State.Idle;
            _succeeded = Result.Failed;
            _filename = null;
        }
    }
    
#if NUNIT
    /// <summary>
    /// A unit testing class for ProgressInfo -class.
    /// </summary>
    [TestFixture]
    public class TestProgressInfo
    {
        [Test]
        public void Construct()
        {
            ProgressInfo prog = new ProgressInfo();
            Assert.AreEqual(0, prog.Max);
            Assert.AreEqual(0, prog.Min);
            Assert.AreEqual(0, prog.Now);
            Assert.AreEqual(null, prog.Filename);
            Assert.AreEqual(ProgressInfo.Result.Failed, prog.Succeeded);
            Assert.AreEqual(false, prog.HasActivity());
        }
        
        [Test]
        public void Setters()
        {
            ProgressInfo prog = new ProgressInfo();
            prog.Min = 10;
            Assert.AreEqual(10, prog.Min);
            prog.Max = 115;
            Assert.AreEqual(115, prog.Max);
            prog.Now = 48;
            Assert.AreEqual(48, prog.Now);
            prog.Filename = "test.cpp";
            Assert.AreEqual("test.cpp", prog.Filename);
            prog.Succeeded = ProgressInfo.Result.Success;
            Assert.AreEqual(ProgressInfo.Result.Success, prog.Succeeded);
        }
        
        [Test]
        public void normalProgress()
        {
            ProgressInfo prog = new ProgressInfo();
            prog.Start();
            Assert.IsTrue(prog.HasActivity());
            Assert.IsTrue(prog.IsRunning());
            Assert.IsFalse(prog.IsStopping());
            Assert.IsFalse(prog.IsReady());
            prog.ActivityEnded();
            Assert.IsFalse(prog.HasActivity());
            prog.Complete();
            Assert.IsFalse(prog.HasActivity());
            Assert.IsFalse(prog.IsRunning());
            Assert.IsFalse(prog.IsStopping());
            Assert.IsTrue(prog.IsReady());
        }
        
        [Test]
        public void stoppingProgress()
        {
            ProgressInfo prog = new ProgressInfo();
            prog.Start();
            Assert.IsTrue(prog.HasActivity());
            Assert.IsTrue(prog.IsRunning());
            Assert.IsFalse(prog.IsStopping());
            Assert.IsFalse(prog.IsReady());
            prog.Stop();
            Assert.IsTrue(prog.HasActivity());
            Assert.IsFalse(prog.IsRunning());
            Assert.IsTrue(prog.IsStopping());
            Assert.IsFalse(prog.IsReady());
            prog.ActivityEnded();
            Assert.IsFalse(prog.HasActivity());
            Assert.IsFalse(prog.IsRunning());
            Assert.IsTrue(prog.IsStopping());
            Assert.IsFalse(prog.IsReady());
            prog.Stopped();
            Assert.IsFalse(prog.HasActivity());
            Assert.IsFalse(prog.IsRunning());
            Assert.IsFalse(prog.IsStopping());
            Assert.IsTrue(prog.IsReady());
        }
    }
#endif
}
