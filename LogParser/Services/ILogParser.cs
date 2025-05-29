using LogParser.Models;

namespace LogParser.Services
{
    public interface ILogParser
    {
        public List<LogEntry> GetLogEntries();
    }
}
