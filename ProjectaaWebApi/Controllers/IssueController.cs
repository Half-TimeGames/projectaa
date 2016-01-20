using System;
using System.Collections.Generic;
using System.Web.Http;
using DataAccess.Repositories;
using System.Web.Http.Description;
using Entities;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/issue")]
    public class IssueController : ApiController
    {
        private readonly IssueRepository _issueRepository = new IssueRepository();

        [HttpGet]
        [Route("issues")]
        public List<Issue> GetAllIssues(int page, int perPage)
        {
            try
            {
                var issues = _issueRepository.GetAll(page, perPage);
                return issues;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("issues")]
        public List<Issue> GetAllIssues()
        {
            try
            {
                var issues = _issueRepository.GetAll();
                return issues;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpGet]
        [Route("{issueId:int}")]
        [ResponseType(typeof(Issue))]
        public Issue GetIssue(int issueId)
        {
            try
            {
                var issue = _issueRepository.Find(issueId);
                return issue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("{issueId:int}/workitem")]
        public WorkItem GetIssueWorkItem(int issueId)
        {
            try
            {
                var workItem = _issueRepository.GetWorkItem(issueId);
                return workItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public Issue PostIssue(Issue issue)
        {
            try
            {
                _issueRepository.Add(issue);
                return issue;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{issueId:int}")]
        public Issue UpdateIssue(int issueId, [FromBody] Issue issue)
        {
            try
            {
                if (issue == null || issue.Id != issueId) throw new Exception("Invalid issue");
                _issueRepository.Update(issue);
                return issue;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
