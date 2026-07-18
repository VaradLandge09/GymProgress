using GymProgress.Application.DTOs.Profile;
using GymProgress.Application.Interfaces.Services;
using GymProgress.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GymProgress.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ProfileResponseDto> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken) 
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

            return new ProfileResponseDto
            (
                userId,
                user.FullName,
                user.Username,
                user.Email,
                user.CreatedAtUtc,
                user.WeeklyGoalDays
            );
        }

        public async Task<ProfileResponseDto> UpdateProfileAsync(Guid userId, UpdateProfileRequestDto request, CancellationToken cancellationToken = default)
        {
            var user = _userRepository.GetByIdAsync(userId, cancellationToken).Result
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");


            if (request.Username is not null) user.Username = request.Username;

            if (request.WeeklyGoalDays is not null) user.WeeklyGoalDays = request.WeeklyGoalDays.Value;

            if (request.FullName is not null) user.FullName = request.FullName;

            if (request.Email is not null) user.Email = request.Email;

            user.UpdatedAtUtc = DateTime.UtcNow;


            await _userRepository.SaveChangesAsync(cancellationToken);

            return new ProfileResponseDto
            (
                userId,
                user.FullName,
                user.Username,
                user.Email,
                user.CreatedAtUtc,
                user.WeeklyGoalDays
            );
        }
    }
}
