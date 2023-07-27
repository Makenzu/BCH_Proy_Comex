using System;

namespace BCH.Comex.Core.BL.XEGI
{
    public class XegiException : Exception
    {
        public XegiException()
        {
        }

        public XegiException(string message)
            : base(message)
        {
        }

        public XegiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
