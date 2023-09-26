using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DeweyDecimalDiscipline.Content
{
    /*
     * Call Number Handler handles any operation being done with call numbers.
     * This will range from generating one or more call numbers, sorting call numbers,
     * Comparing call numbers, etc.
     */
    public static class CallNumberHandler
    {
        // Use random for generating unpredictable content
        static Random random = new Random();


        // Generates a list of random call numbers
        public static List<CallNumber> GenerateCallNumbers(int count)
        {
            List<CallNumber> unsortedList = new List<CallNumber>();
            for (int i = 0; i < count; i++)
            {
                // Form the random call number that always have length of 7 characters
                string number = String.Format("{0}.{1}", random.Next(1, 999).ToString("D3"), random.Next(1, 9999).ToString("D4"));
                // Need random author tag
                unsortedList.Add(new CallNumber() { Number=number, Author="ABC" });
            }
            return unsortedList;
        }

        // Checks if the given list is already sorted
        public static Boolean IsSorted(List<CallNumber> unsortedList)
        {
            // Create a copy of the unsorted list
            List<CallNumber> sortedList = new List<CallNumber>(unsortedList);
            // Sort the copy by the number section of the call number (author tag is not fully implemented yet)
            sortedList.Sort(delegate(CallNumber cn1, CallNumber cn2) { return cn1.Number.CompareTo(cn2.Number); });
            // Check if the given list and the copy are matching
            for (int i = 0; i < unsortedList.Count; i++)
            {
                if (unsortedList[i].Number != sortedList[i].Number)
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetScore(List<CallNumber> list) 
        {
            // Create a copy of the unsorted list
            List<CallNumber> sortedList = new List<CallNumber>(list);
            // Sort the copy by the number section of the call number (author tag is not fully implemented yet)
            sortedList.Sort(delegate (CallNumber cn1, CallNumber cn2) { return cn1.Number.CompareTo(cn2.Number); });
            // Check count of matching call numbers
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Number == sortedList[i].Number)
                {
                    count += 1;
                }
            }
            return count;
        }
    }
}
