using CeciAdminMT.Domain.DTO.User;
using CeciAdminMT.Domain.Entities;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CeciAdminMT.Infra.Data.Repository
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _appDbContext.Set<User>()
                .AsNoTracking()
                .Include(c=> c.Role)
                .FirstOrDefaultAsync(x=> x.Id.Equals(id));
        }

        public async Task<IEnumerable<User>> GetByFilterAsync(UserFilterDTO filter)
        {
            Expression<Func<User, bool>> query = c =>
                    (!string.IsNullOrEmpty(filter.Name) ? c.Name.Contains(filter.Name) : true) &&
                    (!string.IsNullOrEmpty(filter.Email) ? c.Email.Equals(filter.Email) : true) &&
                    (!string.IsNullOrEmpty(filter.Search) 
                        ? (c.Name.Contains(filter.Search) || c.Email.Contains(filter.Search)) 
                        : true);

            return await _appDbContext.Set<User>()
                  .AsNoTracking()
                  .Include(c => c.Role)
                  .Where(query)
                  .Skip((filter.Page - 1) * filter.PerPage)
                  .Take(filter.PerPage)
                  .OrderByDescending(c => c.Id)
                  .ToListAsync();
        }

        public async Task<int> GetTotalByFilterAsync(UserFilterDTO filter)
        {
            Expression<Func<User, bool>> query = c =>
                    (!string.IsNullOrEmpty(filter.Name) ? c.Name.Contains(filter.Name) : true) &&
                    (!string.IsNullOrEmpty(filter.Email) ? c.Email.Equals(filter.Email) : true) &&
                    (!string.IsNullOrEmpty(filter.Search)
                        ? (c.Name.Contains(filter.Search) || c.Email.Contains(filter.Search))
                        : true);

            return await _appDbContext.Set<User>()
                  .AsNoTracking()
                  .Where(query)
                  .CountAsync();
        }
    }
}
