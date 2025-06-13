using System;
using System.Collections.Generic;

namespace VitaTrackAPI.Models;

public partial class ChartDatum
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string? ChartType { get; set; }

    public string? DataLabel { get; set; }

    public decimal? Value { get; set; }

    public DateTime? RecordedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
