using LibGit2Sharp;
using System;

namespace GitRestCore.Models
{
    public class GitBranch : Model, IModel
    {
        public int Id { set; get; }
        public int ProjectId { set; get; }

        /// <summary>
        /// Returns related Repository
        /// </summary>
        /// <returns>Repository</returns>
        public GitRepository getRepository ()
        {
            return new GitRepository { ProjectId = ProjectId };
        }

        /// <summary>
        /// Checks whether the branch exists
        /// </summary>
        /// <returns>bool</returns>
        public bool Exists()
        {
            foreach (Branch branch in getRepository().getLibGitRepository().Branches)
            {
                if (branch.Name == Convert.ToString(Id))
                {
                    return true;
                }
            }

            return false;
        }

        public void Save()
        {
            if (!HasInitialized)
            {
                getRepository().CreateBranch(Convert.ToString(Id));
                HasInitialized = true;
            }
        }

        public string ToJson()
        {
            return ConvertToJson(new { Id, ProjectId, Created = Exists() });
        }
    }
}
