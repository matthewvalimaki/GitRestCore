using System;
using System.IO;

namespace GitRestCore.Models
{
    public class Repository : Model, IRepository
    {
        public int ProjectId { set; get; }
        /// <summary>
        /// Determines whether repository exists
        /// </summary>
        public bool HasInitialized { get; protected set; }

        public string Path()
        {
            return "c:\\Repository\\" + Convert.ToString(ProjectId);
        }

        public bool repositoryPathExists()
        {
            return Directory.Exists(Path());
        }

        public void Save()
        {
            if (!HasInitialized) {
                LibGit2Sharp.Repository.Init(Path());
                HasInitialized = true;
            }
        }

        /// <summary>
        /// Deletes the whole repository
        /// </summary>
        /// <returns>True if success, false otherwise</returns>
        public bool Remove()
        {
            try {
                Directory.Delete(Path(), true);
            } catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
