using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitaTrackAPI.Models;

public partial class PhysicalActivity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("activity_type")]
    public string? ActivityType { get; set; }

    [Column("start_time")]
    public DateTime? StartTime { get; set; }

    [Column("end_time")]
    public DateTime? EndTime { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
