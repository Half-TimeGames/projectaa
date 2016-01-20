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

        //skapa workitem
        //ändra status på workitem
        //ta bort workitem
        //tilldela workitem till user - ANNA
        //hämta workitem baserat på status
        //hämta workitems för ett team
        //hämta workitems för en user
        //söka efter workitem baserat på description
        //hämta workitem med status done för en viss period på "datedone"

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
        
        [HttpGet]
        [Route("workitems")]
        public List<WorkItem> GetAllWorkItems()
        {
            try
            {
                var workItem = _workItemRepository.GetAll();
                return workItem;
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
                var workItem = _workItemRepository.GetAll(page, perPage);
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
                var result = _workItemRepository.FindByStatus(statusId);
                return result;
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
                var result = _teamRepository.GetWorkItems(teamId);
                return result;
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
                var result = _userRepository.WorkItems(userId);
                return result;
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
                var result = _workItemRepository.FindByDescription(text);
                return result;
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
                var result = _workItemRepository.FindIfIssue();
                return result;
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
                var result = _workItemRepository.FindByDate(from, to);
                return result;
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
