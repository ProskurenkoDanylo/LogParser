using LogParser.Models;
using LogParser.Utilities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogParser.Services
{
    public class LogFileParser : ILogParser
    {
        private readonly string _logFilePath;
        public LogFileParser(string logFilePath)
        {
            _logFilePath = logFilePath;
            if (!File.Exists(_logFilePath))
            {
                throw new FileNotFoundException($"Log file not found: {_logFilePath}");
            }
        }

        public List<LogEntry> GetLogEntries()
        {
            List<LogEntry> logEntries = new();
            try
            {
                using var reader = new StreamReader(_logFilePath);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var ipAddress = RegexQueries.ParseIPAddress(line);
                    var username = RegexQueries.ParseUsername(line);
                    var timestamp = RegexQueries.ParseTimestamp(line);
                    var requestMethod = RegexQueries.ParseRequestMethod(line);
                    var requestUrl = RegexQueries.ParseRequestUrl(line);
                    var urlParameters = RegexQueries.ParseURLParameters(line);
                    var responseStatusCode = RegexQueries.ParseResponseStatusCode(line);
                    var responseSize = RegexQueries.ParseResponseSize(line);
                    var referrerURL = RegexQueries.ParseReferrerURL(line);
                    var userAgent = RegexQueries.ParseUserAgent(line);
                    logEntries.Add(new LogEntry(
                        ipAddress,
                        username,
                        timestamp,
                        requestMethod,
                        requestUrl,
                        urlParameters,
                        responseStatusCode,
                        responseSize,
                        referrerURL,
                        userAgent));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the log file: {ex.Message}");
            }
            return logEntries.Where(entry => DataValidation.IsValidLogEntry(entry)).ToList();
        }
    }
}
