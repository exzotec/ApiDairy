using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDairy.Models
{
    //пользоваетль
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public int roleid { get; set; }
        public Role role { get; set; }
    }
}
