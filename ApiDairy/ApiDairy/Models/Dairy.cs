namespace ApiDairy.Models
{
    //дневник
    public class Dairy
    {
        public int dairyid { get; set; }
        public Mark mark { get; set; }  
        public Hometask hometask { get; set; }
        public Timetable timetable { get; set; }
    }
}
