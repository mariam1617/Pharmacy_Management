using PharmacyManagement.Models;

namespace PharmacyManagement.Repository
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<Medicine> GetByIdAsync(int id);
        Task<Medicine> CreateAsync(Medicine medicine);
        Task<Medicine> UpdateAsync(Medicine medicine);
        Task<bool> DeleteAsync(int id);
    }
}
