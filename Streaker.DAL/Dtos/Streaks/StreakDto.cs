﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Dtos.Streaks
{
    public class StreakDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int TargetCount { get; set; }
        public DateTime Created { get; set; }
    }
}
