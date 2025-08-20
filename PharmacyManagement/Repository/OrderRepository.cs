using System.Threading.Tasks;
using PharmacyManagement.Data;
using PharmacyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace PharmacyManagement.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order o)
        {
            await _context.Orders.AddAsync(o);
            await _context.SaveChangesAsync();


        }

        public void Delete(Order o)
        {
           _context.Orders.Remove(o);   
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var entry = await _context.Orders.Include(o=>o.Staff).Include(o => o.Customer)
                                              .Include(o => o.OrderMedicines)
                                                  .ThenInclude(om => om.Medicine)
                .ToListAsync();
            return entry;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var entry = await _context.Orders.Include(o => o.Staff).Include(o => o.Customer)
                                              .Include(o => o.OrderMedicines)
                                                  .ThenInclude(om => om.Medicine).FirstOrDefaultAsync(o=>o.Id==id);
                return entry;
        }


        public void Update(Order o)
        {
            _context.Orders.Update(o);
            _context.SaveChanges();
        }
    }
}
