using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.Repositories;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/team")]
    public class TeamController : ApiController
    {
        //skapa team
        //uppdatera team
        //ta bort team
        //lägga till user till team?

        readonly TeamRepository _teamRepository = new TeamRepository();
        readonly UserRepository _userRepository = new UserRepository();

        [Route("teams")]
        [ResponseType(typeof(List<Team>))]
        public IHttpActionResult GetTeams()
        {
            try
            {
                var result = _teamRepository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [Route("{team}")]
        [ResponseType(typeof(Team))]
        public IHttpActionResult CreateTeam(Team team)
        {
            try
            {
                var newTeam = _teamRepository.Add(team);
                return Ok(newTeam);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Team))]
        public IHttpActionResult GetTeam(int id)
        {
            try
            {
                var team = _teamRepository.Find(id);
                return Ok(team);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
