using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        [HttpGet]
        [Route("users")]
        public List<User> GetAllUsers(int page, int perPage)
        {
            try
            {
                var users = _userRepository.GetAll(page, perPage);
                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{userId:int}")]
        public User GetUserById(int userId)
        {
            try
            {
                var result = _userRepository.Find(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public List<User> GetUsersByName(string name)
        {
            try
            {
                var result = _userRepository.FindByName(name);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{userId:int}/teams")]
        public List<Team> GetTeams(int userId)
        {
            try
            {
                var result = _userRepository.GetTeams(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{userId:int}/workitems")]
        public List<WorkItem> GetWorkItems(int userId)
        {
            try
            {
                var result = _userRepository.WorkItems(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public User PostUser(User user)
        {
            try
            {
                var newUser = _userRepository.Add(user);
                return newUser;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{userId:int}/team/{teamId:int}")]
        public User AddToTeam(int userId, int teamId)
        {
            try
            {
                var user = _userRepository.AddTeamToUser(userId, teamId);
                user.Teams = _userRepository.GetTeams(userId);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{userId:int}")]
        public User PutUser(int userId, [FromBody] User user)
        {
            try
            {
                if (user == null || user.Id != userId) throw new Exception("Invalid user");
                _userRepository.Update(user);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("{userId:int}")]
        public User DeleteUser(int userId)
        {
            try
            {
                var user = _userRepository.Find(userId);
                if (user == null) throw new NullReferenceException();
                _userRepository.Remove(userId);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
