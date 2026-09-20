using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace sem_1_lab_00_server_monitoring
{

    internal class Program
    {
        public static List<List<string>> GetTheData() // returns dictionary {list of all online players, number of total registered players}
        {
            int MoreRegisteredPlayersThanMax = 0; // 0 - false, 1 - true
            int MoreOnlinePlayersThanMax = 0; // 0 - false, 1- true
            int AmountOfLevelErrors = 0;
            int AmountOfLocationIDErrors = 0;
            int AmountOfPositionsErrors = 0;

            int MaxAmountOfPlayersRegistered = 100000;
            int MaxAmountOfPlayersOnline = 100;
            int MaxPlayerLevel = 99;
            int MaxPlayerLocationID = 49;
            int MaxPlayerPositions = 1000;

            Dictionary<string, string> AllRegisteredUsers = new Dictionary<string, string>() // {login, country}
            {
                {"Tobi", "Austria"},{"Ainkrad", "Belarus"},{"Fishman", "Belarus"},{"Hellscream", "Belarus"},{"OneJey", "Belarus"},{"SmilingKnight", "Belarus"},{"Sunlight", "Belarus"},
                {"panto", "Belarus"},{"Davai", "Belgium"},{"Wisper", "Bolivia"},{"4nalog", "Brazil"},{"KJ", "Brazil"},{"bzm", "Bulgaria"},{"Arteezy", "Canada"},{"Ame", "China"},
                {"BoBoKa", "China"},{"Faith_bian", "China"},{"Monet", "China"},{"XinQ", "China"},{"Xm", "China"},{"Xxs", "China"},{"BOOM", "Czechia"},{"SabeRLight-", "Czechia"},
                {"Ace", "Denmark"},{"Cr1t-", "Denmark"},{"Puppey", "Estonia"},{"MATUMBAMAN", "Finland"},{"Topson", "Finland"},{"Ceb", "France"},{"Copy", "Germany"},{"Nine", "Germany"},
                {"tOfu", "Germany"},{"Mikoto", "Indonesia"},{"Whitemon", "Indonesia"},{"ATF", "Jordan"},{"Miracle-", "Jordan"},{"Malady", "Kazakhstan"},{"Malik", "Kazakhstan"},
                {"watson", "Kazakhstan"},{"Zayac", "Kyrgyzstan"},{"No!ob", "Lebanon"},{"OmaR", "Lebanon"},{"GH", "Lebanon"},{"Ghost", "Malaysia"},{"MidOne", "Malaysia"},
                {"NothingToSay", "Malaysia"},{"Ws", "Malaysia"},{"Save-", "Moldova"},{"Crystallis", "Netherlands"},{"Yuma", "Nicaragua"},{"Saksa", "North Macedonia"},
                {"SumaiL", "Pakistan"},{"Parker", "Peru"},{"Scofield", "Peru"},{"Timado", "Peru"},{"payk", "Peru"},{"Abed", "Philippines"},{"Bob", "Philippines"},{"Ekki", "Poland"},
                {"Nisha", "Poland"},{"9Class", "Russia"},{"avice", "Russia"},{"Koma", "Russia"},{"blindzone", "Russia"},{"CHIRA_JUNIOR", "Russia"},{"Collapse", "Russia"},
                {"Daxak", "Russia"},{"Dukalis", "Russia"},{"Larl", "Russia"},{"Malr1ne", "Russia"},{"MieRo", "Russia"},{"Nightfall", "Russia"},{"Noticed", "Russia"},{"Pure", "Russia"},
                {"Rein", "Russia"},{"Satanic", "Russia"},{"Solo", "Russia"},{"TORONTOTOKYO", "Russia"},{"dyrachyo", "Russia"},{"kiyotaka", "Russia"},{"notme", "Russia"},
                {"rue", "Russia"},{"23savage", "Thailand"},{"Boxi", "Sweden"},{"Insania", "Sweden"},{"Xibbe", "Sweden"},{"miCKe", "Sweden"},{"Batyuk", "Ukraine"},{"Mira", "Ukraine"},
                {"No[o]ne-", "Ukraine"},{"Yatoro", "Ukraine"},{"kaori", "Ukraine"},{"fortniteMan", "Ukraine"},{"lorenof", "Ukraine"},{"mangekyou", "Ukraine"},{"Ari", "United Kingdom"},
                {"Quinn", "United States"},{"donk", "Russia"},{"zont1x", "Ukraine"},{"shiro", "Russia"},{"s1mple", "Ukraine"},{"m0nesy", "Russia"},{"mrekk", "Australia"},
                {"Chicony", "Russia"},{"ImJustADoor", "Russia"},{"Wer223playqq", "Ukraine"},{"lucyu", "Ukraine"},{"WhiteCat", "Germany"},{"xootynator", "Canada"}
            };

            int AmountOfRegisteredUsers = AllRegisteredUsers.Count;

            int AmountOfOnlineUsers = RandomNumberGenerator.GetInt32(AmountOfRegisteredUsers); // amount is random for the sake of results being different because its more fun

            List<string> AllUsersOnline = new List<string> { };
            Dictionary<string, string> Copy_AllRegisteredUsers = new Dictionary<string, string>() // {login, country}
            {
                {"Tobi", "Austria"},{"Ainkrad", "Belarus"},{"Fishman", "Belarus"},{"Hellscream", "Belarus"},{"OneJey", "Belarus"},{"SmilingKnight", "Belarus"},{"Sunlight", "Belarus"},
                {"panto", "Belarus"},{"Davai", "Belgium"},{"Wisper", "Bolivia"},{"4nalog", "Brazil"},{"KJ", "Brazil"},{"bzm", "Bulgaria"},{"Arteezy", "Canada"},{"Ame", "China"},
                {"BoBoKa", "China"},{"Faith_bian", "China"},{"Monet", "China"},{"XinQ", "China"},{"Xm", "China"},{"Xxs", "China"},{"BOOM", "Czechia"},{"SabeRLight-", "Czechia"},
                {"Ace", "Denmark"},{"Cr1t-", "Denmark"},{"Puppey", "Estonia"},{"MATUMBAMAN", "Finland"},{"Topson", "Finland"},{"Ceb", "France"},{"Copy", "Germany"},{"Nine", "Germany"},
                {"tOfu", "Germany"},{"Mikoto", "Indonesia"},{"Whitemon", "Indonesia"},{"ATF", "Jordan"},{"Miracle-", "Jordan"},{"Malady", "Kazakhstan"},{"Malik", "Kazakhstan"},
                {"watson", "Kazakhstan"},{"Zayac", "Kyrgyzstan"},{"No!ob", "Lebanon"},{"OmaR", "Lebanon"},{"GH", "Lebanon"},{"Ghost", "Malaysia"},{"MidOne", "Malaysia"},
                {"NothingToSay", "Malaysia"},{"Ws", "Malaysia"},{"Save-", "Moldova"},{"Crystallis", "Netherlands"},{"Yuma", "Nicaragua"},{"Saksa", "North Macedonia"},
                {"SumaiL", "Pakistan"},{"Parker", "Peru"},{"Scofield", "Peru"},{"Timado", "Peru"},{"payk", "Peru"},{"Abed", "Philippines"},{"Bob", "Philippines"},{"Ekki", "Poland"},
                {"Nisha", "Poland"},{"9Class", "Russia"},{"avice", "Russia"},{"Koma", "Russia"},{"blindzone", "Russia"},{"CHIRA_JUNIOR", "Russia"},{"Collapse", "Russia"},
                {"Daxak", "Russia"},{"Dukalis", "Russia"},{"Larl", "Russia"},{"Malr1ne", "Russia"},{"MieRo", "Russia"},{"Nightfall", "Russia"},{"Noticed", "Russia"},{"Pure", "Russia"},
                {"Rein", "Russia"},{"Satanic", "Russia"},{"Solo", "Russia"},{"TORONTOTOKYO", "Russia"},{"dyrachyo", "Russia"},{"kiyotaka", "Russia"},{"notme", "Russia"},
                {"rue", "Russia"},{"23savage", "Thailand"},{"Boxi", "Sweden"},{"Insania", "Sweden"},{"Xibbe", "Sweden"},{"miCKe", "Sweden"},{"Batyuk", "Ukraine"},{"Mira", "Ukraine"},
                {"No[o]ne-", "Ukraine"},{"Yatoro", "Ukraine"},{"kaori", "Ukraine"},{"fortniteMan", "Ukraine"},{"lorenof", "Ukraine"},{"mangekyou", "Ukraine"},{"Ari", "United Kingdom"},
                {"Quinn", "United States"},{"donk", "Russia"},{"zont1x", "Ukraine"},{"shiro", "Russia"},{"s1mple", "Ukraine"},{"m0nesy", "Russia"},{"mrekk", "Australia"},
                {"Chicony", "Russia"},{"ImJustADoor", "Russia"},{"Wer223playqq", "Ukraine"},{"lucyu", "Ukraine"},{"WhiteCat", "Germany"},{"xootynator", "Canada"}
            };

            for (int i = 0; i < AmountOfOnlineUsers; i++)
            {
                int IDOfRandomUser = RandomNumberGenerator.GetInt32(Copy_AllRegisteredUsers.Count);
                int RandomUserLevel = RandomNumberGenerator.GetInt32(101);
                int RandomUserLocationID = RandomNumberGenerator.GetInt32(51);
                int RandomUserXPosition = RandomNumberGenerator.GetInt32(1005);
                int RandomUserYPosition = RandomNumberGenerator.GetInt32(1005);
                string LoginOfRandomUser = Copy_AllRegisteredUsers.ElementAt(IDOfRandomUser).Key;
                string CountryOfRandomUser = Copy_AllRegisteredUsers.ElementAt(IDOfRandomUser).Value;

                string UserOnline = string.Join(", ", LoginOfRandomUser, CountryOfRandomUser, RandomUserLevel.ToString(), RandomUserLocationID.ToString(), RandomUserXPosition.ToString(), RandomUserYPosition.ToString());

                AllUsersOnline.Add(UserOnline);

                Copy_AllRegisteredUsers.Remove(LoginOfRandomUser);

                if (RandomUserLevel > MaxPlayerLevel) AmountOfLevelErrors++;
                if (RandomUserLocationID > MaxPlayerLocationID) AmountOfLocationIDErrors++;
                if (RandomUserXPosition > MaxPlayerPositions || RandomUserYPosition > MaxPlayerPositions) AmountOfPositionsErrors++;
            }

            if (AmountOfRegisteredUsers > MaxAmountOfPlayersRegistered) MoreRegisteredPlayersThanMax++;
            if (AmountOfOnlineUsers > MaxAmountOfPlayersOnline) MoreOnlinePlayersThanMax++;

            List<string> ErrorData = new List<string> {MoreRegisteredPlayersThanMax.ToString(), MoreOnlinePlayersThanMax.ToString(), AmountOfLevelErrors.ToString(), AmountOfLocationIDErrors.ToString(), AmountOfPositionsErrors.ToString()};
            List<string> PlayersData = new List<string> {AmountOfRegisteredUsers.ToString(), AmountOfOnlineUsers.ToString()};

            List<List<string>> PlayersServerData = new List<List<string>> {AllUsersOnline, PlayersData, ErrorData};

            return PlayersServerData;


        }

        public static void Main(string[] args)
        {
            var ServerPlayersData = GetTheData();
            Console.WriteLine("| Checking status of server 14...");
            Console.WriteLine("| ");
            Console.WriteLine("| Login, Country, Character Level, Location ID, X position, Y position");
            Console.WriteLine("| ");
            Console.WriteLine($"| Total players registered: {ServerPlayersData.ElementAt(1).ElementAt(0)}");
            Console.WriteLine($"| Total players online: {ServerPlayersData.ElementAt(1).ElementAt(1)}");
            Console.WriteLine("| ");
            foreach (var player in ServerPlayersData.ElementAt(0))
            {
                Console.WriteLine($"| {player}");
            }
            Console.WriteLine("| ");
            Console.WriteLine("| Checking for errors...");
            Console.WriteLine("| ");
            Console.WriteLine($"| Is there too much players registered?(0-false,1-true): {ServerPlayersData.ElementAt(2).ElementAt(0)}");
            Console.WriteLine($"| Is there too much players online?(0-false,1-true): {ServerPlayersData.ElementAt(2).ElementAt(1)}");
            Console.WriteLine($"| Player Level Errors: {ServerPlayersData.ElementAt(2).ElementAt(2)}");
            Console.WriteLine($"| Player LocationID Errors: {ServerPlayersData.ElementAt(2).ElementAt(3)}");
            Console.WriteLine($"| Player Position Errors: {ServerPlayersData.ElementAt(2).ElementAt(4)}");
            Console.WriteLine("| ");
        }
    }
}

