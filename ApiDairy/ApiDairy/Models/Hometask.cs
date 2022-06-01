using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ApiDairy.Models
{
    public class Hometask
    {
        //дом задание
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int subjectid { get; set; }
        public string task { get; set; }
        public DataTime date { get; set; }
    }
}
