using GymProgress.Application.DTOs.GymLogs;
using GymProgress.Application.Interfaces.Services;
using GymProgress.Shared.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GymProgress.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GymProgress.API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class GymLogsController : ControllerBase
    {
        private readonly IGymLogService _gymLogService;

        public GymLogsController(IGymLogService gymLogService)
        {
            _gymLogService = gymLogService;
        }

        [HttpPost("gymlogs")]
        public async Task<ActionResult<GymLogResponseDto>> CreateGymLogAsync(
            [FromBody] GymLogPostRequestDto request, 
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var result = await _gymLogService.CreateGymLogAsync(userId, request, cancellationToken);
            return Ok(ApiResponse<GymLogResponseDto>.SuccessResponse(result));
        }

        [HttpGet("gymlogs")]
        public async Task<ActionResult<List<GymLogResponseDto>>> GetGymLogsAsync(
            [FromQuery] GymLogGetRequestDto request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var result = await _gymLogService.GetGymLogsByMonthAndYearAsync(userId, request, cancellationToken);
            return Ok(ApiResponse<List<GymLogResponseDto>>.SuccessResponse(result));
        }

    }
}
