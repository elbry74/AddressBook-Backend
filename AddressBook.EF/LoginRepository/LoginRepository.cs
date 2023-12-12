using Address_Book.Models;
using AddressBook.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.EF.LoginRepository
{
    public class LoginRepository : GenericRepository.GenericRepository<Login>
    {
        public MyDbContext _context;

        public LoginRepository(MyDbContext context) : base(context)
        {
        }
    }
}