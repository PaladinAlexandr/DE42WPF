using System;
using System.Collections.Generic;

namespace DE42WPF;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? DateOrder { get; set; }

    public DateTime? DateDelivery { get; set; }

    public int? Address { get; set; }

    public int? Client { get; set; }

    public int? Code { get; set; }

    public string? Status { get; set; }

    public virtual PickPoint? AddressNavigation { get; set; }

    public virtual User? ClientNavigation { get; set; }
}
