namespace FlexiQueryAPI.Models
{
    public class Delete
    {
        public string Table { get; set; } = string.Empty;
        public Dictionary<string, object> Where { get; set; } = new();
    }
}
