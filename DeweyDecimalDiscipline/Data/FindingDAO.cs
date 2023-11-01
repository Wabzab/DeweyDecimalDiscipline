using DeweyDecimalDiscipline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Data
{
    public static class FindingDAO
    {
        // Add new score record to replacements table
        public static void Add(Finding scoreSheet)
        {
            using (var db = new LibraryContext())
            {
                db.Add(scoreSheet);
                db.SaveChanges();
            }
        }

        // Get all replacement score records from replacements table
        public static List<Finding> GetAll()
        {
            using (var db = new LibraryContext())
            {
                return db.findings.OrderByDescending(r => r.Date).ToList();
            }
        }
    }
}
