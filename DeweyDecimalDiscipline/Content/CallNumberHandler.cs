using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content
{
    public static class CallNumberHandler
    {

        static Random random = new Random();

        public static List<CallNumber> GenerateCallNumbers(int count)
        {
            List<CallNumber> unsortedList = new List<CallNumber>();
            for (int i = 0; i < count; i++)
            {
                string number = String.Format("{0}.{1}", random.Next(1, 999).ToString("D3"), random.Next(1, 9999).ToString("D4"));
                unsortedList.Add(new CallNumber() { Number=number, Author="ABC" });
            }
            return unsortedList;
        }
    }
}
