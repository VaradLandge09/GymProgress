using FluentValidation;
using GymProgress.Application.DTOs.Auth;
using GymProgress.Application.Interfaces.Services;
using GymProgress.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace GymProgress.API.Controllers;

/// <summary>
/// Thin HTTP boundary. All it does is: validate the shape of the request,
/// delegate the actual work to IAuthService, and format the response.
/// No business logic lives here — that keeps the controller trivially
/// testable and easy to reason about.
/// </summary>
[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterRequestDto> _registerValidator;
    private readonly IValidator<LoginRequestDto> _loginValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegisterRequestDto> registerValidator,
        IValidator<LoginRequestDto> loginValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    /// <summary>Creates a new Gym Progress account.</summary>
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register(
        [FromBody] RegisterRequestDto request,
        CancellationToken cancellationToken)
    {
        await _registerValidator.ValidateAndThrowAsync(request, cancellationToken);

        var result = await _authService.RegisterAsync(request, cancellationToken);

        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Account created successfully."));
    }

    /// <summary>Authenticates a user and issues an access + refresh token pair.</summary>
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        await _loginValidator.ValidateAndThrowAsync(request, cancellationToken);

        var result = await _authService.LoginAsync(request, cancellationToken);

        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful."));
    }
}
