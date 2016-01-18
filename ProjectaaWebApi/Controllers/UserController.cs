using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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
        [ResponseType(typeof(User))]
        public IHttpActionResult CreateUser(User user)
        {
            try
            {
                var newUser = _userRepository.Add(user);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("delete/{id:int}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                User user = _userRepository.Find(id);
                if (user == null) return NotFound();
                _userRepository.Remove(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("update/{user}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult UpdateUser(User user)
        {
            try
            {
                if (user == null) return NotFound();
                _userRepository.Update(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var result = _userRepository.GetAll();
                foreach (var user in result)
                {
                    user.Teams = _userRepository.GetTeams(user.Id);
                    user.WorkItems = _userRepository.WorkItems(user.Id);
                }
                return Ok(result);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("{id:int}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var result = _userRepository.Find(id);
                result.Teams = _userRepository.GetTeams(result.Id);
                result.WorkItems = _userRepository.WorkItems(result.Id);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("byteam/{id:int}")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetUsersByTeam(int id)
        {
            try
            {
                var result = _teamRepository.GetUsers(id);
                foreach (var user in result)
                {
                    user.Teams = _userRepository.GetTeams(user.Id);
                    user.WorkItems = _userRepository.WorkItems(user.Id);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("{name}")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetUsersByName(string name)
        {
            try
            {
                var result = _userRepository.FindByName(name);
                foreach (var user in result)
                {
                    user.Teams = _userRepository.GetTeams(user.Id);
                    user.WorkItems = _userRepository.WorkItems(user.Id);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
