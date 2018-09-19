namespace RutoNet.API.Models.Gokz
{
    public class Replay
    {
        public int ReplayId { get; set; }
        public int TimeId { get; set; }
        public byte[] ReplayData { get; set; }
        public string Tag { get; set; }
    }
}