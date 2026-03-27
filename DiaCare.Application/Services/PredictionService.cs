using DiaCare.Application.Adapters;
using DiaCare.Application.Interfaces;
using DiaCare.Domain.DTOS;
using DiaCare.Domain.Entities;
using DiaCare.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiaCare.Application.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly string? _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly IPredictionAdapter _predictionAdapter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public PredictionService(HttpClient httpClient, IConfiguration configuration,
            IPredictionAdapter predictionAdapter,IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
            {
                _baseUrl = configuration["AISettings:BaseUrl"]
                    ?? throw new Exception("AI Base URL is missing");

                _httpClient = httpClient;
                _predictionAdapter = predictionAdapter;
                _httpContextAccessor = httpContextAccessor;
                _unitOfWork = unitOfWork;
            }
        public async Task<PredictionResultDto> PredictAsync(PredictionInputDto inputdto)
        {
            try
            {
                // 1. Map to AI
                var ClientAiRequest = _predictionAdapter.MapToAiRequest(inputdto);

                // 2. Call AI
                var ModelReq = await _httpClient.PostAsJsonAsync(_baseUrl, ClientAiRequest);

                if (!ModelReq.IsSuccessStatusCode)
                {
                    return new PredictionResultDto { RiskCategory = "AI Model Error" };
                }

                // 3. Read response
                var aiRawResponse = await ModelReq.Content.ReadFromJsonAsync<JsonElement>();

                // 4. Map response
                var resultDto = _predictionAdapter.MapFromAiResponse(aiRawResponse);

                // 5. Get UserId
                var userId = _httpContextAccessor.HttpContext?.User?
                    .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                // 6. Create HealthProfile
                var healthProfile = new HealthProfile
                {
                    UserId = userId,

                    Bmi = inputdto.Bmi,
                    BloodPressure = inputdto.BloodPressure,
                    FastingGlucoseLevel = inputdto.FastingGlucoseLevel,
                    InsulinLevel = inputdto.InsulinLevel,
                    HbA1cLevel = inputdto.HbA1cLevel,
                    CholesterolLevel = inputdto.CholesterolLevel,
                    TriglyceridesLevel = inputdto.TriglyceridesLevel,
                    PhysicalActivityLevel = inputdto.PhysicalActivityLevel,
                    DailyCalorieIntake = inputdto.DailyCalorieIntake,
                    SugarIntakeGramsPerDay = inputdto.SugarIntakeGramsPerDay,
                    FamilyHistoryDiabetes = inputdto.FamilyHistoryDiabetes,
                    WaistCircumferenceCm = inputdto.WaistCircumferenceCm,

                    CreatedAt = DateTime.UtcNow
                };

                // 7. Save HealthProfile
                await _unitOfWork.HealthProfiles.AddAsync(healthProfile);

                // 8. Create PredictionResult
                var predictionRecord = new PredictionResult
                {
                    UserId = userId,
                    HealthProfile = healthProfile, //  Navigation property
                    RiskScore = resultDto.RiskScore,
                    ResultText = resultDto.RiskCategory,
                    Recommendation = "Please follow up with a specialist for further advice.",
                    CreatedAt = DateTime.UtcNow
                };
                // 9. Save Prediction
                await _unitOfWork.PredictionResults.AddAsync(predictionRecord);
                await _unitOfWork.SaveChangesAsync();

                return resultDto;
            }
            catch (Exception ex)
            {

                return new PredictionResultDto { RiskCategory = "Service unavailable" };
                // to find error =>
                //  return new PredictionResultDto { RiskCategory = $"Debug Error: {ex.Message}" };
            }
           
        }
    }
}
