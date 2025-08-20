using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;
using PharmacyManagement.Repository;

namespace PharmacyManagement.Controllers
{
      [PharmayStaffOnlyAttribute]
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : Controller
    {
        private IOrderRepository dbRepo;

        public OrderController(IOrderRepository repo)
        {
            dbRepo = repo;
        }
            
        public async Task<IActionResult> Index()
        {
            var entries=await dbRepo.GetAllAsync();
            return View(entries);
        }
        public async Task< IActionResult> PartialDetails(int Id)
        {
            var order = await dbRepo.GetByIdAsync(Id);
            if (order == null)
            {
                return NotFound();
            }

            return PartialView("_Details", order);
        }
    }
}
