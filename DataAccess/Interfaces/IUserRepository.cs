using System.Collections.Generic;
using Entities;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll(int pageNumber, int rowsPerPage);
        User Find(int id);
        User Add(User user);
        User Update(User user);
        void Remove(int id);
        List<Team> GetTeams(int id);
        List<WorkItem> WorkItems(int id);
        List<User> FindByName(string name);
        User AddTeamToUser(int userId, int teamId);
    }
}
