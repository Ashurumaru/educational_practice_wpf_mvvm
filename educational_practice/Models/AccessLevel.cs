using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_practice.Models
{
    internal class AccessLevelUser
    {
        public AccessLevelUser(AccessLevel level)
        {
            Level = level;
        }
        public AccessLevel Level { get; }
        public enum AccessLevel
        {
            User,
            Admin
        }
    }
}
