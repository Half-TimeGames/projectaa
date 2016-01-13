using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private IDbConnection _dbConnection = new SqlConnection("Data Source=MAJOR\\S" +
                                                                "QLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public List<WorkItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public WorkItem Find(int id)
        {
            throw new NotImplementedException();
        }

        public WorkItem Add(WorkItem workItem)
        {
            throw new NotImplementedException();
        }

        public WorkItem Update(WorkItem workItem)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public WorkItem FindByDescription(string text)
        {
            throw new NotImplementedException();
        }

        public List<WorkItem> FindIfIssue()
        {
            throw new NotImplementedException();
        }

        public List<WorkItem> FindByStatus(Status status)
        {
            throw new NotImplementedException();
        }
    }
}
