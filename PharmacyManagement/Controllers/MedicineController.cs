using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.Models;
using PharmacyManagement.Repository;

namespace PharmacyManagement.Controllers
{
    public class MedicineController : Controller

    {
        public class MedicinesController : ControllerBase
        {
            private readonly IMedicineRepository _medicineRepository;
            private readonly ILogger<MedicinesController> _logger;

            public MedicinesController(IMedicineRepository medicineRepository, ILogger<MedicinesController> logger)
            {
                _medicineRepository = medicineRepository;
                _logger = logger;
            }
            [HttpGet]
            [AllowAnonymous] // Allow public access to browse medicines
            public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
            {
                try
                {
                    var medicines = await _medicineRepository.GetAllAsync();
                    return Ok(medicines);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting medicines");
                    return StatusCode(500, "Internal server error");
                }
            }

            // GET: api/v1/medicines/5
            [HttpGet("{id}")]
            [AllowAnonymous]
            public async Task<ActionResult<Medicine>> GetMedicine(int id)
            {
                try
                {
                    var medicine = await _medicineRepository.GetByIdAsync(id);
                    if (medicine == null)
                    {
                        return NotFound();
                    }
                    return Ok(medicine);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting medicine {Id}", id);
                    return StatusCode(500, "Internal server error");
                }
            }

            // POST: api/v1/medicines
            [HttpPost]
            [Authorize(Roles = "admin,pharmacist")]
            public async Task<ActionResult<Medicine>> CreateMedicine(Medicine medicine)
            {
                try
                {
                    var createdMedicine = await _medicineRepository.CreateAsync(medicine);
                    return CreatedAtAction(nameof(GetMedicine), new { id = createdMedicine.Id, version = "1" }, createdMedicine);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating medicine");
                    return StatusCode(500, "Internal server error");
                }
            }

            // PUT: api/v1/medicines/5
            [HttpPut("{id}")]
            [Authorize(Roles = "admin,pharmacist")]
            public async Task<IActionResult> UpdateMedicine(int id, Medicine medicine)
            {
                try
                {
                    if (id != medicine.Id)
                    {
                        return BadRequest();
                    }

                    await _medicineRepository.UpdateAsync(medicine);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating medicine {Id}", id);
                    return StatusCode(500, "Internal server error");
                }
            }

            // DELETE: api/v1/medicines/5
            [HttpDelete("{id}")]
            [Authorize(Roles = "admin")]
            public async Task<IActionResult> DeleteMedicine(int id)
            {
                try
                {
                    var success = await _medicineRepository.DeleteAsync(id);
                    if (!success)
                    {
                        return NotFound();
                    }
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting medicine {Id}", id);
                    return StatusCode(500, "Internal server error");
                }
            }
        }
    }
}
