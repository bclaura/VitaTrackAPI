using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VitaTrackAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    mobile_number = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    profile_picture_base64 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F33D92526", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    honorific_title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    is_favorite = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    bio = table.Column<string>(type: "text", nullable: true),
                    availability_hours = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    clinic_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    profile_picture_base64 = table.Column<string>(type: "text", nullable: true),
                    Specialization = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__doctors__3213E83FDA0EFD9A", x => x.id);
                    table.ForeignKey(
                        name: "FK__doctors__user_id__3D5E1FD2",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    receiver_id = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_read = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__messages__3213E83FF8DD5F48", x => x.id);
                    table.ForeignKey(
                        name: "FK__messages__receiv__46E78A0C",
                        column: x => x.receiver_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__messages__sender__45F365D3",
                        column: x => x.sender_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: true),
                    cnp = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    adress_street = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    adress_city = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    adress_county = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    occupation = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    workplace = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__patients__3213E83F54B11EF6", x => x.id);
                    table.ForeignKey(
                        name: "FK__patients__user_i__412EB0B6",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_favorites",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_favorites", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_favorites_doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_favorites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alarms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    alarm_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    description = table.Column<string>(type: "text", nullable: true),
                    activated = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__alarms__3213E83FA2888BF9", x => x.id);
                    table.ForeignKey(
                        name: "FK__alarms__patient___534D60F1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chart_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    chart_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    data_label = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    value = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    recorded_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chart_da__3213E83F972530AF", x => x.id);
                    table.ForeignKey(
                        name: "FK__chart_dat__patie__59FA5E80",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ecg_signals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    signal = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ecg_sign__3213E83F4AEA53E8", x => x.id);
                    table.ForeignKey(
                        name: "FK__ecg_signa__patie__4AB81AF0",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "location_map",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    latitude = table.Column<decimal>(type: "numeric(9,6)", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric(9,6)", nullable: true),
                    recorded_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__location__3213E83FADF021A2", x => x.id);
                    table.ForeignKey(
                        name: "FK__location___patie__5DCAEF64",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    history = table.Column<string>(type: "text", nullable: true),
                    allergies = table.Column<string>(type: "text", nullable: true),
                    cardiology_consultations = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medical___3213E83F01EAF7E9", x => x.id);
                    table.ForeignKey(
                        name: "FK__medical_h__patie__6383C8BA",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "physical_activities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    activity_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp", nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__physical__3213E83F33CA2DD5", x => x.id);
                    table.ForeignKey(
                        name: "FK__physical___patie__60A75C0F",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    recommendation_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    daily_duration = table.Column<int>(type: "integer", nullable: true),
                    additional_instructions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recommen__3213E83FD58ECA46", x => x.id);
                    table.ForeignKey(
                        name: "FK__recommend__patie__5629CD9C",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sensor_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sensor_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    value = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sensor_d__3213E83FAF80C8AE", x => x.id);
                    table.ForeignKey(
                        name: "FK__sensor_da__patie__4E88ABD4",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alarms_patient_id",
                table: "alarms",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_chart_data_patient_id",
                table: "chart_data",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_user_id",
                table: "doctors",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ecg_signals_patient_id",
                table: "ecg_signals",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_location_map_patient_id",
                table: "location_map",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_history_patient_id",
                table: "medical_history",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_receiver_id",
                table: "messages",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_sender_id",
                table: "messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_patients_user_id",
                table: "patients",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__patients__D8361757BFB3D0E9",
                table: "patients",
                column: "cnp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_physical_activities_patient_id",
                table: "physical_activities",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_recommendations_patient_id",
                table: "recommendations",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_sensor_data_patient_id",
                table: "sensor_data",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_favorites_doctor_id",
                table: "user_favorites",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_favorites_user_id",
                table: "user_favorites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E6164BF0A37B1",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alarms");

            migrationBuilder.DropTable(
                name: "chart_data");

            migrationBuilder.DropTable(
                name: "ecg_signals");

            migrationBuilder.DropTable(
                name: "location_map");

            migrationBuilder.DropTable(
                name: "medical_history");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "physical_activities");

            migrationBuilder.DropTable(
                name: "recommendations");

            migrationBuilder.DropTable(
                name: "sensor_data");

            migrationBuilder.DropTable(
                name: "user_favorites");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
