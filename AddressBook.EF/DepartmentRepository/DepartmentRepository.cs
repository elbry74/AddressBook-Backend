using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Address_Book.Models;

namespace AddressBook.EF.DepartmentRepository
{
    public class DepartmentRepository : GenericRepository.GenericRepository<Departments>
    {
        public Address_Book.Models.MyDbContext _context;

        public DepartmentRepository(Address_Book.Models.MyDbContext context) : base(context)
        {
        }
    }
}