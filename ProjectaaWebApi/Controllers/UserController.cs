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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

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

        [Route("{id:int}/delete")]
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

        [Route("{user}/update")]
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

        [Route("users")]
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
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("{id:int}/teams")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetUsersByTeam(int id)
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

        [Route("{id:int}/workitems")]
        [ResponseType(typeof (List<WorkItem>))]
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
    }
}
