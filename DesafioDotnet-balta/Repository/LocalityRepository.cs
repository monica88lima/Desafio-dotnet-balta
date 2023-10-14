using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class LocalityRepository : Repository<LocalityModel>, ILocalityRepository
    {
        public LocalityRepository(AppDbContext context) : base(context)
        {
                
        }

        public async Task<IEnumerable<LocalityModel>> GetSpecificLocality(Expression<Func<LocalityModel, bool>> predicate)
        {
            return await _context.Set<LocalityModel>().Where(predicate).ToListAsync();
        }



    }
}
