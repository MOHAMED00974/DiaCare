using System.Text.Json.Serialization;

public class PredictionInputDto
{
    public double Bmi { get; set; }

    [JsonPropertyName("blood_pressure")]
    public int BloodPressure { get; set; }

    [JsonPropertyName("fasting_glucose_level")]
    public int FastingGlucoseLevel { get; set; }

    [JsonPropertyName("insulin_level")]
    public double InsulinLevel { get; set; }

    [JsonPropertyName("HbA1c_level")]
    public double HbA1cLevel { get; set; }

    [JsonPropertyName("cholesterol_level")]
    public int CholesterolLevel { get; set; }

    [JsonPropertyName("triglycerides_level")]
    public int TriglyceridesLevel { get; set; }

    [JsonPropertyName("physical_activity_level")]
    public int PhysicalActivityLevel { get; set; } //1,2,3

    [JsonPropertyName("daily_calorie_intake")]
    public int DailyCalorieIntake { get; set; }

    [JsonPropertyName("sugar_intake_grams_per_day")]
    public double SugarIntakeGramsPerDay { get; set; }

    [JsonPropertyName("family_history_diabetes")]
    public int FamilyHistoryDiabetes { get; set; } //0,1

    [JsonPropertyName("waist_circumference_cm")]
    public double WaistCircumferenceCm { get; set; }
}