using System;
using System.Diagnostics;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Libraries.Benchmark
{
    public class TimeCounter
            : IDisposable
    {
        #region Variables

        private static TimeCounter _instance;
        private Stopwatch _stopWatch;
        private TimeSpan _begin = TimeSpan.Zero;
        private TimeSpan _end = TimeSpan.Zero;
        private double _responseTime = 0;

        #endregion

        #region Constructors

        private TimeCounter()
        {
            this._stopWatch = new Stopwatch();

            this.Start();
        }

        #endregion

        #region Attributes

        public static TimeCounter Instance
        {
            get
            {
                _instance = new TimeCounter();

                return _instance;
            }
        }

        public TimeSpan Begin { get { return this._begin; } }

        public TimeSpan End { get { return this._end; } }

        //Em Milisegundos
        public double ResponseTime
        {
            get
            {
                this.Stop();

                var result = this._responseTime;

                this.Restart();

                return result;
            }
        }

        //Em Minutos
        public double ResponseTimeMinutes
        {
            get
            {
                this.Stop();

                var result = this._responseTime / 100000;

                this.Restart();

                return result;
            }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            this._stopWatch = null;
        }

        private void Start()
        {
            if (!this._stopWatch.IsNull())
                this._stopWatch.Start();

            this._begin = new TimeSpan
                (
                    0,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    DateTime.Now.Second,
                    DateTime.Now.Millisecond
                );
        }

        private void Stop()
        {
            if (!this._stopWatch.IsNull())
                this._stopWatch.Stop();

            this._end = new TimeSpan
                (
                    0,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    DateTime.Now.Second,
                    DateTime.Now.Millisecond
                );

            if (!this._stopWatch.IsNull())
                this._responseTime = (this._stopWatch.Elapsed).TotalMilliseconds;
        }

        private void Restart()
        {
            if (!this._stopWatch.IsNull())
                this._stopWatch.Restart();
        }

        public void Reset()
        {
            if (!this._stopWatch.IsNull())
            {
                this._stopWatch.Reset();

                this._stopWatch.Start();
            }

            this._begin = TimeSpan.Zero;
            this._end = TimeSpan.Zero;
        }

        #endregion
    }
}
