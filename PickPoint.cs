using System;
using System.Collections.Generic;

namespace DE42WPF;

public partial class PickPoint
{
    public int Id { get; set; }

    public int? IndexCity { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public double? Home { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
