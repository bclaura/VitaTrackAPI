using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitaTrackAPI.Models;

public partial class Recommendation
{
    [Column("id")]
    public int Id { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("recommendation_type")]
    public string? RecommendationType { get; set; }

    [Column("daily_duration")]
    public int? DailyDuration { get; set; }

    [Column("additional_instructions")]
    public string? AdditionalInstructions { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
