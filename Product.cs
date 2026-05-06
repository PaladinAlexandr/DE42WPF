using System;
using System.Collections.Generic;

namespace DE42WPF;

public partial class Product
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public string? Name { get; set; }

    public string? UnitMetric { get; set; }

    public decimal? Price { get; set; }

    public int? Supplier { get; set; }

    public int? Manufacture { get; set; }

    public string? Category { get; set; }

    public double? Discount { get; set; }

    public int? Amount { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual Manufacture? ManufactureNavigation { get; set; }

    public virtual Supplier? SupplierNavigation { get; set; }
}
