using System;
using System.Collections.Generic;

namespace VitaTrackAPI.Models;

public partial class SensorDatum
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? SensorType { get; set; }

    public decimal? Value { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
