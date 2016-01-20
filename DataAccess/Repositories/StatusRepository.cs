using Dapper;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly SqlConnection _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["azureConnectionString"].ConnectionString);

        public Status Find(int id)
        {
            return _dbConnection.QueryWithRetry<Status>("SELECT * FROM Status WHERE Id = @Id", new {Id = id}).SingleOrDefault();
        }

        public Status Update(Status status)
        {
            const string sqlQuery = "UPDATE Status SET " +
                                    "Name = @Name " +
                                    "WHERE Id = @Id";
            _dbConnection.QueryWithRetry<Status>(sqlQuery, status);
            return status;
        }

        public Status Add(Status status)
        {
            const string sqlQuery = "INSERT INTO Status (Name) " +
                                    "VALUES (@Name)";
            var statusId = _dbConnection.QueryWithRetry<int>(sqlQuery, status).Single();
            status.Id = statusId;
            return status;
        }

        public List<Status> GetAll()
        {
            return _dbConnection.QueryWithRetry<Status>("SELECT * FROM Status").ToList();
        }
    }
}
