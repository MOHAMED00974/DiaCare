using DiaCare.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiaCare.Application.Adapters
{
    public class PredictionAdapter : IPredictionAdapter
    {

        //convert PascalCase => snake_case
        //TO AI
        public object MapToAiRequest(PredictionInputDto dto)
        {
            return new Dictionary<string, object>
            {
                ["bmi"] = dto.Bmi,
                ["blood_pressure"] = dto.BloodPressure,
                ["fasting_glucose_level"] = dto.FastingGlucoseLevel,
                ["insulin_level"] = dto.InsulinLevel,
                ["HbA1c_level"] = dto.HbA1cLevel,
                ["cholesterol_level"] = dto.CholesterolLevel,
                ["triglycerides_level"] = dto.TriglyceridesLevel,
                ["physical_activity_level"] = dto.PhysicalActivityLevel,
                ["daily_calorie_intake"] = dto.DailyCalorieIntake,
                ["sugar_intake_grams_per_day"] = dto.SugarIntakeGramsPerDay,
                ["family_history_diabetes"] = dto.FamilyHistoryDiabetes,
                ["waist_circumference_cm"] = dto.WaistCircumferenceCm
            };
        }
        public PredictionResultDto MapFromAiResponse(JsonElement aiResponse)
        {
            //From AI
            return new PredictionResultDto
            {
                RiskCategory = aiResponse.GetProperty("risk_category").GetString(),
                RiskScore = aiResponse.GetProperty("risk_score").GetDouble()
            };
        }
    }
}
