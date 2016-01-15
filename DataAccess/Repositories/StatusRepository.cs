﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection("Server=tcp:projectaa.database.windows.net,1433;Database=projactaa_db;User ID=andreas.dellrud@projectaa;Password=TeAnAn2016;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public Status Find(int id)
        {
            return _dbConnection.Query<Status>("select * from Status where Id = @Id", new {Id = id}).SingleOrDefault();
        }

        public Status Update(Status status)
        {
            var sqlQuery = "UPDATE Status SET " +
                           "Name = @Name " +
                           "WHERE Id = @Id";
            _dbConnection.Execute(sqlQuery, status);
            return status;
        }

        public Status Add(Status status)
        {
            var sqlQuery = "INSERT INTO Status (Name) " +
                           "VALUES (@Name)";
            var statusId = _dbConnection.Query<int>(sqlQuery, status).Single();
            status.Id = statusId;
            return status;
        }

        public List<Status> GetAll()
        {
            return _dbConnection.Query<Status>("select * from Status").ToList();
        }
    }
}
