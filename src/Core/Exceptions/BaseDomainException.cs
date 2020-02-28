using System;
using System.Net;
using Core.Enums;

namespace Core.Exceptions
{
    public class BaseDomainException : Exception
    {
        public virtual ErrorCode ErrorCode { get; }
        public virtual HttpStatusCode HttpStatusCode { get; }
        public BaseDomainException(ErrorCode code, string message, HttpStatusCode httpCode = HttpStatusCode.BadRequest) : base(message)
        {
            ErrorCode = code;
            HttpStatusCode = httpCode;
        }
    }
}