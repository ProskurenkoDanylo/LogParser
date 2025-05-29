using LogParser.Models;
using System.Text;

namespace LogParser.Utilities
{
    public class LogEntriesAnalyzer : ILogsAnalyzer
    {
        private readonly List<LogEntry> _logEntries;

        public LogEntriesAnalyzer(List<LogEntry> logEntries)
        {
            _logEntries = logEntries ?? throw new ArgumentNullException(nameof(logEntries));
        }

        public int GetUniqueIPCount()
        {
            return _logEntries.Select(entry => entry.IPAddress).Distinct().Count();
        }

        public List<string> GetMostRequestedResources(int topN)
        {
            if (topN <= 0) throw new ArgumentOutOfRangeException(nameof(topN), "Top N must be greater than zero.");
            return _logEntries
                .GroupBy(entry => entry.RequestUrl)
                .OrderByDescending(group => group.Count())
                .Take(topN)
                .Select(group => $"{group.Key} ({group.Count()} requests)")
                .ToList();
        }

        public List<KeyValuePair<int, int>> GetStatusCodesCount()
        {
            return _logEntries
                .GroupBy(entry => entry.ResponseStatusCode)
                .Select(group => new KeyValuePair<int, int>(group.Key, group.Count()))
                .OrderByDescending(pair => pair.Value)
                .ToList();
        }

        public int GetLogsCount()
        {
            return _logEntries.Count;
        }

        public string GetErrorList()
        {
            var errorEntries = _logEntries.Where(entry => entry.ResponseStatusCode >= 400).ToList();
            if (errorEntries.Count == 0)
            {
                return "No errors found in the logs.";
            }
            var errorReport = new StringBuilder();
            errorReport.AppendLine("Error Report:");
            foreach (var entry in errorEntries)
            {
                errorReport.AppendLine($"Status Code: {entry.ResponseStatusCode}, URL: {entry.RequestUrl}, Timestamp: {entry.Timestamp}");
            }
            return errorReport.ToString();
        }

        public string GetMostCommonErrorsCategory()
        {
            var _errorEntries = _logEntries.Where(entry => entry.ResponseStatusCode >= 400).ToList();
            var commonErrors = _errorEntries
                .GroupBy(entry => entry.ResponseCategory)
                .OrderByDescending(group => group.Count()).FirstOrDefault();
            return commonErrors != null
                ? $"{commonErrors.Key} errors occurred {commonErrors.Count()} times."
                : "No common errors found.";
        }

        public List<KeyValuePair<string, int>> GetTopErrorResources(int topN)
        {
            return _logEntries
                .Where(entry => entry.ResponseStatusCode >= 400)
                .GroupBy(entry => entry.RequestUrl)
                .Select(group => new KeyValuePair<string, int>(group.Key, group.Count()))
                .OrderByDescending(pair => pair.Value)
                .Take(topN)
                .ToList();
        }

        public double GetAverageResponseSize()
        {
            return _logEntries.Average(entry => entry.ResponseSize);
        }

        public List<KeyValuePair<string, int>> GetBusiestTimeAnalysis()
        {
            return _logEntries
                .GroupBy(entry => entry.Timestamp.Value.Hour)
                .Select(group => new KeyValuePair<string, int>($"{group.Key}:00 - {group.Key + 1}:00", group.Count()))
                .OrderByDescending(pair => pair.Value)
                .ToList();
        }

        public List<string> GetTopUserAgents()
        {
            return _logEntries
                .GroupBy(entry => entry.UserAgent)
                .OrderByDescending(group => group.Count())
                .Take(10)
                .Select(group => $"{group.Key} ({group.Count()} requests)")
                .ToList();
        }
    }
}
