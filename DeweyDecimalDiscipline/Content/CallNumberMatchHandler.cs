using DeweyDecimalDiscipline.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content
{
    public static class CallNumberMatchHandler
    {
        
        public static List<CallNumber> GetCallNumbers()
        {
            string PATH = "../../../Content/CallNumberData.json";
            string CallNumberData = File.ReadAllText(PATH);
            CallNumberJsonObject? data = JsonConvert.DeserializeObject<CallNumberJsonObject>(CallNumberData);
            return data != null ? data.callNumbers!: new List<CallNumber>();
        }

        public static int getScore(List<MatchedPair> matchedPairs)
        {
            int score = 0;
            foreach (MatchedPair pair in matchedPairs)
            {
                if(pair.A == pair.B) score++;
            }
            return score;
        }

        class CallNumberJsonObject
        {
            public List<CallNumber>? callNumbers;
        }

    }
}
