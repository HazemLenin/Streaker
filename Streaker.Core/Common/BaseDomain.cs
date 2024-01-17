using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.Core.Common
{
    public abstract class BaseDomain
    {
        public string Id { get; set; } = new Guid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
