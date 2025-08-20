using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Models;

namespace PharmacyManagement.Repository
{
    public class MedicineRepository:IMedicineRepository

    {
        private readonly AppDbContext _context;

    public MedicineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Medicine>> GetAllAsync()
    {
        return await _context.Medicines.Include(e=>e.MedicinesOrder)
            .ToListAsync();
    }

    public async Task<Medicine?> GetByIdAsync(int id)
            
        {
            return await _context.Medicines.Include(e => e.MedicinesOrder).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Medicine> CreateAsync(Medicine order)
    {
        _context.Medicines.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Medicine> UpdateAsync(Medicine order)
    {
        _context.Medicines.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await GetByIdAsync(id);
        if (order == null) return false;

        _context.Medicines.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

}
}
