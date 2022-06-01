using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDairy.Models
{
    //расписание
    public class Timetable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Date { get; set; }
        public int Lesson { get; set; }
        public Subject Subject { get; set; }
        public User User { get; set; }
        public Class Class { get; set; }
        public Office Office { get; set; }
    }
}
