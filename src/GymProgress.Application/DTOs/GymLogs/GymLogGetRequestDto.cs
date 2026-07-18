using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Application.DTOs.GymLogs
{
    public record GymLogGetRequestDto(
        int? Month = null,
        int? Year = null
        );
}
