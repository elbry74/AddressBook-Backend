using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Address_Book.Models
{
    public class AddressBooks
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "MobileNumber is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "MobileNumber must be numeric")]
        public int MobileNumber { get; set; }

        public byte[] Photo { get; set; }

        public string PhotoFileName { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "JobId is required")]
        public int JobId { get; set; }

        public Jobs Job { get; set; }

        [Required(ErrorMessage = "DepartmentId is required")]
        public int DepartmentId { get; set; }

        public Departments Department { get; set; }
    }
}