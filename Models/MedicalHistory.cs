using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitaTrackAPI.Models;

public partial class MedicalHistory
{
    [Column("id")]
    public int Id { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("history")]
    public string? History { get; set; }

    [Column("allergies")]
    public string? Allergies { get; set; }

    [Column("cardiology_consultations")]
    public string? CardiologyConsultations { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);

    public virtual Patient Patient { get; set; } = null!;
}
