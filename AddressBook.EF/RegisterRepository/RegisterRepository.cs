using Address_Book.Models;
using AddressBook.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.EF.RegisterRepository
{
    public class UserRepository : GenericRepository.GenericRepository<Register>
    {
        public MyDbContext _context;

        public UserRepository(MyDbContext context) : base(context)
        {
        }
    }
}