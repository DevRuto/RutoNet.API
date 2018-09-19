namespace RutoNet.API.Models.Gokz
{
    public class Player
    {
        public int SteamId32 { get; set; }
        public string Alias { get; set; }
        public string Country { get; set; }
        public string IP { get; set; }
        public bool Cheater { get; set; }
        public System.DateTime LastPlayed { get; set; }
        public System.DateTime Created { get; set; }
    }
}