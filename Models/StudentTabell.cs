using System;
using System.Collections.Generic;

namespace LABB3.Models;

public partial class StudentTabell
{
    public int StudentIdPk { get; set; }

    public string? FörNamn { get; set; }

    public string? EfterNamn { get; set; }

    public string? Personnummer { get; set; }

    public int? KlassIdFk { get; set; }

    public string? Kön { get; set; }

    public virtual ICollection<BetygTabell> BetygTabells { get; set; } = new List<BetygTabell>();

    public virtual KlassTabell? KlassIdFkNavigation { get; set; }
}
