using Streaker.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.Core.Domains
{
    public class Commit : BaseDomain
    {
        public string StreakId { get; set; }
        public Streak Streak { get; set; }
    }
}
