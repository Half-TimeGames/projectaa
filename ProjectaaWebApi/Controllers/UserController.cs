using System;
using System.Collections.Generic;
using System.Web.Http;
using DataAccess.Repositories;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly TeamRepository _teamRepository = new TeamRepository();

        [Route("{user}")]
        public User CreateUser(User user)
        {
            try
            {
                var newUser = _userRepository.Add(user);
                return newUser;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [Route("")]
        public List<User> GetAllUsers()
        {
            try
            {
                var result = _userRepository.GetAll();
                foreach (var user in result)
                {
                    user.Teams = _userRepository.GetTeams(user.Id);
                    user.WorkItems = _userRepository.WorkItems(user.Id);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        [Route("{id:int}")]
        public User GetUserById(int id)
        {
            try
            {
                var result = _userRepository.Find(id);
                result.Teams = _userRepository.GetTeams(result.Id);
                result.WorkItems = _userRepository.WorkItems(result.Id);

                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        [Route("byteam/{id:int}")]
        public List<User> GetUsersByTeam(int id)
        {
            try
            {
                var result = _teamRepository.GetUsers(id);
                foreach (var user in result)
                {
                    user.Teams = _userRepository.GetTeams(user.Id);
                    user.WorkItems = _userRepository.WorkItems(user.Id);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        [Route("{name}")]
        public List<User> GetUsersByName(string name)
        {
            try
            {
                var result = _userRepository.FindByName(name);
                foreach (var user in result)
                {
                    user.Teams = _userRepository.GetTeams(user.Id);
                    user.WorkItems = _userRepository.WorkItems(user.Id);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
