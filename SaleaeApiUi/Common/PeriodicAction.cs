using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaleaeApiUi.Common
{
    public abstract class PeriodicAction : IDisposable
    {
        //==========================================================================================
        // Set these up before calling Start()!!!

        public TimeSpan Period { get; set; } = TimeSpan.FromSeconds(2);
        public TimeSpan StartAfter { get; set; } = TimeSpan.Zero;
        public object StateObj { get; set; }

        //==========================================================================================

        public abstract void PeriodicFunction(object state);


        public void Start()
        {
            Stop();
            lock (TimerObjLock)
            {
                TimerObj = new Timer(PeriodicFunction, StateObj, StartAfter, Period);
            }
        }

        public void Stop()
        {
            lock (TimerObjLock)
            {
                TimerObj?.Dispose();
                TimerObj = null;
            }
        }

        //================================================================================ Timer
        private Timer TimerObj;
        private object TimerObjLock = new object();

        //================================================================================ Implement IDisposable
        #region Implement IDisposable
        private bool disposed = false;
        private object disposeLock = new object();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            lock (disposeLock)
            {
                // Check to see if Dispose has already been called.
                if (!this.disposed)
                {
                    // If disposing equals true, dispose all managed
                    // and unmanaged resources.
                    if (disposing)
                    {
                        // Dispose managed resources.
                        lock (TimerObjLock)
                        {
                            if (TimerObj != null)
                            {
                                TimerObj.Dispose();
                                TimerObj = null;
                            }
                        }
                    }

                    // Call the appropriate methods to clean up
                    // unmanaged resources here.
                    // If disposing is false,
                    // only the following code is executed.


                    // Note disposing has been done.
                    disposed = true;
                }
            }
        }

        ~PeriodicAction()
        {
            Dispose(false);
        }

        #endregion Implement IDisposable
        //================================================================================



    }
}
