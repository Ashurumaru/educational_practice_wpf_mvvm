using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_practice.Models
{
    internal class AccessLevelModel
    {
        public AccessLevel Level { get; set; }
        public enum AccessLevel
        {
            User,
            Admin
        }
    }
}
