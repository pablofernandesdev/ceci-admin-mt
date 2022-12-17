using CeciAdminMT.Domain.DTO.Company;
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
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<Company>> GetByFilterAsync(CompanyFilterDTO filter)
        {
            Expression<Func<Company, bool>> query = c =>
                    (!string.IsNullOrEmpty(filter.Name) ? c.Name.Contains(filter.Name) : true) &&
                    (!string.IsNullOrEmpty(filter.DocumentNumber) ? c.DocumentNumber.Equals(filter.DocumentNumber) : true) &&
                    (!string.IsNullOrEmpty(filter.Search)
                        ? (c.Name.Contains(filter.Search) || c.DocumentNumber.Contains(filter.Search))
                        : true);

            return await _appDbContext.Set<Company>()
                  .AsNoTracking()
                  .Where(query)
                  .Skip((filter.Page - 1) * filter.PerPage)
                  .Take(filter.PerPage)
                  .OrderByDescending(c => c.Id)
                  .ToListAsync();
        }

        public async Task<int> GetTotalByFilterAsync(CompanyFilterDTO filter)
        {
            Expression<Func<Company, bool>> query = c =>
                    (!string.IsNullOrEmpty(filter.Name) ? c.Name.Contains(filter.Name) : true) &&
                    (!string.IsNullOrEmpty(filter.DocumentNumber) ? c.DocumentNumber.Equals(filter.DocumentNumber) : true) &&
                    (!string.IsNullOrEmpty(filter.Search)
                        ? (c.Name.Contains(filter.Search) || c.DocumentNumber.Contains(filter.Search))
                        : true);

            return await _appDbContext.Set<Company>()
                  .AsNoTracking()
                  .Where(query)
                  .CountAsync();
        }
    }
}
