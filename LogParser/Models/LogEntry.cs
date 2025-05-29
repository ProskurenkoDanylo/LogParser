namespace LogParser.Models
{
    public class LogEntry
    {
        public string IPAddress { get; set; }
        public string Username { get; set; }
        public DateTime? Timestamp { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUrl { get; set; }
        public List<KeyValuePair<string, object>> URLParameters { get; set; }
        public int ResponseStatusCode { get; set; }
        public long ResponseSize { get; set; }
        public string ResponseCategory;
        public string ReferrerURL { get; set; }
        public string UserAgent { get; set; }

        public LogEntry(string ipAddress,
                        string username,
                        DateTime? timestamp,
                        string requestMethod,
                        string requestUrl,
                        List<KeyValuePair<string, object>> uRLParameters,
                        int responseStatusCode,
                        long responseSize,
                        string referrerURL,
                        string userAgent)
        {
            IPAddress = ipAddress;
            Timestamp = timestamp;
            RequestMethod = requestMethod;
            RequestUrl = requestUrl;
            ResponseStatusCode = responseStatusCode;
            ResponseSize = responseSize;

            if (responseStatusCode >= 100 && responseStatusCode < 200)
            {
                ResponseCategory = "Info";
            }
            else if (responseStatusCode >= 200 && responseStatusCode < 300)
            {
                ResponseCategory = "Success";
            }
            else if (responseStatusCode >= 300 && responseStatusCode < 400)
            {
                ResponseCategory = "Redirection";
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                ResponseCategory = "ClientError";
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                ResponseCategory = "ServerError";
            }
            else
            {
                ResponseCategory = "Unknown";
            }

            Username = username;
            URLParameters = uRLParameters;
            ReferrerURL = referrerURL;
            UserAgent = userAgent;
        }
    }
}
