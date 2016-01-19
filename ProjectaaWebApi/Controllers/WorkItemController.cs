using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/workitem")]
    public class WorkItemController : ApiController
    {
        readonly WorkItemRepository _workItemRepository = new WorkItemRepository();
        readonly UserRepository _userRepository = new UserRepository();
        readonly TeamRepository _teamRepository = new TeamRepository();

        [HttpPost]
        [Route("")]
        public WorkItem CreateWorkItem(WorkItem workItem)
        {
            try
            {
                if(workItem == null) throw new NullReferenceException();
                var newWorkItem = _workItemRepository.Add(workItem);
                return newWorkItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{workItemId:int}")]
        public WorkItem UpdateWorkItem(int workItemId, [FromBody]WorkItem workItem)
        {
            try
            {
                if(workItem == null || workItem.Id != workItemId) throw new Exception("Invalid WorkItem");
                var newWorkItem = _workItemRepository.Update(workItem);
                return newWorkItem;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        //[HttpDelete]
        //[Route("{workItemId:int}")]
        //public WorkItem DeleteWorkItem(int workItemId, [FromBody] WorkItem workItem)
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception e)
        //    {                
        //        throw new ArgumentException(e.Message);
        //    }
        //}

        [Route("status/{statusId:int}")]
        public List<WorkItem> GetWorkItemsByStatus(int statusId)
        {
            try
            {
                var result = _workItemRepository.FindByStatus(statusId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [Route("team/{teamId:int}")]
        public List<WorkItem> GetWorkItemsByTeam(int teamId)
        {
            try
            {
                var result = _teamRepository.GetWorkItems(teamId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [Route("user/{userId:int}")]
        public List<WorkItem> GetWorkItemsByUser(int userId)
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

        [Route("search/{text}")]
        public List<WorkItem> GetWorkItemsByDescription(string text)
        {
            try
            {
                var result = _workItemRepository.FindByDescription(text);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
            }

        }
    }
}
