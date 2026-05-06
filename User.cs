using System;
using System.Collections.Generic;

namespace DE42WPF;

public partial class User
{
    public int Id { get; set; }

    public int? Role { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual UserRole? RoleNavigation { get; set; }
}
