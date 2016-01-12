using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess
{
    public class StatusRepository : IStatusRepository
    {
        public Status Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Status> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
