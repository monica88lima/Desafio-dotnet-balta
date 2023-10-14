using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ILocalityRepository : IRepository<LocalityModel>
    {
        Task<IEnumerable<LocalityModel>> GetSpecificLocality(Expression<Func<LocalityModel, bool>> predicate);




    }
}
