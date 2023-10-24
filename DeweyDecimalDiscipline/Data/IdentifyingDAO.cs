using DeweyDecimalDiscipline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Data
{
    /*
     * Intended to handle the user's identifying areas task scores.
     * It will have the following methods:
     *      - Add(scoreSheet)
     *      - Delete(id or scoreSheet)
     *      - GetAll()
     *      - GetById(id)
     * We can convert this into an implemention of an interface to allow
     * multiple data source options in the future but for now this fits.
     */
    public static class IdentifyingDAO
    {
        // Add new score record to replacements table
        public static void Add(Identifying scoreSheet)
        {
            using (var db = new LibraryContext())
            {
                db.Add(scoreSheet);
                db.SaveChanges();
            }
        }

        // Get all replacement score records from replacements table
        public static List<Identifying> GetAll()
        {
            using (var db = new LibraryContext())
            {
                return db.identifyings.OrderByDescending(r => r.Date).ToList();
            }
        }
    }
}
