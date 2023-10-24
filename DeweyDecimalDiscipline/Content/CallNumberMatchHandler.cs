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
            // C:\Users\pater\Desktop\School Dev\Prog\DeweyDecimalDiscipline\DeweyDecimalDiscipline\bin\Debug\net6.0-windows\CallNumberData.json
            string PATH = Path.Combine("../../../Content/CallNumberData.json");
            string CallNumberData = File.ReadAllText(PATH);
            CallNumberJsonObject? data = JsonConvert.DeserializeObject<CallNumberJsonObject>(CallNumberData);
            return data != null ? data.callNumbers!: new List<CallNumber>();
        }

        class CallNumberJsonObject
        {
            public List<CallNumber>? callNumbers;
        }

    }
}
