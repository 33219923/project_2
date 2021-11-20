using System;
using System.Runtime.Serialization;

namespace PhotoAlbum.Shared.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException() : base()
        {
        }

        public InvalidRequestException(string message) : base(message)
        {
        }

        protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

