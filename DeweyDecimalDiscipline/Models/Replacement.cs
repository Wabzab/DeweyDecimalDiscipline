using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Models
{
    /*
     * Data class to model what the Replacement score sheet looks like
     * Naming wise it could be better but it gets the job done :/
     */
    public class Replacement
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public int Score { get; set; }
        
        public TimeSpan Time { get; set; }
    }
}
