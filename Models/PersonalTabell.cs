using System;
using System.Collections.Generic;

namespace LABB3.Models;

public partial class PersonalTabell
{
    public int PersonIdPk { get; set; }

    public string? FörNamn { get; set; }

    public string? EfterNamn { get; set; }

    public string? Personnummer { get; set; }

    public string? Befattning { get; set; }

    public int? LärareIdFk { get; set; }

    public virtual ICollection<BetygTabell> BetygTabells { get; set; } = new List<BetygTabell>();

    public virtual Lärare? LärareIdFkNavigation { get; set; }
}
