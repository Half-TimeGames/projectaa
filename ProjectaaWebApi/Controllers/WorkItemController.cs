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

        [HttpGet]
        [Route("workitems")]
        public List<WorkItem> GetAllWorkItems()
        {
            try
            {
                var workItems = _workItemRepository.GetAll();
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("workitems")]
        public List<WorkItem> GetAllWorkItems(int page, int perPage)
        {
            try
            {
                var workItems = _workItemRepository.GetAll(page, perPage);
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{workitemid:int}")]
        public WorkItem GetWorkItem(int workItemId)
        {
            try
            {
                var workItem = _workItemRepository.Find(workItemId);
                return workItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("status/{statusId:int}")]
        public List<WorkItem> GetWorkItemsByStatus(int statusId)
        {
            try
            {
                var workItems = _workItemRepository.FindByStatus(statusId);
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("team/{teamId:int}")]
        public List<WorkItem> GetWorkItemsByTeam(int teamId)
        {
            try
            {
                var workItems = _teamRepository.GetWorkItems(teamId);
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("user/{userId:int}")]
        public List<WorkItem> GetWorkItemsByUser(int userId)
        {
            try
            {
                var workItems = _userRepository.WorkItems(userId);
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("search/desc")]
        public List<WorkItem> GetWorkItemsByDescription(string text)
        {
            try
            {
                var workItems = _workItemRepository.FindByDescription(text);
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("issues")]
        public List<WorkItem> GetWorkItemsWithIssue()
        {
            try
            {
                var workItems = _workItemRepository.FindIfIssue();
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpGet]
        [Route("search/done/date")]
        public List<WorkItem> HistoricSearch(DateTime from, DateTime to)
        {
            try
            {
                var workItems = _workItemRepository.FindByDate(from, to);
                return workItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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
        public WorkItem UpdateStatus(int workItemId, int statusId)
        {
            try
            {
                var workItem = _workItemRepository.Find(workItemId);
                if(workItem == null) throw new Exception("Invalid WorkItem");
                var newWorkItem = _workItemRepository.UpdateStatus(statusId, workItemId);
                return newWorkItem;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        [HttpPut]
        [Route("{workItemId:int}/user/{userId:int}")]
        public WorkItem AddWorkItemToUser(int workItemId, int userId)
        {
            try
            {
                var workItem = _workItemRepository.AddUserToWorkItem(userId, workItemId);
                return workItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{workItemId:int}")]
        public WorkItem DeleteWorkItem(int workItemId)
        {
            try
            {
                var workItem = _workItemRepository.Find(workItemId);
                if (workItem == null) throw new NullReferenceException();
                _workItemRepository.Remove(workItemId);
                return workItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
