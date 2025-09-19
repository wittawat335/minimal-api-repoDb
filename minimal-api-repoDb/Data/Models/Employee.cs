﻿using System;
using System.Collections.Generic;

namespace minimal_api_repoDb.Data.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
