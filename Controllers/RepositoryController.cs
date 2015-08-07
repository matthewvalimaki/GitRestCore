using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using GitRestCore.Models;
using System.Net;
using System;

namespace GitRestCore.Controllers
{
    [Route("api/[controller]")]
    public class RepositoryController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/1
        [HttpGet("{projectId:int:min(1)}")]
        public string Get(int projectId)
        {
            Repository repository = new Repository { ProjectId = projectId };

            // make sure the repository exists
            if (!repository.repositoryPathExists())
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.NotFound);
            }

            return repository.ToJson();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/repository/1
        [HttpPut("{projectId:int:min(1)}")]
        public void Put(int projectId)
        {
            Repository repository = new Repository { ProjectId = projectId };

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
            Repository repository = new Repository { ProjectId = projectId };

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
