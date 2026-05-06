using System;
using System.Collections.Generic;

namespace DE42WPF;

public partial class Manufacture
{
    public int Id { get; set; }

    public string? NameManufacture { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
