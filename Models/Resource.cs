using System;
using System.Collections.Generic;

namespace BookingApp.Models;

public partial class Resource
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; }

    public int? Capacity { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
