using LogParser.Models;

namespace LogParser.Services
{
    public static class DataValidation
    {
        public static bool IsValidLogEntry(LogEntry entry)
        {
            if (!IsValidIPAddress(entry.IPAddress))
                return false;
            if (!entry.Timestamp.HasValue || entry.Timestamp.Value == default)
                return false;
            if (string.IsNullOrWhiteSpace(entry.RequestMethod) || string.IsNullOrWhiteSpace(entry.RequestUrl))
                return false;
            if (entry.ResponseStatusCode < 100 || entry.ResponseStatusCode > 599)
                return false;
            if (entry.ResponseSize < 0)
                return false;
            return true;
        }

        public static bool IsValidIPAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;
            var parts = ipAddress.Split('.');
            if (parts.Length != 4)
                return false;
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int num) || num < 0 || num > 255)
                    return false;
            }
            return true;
        }
    }
}
