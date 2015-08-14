using LibGit2Sharp;
using System;
using System.Diagnostics;
using System.IO;

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

        public string ReadmePath()
        {
            return getRepository().ClonePath() + Path.DirectorySeparatorChar + "README.md";
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
                try {
                    using (var repo = getRepository().Clone()) {
                        repo.Commit("[Automation 1] Repository Created.");
                        repo.CreateBranch(Convert.ToString(Id));
                        repo.Network.Push(repo.Network.Remotes["origin"], @"refs/heads/" + Convert.ToString(Id));
                        repo.Dispose();
                    }

                    getRepository().RemoveClone();

                    HasInitialized = true;
                } catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Removes 
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            try {
                getRepository().getLibGitRepository().Branches.Remove(Convert.ToString(Id));

                return true;
            } catch
            {
                return false;
            }
        }

        public string ToJson()
        {
            return ConvertToJson(new { Id, ProjectId, Created = Exists() });
        }
    }
}
