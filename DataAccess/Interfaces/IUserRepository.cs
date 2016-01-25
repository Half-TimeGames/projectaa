using Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll(int page, int perPage);
        User Find(int id);
        User Add(User user);
        User Update(User user);
        void Remove(int id);
        List<Team> GetTeams(int id);
        List<WorkItem> WorkItems(int id);
        List<User> FindByName(string firstName = null, string lastName = null, string userName = null);
        User AddTeamToUser(int userId, int teamId);
    }
}
