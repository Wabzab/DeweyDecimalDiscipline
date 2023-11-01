using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Models
{
    /*
     * Data class to model what the Finding Call Number task score sheet looks like
     * Naming wise it could be better but it gets the job done :/
     */
    public class Finding
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Score { get; set; }

        public TimeSpan Time { get; set; }
    }
}
