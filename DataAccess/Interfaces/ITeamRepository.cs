using Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface ITeamRepository
    {
        List<Team> GetAll();
        Team Find(int id);
        Team Add(Team team);
        Team Update(Team team);
        void Remove(int id);
        List<User> GetUsers(int id);
        List<WorkItem> GetWorkItems(int id);
        Team AddUserToTeam(int teamId, int userId);
    }
}
