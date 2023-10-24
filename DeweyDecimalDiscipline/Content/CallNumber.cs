using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content
{
    /*
     * Data class for a CallNumber
     * Used to keep track of all the data related to a call number.
     * Has a number and an author, and a description which is the conjunction of both.
     */
    public class CallNumber
    {


        public string Name
        {
            get { return Number + " " + Author; }
            set { /* Do Nothing */ }
        }
        public string Number { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public CallNumber() 
        {
            Number = "000";
            Author = "ABC";
            Description = "A description about the topic this call number represents.";
        }
    }
}
