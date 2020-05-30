using System;
using System.Collections;

namespace RCheck
{
    class TRun
    {
        public TCheck Check;
        public TCell PosTo;
        public ArrayList Killed;
        public TRun(TCheck Check, TCell PosTo)
        {
            this.Check = Check;
            this.PosTo = PosTo;
            Killed = new ArrayList();
        }
    }

    class TRuns
    {
        ArrayList arr;
        Random rnd;

        public TRuns()
        {
            arr = new ArrayList();
            rnd = new Random();
        }

        public TRun GetR()
        {
            if(Count < 1)
            {
                return null;
            }
            else
            {
                return this[rnd.Next(Count)];
            }
        }

        public void Add(TRun Run)
        {
            arr.Add(Run);
        }

        public int Count
        {
            get
            {
                return arr.Count;
            }
        }

        public TRun this[int i]
        {
            get
            {
                return (TRun)arr[i];
            }
            set
            {
                arr[i] = value;
            }
        }
    }
}
