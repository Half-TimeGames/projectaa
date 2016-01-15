using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Repositories;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/teams")]
    public class TeamController : ApiController
    {
        //skapa team
        //uppdatera team
        //ta bort team
        //lägga till user till team?

        readonly TeamRepository _teamRepository = new TeamRepository();

        [Route("")]
        public List<Team> GetTeams()
        {
            try
            {
                var result = _teamRepository.GetAll();
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }
    }
}
