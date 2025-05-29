using LogParser.Models;
using LogParser.Services;
using LogParser.Utilities;

var logFilePath = "";
Console.WriteLine("Please enter the path to the log file:");
logFilePath = Console.ReadLine()?.Trim() ?? string.Empty;

LogFileParser logParser;
List<LogEntry> logEntries = new List<LogEntry>();
while (true) { 
try {
    logParser = new LogFileParser(logFilePath);
    logEntries = logParser.GetLogEntries();
    break;
} catch (FileNotFoundException)
{
    Console.WriteLine("File not found.");
    Console.WriteLine("Please enter the path to the log file:");
    logFilePath = Console.ReadLine()?.Trim() ?? string.Empty;
}
}

var logAnalyzer = new LogEntriesAnalyzer(logEntries);
var reportGenerator = new ReportGenerator(logAnalyzer);

WriteOptionsMenu();
var choice = string.Empty;
while (choice != "4")
{
    Console.WriteLine();
    choice = Console.ReadLine()?.Trim() ?? string.Empty;
    switch (choice)
    {
        case "1":
            Console.WriteLine(reportGenerator.GenerateTrafficSummaryReport());
            break;
        case "2":
            Console.WriteLine(reportGenerator.GenerateErrorAnalysisReport());
            break;
        case "3":
            Console.WriteLine(reportGenerator.GeneratePerformanceMetricsReport());
            break;
        case "4":
            Console.WriteLine("Exiting the application.");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

    WriteOptionsMenu();
}

void WriteOptionsMenu()
{
    Console.WriteLine("Log Parser Application");
    Console.WriteLine("1. Generate Traffic Summary Report");
    Console.WriteLine("2. Generate Error Analysis Report");
    Console.WriteLine("3. Generate Performance Metrics Report");
    Console.WriteLine("4. Exit");
    Console.Write("Please enter your choice: ");
}