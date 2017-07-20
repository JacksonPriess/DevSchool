using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevSchool.Models
{
    public class Task
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public bool Status { get; set; }

        public DateTime CreationDate { get; set; }
    }
    
}