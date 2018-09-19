namespace RutoNet.API.Models.Gokz
{
    public class Time
    {
        public int TimeId { get; set; }
        public int SteamId32 { get; set; }
        public int MapCourseId { get; set; }
        public int Mode { get; set; }
        public int Style { get; set; }
        public long RunTime { get; set; }
        public int Teleports { get; set; }
        public System.DateTime Created { get; set; }
    }
}