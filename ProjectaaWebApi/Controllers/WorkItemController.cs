using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

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
        //tilldela workitem till user
        //hämta workitem baserat på status
        //hämta workitems för ett team
        //hämta workitems för en user
        //söka efter workitem baserat på description
        //hämta workitem med status done för en viss period på "datedone"

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(WorkItem))]
        public IHttpActionResult CreateWorkItem(WorkItem workItem)
        {
            try
            {
                if(workItem == null) return BadRequest("WorkItem is null"); 
                var newWorkItem = _workItemRepository.Add(workItem);
                return Ok(newWorkItem);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{workItemId:int}")]
        public WorkItem UpdateWorkItem(WorkItem workItem)
        {
            try
            {
                var newWorkItem = _workItemRepository.Update(workItem);
                return newWorkItem;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

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
