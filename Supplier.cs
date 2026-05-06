using System;
using System.Collections.Generic;

namespace DE42WPF;

public partial class Supplier
{
    public int Id { get; set; }

    public string? NameSupplier { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
