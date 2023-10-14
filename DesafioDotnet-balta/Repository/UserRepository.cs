using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository: Repository<UserModel>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) {

           
        }

        public UserModel Get(string email,string password)
        { 
            return  _context.USER.Where(x => x.Email == email && x.Password == password).FirstOrDefault();        
        
        }

    }
}
