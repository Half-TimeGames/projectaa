using System.Collections.Generic;
using Entities;

namespace DataAccess.Interfaces
{
    public interface IStatusRepository
    {
        List<Status> GetAll();
        Status Find(int id);
        Status Update(Status status);
        Status Add(Status status);
    }
}
