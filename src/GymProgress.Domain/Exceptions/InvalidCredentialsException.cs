using GymProgress.Shared.Exceptions;

namespace GymProgress.Domain.Exceptions;

public class InvalidCredentialsException : AppException
{
    public InvalidCredentialsException()
        : base("The email or password provided is incorrect.", statusCode: 401)
    {
    }
}
