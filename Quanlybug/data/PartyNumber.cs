using System;
using System.Collections.Generic;

namespace Quanlybug.data
{
    public partial class PartyNumber
    {
        public int IdPartyNumber { get; set; }
        public string? NamePartyNumber { get; set; }
        public int? IdParty { get; set; }

        public virtual Party? IdPartyNavigation { get; set; }
    }
}
