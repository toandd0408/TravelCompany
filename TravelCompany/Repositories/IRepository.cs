using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Models;

namespace TravelCompany.Repositories
{
    public interface IRepository<T> where T : ModelBase
    {
        List<T> GetAll();
        public bool Register(T obj);
        public bool DeleteById(Guid id);
        public bool UpdateDriver(T updatedObj);
    }
}
