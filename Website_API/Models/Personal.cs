using System;
using System.Collections.Generic;

namespace Website_API.Models;

public partial class Personal
{
    public int Userid { get; set; }

    public string Idcard { get; set; } = null!;
}
