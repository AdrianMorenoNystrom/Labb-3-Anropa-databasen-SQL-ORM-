using System;
using System.Collections.Generic;

namespace LABB3.Models;

public partial class KursTabell
{
    public int KursIdPk { get; set; }

    public string? KursNamn { get; set; }

    public virtual ICollection<BetygTabell> BetygTabells { get; set; } = new List<BetygTabell>();
}
