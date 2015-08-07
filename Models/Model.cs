using Newtonsoft.Json;

namespace GitRestCore.Models
{
    abstract public class Model
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject();
        }
    }
}
