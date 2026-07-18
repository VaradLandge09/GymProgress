using GymProgress.Shared.Exceptions;

namespace GymProgress.Domain.Exceptions;

public class UserAlreadyExistsException : AppException
{
    public UserAlreadyExistsException(string email)
        : base($"A user with email '{email}' already exists.", statusCode: 409)
    {
    }
}
