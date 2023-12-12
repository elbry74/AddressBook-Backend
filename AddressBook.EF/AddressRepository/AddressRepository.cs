using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Address_Book.Models;

namespace AddressBook.EF.AddressRepository
{
    public class AddressRepository : GenericRepository.GenericRepository<AddressBooks>
    {
        public Address_Book.Models.MyDbContext _context;

        public AddressRepository(Address_Book.Models.MyDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}