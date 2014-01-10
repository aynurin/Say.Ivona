using System;
using System.Net;
using System.Runtime.Serialization;
using Say.Ivona.Model;

namespace Say.Ivona
{
    [Serializable]
    public class IvonaException : Exception
    {
        private readonly IvonaExceptionDetails _details;

        public IvonaException()
        {
        }

        public IvonaException(string message) : base(message)
        {
        }

        public IvonaException(string message, Exception inner) : base(message, inner)
        {
        }

        protected IvonaException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public IvonaException(IvonaExceptionDetails details, WebException ex) : base(details.Description, ex)
        {
            _details = details;
        }

        public IvonaExceptionDetails Details
        {
            get { return _details; }
        }
    }
}