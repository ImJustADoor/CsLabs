using Microsoft.VisualStudio.TestPlatform.TestHost;
using sem_1_lab_02_server_config;

namespace ProgramTests
{
    public class Tests
    {

        [Test]
        public void CheckConfiguration_ValidConfiguration_ServerIsReady()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(50, 8, true, false);

            Assert.That(result, Is.EqualTo("Сервер готов к запуску."));
        }

        [Test]
        public void CheckConfiguration_ZeroPlayers_LaunchIsImpossible()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(0, 8, true, false);

            Assert.That(
                result,
                Is.EqualTo("Запуск невозможен: количество игроков должно быть больше нуля."));
        }

        [Test]
        public void CheckConfiguration_NotEnoughMemory_LaunchIsImpossible()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(50, 1, true, false);

            Assert.That(
                result,
                Is.EqualTo("Запуск невозможен: серверу недостаточно оперативной памяти."));
        }

        [Test]
        public void CheckConfiguration_PublicServerWithPassword_LaunchWithWarning()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(50, 8, true, true);

            Assert.That(
                result,
                Is.EqualTo("Запуск возможен с предупреждением: публичный сервер защищён паролем."));
        }

        [Test]
        public void CheckConfiguration_PrivateServerWithoutPassword_LaunchWithWarning()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(50, 8, false, false);

            Assert.That(
                result,
                Is.EqualTo("Запуск возможен с предупреждением: приватный сервер не защищён паролем."));
        }

        [Test]
        public void CheckConfiguration_TooManyPlayersForAvailableMemory_LaunchWithWarning()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(70, 4, true, false);

            Assert.That(
                result,
                Is.EqualTo(
                    "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти."));
        }

        [Test]

        public void CheckConfiguration_ZeroPlayersAndPublicServerHasPassword_TwoErrors()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(0, 8, true, true);

            Assert.That(
                result,
                Is.EqualTo(
                    "1 Ошибка: Запуск невозможен: количество игроков должно быть больше нуля.\n2 Ошибка: Запуск возможен с предупреждением: публичный сервер защищён паролем."));
        }

        [Test]

        public void CheckConfiguration_ZeroPlayersAndPrivateServerWithoutPassword_TwoErrors()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(0, 8, false, false);

            Assert.That(
                result,
                Is.EqualTo(
                    "1 Ошибка: Запуск невозможен: количество игроков должно быть больше нуля.\n2 Ошибка: Запуск возможен с предупреждением: приватный сервер не защищён паролем."));
        }

        [Test]

        public void CheckConfiguration_NotEnoughMemoryAndPublicServerHasPassword_TwoErrors()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(50, 1, true, true);

            Assert.That(
                result,
                Is.EqualTo(
                    "1 Ошибка: Запуск невозможен: серверу недостаточно оперативной памяти.\n2 Ошибка: Запуск возможен с предупреждением: публичный сервер защищён паролем."));
        }

        [Test]

        public void CheckConfiguration_NotEnoughMemoryAndPrivateServerWithoutPassword_TwoErrors()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(50, 1, false, false);

            Assert.That(
                result,
                Is.EqualTo(
                    "1 Ошибка: Запуск невозможен: серверу недостаточно оперативной памяти.\n2 Ошибка: Запуск возможен с предупреждением: приватный сервер не защищён паролем."));
        }

        [Test]

        public void CheckConfiguration_TooManyPlayersForAvailableMemoryAndPublicServerHasPassword_TwoErrors()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(70, 4, true, true);

            Assert.That(
                result,
                Is.EqualTo(
                    "1 Ошибка: Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.\n2 Ошибка: Запуск возможен с предупреждением: публичный сервер защищён паролем."));
        }

        [Test]

        public void CheckConfiguration_TooManyPlayersForAvailableMemoryAndPrivateServerWithoutPassword_TwoErrors()
        {
            var result = sem_1_lab_02_server_config.Program.CheckConfiguration(70, 4, false, false);

            Assert.That(
                result,
                Is.EqualTo(
                    "1 Ошибка: Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.\n2 Ошибка: Запуск возможен с предупреждением: приватный сервер не защищён паролем."));
        }
    }
}
