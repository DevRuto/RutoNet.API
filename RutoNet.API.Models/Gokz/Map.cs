namespace RutoNet.API.Models.Gokz
{
    public class Map
    {
        public int MapId { get; set; }
        public string Name { get; set; }
        public System.DateTime LastPlayed { get; set; }
        public System.DateTime Created { get; set; }
        public bool InRankedPool { get; set; }
    }
}