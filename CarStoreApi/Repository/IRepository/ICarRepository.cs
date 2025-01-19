using System.Linq.Expressions;
using CarStoreApi.Models;

namespace CarStoreApi.Repository.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<Car> UpdateAsync(Car entity);
    }
}
