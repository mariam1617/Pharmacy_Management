using PharmacyManagement.Models;

namespace PharmacyManagement.Repository
{
    public interface IOrderRepository
    {

        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order entity);
        void Update(Order entity);
        void Delete(Order entity);

    }
}
