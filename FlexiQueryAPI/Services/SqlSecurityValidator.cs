using System.Text.RegularExpressions;

namespace FlexiQueryAPI.Services
{
    public class SqlSecurityValidator
    {
        private static readonly string[] BlockedPatterns = [
            "DROP\\s+TABLE",
            "DROP\\s+DATABASE",
            "TRUNCATE",
            "ALTER\\s+TABLE",
            "ALTER\\s+DATABASE",
            "SHUTDOWN",
            "EXEC\\s+xp_cmdshell"
        ];

        private static readonly string[] AllowedCommands = ["SELECT", "INSERT", "UPDATE", "DELETE"];

        public static void Validate(string sql)
        {
            var cleaned = sql.Trim().ToUpperInvariant();

            if (!AllowedCommands.Any(cmd => cleaned.StartsWith(cmd)))
            {
                throw new InvalidOperationException("Solo se permiten comandos SQL básicos: SELECT, INSERT, UPDATE, DELETE.");
            }

            foreach (var pattern in BlockedPatterns)
            {
                if (Regex.IsMatch(cleaned, pattern, RegexOptions.IgnoreCase))
                {
                    throw new InvalidOperationException($"El comando contiene una operación prohibida: {pattern}");
                }
            }
        }
    }
}
