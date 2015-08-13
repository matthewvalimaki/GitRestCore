using Newtonsoft.Json;

namespace GitRestCore.Models
{
    public abstract class Model
    {
        /// <summary>
        /// Determines whether resource exists
        /// </summary>
        public bool HasInitialized { get; protected set; }

        /// <summary>
        /// Converts given object to JSON string
        /// </summary>
        /// <param name="publicEntity"></param>
        /// <returns>string</returns>
        protected string ConvertToJson(object publicEntity)
        {
            return JsonConvert.SerializeObject(publicEntity);
        }
    }
}
