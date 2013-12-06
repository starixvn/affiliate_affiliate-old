// -----------------------------------------------------------------------
// <copyright file="IBCException.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System;
 
    public class IBCException : Exception
    {
        public IBCException() : base() { }
        public IBCException(string message) : base(message) { }
        public IBCException(string message, Exception ex) : base(message, ex) { }
        protected IBCException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
