using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessCardManagement.Models
{
    public class BusinessCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int BusinessCardID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }
        

    }
}