using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Dtos.Streaks
{
    public class StreakCalendarDto
    {
        public List<int> Commits { get; set; } = [];
        public Boolean CommitedToday { get; set; }
    }
}
