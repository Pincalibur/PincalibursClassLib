using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PincalibursClassLib
{
    public class RuntimeTimer
    {
        /// <summary>
        /// Alert! Bad english ;)
        /// This Class is for returning a time (default in seconds with 1 decimal place) for working a list of Methods added to the MethodHandler.
        /// 
        /// PincalibursClassLib
        /// </summary>

        #region Attribute
        public delegate void MethodHandler();
        public event MethodHandler MethodWorkEvent;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private System.Threading.Thread workThread;
        int timeCounter = 0;
        bool workFinished = false;
        #endregion
        #region Getter Setter

        /// <summary>
        /// The Interval the TimeCounter tries to count
        /// </summary>
        public double Interval
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }
        #endregion

        public RuntimeTimer()
        {
            timer.Interval = 100;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Tick);
            workThread = new System.Threading.Thread(DoWork);
        }

        /// <summary>
        /// Starts the Worktask and gives back the time elapsed in seconds
        /// </summary>
        /// <returns>string time elapsed in seconds</returns>
        public string GetRunTimeString()
        {
            string time = "";
            double timeInMiliSec = GetRunTimeInt();
            double timeInSec = timeInMiliSec / (1000 / timer.Interval);
            time = timeInSec + " sec";
            return time;
        }

        /// <summary>
        /// Starts the Worktask and gives back the count of the interval (by default 100)
        /// </summary>
        /// <returns>int count the interval was elapsed</returns>
        public int GetRunTimeInt()
        {
            workFinished = false;
            timeCounter = 0;
            timer.Start();
            workThread.Start();
            do { } while (!workFinished);
            return timeCounter;
        }

        private void DoWork()
        {
            MethodWorkEvent();
            timer.Stop();
            workFinished = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeCounter++;
        }
    }
}
