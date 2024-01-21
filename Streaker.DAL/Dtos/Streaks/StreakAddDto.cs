using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Dtos.Streaks
{
    public class StreakAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TargetCount { get; set; }
        public string Category { get; set; }
    }
}
