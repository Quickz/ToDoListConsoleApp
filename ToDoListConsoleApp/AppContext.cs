using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListConsoleApp
{
    class AppContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public string ConnectionString { get; set; }

        public AppContext(string databasePath = null)
        {
            if (databasePath == null)
                return;

            ConnectionString =
                @$"Data Source=(LocalDB)\MSSQLLocalDB;" +
                @$"AttachDbFilename={databasePath};" +
                @$"Integrated Security=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectionString == null)
                optionsBuilder.UseSqlServer(AppSettings.DefaultConnectionString);
            else
                optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
