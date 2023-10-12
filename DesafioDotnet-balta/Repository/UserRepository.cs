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

       

    }
}
