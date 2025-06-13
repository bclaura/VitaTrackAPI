using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VitaTrackAPI.Models;

public partial class VitaTrackContext : DbContext
{
    public VitaTrackContext()
    {
    }

    public VitaTrackContext(DbContextOptions<VitaTrackContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alarm> Alarms { get; set; }

    public virtual DbSet<ChartDatum> ChartData { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<EcgSignal> EcgSignals { get; set; }

    public virtual DbSet<LocationMap> LocationMaps { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PhysicalActivity> PhysicalActivities { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    public virtual DbSet<SensorDatum> SensorData { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<UserFavorite> UserFavorites { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=VitaTrack;Trusted_Connection=True;TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alarm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__alarms__3213E83FA2888BF9");

            entity.ToTable("alarms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activated)
                .HasDefaultValue(false)
                .HasColumnName("activated");
            entity.Property(e => e.AlarmType)
                .HasMaxLength(20)
                .HasColumnName("alarm_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.Alarms)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__alarms__patient___534D60F1");
        });

        modelBuilder.Entity<ChartDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chart_da__3213E83F972530AF");

            entity.ToTable("chart_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChartType)
                .HasMaxLength(50)
                .HasColumnName("chart_type");
            entity.Property(e => e.DataLabel)
                .HasMaxLength(225)
                .HasColumnName("data_label");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("recorded_at");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("value");

            entity.HasOne(d => d.Patient).WithMany(p => p.ChartData)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__chart_dat__patie__59FA5E80");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__doctors__3213E83FDA0EFD9A");

            entity.ToTable("doctors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailabilityHours)
                .HasMaxLength(255)
                .HasColumnName("availability_hours");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.ClinicAddress)
                .HasMaxLength(255)
                .HasColumnName("clinic_address");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.HonorificTitle)
                .HasMaxLength(20)
                .HasColumnName("honorific_title");
            entity.Property(e => e.IsFavorite)
                .HasDefaultValue(false)
                .HasColumnName("is_favorite");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__doctors__user_id__3D5E1FD2");
        });

        modelBuilder.Entity<EcgSignal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ecg_sign__3213E83F4AEA53E8");

            entity.ToTable("ecg_signals");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Signal)
                .HasMaxLength(1000)
                .HasColumnName("signal");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Patient).WithMany(p => p.EcgSignals)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__ecg_signa__patie__4AB81AF0");
        });

        modelBuilder.Entity<LocationMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__location__3213E83FADF021A2");

            entity.ToTable("location_map");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitude");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("recorded_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.LocationMaps)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__location___patie__5DCAEF64");
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__medical___3213E83F01EAF7E9");

            entity.ToTable("medical_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allergies).HasColumnName("allergies");
            entity.Property(e => e.CardiologyConsultations).HasColumnName("cardiology_consultations");
            entity.Property(e => e.History).HasColumnName("history");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__medical_h__patie__6383C8BA");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83FF8DD5F48");

            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message1).HasColumnName("message");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_at");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__receiv__46E78A0C");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__sender__45F365D3");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patients__3213E83F54B11EF6");

            entity.ToTable("patients");

            entity.HasIndex(e => e.Cnp, "UQ__patients__D8361757BFB3D0E9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdressCity)
                .HasMaxLength(225)
                .HasColumnName("adress_city");
            entity.Property(e => e.AdressCounty)
                .HasMaxLength(225)
                .HasColumnName("adress_county");
            entity.Property(e => e.AdressStreet)
                .HasMaxLength(225)
                .HasColumnName("adress_street");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Cnp)
                .HasMaxLength(13)
                .HasColumnName("cnp");
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .HasColumnName("email");
            entity.Property(e => e.Occupation)
                .HasMaxLength(225)
                .HasColumnName("occupation");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Workplace)
                .HasMaxLength(225)
                .HasColumnName("workplace");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__patients__user_i__412EB0B6");
        });

        modelBuilder.Entity<PhysicalActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__physical__3213E83F33CA2DD5");

            entity.ToTable("physical_activities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(20)
                .HasColumnName("activity_type");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Patient).WithMany(p => p.PhysicalActivities)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__physical___patie__60A75C0F");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recommen__3213E83FD58ECA46");

            entity.ToTable("recommendations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalInstructions).HasColumnName("additional_instructions");
            entity.Property(e => e.DailyDuration).HasColumnName("daily_duration");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.RecommendationType)
                .HasMaxLength(20)
                .HasColumnName("recommendation_type");

            entity.HasOne(d => d.Patient).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__recommend__patie__5629CD9C");
        });

        modelBuilder.Entity<SensorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sensor_d__3213E83FAF80C8AE");

            entity.ToTable("sensor_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.SensorType)
                .HasMaxLength(20)
                .HasColumnName("sensor_type");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("value");

            entity.HasOne(d => d.Patient).WithMany(p => p.SensorData)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__sensor_da__patie__4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F33D92526");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164BF0A37B1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
