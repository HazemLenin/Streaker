using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Streaker.DAL.Context;
using Streaker.DAL.Repositories;
using Streaker.Core.Domains;

namespace Streaker.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<Streak> StreaksRepository { get; private set; }
        public IRepository<Commit> CommitsRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            StreaksRepository = new Repository<Streak>(_context);
            CommitsRepository = new Repository<Commit>(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}