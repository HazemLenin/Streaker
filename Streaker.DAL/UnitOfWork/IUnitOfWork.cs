using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Streaker.DAL.Repositories;
using Streaker.Core.Domains;

namespace Streaker.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Streak> StreaksRepository { get; }
        IRepository<Commit> CommitsRepository { get; }
        int Save();
    }
}