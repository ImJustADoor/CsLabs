namespace sem_1_lab_02_server_config
{
    public class Program
    {
        public static string CheckConfiguration(int Player_Amount, int RAM_Amount, bool Is_Server_Public, bool Server_Has_Password)
        {
            Dictionary<string, string> Possible_Errors = new Dictionary<string, string>
            {
                {"LessThan1Player", "Сервер готов к запуску."},
                {"NotEnoughMemory", "Сервер готов к запуску."},
                {"TooMuchPlayersForAvailableMemory", "Сервер готов к запуску."},
                {"PublicServerHasPassword", "Сервер готов к запуску."},
                {"PrivateServerWithoutPassword", "Сервер готов к запуску."}
            };

            if (Player_Amount <= 0)
            {
                Possible_Errors["LessThan1Player"] = "Запуск невозможен: количество игроков должно быть больше нуля.";
            }

            // lets say that 1 gb of ram can support like 10 players with risk and 20 players is like maximum or smth
            if (Player_Amount / RAM_Amount >= 10)
            {
                if (Player_Amount / RAM_Amount > 20)
                {
                    Possible_Errors["NotEnoughMemory"] = "Запуск невозможен: серверу недостаточно оперативной памяти.";
                }
                else
                {
                    Possible_Errors["TooMuchPlayersForAvailableMemory"] = "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";
                }
            }

            if (Is_Server_Public && Server_Has_Password)
            {
                Possible_Errors["PublicServerHasPassword"] = "Запуск возможен с предупреждением: публичный сервер защищён паролем.";
            }

            if (!Is_Server_Public && !Server_Has_Password)
            {
                Possible_Errors["PrivateServerWithoutPassword"] = "Запуск возможен с предупреждением: приватный сервер не защищён паролем.";
            }

            List<string> ServerErrors = new List<string> {};

            foreach (string result in Possible_Errors.Values)
            {
                if (result != "Сервер готов к запуску.")
                {
                    ServerErrors.Add(result);
                }
            }

            if (ServerErrors.Count == 0)
            {
                return "Сервер готов к запуску.";
            }
            else
            {
                if (ServerErrors.Count == 1)
                {
                    return ServerErrors[0];
                }

                return $"1 Ошибка: {ServerErrors[0]}\n2 Ошибка: {ServerErrors[1]}"; 
                // maximum amount of errors is 2 because first, second and last errors cannot be at the same time and third and fourth also cannot be at the same time
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите кол-во игроков: ");
            string PlayerAmountBuffer = Console.ReadLine();
            int PlayerAmount;
            bool IsPlayerAmountBufferInt = int.TryParse(PlayerAmountBuffer, out PlayerAmount);
            while (!IsPlayerAmountBufferInt)
            {
                Console.Write("Введите ЧИСЛО игроков: ");
                PlayerAmountBuffer = Console.ReadLine();
                IsPlayerAmountBufferInt = int.TryParse(PlayerAmountBuffer, out PlayerAmount);
            }

            Console.Write("Введите кол-во выделенной ОЗУ: ");
            string MemoryAmountBuffer = Console.ReadLine();
            int MemoryAmount;
            bool IsMemoryAmountBufferInt = int.TryParse(MemoryAmountBuffer, out MemoryAmount);
            while (!IsMemoryAmountBufferInt)
            {
                Console.Write("Введите ЧИСЛО ОЗУ: ");
                MemoryAmountBuffer = Console.ReadLine();
                IsMemoryAmountBufferInt = int.TryParse(MemoryAmountBuffer, out MemoryAmount);
            }

            Console.Write("Сервер публичный? (Y/N): ");
            string IsPublicBuffer = Console.ReadLine();
            bool IsPublic;
            while (IsPublicBuffer != "Y" && IsPublicBuffer != "y" && IsPublicBuffer != "N" && IsPublicBuffer != "n")
            {
                Console.Write("Введите либо Y либо N (регистр не важен): ");
                IsPublicBuffer = Console.ReadLine();
            }
            if (IsPublicBuffer == "Y" || IsPublicBuffer == "y")
            {
                IsPublic = true;
            }
            else
            {
                IsPublic = false;
            }

            Console.Write("У сервера есть пароль? (Y/N): ");
            string IsTherePasswordBuffer = Console.ReadLine();
            bool IsTherePassword;
            while (IsTherePasswordBuffer != "Y" && IsTherePasswordBuffer != "y" && IsTherePasswordBuffer != "N" && IsTherePasswordBuffer != "n")
            {
                Console.Write("Введите либо Y либо N (регистр не важен): ");
                IsTherePasswordBuffer = Console.ReadLine();
            }
            if (IsTherePasswordBuffer == "Y" || IsTherePasswordBuffer == "y")
            {
                IsTherePassword = true;
            }
            else
            {
                IsTherePassword = false;
            }

            Console.WriteLine($"\n{CheckConfiguration(PlayerAmount, MemoryAmount, IsPublic, IsTherePassword)}");
        }
    }
}
