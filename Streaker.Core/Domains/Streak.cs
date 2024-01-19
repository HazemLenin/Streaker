using Streaker.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.Core.Domains
{
    public class Streak : BaseDomain
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StreakCount { get; set; }
        public int TargetCount { get; set; }
        public string Category { get; set; }
    }
}
