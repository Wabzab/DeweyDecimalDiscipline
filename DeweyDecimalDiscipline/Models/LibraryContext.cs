using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeweyDecimalDiscipline.Models
{
    /*
     * Library Context is used to handle the SQLite database connection.
     * Database is used to store score sheets for each of the three games.
     * These score sheets are used for achievement tracking and for the 
     * user to look back at how they have progressed.
     */
    public class LibraryContext : DbContext
    {
        // Using Entity Framework Core
        public DbSet<Replacement> replacements { get; set; }

        public string folderPath;
        public string databasePath;

        // Configure the database source
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={databasePath}");
        
        public LibraryContext()
        {
            // The database folder is created in the users `My Documents` or `Documents` folder under `DeweyDecimal`
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"DeweyDecimal");
            // The database is named `library.db`
            databasePath = Path.Combine(folderPath, @"library.db");
            // If the file does not exist, we create it to ensure proper app functionality
            // The user could clear their data by deleting the database containing folder
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Database.Migrate();
        }

    }
}
