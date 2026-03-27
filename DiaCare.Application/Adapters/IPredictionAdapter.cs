using DiaCare.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiaCare.Application.Adapters
{
    public interface IPredictionAdapter
    {
        // Req
        object MapToAiRequest(PredictionInputDto dto);

        // Res
        PredictionResultDto MapFromAiResponse(JsonElement aiResponse);

    }
}
