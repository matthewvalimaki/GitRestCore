using Microsoft.AspNet.Mvc;
using GitRestCore.Models;
using System.Net;
using System;

namespace GitRestCore.Controllers
{
    [Route("api/v1/[controller]")]
    public class RepositoryController : Controller
    {
        // GET api/v1/repository/1
        [HttpGet("{projectId:int:min(1)}")]
        public string Get(int projectId)
        {
            GitRepository repository = new GitRepository { ProjectId = projectId };

            // make sure the repository exists
            if (!repository.repositoryPathExists())
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.NotFound);
            }

            return repository.ToJson();
        }

        // PUT api/repository/1
        [HttpPut("{projectId:int:min(1)}")]
        public void Put(int projectId)
        {
            GitRepository repository = new GitRepository { ProjectId = projectId };

            // check if path already exists
            if (repository.repositoryPathExists())
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.OK);
            } else
            {
                repository.Save();

                // repository was created
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.Created);
            }
        }

        // DELETE api/repository/1
        [HttpDelete("{projectId:int:min(1)}")]
        public void Delete(int projectId)
        {
            GitRepository repository = new GitRepository { ProjectId = projectId };

            // make sure the repository exists
            if (!repository.repositoryPathExists())
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.NotFound);
            }

            if (repository.Remove())
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.NoContent);
            }
        }
    }
}
