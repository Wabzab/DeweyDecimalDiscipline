using DeweyDecimalDiscipline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Data
{
    /*
     * Intended to handle the user's book replacement task scores.
     * It will have the following methods:
     *      - Add(scoreSheet)
     *      - Delete(id or scoreSheet)
     *      - GetAll()
     *      - GetById(id)
     * We can convert this into an implemention of an interface to allow
     * multiple data source options in the future but for now this fits.
     */
    public static class ReplacementDAO
    {
        // Add new score record to replacements table
        public static void Add(Replacement scoreSheet)
        {
            using(var db = new LibraryContext())
            {
                db.Add(scoreSheet);
                db.SaveChanges();
            }
        }

        // Get all replacement score records from replacements table
        public static List<Replacement> GetAll() 
        {
            using(var db = new LibraryContext())
            {
                return db.replacements.OrderByDescending(r => r.Date).ToList();
            }
        }
    }
}
