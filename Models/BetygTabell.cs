using System;
using System.Collections.Generic;

namespace LABB3.Models;

public partial class BetygTabell
{
    public int BetygIdPk { get; set; }

    public int? StudentIdFk { get; set; }

    public int? KursIdFk { get; set; }

    public int? LärareIdFk { get; set; }

    public string? Betyg { get; set; }

    public DateTime? BetygDatum { get; set; }

    public virtual KursTabell? KursIdFkNavigation { get; set; }

    public virtual PersonalTabell? LärareIdFkNavigation { get; set; }

    public virtual StudentTabell? StudentIdFkNavigation { get; set; }
}
