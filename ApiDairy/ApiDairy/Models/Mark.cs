using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDairy.Models
{
    public class Mark
    {
        //оценка
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int markid { get; set; }
        public DateTime date { get; set; }
        public int subjectid { get; set; }
        public int mark { get; set; }

        public int? userid { get; set; }
        public User user { get; set; }
    }
}
