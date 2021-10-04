using System;

namespace EventsAndDelegates
{
    public class WorkPerformedEventArgs : EventArgs
    {
        int _Hours;

        public int Hours
        {
            get { return _Hours; }
            set { _Hours = value; }
        }
        DateTime _WorkDate;

        public DateTime WorkDate
        {
            get { return _WorkDate; }
            set { _WorkDate = value; }
        }

        public WorkPerformedEventArgs(int hours, DateTime workDate)
        {
            this.Hours = hours;
            this.WorkDate = workDate;
        }
    }
}
