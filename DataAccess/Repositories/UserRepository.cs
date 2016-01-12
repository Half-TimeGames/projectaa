using System;
using System.Collections.Generic;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User Add(User user)
        {
            throw new NotImplementedException();
        }

        public User Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Team> GetTeams(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }

        public List<WorkItem> WorkItems(int id)
        {
            throw new NotImplementedException();
        }
    }
}
