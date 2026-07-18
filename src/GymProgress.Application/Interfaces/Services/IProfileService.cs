using GymProgress.Application.DTOs.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfileResponseDto> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<ProfileResponseDto> UpdateProfileAsync(Guid userId, UpdateProfileRequestDto request, CancellationToken cancellationToken = default);
    }
}
