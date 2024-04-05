using System;
using System.Collections.Generic;

namespace Website_API.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Age { get; set; } = null!;

    public string Salary { get; set; } = null!;
}
