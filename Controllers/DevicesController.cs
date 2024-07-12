using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITInventoryAPI.Data;
using ITInventoryAPI.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;


namespace ITInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(InventoryContext context, ILogger<DevicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _logger.LogInformation($"Received device: {JsonSerializer.Serialize(device)}");
          if (!ModelState.IsValid)
          {
            var errors = string.Join("; ", ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage));
            _logger.LogWarning($"Invalid ModelState: {errors}");
            return BadRequest(ModelState);
          }

          device.Id = 0;
          if (device.EmployeeId.HasValue)
    {
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == device.EmployeeId);
            if (!employeeExists)
            {
                _logger.LogWarning($"Attempted to assign device to non-existent employee: {device.EmployeeId}");
                return BadRequest("Employee does not exist.");
            }
          }

            try
            {
                _context.Devices.Add(device);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully added device with ID: {device.Id}");
                return CreatedAtAction("GetDevice", new { id = device.Id }, device);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred while saving the device");
                return StatusCode(500, "An error occurred while saving the device. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while processing the request");
                return StatusCode(500, "An unexpected error occurred. Please try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}