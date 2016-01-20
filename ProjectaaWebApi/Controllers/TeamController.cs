using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

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

        [HttpGet]
        [Route("teams")]
        public List<Team> GetTeams()
        {
            try
            {
                var teams = _teamRepository.GetAll();
                return teams;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpGet]
        [Route("{teamId:int}")]
        public Team GetTeam(int teamId)
        {
            try
            {
                var team = _teamRepository.Find(teamId);
                team.Users = GetUsers(team.Id);
                team.WorkItems = GetWorkItems(team.Id);
                return team;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{teamId:int}/users")]
        public List<User> GetUsers(int teamId)
        {
            try
            {
                var users = _teamRepository.GetUsers(teamId);
                return users;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{teamId:int}/workitems")]
        public List<WorkItem> GetWorkItems(int teamId)
        {
            try
            {
                var workItems = _teamRepository.GetWorkItems(teamId);
                return workItems;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpPost]
        [Route("")]
        public Team CreateTeam(Team team)
        {
            try
            {
                var newTeam = _teamRepository.Add(team);
                return newTeam;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpPut]
        [Route("{teamId:int}")]
        public Team UpdateTeam(int teamId,[FromBody]Team team)
        {
            try
            {
                if (team == null || team.Id != teamId) throw new Exception("Invalid team");

                _teamRepository.Update(team);
                return team;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{teamId:int}/user/{userId:int}")]
        public Team AddToTeam(int teamId, int userId)
        {
            try
            {
                var team = _teamRepository.AddUserToTeam(userId, teamId);
                team.Users = _teamRepository.GetUsers(teamId);
                return team;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("{teamId:int}")]
        public Team DeleteTeam(int teamId)
        {
            try
            {
                var team = _teamRepository.Find(teamId);
                if (team == null) throw new NullReferenceException();
                _teamRepository.Remove(teamId);
                return team;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
