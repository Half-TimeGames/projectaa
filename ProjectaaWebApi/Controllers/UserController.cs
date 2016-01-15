﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Repositories;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        readonly UserRepository _userRepository = new UserRepository();
        readonly TeamRepository _teamRepository = new TeamRepository();

        [Route("{user}")]
        [HttpPost]
        public HttpResponseMessage AddUser(User user)
        {
<<<<<<< Updated upstream
            var user = new User { FirstName = firstName, LastName = lastName, UserName = userName };
            _userRepository.Add(user);
        }

        //[Route("api/users")]
        //public HttpResponseMessage Post(User user)
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.Created);
        //    string uri = Url.Link("GetUserById", new { id = user.Id });
        //    response.Headers.Location = new Uri(uri);
        //    return response;
        //}
            
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    User user = _userRepository.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    _userRepository.Remove(id);
        //    return Ok(user);
=======
            var newUser = _userRepository.Add(user);
            var response = Request.CreateResponse(HttpStatusCode.Created, newUser);
            response.Headers.Location = new Uri(Request.RequestUri + user.Id.ToString());
            return response;
        }

        //public User CreateUser(User user)
        //{
        //    try
        //    {
        //        var newUser = _userRepository.Add(user);
        //        return newUser;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException(e.Message);
        //    }
        //}

        //[Route("")]
        //public HttpResponseMessage UpdateUser()
        //{

>>>>>>> Stashed changes
        //}

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
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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
