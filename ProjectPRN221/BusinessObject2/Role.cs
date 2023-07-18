using System;
using System.Collections.Generic;

namespace ProjectPRN221.BusinessObject2;

public partial class Role
{
    public string? RoleName { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
