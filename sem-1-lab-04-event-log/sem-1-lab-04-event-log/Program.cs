using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace sem_1_lab_04_event_log
{
    public struct LogEntry
    {
        public DateTime Timestamp;
        public string Level;
        public string Category;
        public string Message;
    }

    public class Program
    {
        public static LogEntry[] ParseLog(string[] lines)
        {
            LogEntry[] logs = new LogEntry[1000];
            for (int i = 0; i < 1000; i++)
            {
                logs[i].Timestamp = DateTime.Parse(lines[i].Substring(0, 23));
                logs[i].Level = lines[i].Substring(lines[i].IndexOf(" [") + 2, lines[i].IndexOf("][") - (lines[i].IndexOf(" [") + 2));
                logs[i].Category = lines[i].Substring(lines[i].IndexOf("][") + 2, lines[i].IndexOf("] ") - (lines[i].IndexOf("][") + 2));
                logs[i].Message = lines[i].Substring(lines[i].IndexOf("] ") + 2);
            }

            return logs;
        }


        public static List<LogEntry> FilterByDate(LogEntry[] entries, DateTime date)
        {
            List<LogEntry> filtered_logs = new List<LogEntry>();
            foreach (LogEntry line in entries)
            {
                if (line.Timestamp.Date == date.Date)
                {
                    filtered_logs.Add(line);
                }
            }

            return filtered_logs;
        }

        public static List<LogEntry> FilterByLevel(LogEntry[] entries, string level)
        {
            List<LogEntry> filtered_logs = new List<LogEntry>();
            foreach (LogEntry line in entries)
            {
                if (line.Level == level)
                {
                    filtered_logs.Add(line);
                }
            }

            return filtered_logs;
        }

        public static List<LogEntry> FilterByCategory(LogEntry[] entries, string category)
        {
            List<LogEntry> filtered_logs = new List<LogEntry>();
            foreach (LogEntry line in entries)
            {
                if (line.Category == category)
                {
                    filtered_logs.Add(line);
                }
            }

            return filtered_logs;
        }

        public static List<LogEntry> Search(LogEntry[] entries, string text)
        {
            List<LogEntry> filtered_logs = new List<LogEntry>();
            foreach (LogEntry line in entries)
            {
                if (line.Message.ToLower().Contains(text.ToLower()))
                {
                    filtered_logs.Add(line);
                }
            }

            return filtered_logs;
        }

        public static int CountByLevel(LogEntry[] entries, string level)
        {
            int Level_Count = entries.Count(x => x.Level == level);

            return Level_Count;
        }

        public static string GetServerStatus(LogEntry[] entries)
        {
            if (entries.Count(x => x.Level == "Error") > 0 && entries.Count(x => x.Level == "Fatal") == 0)
            {
                return "Есть ошибки: требуется проверка";
            }

            if (entries.Count(x => x.Level == "Fatal" && x.Category == "Server") > 0)
            {
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
            }

            return "Сервер работает штатно";
        }

        static void Main(string[] args)
        {
            
        }
    }
}
