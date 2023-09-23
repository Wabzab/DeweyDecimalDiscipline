using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content
{
    public class CallNumber
    {

        public string Number { get; set; }
        public string Author { get; set; }
        public string Description {
            get { return Number + " " + Author; }
            set { /* Do Nothing */ } }

        public CallNumber() 
        {
            Number = "000";
            Author = "ABC";
        }
    }
}
