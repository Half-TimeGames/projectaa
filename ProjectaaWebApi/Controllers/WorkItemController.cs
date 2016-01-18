using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.Repositories;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/workitem")]
    public class WorkItemController : ApiController
    {
        readonly WorkItemRepository _workItemRepository = new WorkItemRepository();
        readonly TeamRepository _teamRepository = new TeamRepository();
        readonly UserRepository _userRepository = new UserRepository();

        [HttpPost]
        [Route("add/{workItem}")]
        [ResponseType(typeof(WorkItem))]
        public IHttpActionResult CreateWorkItem(WorkItem workItem)
        {
            try
            {
                var newWorkItem = _workItemRepository.Add(workItem);
                return Ok(newWorkItem);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //[HttpPut]
        //[Route("{workitem}")]
        //public WorkItem UpdateWorkItem(WorkItem workItem)
        //{
        //    try
        //    {
        //        var newWorkItem = _workItemRepository.Update(workItem);
        //        return newWorkItem;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException(e.Message);
        //    }
        //}

        [Route("{statusId:int}/bystatus")]
        public List<WorkItem> GetWorkItemsByStatus(int statusId)
        {
            try
            {
                var result = _workItemRepository.FindByStatus(statusId);
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }

        [Route("{teamId:int}/byteam")]
        public List<WorkItem> GetWorkItemsByTeam(int teamId)
        {
            try
            {
                var result = _teamRepository.GetWorkItems(teamId);
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }

        [Route("{userId:int}/byuser")]
        public List<WorkItem> GetWorkItemsByUser(int userId)
        {
            try
            {
                var result = _userRepository.WorkItems(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }

        [Route("{text}")]
        public List<WorkItem> GetWorkItemsByDescription(string text)
        {
            try
            {
                var result = _workItemRepository.FindByDescription(text);
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }

        [Route("issues")]
        public List<WorkItem> GetWorkItemsWithIssue()
        {
            try
            {
                var result = _workItemRepository.FindIfIssue();
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }
    }
}
