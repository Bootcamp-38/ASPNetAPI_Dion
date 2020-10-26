using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Division")]
    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    }
}