using System.Net;
using Core.Enums;

namespace Core.Exceptions.Account
{
    public class CannotCreateUserException : BaseDomainException
    {
        public CannotCreateUserException(string message) : base(ErrorCode.CannotCreateUser, message)
        { }
    }
}