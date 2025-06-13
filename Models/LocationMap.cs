using System;
using System.Collections.Generic;

namespace VitaTrackAPI.Models;

public partial class LocationMap
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public DateTime? RecordedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
