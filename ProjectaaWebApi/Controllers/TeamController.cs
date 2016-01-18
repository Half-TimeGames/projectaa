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

        [HttpGet]
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
                throw new ArgumentNullException(e.Message);
            }

        }

        [HttpGet]
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

        [HttpGet]
        [Route("{id:int}/users")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetUsers(int id)
        {
            try
            {
                var users = _teamRepository.GetUsers(id);
                return Ok(users);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}/workitems")]
        [ResponseType(typeof(List<WorkItem>))]
        public IHttpActionResult GetWorkItems(int id)
        {
            try
            {
                var users = _teamRepository.GetWorkItems(id);
                return Ok(users);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{teamId:int}/adduser/{userId:int}")]
        [ResponseType(typeof (Team))]
        public IHttpActionResult AddToTeam(int teamId, int userId)
        {
            try
            {
                var team = _teamRepository.AddUserToTeam(userId, teamId);
                team.Users = _teamRepository.GetUsers(teamId);
                return Ok(team);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Team))]
        public IHttpActionResult CreateTeam(Team team)
        {
            try
            {
                _teamRepository.Add(team);
                return Ok(team);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpPut]
        [Route("")]
        [ResponseType(typeof (Team))]
        public IHttpActionResult UpdateTeam(Team team)
        {
            try
            {
                if (team == null) return BadRequest("User is null");

                _teamRepository.Update(team);
                return Ok(team);
            }

            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

    }
}
