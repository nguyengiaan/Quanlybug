using System;
using System.Collections.Generic;

namespace Quanlybug.data
{
    public partial class Party
    {
        public Party()
        {
            PartyNumbers = new HashSet<PartyNumber>();
        }

        public int IdParty { get; set; }
        public string? NameParty { get; set; }
        public int? Number { get; set; }

        public virtual ICollection<PartyNumber> PartyNumbers { get; set; }
    }
}
