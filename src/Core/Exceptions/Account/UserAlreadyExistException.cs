using System.Net;
using Core.Enums;

namespace Core.Exceptions.Account
{
    public class UserAlreadyExistException : BaseDomainException
    {
        public UserAlreadyExistException()
            : base(ErrorCode.UserAlreadyExist, "User already exist")
        { }
    }
}