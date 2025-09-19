using System;
using System.Collections.Generic;

namespace minimal_api_repoDb.Data.Models;

public partial class Review
{
    public int Id { get; set; }

    public int Rate { get; set; }

    public string Comment { get; set; } = null!;

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
