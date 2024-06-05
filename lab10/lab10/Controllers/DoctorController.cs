using lab10.Context;
using lab10.DTOs;
using lab10.Models;
using lab10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly ApbdContext _apbdContext;
    private readonly Service _service;

    public DoctorController(ApbdContext apbdContext, Service service)
    {
        _apbdContext = apbdContext;
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription(PrescriptionDTO prescriptionDTO)
    {
        var result = await _service.AddPrescription(prescriptionDTO);

        if (result.Contains("nie istnieje") || result.Contains("Przekroczono limit") || result.Contains("musi być większe"))
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
}

