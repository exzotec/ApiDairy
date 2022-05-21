namespace ApiDairy.Models
{
    //расписание
    public class Timetable
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public int Lesson { get; set; }
        public Subject subject { get; set; }
        public User User { get; set; }
        public Class Class { get; set; }
        public Office Office { get; set; }
    }
}
