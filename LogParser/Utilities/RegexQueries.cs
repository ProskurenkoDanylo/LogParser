using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogParser.Utilities
{
    public static class RegexQueries
    {
        public static string ParseIPAddress(string line)
        {
            var match = Regex.Match(line, @"(\d{1,3}\.){3}\d{1,3}");
            if (match.Success)
            {
                var parts = match.Value.Split('.');
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = parts[i].TrimStart('0');
                    if (parts[i] == string.Empty)
                    {
                        parts[i] = "0"; // Ensure no empty segments
                    }
                }
                return string.Join(".", parts);
            }
            return string.Empty;
        }

        public static DateTime? ParseTimestamp(string line)
        {
            var match = Regex.Match(line, @"\[(.*?)\]");
            string format = "dd/MMM/yyyy:HH:mm:ss zzz";
            if (match.Success && DateTime.TryParseExact(match.Groups[1].Value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp))
            {
                return timestamp;
            }
            return null;
        }

        public static string ParseRequestMethod(string line)
        {
            var match = Regex.Match(line, @"\""(GET|POST|PUT|DELETE|PATCH|OPTIONS) ");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        public static string ParseRequestUrl(string line)
        {
            var match = Regex.Match(line, @"\""(GET|POST|PUT|DELETE|PATCH|OPTIONS) (.*?) HTTP/");
            return match.Success ? match.Groups[2].Value.Split("?")[0] : string.Empty;
        }

        public static int ParseResponseStatusCode(string line)
        {
            var match = Regex.Match(line, @"\""\s(\d{3})\s");
            return match.Success && int.TryParse(match.Groups[1].Value, out var statusCode) ? statusCode : 0;
        }

        public static long ParseResponseSize(string line)
        {
            var match = Regex.Match(line, "\"[^\"]*\"\\s+\\d{3}\\s+(\\d+)");
            return match.Success && long.TryParse(match.Groups[1].Value, out var size) ? size : 0;
        }

        public static string ParseUsername(string line)
        {
            var match = Regex.Match(line, @"\s-\s(\w+)\s");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        public static List<KeyValuePair<string, object>> ParseURLParameters(string line)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            var match = Regex.Match(line, @"\?(.*?)(\s|$)");
            if (match.Success)
            {
                var paramString = match.Groups[1].Value;
                var pairs = paramString.Split('&');
                foreach (var pair in pairs)
                {
                    var keyValue = pair.Split('=');
                    if (keyValue.Length == 2)
                    {
                        parameters.Add(new KeyValuePair<string, object>(keyValue[0], keyValue[1]));
                    }
                }
            }
            return parameters;
        }

        public static string ParseReferrerURL(string line)
        {
            var match = Regex.Match(line, "\"[^\"]*\" \\d+ \\d+ \"([^\"]+)\"");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        public static string ParseUserAgent(string line)
        {
            var match = Regex.Match(line, "\"[^\"]*\" \\d+ \\d+ \"[^\"]*\" \"([^\"]+)\"");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
    }
}
