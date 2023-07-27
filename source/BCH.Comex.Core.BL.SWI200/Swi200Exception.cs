using System;

namespace BCH.Comex.Core.BL.SWI200
{
    public class Swi200Exception : Exception
    {
        public Swi200Exception()
        {
        }

        public Swi200Exception(string message)
            : base(message)
        {
        }

        public Swi200Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
