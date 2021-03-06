using dotNet_TWITTER.Applications.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task AddRange(List<T> entity);
        Task Remove(T entity);
        Task RemoveRange(T entity);
        Task<T> GetById(string id);
    }


}
