using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }

        public string Name { get; set; }
    }
}