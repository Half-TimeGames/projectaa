using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Repositories;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/teams")]
    public class TeamController : ApiController
    {
        readonly TeamRepository _teamRepository = new TeamRepository();

        [Route("")]
        public HttpResponseMessage GetTeams()
        {
            try
            {
                var result = _teamRepository.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }
    }
}
