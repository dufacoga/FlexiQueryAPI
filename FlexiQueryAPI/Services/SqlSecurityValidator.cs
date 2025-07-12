using System.Text.RegularExpressions;

namespace FlexiQueryAPI.Services
{
    public class SqlSecurityValidator
    {
        private static readonly string[] BlockedPatterns = [
            @"\bDROP\s+TABLE\b",
            @"\bDROP\s+DATABASE\b",
            @"\bTRUNCATE\b",
            @"\bALTER\s+TABLE\b",
            @"\bALTER\s+DATABASE\b",
            @"\bSHUTDOWN\b",
            @"\bEXEC\s+xp_cmdshell\b"
        ];

        private static readonly string[] AllowedCommands = ["SELECT", "INSERT", "UPDATE", "DELETE"];

        public static void Validate(string sql)
        {

            if (string.IsNullOrWhiteSpace(sql))
                throw new InvalidOperationException("La consulta SQL no puede estar vacía.");

            var cleaned = sql.Trim();

            var upper = cleaned.ToUpperInvariant();

            if (!AllowedCommands.Any(cmd => upper.StartsWith(cmd)))
                throw new InvalidOperationException("Solo se permiten comandos SQL básicos: SELECT, INSERT, UPDATE, DELETE.");

            var statementCount = cleaned.Split(';').Count(s => !string.IsNullOrWhiteSpace(s));
            if (statementCount > 1)
                throw new InvalidOperationException("No se permiten múltiples sentencias SQL.");

            if (cleaned.Contains("--") || cleaned.Contains("/*") || cleaned.Contains("*/"))
                throw new InvalidOperationException("No se permiten comentarios en las consultas SQL.");

            if (Regex.IsMatch(cleaned, @"(\bOR\b|\bAND\b)\s+.+=", RegexOptions.IgnoreCase))
                throw new InvalidOperationException("Posible intento de inyección SQL detectado.");

            foreach (var pattern in BlockedPatterns)
            {
                if (Regex.IsMatch(cleaned, pattern, RegexOptions.IgnoreCase))
                    throw new InvalidOperationException($"El comando contiene una operación prohibida: {pattern}");
            }
        }
    }
}
