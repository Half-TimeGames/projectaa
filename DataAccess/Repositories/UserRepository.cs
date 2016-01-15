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

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection = new SqlConnection("Data Source=MAJOR\\S" +
                                                                "QLEXPRESS;Initial Catalog=Projectaa_Db;Integrated Security=True");

        public User Add(User user)
        {
            const string sqlQuery = "insert into User (FirstName, LastName, UserName) values (@FirstName, @LastName, @UserName)";
            var userId = _dbConnection.Query<int>(sqlQuery, user).Single();
            user.Id = userId;
            return user;
        }

        public User Find(int id)
        {
            return _dbConnection.Query<User>("select * from User where Id = @Id", id).SingleOrDefault();
        }

        public List<User> FindByName(string name)
        {
            return
                _dbConnection.Query<User>(
                    "select * from User where (FirstName like @name) or (LastName like @Name) or (UserName like @Name)", name)
                    .ToList();
        }

        public List<User> GetAll()
        {
            return _dbConnection.Query<User>("select * from User").ToList();
        }

        public List<Team> GetTeams(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            const string sqlQuery = "delete from User where Id = @Id";
            _dbConnection.Execute(sqlQuery, id);
        }

        public User Update(User user)
        {
            const string sqlQuery = "update User set " +
                                    "FirstName = @FirstName," +
                                    "LastName = @LastName" +
                                    "UserName = @UserName" +
                                    " where Id = @Id";
            _dbConnection.Execute(sqlQuery, user);
            return user;
        }

        public List<WorkItem> WorkItems(int id)
        {
            throw new NotImplementedException();
        }
    }
}
