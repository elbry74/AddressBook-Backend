using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Common.ViewModels
{
    public class AddressVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoFileName { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}