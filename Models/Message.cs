using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitaTrackAPI.Models;

public partial class Message
{
    [Column("id")]
    public int Id { get; set; }

    [Column("sender_id")]
    public int SenderId { get; set; }

    [Column("receiver_id")]
    public int ReceiverId { get; set; }

    [Column("message")]
    public string Message1 { get; set; } = null!;

    [Column("sent_at")]
    public DateTime? SentAt { get; set; }

    [Column("is_read")]
    public bool? IsRead { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
