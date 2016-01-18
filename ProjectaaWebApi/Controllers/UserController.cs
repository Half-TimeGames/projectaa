using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly TeamRepository _teamRepository = new TeamRepository();


        [HttpGet]
        [Route("users")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var result = _userRepository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var result = _userRepository.Find(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{name}")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetUsersByName(string name)
        {
            try
            {
                var result = _userRepository.FindByName(name);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}/teams")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetTeams(int id)
        {
            try
            {
                var result = _userRepository.GetTeams(id);
                return Ok(result);
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
                var result = _userRepository.WorkItems(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{userId:int}/addteam/{teamId:int}")]
        [ResponseType(typeof(Team))]
        public IHttpActionResult AddToTeam(int teamId, int userId)
        {
            try
            {
                var user = _userRepository.AddUserToTeam(userId, teamId);
                user.Teams = _userRepository.GetTeams(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            try
            {
                if (user == null) return BadRequest("user is null");
                var newUser = _userRepository.Add(user);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpPut]
        [Route("")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PutUser(User user)
        {
            try
            {
                if (user == null) return BadRequest("No user");
                _userRepository.Update(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                var user = _userRepository.Find(id);
                if (user == null) return NotFound();
                _userRepository.Remove(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
