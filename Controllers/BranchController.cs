using GitRestCore.Library.Net;
using GitRestCore.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Net;

namespace GitRestCore.Controllers
{
    [Route("api/v1/repository/{projectId:int:min(1)}/[controller]")]
    public class BranchController : Controller
    {
        // GET api/repository/1/branch/2
        [HttpGet("{branchId:int:min(1)}")]
        public string Get(int projectId, int branchId)
        {
            GitBranch branch = new GitBranch { Id = branchId, ProjectId = projectId };

            try {
                return branch.ToJson();
            } catch (Exception exception)
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.InternalServerError);
                return new Error { Exception = exception }.ToJson();
            }
        }

        // POST api/repository/1/branch/2
        [HttpPost("{branchId:int:min(1)}")]
        public void Post([FromBody]string value, int projectId, int branchId)
        {
            GitBranch branch = new GitBranch { Id = branchId, ProjectId = projectId };

            if (branch.Exists())
            {
                Response.StatusCode = Convert.ToInt16(HttpStatusCode.OK);
            } else
            {
                try {
                    branch.Save();

                    Response.StatusCode = Convert.ToInt16(HttpStatusCode.Created);
                } catch (Exception exception)
                {
                    var tes = exception;
                    Response.StatusCode = Convert.ToInt16(HttpStatusCode.InternalServerError);
                }
            }
            
        }

        // PUT api/v1/repository/1/branch/2
        [HttpPut("{branchId:int:min(1)}")]
        public void Put(int projectId, int branchId)
        {

        }

        // DELETE api/repository/1
        [HttpDelete("{branchId:int:min(1)}")]
        public void Delete(int branchId)
        {

        }
    }
}
