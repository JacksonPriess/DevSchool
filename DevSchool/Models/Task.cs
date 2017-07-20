using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevSchool.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required]
        public String Title { get; set; }

        [Display(Name = "Descrição")]
        [Required]
        public String Description { get; set; }

        [Display(Name = "Completa")]
        public bool Status { get; set; }

        [Display(Name = "Criação")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
    
}