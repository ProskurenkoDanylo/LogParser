using LogParser.Utilities;
using System.Text;

namespace LogParser.Services
{
    public class ReportGenerator { 
        private readonly ILogsAnalyzer _logsAnalyzer;
        public ReportGenerator(ILogsAnalyzer logsAnalyzer)
        {
            _logsAnalyzer = logsAnalyzer;
        }
        public string GenerateTrafficSummaryReport()
        {
            var summary = new StringBuilder();
            summary.AppendLine("");
            summary.AppendLine("Traffic Summary Report");
            summary.AppendLine("===================================");
            summary.AppendLine($"Total Requests Processed: {_logsAnalyzer.GetLogsCount()}");
            summary.AppendLine($"Unique IP Addresses: {_logsAnalyzer.GetUniqueIPCount()}");
            summary.AppendLine("Most requested resources (top 5):");
            summary.AppendLine($"{string.Join('\n', _logsAnalyzer.GetMostRequestedResources(5))}");
            summary.AppendLine("Status Code Distribution:");
            summary.AppendLine($"{string.Join('\n', _logsAnalyzer.GetStatusCodesCount())}");
            summary.AppendLine("===================================");
            summary.AppendLine("");
            return summary.ToString();
        }

        public string GenerateErrorAnalysisReport()
        {
            var errorReport = new StringBuilder();
            errorReport.AppendLine("");
            errorReport.AppendLine("Error Analysis Report");
            errorReport.AppendLine("===================================");
            errorReport.AppendLine(_logsAnalyzer.GetErrorList());
            errorReport.AppendLine(_logsAnalyzer.GetMostCommonErrorsCategory());
            errorReport.AppendLine("Most error-causing resources (top 5):");
            errorReport.AppendLine($"{string.Join('\n', _logsAnalyzer.GetTopErrorResources(5))}");
            errorReport.AppendLine("===================================");
            errorReport.AppendLine("");
            return errorReport.ToString();
        }   

        public string GeneratePerformanceMetricsReport()
        {
            var summary = new StringBuilder();
            summary.AppendLine("");
            summary.AppendLine("Performance Metrics Report");
            summary.AppendLine("===================================");
            summary.AppendLine($"Average Response Size: {_logsAnalyzer.GetAverageResponseSize()}");
            summary.AppendLine("The busiest time analysis:");
            summary.AppendLine($"{string.Join('\n', _logsAnalyzer.GetBusiestTimeAnalysis())}");
            summary.AppendLine("Top User Agents: ");
            summary.AppendLine($"{string.Join('\n', _logsAnalyzer.GetTopUserAgents())}");
            summary.AppendLine("===================================");
            summary.AppendLine("");
            return summary.ToString();
        }
    }
}
