using Newtonsoft.Json;

namespace RutoNet.API.Models.Gokz
{
    public class MapCourse
    {
        public int MapCourseId { get; set; }
        public int MapId { get; set; }
        public long Course { get; set; }
        public System.DateTime Created { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }
    }
}