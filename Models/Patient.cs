using System;
using System.Collections.Generic;

namespace VitaTrackAPI.Models;

public partial class Patient
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? Age { get; set; }

    public string Cnp { get; set; } = null!;

    public string? AdressStreet { get; set; }

    public string? AdressCity { get; set; }

    public string? AdressCounty { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Occupation { get; set; }

    public string? Workplace { get; set; }

    public virtual ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();

    public virtual ICollection<ChartDatum> ChartData { get; set; } = new List<ChartDatum>();

    public virtual ICollection<EcgSignal> EcgSignals { get; set; } = new List<EcgSignal>();

    public virtual ICollection<LocationMap> LocationMaps { get; set; } = new List<LocationMap>();

    public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

    public virtual ICollection<PhysicalActivity> PhysicalActivities { get; set; } = new List<PhysicalActivity>();

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual ICollection<SensorDatum> SensorData { get; set; } = new List<SensorDatum>();

    public virtual User User { get; set; } = null!;
}
