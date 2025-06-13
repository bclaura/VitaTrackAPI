using System;
using System.Collections.Generic;

namespace VitaTrackAPI.Models;

public partial class EcgSignal
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Signal { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
