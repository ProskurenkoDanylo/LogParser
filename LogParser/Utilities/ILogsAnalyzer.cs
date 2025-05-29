using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Utilities
{
    public interface ILogsAnalyzer
    {
        public int GetUniqueIPCount();
        public List<string> GetMostRequestedResources(int topN);
        public List<KeyValuePair<int, int>> GetStatusCodesCount();
        public int GetLogsCount();
        public string GetErrorList();
        public string GetMostCommonErrorsCategory();
        public List<KeyValuePair<string, int>> GetTopErrorResources(int topN);
        public double GetAverageResponseSize();
        public List<KeyValuePair<string, int>> GetBusiestTimeAnalysis();
        public List<string> GetTopUserAgents();
    }
}
