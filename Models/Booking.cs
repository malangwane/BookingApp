using System;
using System.Collections.Generic;

namespace BookingApp.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int ResourceId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string BookedBy { get; set; } = null!;

    public string? Purpose { get; set; }

    public virtual Resource Resource { get; set; } = null!;
}
