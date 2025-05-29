# 🔍 Web Server Log Parser

A C# console app that parses web server logs and generates useful reports. Extracts visitor data, tracks errors, and analyzes traffic patterns from Apache/Nginx access logs.

## What it does

Turns messy log files into readable reports:
- **Traffic analysis** - visitor counts, popular pages, request patterns
- **Error tracking** - 404s, 500s, and problematic endpoints  
- **Performance insights** - peak traffic hours, response sizes, user agents

## Features

- Supports Common Log Format and Extended formats
- Handles malformed entries gracefully
- Normalizes IP addresses and cleans data
- Extracts usernames, referrers, and user agents
- Groups errors by type and frequency
- Shows busiest time periods

## Sample output

```
Traffic Summary Report
===================================
Total Requests: 1,247
Unique Visitors: 156
Top Resources:
  /index.html (234 requests)
  /api/users (187 requests)

Status Codes:
  200: 891 (Success)
  404: 156 (Not Found)  
  500: 78 (Server Error)
```

## Quick start

1. Clone the repo
2. Update the log file path in `Program.cs`
3. Run with `dotnet run`
