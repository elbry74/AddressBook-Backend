using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Address_Book.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }

        public string Title { get; set; }
    }
}