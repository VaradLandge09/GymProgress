using GymProgress.API.Extensions;
using GymProgress.Application.DTOs.GymLogs;
using GymProgress.Application.DTOs.Profile;
using GymProgress.Application.Interfaces.Services;
using GymProgress.Shared.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymProgress.API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ProfileResponseDto>> GetProfileAsync(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var result = await _profileService.GetProfileAsync(userId, cancellationToken);
            return Ok(ApiResponse<ProfileResponseDto>.SuccessResponse(result));
        }

        [HttpPatch("profile")]
        public async Task<ActionResult<ProfileResponseDto>> UpdateProfileAsync(
            [FromBody] UpdateProfileRequestDto request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var result = await _profileService.UpdateProfileAsync(userId, request, cancellationToken);
            return Ok(ApiResponse<ProfileResponseDto>.SuccessResponse(result));
        }
    }
}
