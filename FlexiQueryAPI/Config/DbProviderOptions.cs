namespace FlexiQueryAPI.Config
{
    public class DbProviderOptions
    {
        public string DatabaseProvider { get; set; } = "SQLite";
        public ConnectionStrings ConnectionStrings { get; set; } = new();
    }

    public class ConnectionStrings
    {
        public string SqlServer { get; set; } = string.Empty;
        public string MySQL { get; set; } = string.Empty;
        public string SQLite { get; set; } = string.Empty;
    }
}
