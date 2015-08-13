namespace GitRestCore.Models
{
    public interface IModel
    {
        void Save();
        /// <summary>
        /// Converts resource to JSON string
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
