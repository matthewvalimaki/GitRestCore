using LibGit2Sharp;
using System;
using System.IO;

namespace GitRestCore.Models
{
    public class GitRepository : Model, IModel
    {
        public int ProjectId { set; get; }

        /// <summary>
        /// Returns <see cref="Repository">LibGit2Sharp.Repository</see>
        /// </summary>
        /// <returns>Repository</returns>
        public Repository getLibGitRepository()
        {
            return new Repository(Path());
        }

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
                Repository.Init(Path());
                var repo = getLibGitRepository();
                repo.Commit("Repository Created");

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

                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new branch
        /// </summary>
        /// <param name="name"></param>
        public void CreateBranch(string name)
        {
            getLibGitRepository().CreateBranch(name);
        }

        public string ToJson()
        {
            return ConvertToJson(new { ProjectId });
        }
    }
}
