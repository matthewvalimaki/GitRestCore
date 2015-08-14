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

        public Repository Clone()
        {
            Repository.Clone(Path(), ClonePath());

            return new Repository(ClonePath());
        }

        public string Path()
        {
            return "c:\\Repository\\" + Convert.ToString(ProjectId);
        }

        public string ClonePath()
        {
            return Path() + "_clone";
        }

        public bool repositoryPathExists()
        {
            return Directory.Exists(Path());
        }

        public void Save()
        {
            if (!HasInitialized) {
                Repository.Init(Path(), true);
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

        public bool RemoveClone()
        {
            try
            {
                DeleteReadOnlyDirectory(ClonePath());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Due to .git containing read-only files this method is needed to remove those files.
        /// </summary>
        /// <param name="directory"></param>
        protected static void DeleteReadOnlyDirectory(string directory)
        {
            foreach (var subdirectory in Directory.EnumerateDirectories(directory))
            {
                DeleteReadOnlyDirectory(subdirectory);
            }
            foreach (var fileName in Directory.EnumerateFiles(directory))
            {
                var fileInfo = new FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Normal;
                fileInfo.Delete();
            }
            Directory.Delete(directory);
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
