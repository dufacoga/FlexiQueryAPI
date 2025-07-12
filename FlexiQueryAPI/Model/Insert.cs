namespace FlexiQueryAPI.Models
{
    public class Insert
    {
        public string Table { get; set; } = string.Empty;
        public Dictionary<string, object> Values { get; set; } = new();
    }
}
