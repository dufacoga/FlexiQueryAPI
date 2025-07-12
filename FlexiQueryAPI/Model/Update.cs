namespace FlexiQueryAPI.Models
{
    public class Update
    {
        public string Table { get; set; } = string.Empty;
        public Dictionary<string, object> Set { get; set; } = new();
        public Dictionary<string, object> Where { get; set; } = new();
    }
}
