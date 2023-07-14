using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Models;

namespace TravelCompany.Repositories
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        private List<T> _lsObj;

        public Repository()
        {
            _lsObj = new List<T>();
        }
        public List<T> GetAll()
        {
            return _lsObj;
        }
        public Repository(List<T> lsObj) 
        { 
        }
        public bool Register(T obj)
        {
            try
            {
                _lsObj.Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool DeleteById(Guid id)
        {
            try
            {
                T obj = _lsObj.Find(d => d.Id == id);
                if (obj != null)
                {
                    _lsObj.Remove(obj);
                }
                return true;
            }
            catch  
            {
                return false;
            }

        }

        public bool UpdateDriver(T updatedObj)
        {
            try
            {
                int index = _lsObj.FindIndex(d => d.Id == updatedObj.Id);

                if (index != -1)
                {
                    _lsObj[index] = updatedObj;
                }
                return true;
            }
            catch 
            {
                return false;
            }

            
        }
    }
}
