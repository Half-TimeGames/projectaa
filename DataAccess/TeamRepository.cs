using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess
{
    public class TeamRepository : ITeamRepository
    {
        private IDbConnection _dbConnection = new SqlConnection("Data Source=MAJOR\\SQLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");
        public Team Add(Team team)
        {
            throw new NotImplementedException();
        }

        public Team Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Team> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers(int id)
        {
            throw new NotImplementedException();
        }

        public List<WorkItem> GetWorkItems(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Team Update(Team team)
        {
            throw new NotImplementedException();
        }
    }
}
