using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;

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
        public IHttpActionResult GetTeams()
        {
            try
            {
                var result = _teamRepository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }

        [Route("teams/{team}")]
        public Team CreateTeam(Team team)
        {
            try
            {
                var newTeam = _teamRepository.Add(team);
                return newTeam;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
