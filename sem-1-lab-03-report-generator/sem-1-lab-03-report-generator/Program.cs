namespace sem_1_lab_03_report_generator
{
    public class Program
    {
        public static string BuildReport(string[] log_text)
        {
            bool isEventRunning = false;
            string event_name = "";
            DateTime event_date = new DateTime();
            string winner = "";
            int winners_points = 0;
            string event_item = "";
            string loser = "";
            int losers_points = 0;
            int warnings_during_event = 0;
            int errors_during_event = 0;
            string best_player = "";
            int best_player_stat = 0;

            foreach (string line in log_text)
            {
                if (line.Contains("Событие началось:"))
                {
                    isEventRunning = true;
                    event_name = line.Substring(line.IndexOf("Событие началось: ") + 18);
                    
                    
                    string date = line.Substring(0, 10);

                    event_date = DateTime.Parse(date);
                }
                if (line.Contains("закрыто"))
                {
                    isEventRunning = false;
                    break;   
                }

                if (isEventRunning)
                {
                    if (line.Contains("объявлены победителями события"))
                    {
                        int messageStart = line.IndexOf("] ") + 2;
                        int winnerEnd = line.IndexOf(" объявлены победителями");
                        winner = line.Substring(messageStart, winnerEnd - messageStart);
                    }

                    if (line.Contains("очков события"))
                    {
                        if (line.Contains("утешительную награду"))
                        {
                            int messageStart = line.IndexOf("] ") + 2;
                            int loserEnd = line.IndexOf(" получили утешительную");
                            loser = line.Substring(messageStart, loserEnd - messageStart);

                            int messageStart2 = line.IndexOf(": ") + 2;
                            int pointsEnd = line.IndexOf(" очков события");
                            losers_points = int.Parse(line.Substring(messageStart2, pointsEnd - messageStart2));

                        }

                        else
                        {
                            int messageStart = line.IndexOf("получили ") + 9;
                            int pointsEnd = line.IndexOf(" очков события");
                            winners_points = int.Parse(line.Substring(messageStart, pointsEnd - messageStart));
                        }
                    }

                    if (line.Contains("ивентовый предмет"))
                    {
                        event_item = line.Substring(line.IndexOf(": ") + 2);
                    }

                    if (line.Contains("[Warning]"))
                    {
                        warnings_during_event++;
                    }

                    if (line.Contains("[Error]"))
                    {
                        errors_during_event++;
                    }

                    if (line.Contains("Игрок"))
                    {
                        int messageStart = line.IndexOf("победил ") + 8;
                        int killsEnd = line.IndexOf(" врагов");
                        int kills_of_current_line = int.Parse(line.Substring(messageStart, killsEnd - messageStart));

                        if (best_player_stat < kills_of_current_line)
                        {
                            best_player_stat = kills_of_current_line;

                            int messageStart2 = line.IndexOf("Игрок ") + 6;
                            int playerEnd = line.IndexOf(" победил");
                            best_player = line.Substring(messageStart2, playerEnd - messageStart2);
                        }
                    }
                }
            }

            return ($"# Итоги события: {event_name}\n\nДата: {event_date.ToString("dd.MM.yyyy")}\nПобедитель: {winner}\nОчки победителя: {winners_points}\nИвентовый предмет: {event_item}\nУтешительная награда клана {loser}: {losers_points} очков\nЛучший игрок ивента: {best_player} ({best_player_stat} побед)\n\nПредупреждений во время события: {warnings_during_event}\nОшибок во время события: {errors_during_event}");
        }

        static void Main(string[] args)
        {
            string[] log_text = File.ReadAllLines("event_server.log");

            string Report = BuildReport(log_text);

            Console.WriteLine(Report);
        }
    }
}
