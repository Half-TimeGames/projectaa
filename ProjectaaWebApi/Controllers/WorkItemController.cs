using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Repositories;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/workitems")]
    public class WorkItemController : ApiController
    {
        readonly WorkItemRepository _workItemRepository = new WorkItemRepository();
        readonly TeamRepository _teamRepository = new TeamRepository();
        readonly UserRepository _userRepository = new UserRepository();


        [Route("bystatus/{statusId:int}")]
        public HttpResponseMessage GetWorkItemsByStatus(int statusId)
        {
            try
            {
                var result = _workItemRepository.FindByStatus(statusId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [Route("byteam/{teamId:int}")]
        public HttpResponseMessage GetWorkItemsByTeam(int teamId)
        {
            try
            {
                var result = _teamRepository.GetWorkItems(teamId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [Route("byuser/{userId:int}")]
        public HttpResponseMessage GetWorkItemsByUser(int userId)
        {
            try
            {
                var result = _userRepository.WorkItems(userId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [Route("{text}")]
        public HttpResponseMessage GetWorkItemsByDescription(string text)
        {
            try
            {
                var result = _workItemRepository.FindByDescription(text);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [Route("issues")]
        public HttpResponseMessage GetWorkItemsWithIssue()
        {
            try
            {
                var result = _workItemRepository.FindIfIssue();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }
    }
}
