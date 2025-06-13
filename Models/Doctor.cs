using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitaTrackAPI.Models;

public partial class Doctor
{
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("honorific_title")]
    public string? HonorificTitle { get; set; }

    [Column("gender")]
    public string? Gender { get; set; }

    [Column("is_favorite")]
    public bool? IsFavorite { get; set; }

    [Column("bio")]
    public string? Bio { get; set; }

    [Column("availability_hours")]
    public string? AvailabilityHours { get; set; }

    [Column("clinic_address")]
    public string? ClinicAddress { get; set; }

    [Column("profile_picture_base64")]
    public string? ProfilePictureBase64 { get; set; }

    [Column("Specialization")]
    public string? Specialization {  get; set; }

    public virtual User User { get; set; } = null!;
}
