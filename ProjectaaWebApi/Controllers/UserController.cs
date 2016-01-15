using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
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
        public void CreateUser(int id, string firstName, string lastName, string userName)
        {
            var user = new User {FirstName = firstName, LastName = lastName, UserName = userName};
            _userRepository.Add(user);
        }

        //[Route("")]
        //public HttpResponseMessage UpdateUser()
        //{
            
        //}

        [Route("")]
        public HttpResponseMessage GetAllUsers()
        {
            try
            {
                var result = _userRepository.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            
        }

        [Route("{id:int}")]
        public HttpResponseMessage GetUserById(int id)
        {
            try
            {
                var result = _userRepository.Find(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("byteam/{id:int}")]
        public HttpResponseMessage GetUsersByTeam(int id)
        {
            try
            {
                var result = _teamRepository.GetUsers(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("{name}")]
        public HttpResponseMessage GetUsersByName(string name)
        {
            try
            {
                var result = _userRepository.FindByName(name);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
