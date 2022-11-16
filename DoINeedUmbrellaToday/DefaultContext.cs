using DoINeedUmbrellaToday.Models;
using Microsoft.EntityFrameworkCore;

namespace DoINeedUmbrellaToday
{
    /// <summary>
    /// Контекст текущей базы данных
    /// </summary>
    public class DefaultContext : DbContext
    {
        /// <summary>
        /// Коллекция объектов чатов
        /// </summary>
        public DbSet<TelegramChat> TelegramChats { get; set; }

        /// <summary>
        /// Коллекция объектов прогрессов пользователей
        /// </summary>
        public DbSet<TelegramBotProgress> TelegramBotProgresses { get; set; }

        public string DbPath { get; }

        public string DbFilename = "umbrellabot.db";

        public DefaultContext()
        {
            DbPath = System.IO.Path.Join(Directory.GetCurrentDirectory(), DbFilename);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath};Cache=Shared");
        }

    }
}
