using Microsoft.VisualStudio.TestPlatform.TestHost;
using sem_1_lab_03_report_generator;
using System.IO;
using NUnit.Framework;

namespace sem_1_lab_03_tests
{
    [TestFixture]
    public class ProgramTests
    {
        private string[] _lines = null!;

        [SetUp]
        public void SetUp() => _lines = File.ReadAllLines("../../../event_server.log");

        [Test]
        public void Build_UsesOnlyTheEventTimeRange()
        {
            var report = sem_1_lab_03_report_generator.Program.BuildReport(_lines);
            Assert.That(report, Does.Not.Contain("ежедневное техническое обслуживание"));
        }

        [Test]
        public void Build_ReportsWinnerAndEventItem()
        {
            var report = sem_1_lab_03_report_generator.Program.BuildReport(_lines);
            Assert.That(report, Does.Contain("Победитель: Ночные совы"));
            Assert.That(report, Does.Contain("Очки победителя: 2500"));
        }

        [Test]
        public void Build_ReportsClanWithoutEventItem()
        {
            var report = sem_1_lab_03_report_generator.Program.BuildReport(_lines);
            Assert.That(report, Does.Contain("Ивентовый предмет: Сердце Эмберфанга"));
            Assert.That(report, Does.Contain("Утешительная награда клана Железные волки: 800 очков"));
        }

        [Test]
        public void Build_ReportsTechnicalCounters()
        {
            var report = sem_1_lab_03_report_generator.Program.BuildReport(_lines);
            Assert.That(report, Does.Contain("Предупреждений во время события: 6"));
            Assert.That(report, Does.Contain("Ошибок во время события: 1"));
        }

        [Test]
        public void Build_ReportsBestPlayer()
        {
            var report = sem_1_lab_03_report_generator.Program.BuildReport(_lines);
            Assert.That(report, Does.Contain("Лучший игрок ивента: ВоронФокс (14 побед)"));
        }
    }
}
