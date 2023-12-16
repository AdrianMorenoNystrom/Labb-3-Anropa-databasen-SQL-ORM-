using System;
using System.Collections.Generic;

namespace LABB3.Models;

public partial class Lärare
{
    public int LärareIdPk { get; set; }

    public string? Lärare1 { get; set; }

    public virtual ICollection<PersonalTabell> PersonalTabells { get; set; } = new List<PersonalTabell>();
}
