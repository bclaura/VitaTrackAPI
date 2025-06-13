using System;
using System.Collections.Generic;

namespace VitaTrackAPI.Models;

public partial class Alarm
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string? AlarmType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public bool? Activated { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
