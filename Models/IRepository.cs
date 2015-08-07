namespace GitRestCore.Models
{
    interface IRepository
    {
        /// <summary>
        /// Converts resource to JSON string
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
