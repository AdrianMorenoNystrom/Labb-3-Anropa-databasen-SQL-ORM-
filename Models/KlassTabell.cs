using System;
using System.Collections.Generic;

namespace LABB3.Models;

public partial class KlassTabell
{
    public int KlassIdPk { get; set; }

    public string? KlassNamn { get; set; }

    public virtual ICollection<StudentTabell> StudentTabells { get; set; } = new List<StudentTabell>();
}
