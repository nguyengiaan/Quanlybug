using System;
using System.Collections.Generic;

namespace Quanlybug.data
{
    public partial class Project
    {
        public int IdProject { get; set; }
        public string? NameProject { get; set; }
        public string? ContextProject { get; set; }
        public int? IdUser { get; set; }

        public virtual UserMember? IdUserNavigation { get; set; }
    }
}
