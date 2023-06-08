using System;
using System.Collections.Generic;

namespace Quanlybug.data
{
    public partial class UserMember
    {
        public UserMember()
        {
            Projects = new HashSet<Project>();
        }

        public int IdUser { get; set; }
        public string? NameUser { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Permission { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
