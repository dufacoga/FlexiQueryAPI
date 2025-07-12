namespace FlexiQueryAPI.Models
{
    public class Select
    {
        public string Table { get; set; } = string.Empty;
        public List<string> Columns { get; set; } = new() { "*" };
        public Dictionary<string, object>? Where { get; set; }
    }
}
