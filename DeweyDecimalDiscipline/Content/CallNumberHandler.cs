using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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


        public static Boolean IsSorted(List<CallNumber> unsortedList)
        {
            List<CallNumber> sortedList = new List<CallNumber>(unsortedList);
            sortedList.Sort(delegate(CallNumber cn1, CallNumber cn2) { return cn1.Number.CompareTo(cn2.Number); });
            for (int i = 0; i < unsortedList.Count; i++)
            {
                if (unsortedList[i].Number != sortedList[i].Number)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
