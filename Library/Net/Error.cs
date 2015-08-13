using Newtonsoft.Json;
using System;

namespace GitRestCore.Library.Net
{
    public class Error
    {
        public Exception Exception { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new { Exception });
        }
    }
}
