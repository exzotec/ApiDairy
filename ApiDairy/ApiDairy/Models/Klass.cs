using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDairy.Models
{
    //класс
    public class Klass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int classid { get; set; }
        public int number { get; set; }
        public string letter { get; set; }

        public int? userid { get; set; }
        public User User { get; set; }
        public List<User> users { get; set; }
        public Klass()
        {
            users = new List<User>();
        }
    }
}
