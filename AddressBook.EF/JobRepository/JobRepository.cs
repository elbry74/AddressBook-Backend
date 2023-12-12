using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Address_Book.Models;

namespace AddressBook.EF.JobRepository
{
    public class JobRepository : GenericRepository.GenericRepository<Jobs>
    {
        public Address_Book.Models.MyDbContext _context;

        public JobRepository(Address_Book.Models.MyDbContext context) : base(context)
        {
            //_context = context;
        }
    }
}