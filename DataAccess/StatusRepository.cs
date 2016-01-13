using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess
{
    public class StatusRepository : IStatusRepository
    {
        private IDbConnection _dbConnection = new SqlConnection("Data Source=MAJOR\\S" +
                                                                "QLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public Status Find(int id)
        {
            return _dbConnection.Query<Status>("select * from Status where Id = @Id", new {Id = id}).SingleOrDefault();
        }

        public List<Status> GetAll()
        {
            return _dbConnection.Query<Status>("select * from Status").ToList();
        }
    }
}
